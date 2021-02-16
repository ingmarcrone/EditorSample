using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpriteRenderersManager_Position))]
public class SpriteRenderersManager_Position_Editor : ControlEditor_Base, ISpriteRenderersManager_Editor
{
    private SpriteRenderersManager_Position Target;

    public void OnEnable()
    {
        Target = (SpriteRenderersManager_Position)target;
        Target.Title = "Position";
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        FoldoutHeaderGroup_RandomPosition_InBox();

        EditorGUILayout.Space();

        FoldoutHeaderGroup_PositiePlusHuidig();

    }

    private bool _isPositiePlusHuidig = true;
    private void FoldoutHeaderGroup_PositiePlusHuidig()
    {
        _isPositiePlusHuidig = EditorGUILayout.BeginFoldoutHeaderGroup(_isPositiePlusHuidig, "Huidige positie + ..", Style.FoldoutHeader.FontSize_13);
        EditorGUI.indentLevel++;
        if (_isPositiePlusHuidig)
        {
            EditorGUILayout.Space();
            Popup();
            EditorGUILayout.Space(5);

            WaardesVan();
            WaardesTot();

            RangePositionButton();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }

        ControlIsUitgezet_QueuePlayerLoopUpdate();

        EditorGUI.EndFoldoutHeaderGroup();
        EditorGUI.indentLevel--;
    }

    private int _buttonIndent = 15;
    private void RangePositionButton()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Space(_buttonIndent);
        if (GUILayout.Button(GetSelectedPopupOptionName(), Style.Button.Horizontaal_100__FontSize_15))
        {
            SetPosition();
            Target.UpdateChangesReport();
        }
        GUILayout.EndHorizontal();
    }

    private Vector2 _waardesTot_Huidig = Vector2.zero;
    private Vector2 _waardesTot_LaatstBekeken = Vector2.zero;
    private void WaardesTot()
    {
        _waardesTot_Huidig = EditorGUILayout.Vector2Field($"{GetSelectedPopupOptionName()} - Tot", _waardesTot_Huidig);
        if (Vector2.Distance(_waardesTot_Huidig, _waardesTot_LaatstBekeken) != 0)
        {
            _waardesTot_LaatstBekeken = _waardesTot_Huidig;
            SetPosition();
            Target.UpdateChangesReport();
        }
    }

    private Vector2 _waardesVan_Huidig = Vector2.zero;
    private Vector2 _waardesVan_LaatstBekeken = Vector2.zero;
    private void WaardesVan()
    {
        _waardesVan_Huidig = EditorGUILayout.Vector2Field($"{GetSelectedPopupOptionName()} - Van", _waardesVan_Huidig);
        if (Vector2.Distance(_waardesVan_Huidig, _waardesVan_LaatstBekeken) != 0)
        {
            _waardesVan_LaatstBekeken = _waardesVan_Huidig;
            SetPosition();
            Target.UpdateChangesReport();
        }
    }

    private int _popupIndex_Huidig = 0;
    private int _popupIndex_LaatstBekeken = 0;
    string[] _popupOptions = { "Random", "Trap", "Stretch" };
    private void Popup()
    {
        _popupIndex_Huidig = EditorGUILayout.Popup(_popupIndex_Huidig, _popupOptions, Style.Popup.FontSize_15);
        if (_popupIndex_Huidig != _popupIndex_LaatstBekeken)
        {
            _popupIndex_LaatstBekeken = _popupIndex_Huidig;
            SetPosition();
            Target.UpdateChangesReport();
        }
    }

    private string GetSelectedPopupOptionName() => _popupOptions[_popupIndex_Huidig];

    private void SetPosition()
    {
        switch (_popupIndex_Huidig)
        {
            // "Random"
            case 0:
                Target.HuidigPlusRandom(_waardesVan_Huidig, _waardesTot_Huidig);
                break;

            // "Trap" 
            case 1:
                Target.HuidigPlusTrap(_waardesVan_Huidig, _waardesTot_Huidig);
                break;

            // "Stretch" 
            case 2:
                Target.HuidigPlusStretch(_waardesVan_Huidig, _waardesTot_Huidig);
                break;


        }
    }







    private bool _isShowBox_LastChecked = false;
    private void FoldoutHeaderGroup_RandomPosition_InBox()
    {
        Target.IsShowBox = EditorGUILayout.BeginFoldoutHeaderGroup(Target.IsShowBox, "Random in Box", Style.FoldoutHeader.FontSize_13);
        EditorGUI.indentLevel++;
        if (Target.IsShowBox)
        {
            ControlIsAangezet_QueuePlayerLoopUpdate();

            SetBox();

            int _buttonIndent = 15;
            GUILayout.BeginHorizontal();
            GUILayout.Space(_buttonIndent);
            if (GUILayout.Button("Voer uit"))
            {
                Target.PlaceInBox();
                Target.UpdateChangesReport();

            }
            GUILayout.EndHorizontal();


            _isShowBox_LastChecked = Target.IsShowBox;

            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }

        ControlIsUitgezet_QueuePlayerLoopUpdate();

        EditorGUI.EndFoldoutHeaderGroup();
        EditorGUI.indentLevel--;
    }

    private void ControlIsUitgezet_QueuePlayerLoopUpdate()
    {
        if (_isShowBox_LastChecked && !Target.IsShowBox)
            EditorApplication.QueuePlayerLoopUpdate();
    }

    private void ControlIsAangezet_QueuePlayerLoopUpdate()
    {
        if (!_isShowBox_LastChecked && Target.IsShowBox)
            EditorApplication.QueuePlayerLoopUpdate();
    }

    private Vector3 _puntA_LaatstBekeken = new Vector3(0f, 5f, 0f);
    private Vector3 _puntB_LaatstBekeken = new Vector3(4f, 0f, 0f);
    private void SetBox()
    {
        Target.BoxHoekA = EditorGUILayout.Vector2Field("PuntA (rood)", Target.BoxHoekA);
        Target.BoxHoekB = EditorGUILayout.Vector2Field("PuntB (groen)", Target.BoxHoekB);
        if (Vector2.Distance(Target.BoxHoekA, _puntA_LaatstBekeken) != 0 || Vector2.Distance(Target.BoxHoekB, _puntB_LaatstBekeken) != 0)
        {
            _puntA_LaatstBekeken = Target.BoxHoekA;
            _puntB_LaatstBekeken = Target.BoxHoekB;
            ControlHeeftEenUpdate_QueuePlayerLoopUpdate();
        }
    }

    private static void ControlHeeftEenUpdate_QueuePlayerLoopUpdate() => EditorApplication.QueuePlayerLoopUpdate();

}
