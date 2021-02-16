using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpriteRenderersManager_Materials))]
public class SpriteRenderersManager_Materials_Editor : ControlEditor_Base, ISpriteRenderersManager_Editor
{
    private SpriteRenderersManager_Materials Target;

    public void OnEnable()
    {
        Target = (SpriteRenderersManager_Materials)target;
        Target.GetGebruikteMaterials();
        Target.Title = "Materials";
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Get gebuikte Materials"))
            Target.GetGebruikteMaterials();

        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Materials"));
        serializedObject.ApplyModifiedProperties();

        if (GUILayout.Button("Set Materials"))
        {
            Target.SetMaterials();
            Target.UpdateChangesReport();
        }

    }
}
