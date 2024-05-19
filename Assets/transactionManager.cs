using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class transactionManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Trying to run transaction");
        Debug.Log(Application.dataPath);
        string FileName = Application.dataPath + "/foo/bar.sh";

        RunShellScriptOSX(FileName);
    }

    public static void RunShellScriptOSX(string scriptPath, string arguments = null)
    {
        ProcessStartInfo startInfo = new ProcessStartInfo()
        {
            FileName = "osascript",
            Arguments = $"-e 'tell application \"Terminal\" to activate' -e 'tell application \"Terminal\" to do script \"sh {scriptPath} {arguments}\"'",
        
            UseShellExecute = true,
            CreateNoWindow = false,
            Verb = "runas",
            RedirectStandardOutput = false,
            RedirectStandardInput = false,
        };
        Process process = new Process()
        {
            StartInfo = startInfo,
        };
        process.Start();
    }
}
