using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class startprocess_test : MonoBehaviour {

   public Process process;
	// Use this for initialization
	void Start () {
        // Process.Start("process.exe");
        Process.Start(@"c:\Users\Richard\Documents\sdfasdfasdfasdfasdfasdfasdfasdf\build\1.exe");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
