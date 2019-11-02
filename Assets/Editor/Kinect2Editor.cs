using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DepthRosGeometryView))]
public class Kinect2Editor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawDefaultInspector();
        DepthRosGeometryView script = (DepthRosGeometryView)target;
        if (GUILayout.Button("Check Messages"))
        {
            script.CheckMessages();
        }
    }
}
