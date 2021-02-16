using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpriteRenderersManager_SortingOrder))]
public class SpriteRenderersManager_SortingOrder_Editor : ControlEditor_Base, ISpriteRenderersManager_Editor
{
    private SpriteRenderersManager_SortingOrder Target;

    public void OnEnable()
    {
        Target = (SpriteRenderersManager_SortingOrder)target;
        Target.Title = "Sorting Order";
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Number"));
        serializedObject.ApplyModifiedProperties();

        if (GUILayout.Button("Set OrderLayer to Number"))
            Target.SetOrderLayer();

        EditorGUILayout.Space();

        if (GUILayout.Button("Add Number to OrderLayer"))
            Target.AddToOrderLayer();

        EditorGUILayout.Space();

        if (GUILayout.Button("Subtract Number from OrderLayer"))
            Target.SubtractFromOrderLayer();

    }

}

