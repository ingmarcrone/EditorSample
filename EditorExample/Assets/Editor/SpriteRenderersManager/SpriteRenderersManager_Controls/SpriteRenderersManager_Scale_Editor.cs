using UnityEngine;
using UnityEditor;
using NUnit.Framework.Constraints;

[CustomEditor(typeof(SpriteRenderersManager_Scale))]
public class SpriteRenderersManager_Scale_Editor : ControlEditor_Base, ISpriteRenderersManager_Editor
{
    private SpriteRenderersManager_Scale Target;

    public void OnEnable()
    {
        Target = (SpriteRenderersManager_Scale)target;
        Target.Title = "Scale";
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        FoldoutHeaderGroup_ExactScale();
        EditorGUILayout.Space();
        FoldoutHeaderGroup_RangeScale();

    }

    private bool _showRangeScale = true;
    private void FoldoutHeaderGroup_RangeScale()
    {
        _showRangeScale = EditorGUILayout.BeginFoldoutHeaderGroup(_showRangeScale, "Range Scale", Style.FoldoutHeader.FontSize_13);
        EditorGUI.indentLevel++;
        if (_showRangeScale)
        {
            EditorGUILayout.Space();
            Popup();
            EditorGUILayout.Space(5);

            WaardeVan();
            WaardeTot();
            WaardesVan();
            WaardesTot();
            RangeScaleButton();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }
        EditorGUI.EndFoldoutHeaderGroup();
        EditorGUI.indentLevel--;
    }

    private bool _showExactScale = false;
    private void FoldoutHeaderGroup_ExactScale()
    {
        _showExactScale = EditorGUILayout.BeginFoldoutHeaderGroup(_showExactScale, "Exact Scale", Style.FoldoutHeader.FontSize_13);
        EditorGUI.indentLevel++;
        if (_showExactScale)
        {
            EditorGUILayout.Space();
            ExactScaleFloat();
            ExactScaleVector2();
            ExactScaleButton();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }
        EditorGUI.EndFoldoutHeaderGroup();
        EditorGUI.indentLevel--;
    }

