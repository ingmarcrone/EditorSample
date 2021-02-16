using UnityEditor;
using UnityEngine;
using System.Linq;
using UnityEditorInternal;

[CustomEditor(typeof(SpriteRenderersManager_Property_Base), true)]
public class ControlEditor_Base : Editor
{
    private SpriteRenderersManager_Property_Base _baseTarget;
    private SpriteRenderersManager_Property_Base BaseTarget
    {
        get
        {
            if (_baseTarget == null)
                Enable();
            return _baseTarget;
        }
    }

    private string[] unityTags;
    private string[] names;

    private void Enable()
    {
        _baseTarget = (this.target as SpriteRenderersManager_Property_Base);
        unityTags = InternalEditorUtility.tags;
        names = BaseTarget.NamesOfObject();

        Enable_TagExclusiveList();
        Enable_NameExclusiveList();
        Enable_TagIgnoreList();
        Enable_NameIgnoreList();
    }

    private bool _isShowBase = false;
    public override void OnInspectorGUI()
    {
        EditorGUILayout.Space(10);
        BaseTarget.IsIncluissiefInActieve = EditorGUILayout.Toggle("Inclussief inactieve", BaseTarget.IsIncluissiefInActieve, Style.Toggle.Font_15);
        EditorGUILayout.Space(10);

        _isShowBase = EditorGUILayout.BeginFoldoutHeaderGroup(_isShowBase, "Exclusive and Ignore GameObjects", Style.FoldoutHeader.FontSize_13);
        EditorGUI.indentLevel++;
        if (_isShowBase)
        {
            if (RL_TagExclusiveList == null || RL_TagIgnoreList == null || RL_NameExclusiveList == null || RL_NameIgnoreList == null)
                Enable();

            GUILayout.Space(6);
            serializedObject.Update();
            string textArea = $"#1. Alle lijsten leeg == Methode wordt uitgevoerd op alle SpriteRenderers.\r\n" +
                $"#2. Anders een Selectie van wat je wel of niet wilt\r\n" +
                $"#3. Bij lijsten van zowel WEL en NIET, worden eerst de lijsten van WEL opgehaald, en daarna de lijsten van NIET er weer af gehaald.";
            EditorGUILayout.LabelField(textArea, Style.TextArea.FontSize_13);
            EditorGUILayout.Space(17);
            RL_TagExclusiveList.DoLayoutList();
            RL_NameExclusiveList.DoLayoutList();
            RL_TagIgnoreList.DoLayoutList();
            RL_NameIgnoreList.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
            GUILayout.Space(3);

        }

        EditorGUI.EndFoldoutHeaderGroup();
        EditorGUI.indentLevel--;
        EditorGUILayout.Space(15);
        EditorGUILayout.LabelField(BaseTarget.Title, Style.LabelBold.FontSize_20);
        EditorGUILayout.Space(10);

    }

    #region TagExclusiveList

    private SerializedProperty SP_TagExclusiveList;
    private ReorderableList RL_TagExclusiveList;
    private void Enable_TagExclusiveList()
    {
        SP_TagExclusiveList = serializedObject.FindProperty("TagExclusiveList");
        RL_TagExclusiveList = new ReorderableList(serializedObject, SP_TagExclusiveList, true, true, true, true);
        RL_TagExclusiveList.drawHeaderCallback += DrawHeader_TagExclusiveList;
        RL_TagExclusiveList.drawElementCallback += DrawElement_TagExclusiveList;
        RL_TagExclusiveList.onAddDropdownCallback += OnAddDropdown_TagExclusiveList;
    }

    private void DrawHeader_TagExclusiveList(Rect rect) => EditorGUI.LabelField(rect, new GUIContent("TAG WEL - Moet deze TAG WEL hebben"), EditorStyles.boldLabel);

    private void DrawElement_TagExclusiveList(Rect rect, int index, bool isActive, bool isFocused)
    {
        var element = RL_TagExclusiveList.serializedProperty.GetArrayElementAtIndex(index);
        rect.y += 2;
        EditorGUI.LabelField(rect, element.stringValue);
    }

