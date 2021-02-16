using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.TerrainAPI;
using UnityEngine;

[CustomEditor(typeof(SpriteRenderersManager_OnOff))]
public class SpriteRenderersManager_OnOff_Editor : ControlEditor_Base, ISpriteRenderersManager_Editor
{
    private SpriteRenderersManager_OnOff Target;

    public void OnEnable()
    {
        Target = (SpriteRenderersManager_OnOff)target;
        Target.Title = "On / Off";
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.BeginHorizontal();
        TurnOffButton();
        TurnOnButton();
        GUILayout.EndHorizontal();
    }

    private void TurnOnButton()
    {
        if (GUILayout.Button("Turn On", Style.Button.Horizontaal_50))
        {
            Target.TurnOnOff(true);
            Target.UpdateChangesReport();
        }
    }

    private void TurnOffButton()
    {
        if (GUILayout.Button("Turn Off", Style.Button.Horizontaal_50))
        {
            Target.TurnOnOff(false);
            Target.UpdateChangesReport();
        }
    }
}
