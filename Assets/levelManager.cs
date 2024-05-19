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
        }
    }

    // Use this for initialization
    void Start()
    {
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