    private void OnAddDropdown_TagExclusiveList(Rect buttonRect, ReorderableList list)
    {
        GenericMenu menu = new GenericMenu();

        for (int i = 0; i < unityTags.Length; i++)
        {
            var label = new GUIContent(unityTags[i]);

            if (PropertyContainsString(SP_TagExclusiveList, unityTags[i]))
                menu.AddDisabledItem(label);
            else
                menu.AddItem(label, false, OnAddClickHandler_TagExclusiveList, unityTags[i]);
        }

        menu.ShowAsContext();
    }

    private void OnAddClickHandler_TagExclusiveList(object tag)
    {
        int index = RL_TagExclusiveList.serializedProperty.arraySize;
        RL_TagExclusiveList.serializedProperty.arraySize++;
        RL_TagExclusiveList.index = index;

        var element = RL_TagExclusiveList.serializedProperty.GetArrayElementAtIndex(index);
        element.stringValue = (string)tag;
        serializedObject.ApplyModifiedProperties();
    }

    #endregion

    #region NameExclusiveList

    private SerializedProperty SP_NameExclusiveList;
    private ReorderableList RL_NameExclusiveList;
    private void Enable_NameExclusiveList()
    {
        SP_NameExclusiveList = serializedObject.FindProperty("NameExclusiveList");
        RL_NameExclusiveList = new ReorderableList(serializedObject, SP_NameExclusiveList, true, true, true, true);
        RL_NameExclusiveList.drawHeaderCallback += DrawHeader_NameExclusiveList;
        RL_NameExclusiveList.drawElementCallback += DrawElement_NameExclusiveList;
        RL_NameExclusiveList.onAddDropdownCallback += OnAddDropdown_NameExclusiveList;
    }

    private void DrawHeader_NameExclusiveList(Rect rect) => EditorGUI.LabelField(rect, new GUIContent("NAAM WEL - Moet WEL in NAAM zitten"), EditorStyles.boldLabel);

    private void DrawElement_NameExclusiveList(Rect rect, int index, bool isActive, bool isFocused)
    {
        var element = RL_NameExclusiveList.serializedProperty.GetArrayElementAtIndex(index);
        rect.y += 2;
        EditorGUI.LabelField(rect, element.stringValue);
    }

    private void OnAddDropdown_NameExclusiveList(Rect buttonRect, ReorderableList list)
    {
        GenericMenu menu = new GenericMenu();

        for (int i = 0; i < names.Length; i++)
        {
            var label = new GUIContent(names[i]);

            if (PropertyContainsString(SP_NameExclusiveList, names[i]))
                menu.AddDisabledItem(label);
            else
                menu.AddItem(label, false, OnAddClickHandler_NameExclusiveList, names[i]);
        }

        menu.ShowAsContext();
    }

    private void OnAddClickHandler_NameExclusiveList(object tag)
    {
        int index = RL_NameExclusiveList.serializedProperty.arraySize;
        RL_NameExclusiveList.serializedProperty.arraySize++;
        RL_NameExclusiveList.index = index;

        var element = RL_NameExclusiveList.serializedProperty.GetArrayElementAtIndex(index);
        element.stringValue = (string)tag;
        serializedObject.ApplyModifiedProperties();
    }

    #endregion

    #region TagIgnoreList

    private SerializedProperty SP_TagIgnoreList;
    private ReorderableList RL_TagIgnoreList;
    private void Enable_TagIgnoreList()
    {
        SP_TagIgnoreList = serializedObject.FindProperty("TagIgnoreList");
        RL_TagIgnoreList = new ReorderableList(serializedObject, SP_TagIgnoreList, true, true, true, true);
        RL_TagIgnoreList.drawHeaderCallback += DrawHeader_TagIgnoreList;
        RL_TagIgnoreList.drawElementCallback += DrawElement_TagIgnoreList;
        RL_TagIgnoreList.onAddDropdownCallback += OnAddDropdown_TagIgnoreList;
    }

