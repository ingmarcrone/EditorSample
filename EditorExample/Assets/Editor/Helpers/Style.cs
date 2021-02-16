using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class Style
{
    public static class Button
    {
        public static GUIStyle Horizontaal_100__FontSize_15
        {
            get
            {
                GUIStyle style = new GUIStyle(GUI.skin.button);
                style.fontSize = 15;
                return style;
            }
        }

        public static GUIStyle Horizontaal_50
        {
            get
            {
                GUIStyle style = new GUIStyle(GUI.skin.button);
                style.fontSize = 15;
                style.fixedWidth = (Screen.width - 30) / 2f;
                return style;
            }
        }

        public static GUIStyle Horizontaal_33
        {
            get
            {
                GUIStyle style = new GUIStyle(GUI.skin.button);
                style.fontSize = 15;
                style.fixedWidth = (Screen.width - 30) / 3f;
                return style;
            }
        }

        public static GUIStyle Horizontaal_25
        {
            get
            {
                GUIStyle style = new GUIStyle(GUI.skin.button);
                style.fontSize = 15;
                style.fixedWidth = (Screen.width - 30) / 4f;
                return style;
            }
        }

    }

    public static class FloatField
    {
        public static GUIStyle Horizontaal_20
        {
            get
            {
                GUIStyle style = new GUIStyle(GUI.skin.textField);
                style.fixedWidth = Screen.width / 10f;
                return style;
            }
        }
    }

    public static class FoldoutHeader
    {
        public static GUIStyle FontSize_13
        {
            get
            {
                GUIStyle style = new GUIStyle(EditorStyles.foldoutHeader);
                style.fontSize = 13;
                return style;
            }
        }
    }

    public static class Label
    {
        public static GUIStyle Horizontaal_40
        {
            get
            {
                GUIStyle style = new GUIStyle(EditorStyles.label);
                style.fixedWidth = (Screen.width - 30) / 2.5f;
                return style;
            }
        }
    }

    public static class LabelBold
    {
        public static GUIStyle FontSize_15
        {
            get
            {
                GUIStyle style = new GUIStyle(EditorStyles.boldLabel);
                style.fontSize = 15;
                return style;
            }
        }

        public static GUIStyle FontSize_20
        {
            get
            {
                GUIStyle style = new GUIStyle(EditorStyles.boldLabel);
                style.fontSize = 20;
                return style;
            }
        }

    }

    public static class Popup
    {
        public static GUIStyle FontSize_15
        {
            get
            {
                GUIStyle style = new GUIStyle(EditorStyles.popup);
                style.fontSize = 15;
                style.fixedHeight = 23;

                return style;
            }
        }
    }

    public static class TextArea
    {
        public static GUIStyle FontSize_13
        {
            get
            {
                GUIStyle _gUIStyle_textArea = new GUIStyle(EditorStyles.textArea);
                _gUIStyle_textArea.fontSize = 13;
                return _gUIStyle_textArea;
            }
        }

    }

    public static class Toggle
    {
        public static GUIStyle Font_15
        {
            get
            {
                GUIStyle style = new GUIStyle(GUI.skin.toggle);
                style.fontSize = 15;
                return style;
            }
        }
    }

}
