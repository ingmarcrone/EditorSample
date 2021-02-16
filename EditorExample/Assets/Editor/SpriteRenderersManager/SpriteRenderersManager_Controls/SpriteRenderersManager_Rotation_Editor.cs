using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpriteRenderersManager_Rotation))]
public class SpriteRenderersManager_Rotation_Editor : ControlEditor_Base, ISpriteRenderersManager_Editor
{
    private SpriteRenderersManager_Rotation Target;

    public void OnEnable()
    {
        Target = (SpriteRenderersManager_Rotation)target;
        Target.Title = "Rotation";
        Target.UpdateChangesReport();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ExactRotationZ_FloatField();
        ExactRotationZ_Button();

        EditorGUILayout.Space();

        RandomRotationZ_Van_FloatField();
        RandomRotationZ_Tot_FloatField();
        RandomRotationZ_Button();

    }

    private float _waardeVan_Huidig = -2.5f;
    private float _waardeTot_Huidig = 2.5f;
    private void RandomRotationZ_Button()
    {
        if (GUILayout.Button("Random rotation Z"))
        {
            Target.SetRamdomRotation(_waardeVan_Huidig, _waardeTot_Huidig);
            Target.UpdateChangesReport();
        }
    }

    private float _waardeTot_LaatstBekeken = 5f;
    private void RandomRotationZ_Tot_FloatField()
    {
        _waardeTot_Huidig = EditorGUILayout.FloatField("Random rotation Z - tot", _waardeTot_Huidig);
        if (_waardeTot_Huidig != _waardeTot_LaatstBekeken)
        {
            _waardeTot_LaatstBekeken = _waardeTot_Huidig;
            Target.SetRamdomRotation(_waardeVan_Huidig, _waardeTot_Huidig);
            Target.UpdateChangesReport();
        }
    }

    private float _waardeVan_LaatstBekeken = -5f;
    private void RandomRotationZ_Van_FloatField()
    {
        _waardeVan_Huidig = EditorGUILayout.FloatField("Random rotation Z - van", _waardeVan_Huidig);
        if (_waardeVan_Huidig != _waardeVan_LaatstBekeken)
        {
            _waardeVan_LaatstBekeken = _waardeVan_Huidig;
            Target.SetRamdomRotation(_waardeVan_Huidig, _waardeTot_Huidig);
            Target.UpdateChangesReport();
        }
    }

    private float _exactRotationZ_Huidig = 0;
    private void ExactRotationZ_Button()
    {
        if (GUILayout.Button("Exact rotation Z"))
        {
            Target.SetExactRotation(_exactRotationZ_Huidig);
            Target.UpdateChangesReport();
        }
    }

    private float _exactRotationZ_LaatstBekeken = 0;
    private void ExactRotationZ_FloatField()
    {
        _exactRotationZ_Huidig = EditorGUILayout.FloatField("Exact rotation Z", _exactRotationZ_Huidig);
        if (_exactRotationZ_Huidig != _exactRotationZ_LaatstBekeken)
        {
            _exactRotationZ_LaatstBekeken = _exactRotationZ_Huidig;
            Target.SetExactRotation(_exactRotationZ_Huidig);
            Target.UpdateChangesReport();
        }
    }

}
