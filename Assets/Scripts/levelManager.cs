using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelManager : MonoBehaviour
{
    public TMP_Text jsonOutput;

    public GameObject nearStuff;
    
    public GameObject levelPrefab;
    
    public GameObject spikePrefab;
    public GameObject turretPrefab;
    public GameObject platformPrefab;

    public GameObject sky1Prefab;
    public GameObject sky2Prefab;
    public GameObject sky3Prefab;
    void SpawnObjectsFromData(string data)
    {
        data = Regex.Unescape(data);
        Debug.Log(data);
        data = data.Trim('"').Trim('[').Trim(']');
        string[] entries = data.Split(new string[] { "],[" }, System.StringSplitOptions.RemoveEmptyEntries);

        foreach (string entry in entries)
        {
            string cleanedEntry = entry.Trim(new char[] { '[', ']' });
            string[] parts = cleanedEntry.Split(',');

            string type = parts[0].Trim().Trim('"');
            string x = (parts[1].Trim().Trim('"'));
            string y = parts[2].Trim().Trim('"');
            Debug.Log(parts[0] + " x: " + x + " y: " + y);
            Vector2 position = new Vector2(float.Parse(x), float.Parse(y));

            if (type == "sp")
            {
                Debug.Log("Spawning Spike");
                Instantiate(spikePrefab, position, Quaternion.identity);
            }
            else if (type == "tu")
            {
                Debug.Log("Spawning Turret");
                Instantiate(turretPrefab, position, Quaternion.identity);
            }
            else if (type == "pl")
            {
                Debug.Log("Spawning Platform");
                Instantiate(platformPrefab, position, Quaternion.identity);
            }
            else if (type == "s1")
            {
                Debug.Log("Spawning Sky1");
                GameObject sky = Instantiate(sky1Prefab, position, Quaternion.identity);
                sky.transform.position = new Vector3(sky.transform.position.x, sky.transform.position.y, 1);
            }
            else if (type == "s2")
            {
                Debug.Log("Spawning Sky2");
                GameObject sky = Instantiate(sky2Prefab, position, Quaternion.identity);
                sky.transform.position = new Vector3(sky.transform.position.x, sky.transform.position.y, 1);
            }
            else if (type == "s3")
            {
                Debug.Log("Spawning Sky3");
                GameObject sky = Instantiate(sky3Prefab, position, Quaternion.identity);
                sky.transform.position = new Vector3(sky.transform.position.x, sky.transform.position.y, 1);
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        jsonOutput.text =
            "[[\"s2\", \"0\", \"0\"],[\"sp\", \"13\", \"-10\"],[\"sp\", \"6\", \"-8\"],[\"sp\", \"-4\", \"-8\"],[\"sp\", \"0\", \"-10\"],[\"sp\", \"2\", \"-2\"],[\"tu\", \"-2\", \"1.5\"],[\"tu\", \"1\", \"3.5\"],[\"tu\", \"7.5\", \"3.5\"],[\"tu\", \"13\", \"3.5\"],[\"pl\", \"7\", \"3\"],[\"pl\", \"12\", \"3\"]]";
    }

    public void loadLevel()
    {
        Instantiate(levelPrefab);
        nearStuff.SetActive(false);
        // List<int> result = ConvertStringToList(jsonOutput.text);
        // Debug.Log(result[0].ToString());
        // Debug.Log(result[1].ToString());
        // Instantiate(spikePrefab,new Vector2(result[0], result[1]), transform.rotation);
        SpawnObjectsFromData(jsonOutput.text);
    }
}
