using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(SpriteRenderersManager))]
public class SpriteRenderersManager_Editor : Editor, ISpriteRenderersManager_Editor
{
    private SpriteRenderersManager Target { get; set; }

    public void OnEnable() => Target = (SpriteRenderersManager)target;

    private int _lastCall = -1;
    public override void OnInspectorGUI()
    {
        if (Target.Properties.Count != _lastCall)
        {
            if (Target.Properties.Count == 0 && _lastCall == -1)
                Target.FillInitial();
            _lastCall = Target.Properties.Count;
        }

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("On / Off", Style.Button.Horizontaal_50))
            Target.TogglePropertyOnOf(typeof(SpriteRenderersManager_OnOff));
        SetReportFor(typeof(SpriteRenderersManager_OnOff));
        GUILayout.EndHorizontal();

        EditorGUILayout.Space(3);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Position", Style.Button.Horizontaal_50))
            Target.TogglePropertyOnOf(typeof(SpriteRenderersManager_Position));
        SetReportFor(typeof(SpriteRenderersManager_Position));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Rotation", Style.Button.Horizontaal_50))
            Target.TogglePropertyOnOf(typeof(SpriteRenderersManager_Rotation));
        SetReportFor(typeof(SpriteRenderersManager_Rotation));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Scale", Style.Button.Horizontaal_50))
            Target.TogglePropertyOnOf(typeof(SpriteRenderersManager_Scale));
        SetReportFor(typeof(SpriteRenderersManager_Scale));
        GUILayout.EndHorizontal();

        EditorGUILayout.Space(3);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Materials", Style.Button.Horizontaal_50))
            Target.TogglePropertyOnOf(typeof(SpriteRenderersManager_Materials));
        SetReportFor(typeof(SpriteRenderersManager_Materials));
        GUILayout.EndHorizontal();


        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Sorting order", Style.Button.Horizontaal_50))
            Target.TogglePropertyOnOf(typeof(SpriteRenderersManager_SortingOrder));
        SetReportFor(typeof(SpriteRenderersManager_SortingOrder));
        GUILayout.EndHorizontal();

    }

    private void SetReportFor(Type type)
    {
        int count = Target.GetReportForType(type);
        if (count > 0)
        {
            if (GUILayout.Button($"Keep {count}", Style.Button.Horizontaal_25))
                Target.KeepChanges(type);
            if (GUILayout.Button($"Restore {count}", Style.Button.Horizontaal_25))
                Target.Restore(type);
        }

    }

}
