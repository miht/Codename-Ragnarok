using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FollowTarget))]
public class FollowTargetEditor : Editor
{
    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        FollowTarget tar = (FollowTarget) target;
        if(GUILayout.Button("Apply")) {
            tar.Setup();
        }
    }
}
