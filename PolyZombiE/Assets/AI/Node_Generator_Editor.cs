using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Node_Generator))]
public class Node_Generator_Editor : Editor {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Node_Generator myScript = (Node_Generator)target;
        if (GUILayout.Button("Build Nodes"))
        {
            myScript.buildNodeGraph();
        }

        if (GUILayout.Button("Connect Nodes"))
        {
            myScript.connectNodes();
        }

        if (GUILayout.Button("Clean Nodes"))
        {
            myScript.cleanNodes();
        }
    }
}