    private void DrawHeader_TagIgnoreList(Rect rect) => EditorGUI.LabelField(rect, new GUIContent("TAG NIET - Mag deze TAG NIET hebben"), EditorStyles.boldLabel);

    private void DrawElement_TagIgnoreList(Rect rect, int index, bool isActive, bool isFocused)
    {
        var element = RL_TagIgnoreList.serializedProperty.GetArrayElementAtIndex(index);
        rect.y += 2;
        EditorGUI.LabelField(rect, element.stringValue);
    }

    private void OnAddDropdown_TagIgnoreList(Rect buttonRect, ReorderableList list)
    {
        GenericMenu menu = new GenericMenu();

        for (int i = 0; i < unityTags.Length; i++)
        {
            var label = new GUIContent(unityTags[i]);

            if (PropertyContainsString(SP_TagIgnoreList, unityTags[i]))
                menu.AddDisabledItem(label);
            else
                menu.AddItem(label, false, OnAddClickHandler_TagIgnoreList, unityTags[i]);
        }

        menu.ShowAsContext();
    }

    private void OnAddClickHandler_TagIgnoreList(object tag)
    {
        int index = RL_TagIgnoreList.serializedProperty.arraySize;
        RL_TagIgnoreList.serializedProperty.arraySize++;
        RL_TagIgnoreList.index = index;

        var element = RL_TagIgnoreList.serializedProperty.GetArrayElementAtIndex(index);
        element.stringValue = (string)tag;
        serializedObject.ApplyModifiedProperties();
    }

    #endregion

    #region NameIgnoreList

    private SerializedProperty SP_NameIgnoreList;
    private ReorderableList RL_NameIgnoreList;
    private void Enable_NameIgnoreList()
    {
        SP_NameIgnoreList = serializedObject.FindProperty("NameIgnoreList");
        RL_NameIgnoreList = new ReorderableList(serializedObject, SP_NameIgnoreList, true, true, true, true);
        RL_NameIgnoreList.drawHeaderCallback += DrawHeader_NameIgnoreList;
        RL_NameIgnoreList.drawElementCallback += DrawElement_NameIgnoreList;
        RL_NameIgnoreList.onAddDropdownCallback += OnAddDropdown_NameIgnoreList;
    }

    private void DrawHeader_NameIgnoreList(Rect rect) => EditorGUI.LabelField(rect, new GUIContent("NAAM NIET - Mag NIET in NAAM zitten"), EditorStyles.boldLabel);

    private void DrawElement_NameIgnoreList(Rect rect, int index, bool isActive, bool isFocused)
    {
        var element = RL_NameIgnoreList.serializedProperty.GetArrayElementAtIndex(index);
        rect.y += 2;
        EditorGUI.LabelField(rect, element.stringValue);
    }

    private void OnAddDropdown_NameIgnoreList(Rect buttonRect, ReorderableList list)
    {
        GenericMenu menu = new GenericMenu();

        for (int i = 0; i < names.Length; i++)
        {
            var label = new GUIContent(names[i]);

            if (PropertyContainsString(SP_NameIgnoreList, names[i]))
                menu.AddDisabledItem(label);
            else
                menu.AddItem(label, false, OnAddClickHandler_NameIgnoreList, names[i]);
        }

        menu.ShowAsContext();
    }

    private void OnAddClickHandler_NameIgnoreList(object tag)
    {
        int index = RL_NameIgnoreList.serializedProperty.arraySize;
        RL_NameIgnoreList.serializedProperty.arraySize++;
        RL_NameIgnoreList.index = index;

        var element = RL_NameIgnoreList.serializedProperty.GetArrayElementAtIndex(index);
        element.stringValue = (string)tag;
        serializedObject.ApplyModifiedProperties();
    }

    #endregion

    private bool PropertyContainsString(SerializedProperty property, string value)
    {
        if (property.isArray)
        {
            for (int i = 0; i < property.arraySize; i++)
            {
                if (property.GetArrayElementAtIndex(i).stringValue == value)
                    return true;
            }
        }
        else
            return property.stringValue == value;

        return false;
    }

}