    private int _buttonIndent = 15;
    private void RangeScaleButton()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Space(_buttonIndent);
        if (GUILayout.Button(GetSelectedPopupOptionName()))
        {
            SetScale();
            Target.UpdateChangesReport();
        }
        GUILayout.EndHorizontal();
    }

    private Vector2 _waardesTot_Huidig = Vector2.one;
    private Vector2 _waardesTot_LaatstBekeken = Vector2.one;
    private void WaardesTot()
    {
        _waardesTot_Huidig = EditorGUILayout.Vector2Field($"{GetSelectedPopupOptionName()} - Tot", _waardesTot_Huidig);
        if (Vector2.Distance(_waardesTot_Huidig, _waardesTot_LaatstBekeken) != 0)
        {
            _waardesTot_LaatstBekeken = _waardesTot_Huidig;
            SetScale();
            Target.UpdateChangesReport();
        }
    }

    private Vector2 _waardesVan_Huidig = Vector2.one;
    private Vector2 _waardesVan_LaatstBekeken = Vector2.one;
    private void WaardesVan()
    {
        _waardesVan_Huidig = EditorGUILayout.Vector2Field($"{GetSelectedPopupOptionName()} - Van", _waardesVan_Huidig);
        if (Vector2.Distance(_waardesVan_Huidig, _waardesVan_LaatstBekeken) != 0)
        {
            _waardesVan_LaatstBekeken = _waardesVan_Huidig;
            SetScale();
            Target.UpdateChangesReport();
        }
    }

    private float _waardeTot_Huidig = 1f;
    private float _waardeTot_LaatstBekeken = 1f;
    private void WaardeTot()
    {
        _waardeTot_Huidig = EditorGUILayout.FloatField($"{GetSelectedPopupOptionName()} - Tot", _waardeTot_Huidig);
        if (_waardeTot_Huidig != _waardeTot_LaatstBekeken)
        {
            _waardeTot_LaatstBekeken = _waardeTot_Huidig;
            _waardesTot_Huidig = Vector2.one * _waardeTot_Huidig;
            _waardesTot_LaatstBekeken = Vector2.one * _waardeTot_Huidig;
            SetScale();
            Target.UpdateChangesReport();
        }
    }

    private float _waardeVan_Huidig = 1f;
    private float _waardeVan_LaatstBekeken = 1f;
    private void WaardeVan()
    {
        _waardeVan_Huidig = EditorGUILayout.FloatField($"{GetSelectedPopupOptionName()} - Van", _waardeVan_Huidig);
        if (_waardeVan_Huidig != _waardeVan_LaatstBekeken)
        {
            _waardeVan_LaatstBekeken = _waardeVan_Huidig;
            _waardesVan_Huidig = Vector2.one * _waardeVan_Huidig;
            _waardesVan_LaatstBekeken = Vector2.one * _waardeVan_Huidig;
            SetScale();
            Target.UpdateChangesReport();
        }
    }

    private int _popupIndex_Huidig = 0;
    private int _popupIndex_LaatstBekeken = 0;
    string[] _popupOptions = { "Random Scale (nieuw)", "Random Scale (+ huidige)", "Oplopende Scale - Horizontaal (nieuw)", "Oplopende Scale - Horizontaal (+ huidige)" };
    private void Popup()
    {
        _popupIndex_Huidig = EditorGUILayout.Popup(_popupIndex_Huidig, _popupOptions, Style.Popup.FontSize_15);
        if (_popupIndex_Huidig != _popupIndex_LaatstBekeken)
        {
            _popupIndex_LaatstBekeken = _popupIndex_Huidig;
            SetScale();
            Target.UpdateChangesReport();
        }
    }

    private string GetSelectedPopupOptionName() => _popupOptions[_popupIndex_Huidig];

    private void SetScale()
    {
        switch (_popupIndex_Huidig)
        {
            // "Random Scale (nieuw)"
            case 0:
                Target.SetRandomScale(_waardesVan_Huidig, _waardesTot_Huidig, false);
                break;

            // "Random Scale (+ huidige)", 
            case 1:
                Target.SetRandomScale(_waardesVan_Huidig, _waardesTot_Huidig, true);
                break;

            // "Oplopende Scale - Horizontaal (nieuw)"
            case 2:
                Target.SetOplopendeScaleHorizontaal(_waardesVan_Huidig, _waardesTot_Huidig, false);
                break;

            // "Oplopende Scale - Horizontaal (+ huidige)"
            case 3:
                Target.SetOplopendeScaleHorizontaal(_waardesVan_Huidig, _waardesTot_Huidig, true);
                break;

        }
    }



    private Vector2 _huidigeWaardes = Vector2.one;
    private void ExactScaleButton()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Space(_buttonIndent);
        if (GUILayout.Button("Exact Scale"))
        {
            Target.SetExactScale(_huidigeWaardes);
            Target.UpdateChangesReport();
        }
        GUILayout.EndHorizontal();
    }

    private Vector2 _laatstBekekenWaardes = Vector2.one;
    private void ExactScaleVector2()
    {
        _huidigeWaardes = EditorGUILayout.Vector2Field("Exact Scale", _huidigeWaardes);
        if (Vector2.Distance(_huidigeWaardes, _laatstBekekenWaardes) != 0)
        {
            _laatstBekekenWaardes = _huidigeWaardes;
            Target.SetExactScale(_huidigeWaardes);
            Target.UpdateChangesReport();
        }
    }

    private float _huidigeWaarde = 1f;
    private float _laatstBekekenWaarde = 1f;
    private void ExactScaleFloat()
    {
        _huidigeWaarde = EditorGUILayout.FloatField("Exact Scale", _huidigeWaarde);
        if (_huidigeWaarde != _laatstBekekenWaarde)
        {
            _laatstBekekenWaarde = _huidigeWaarde;
            _huidigeWaardes = Vector2.one * _huidigeWaarde;
            _laatstBekekenWaardes = Vector2.one * _huidigeWaarde;
            Target.SetExactScale(_huidigeWaardes);
            Target.UpdateChangesReport();
        }
    }

}
