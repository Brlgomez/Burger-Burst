using UnityEngine;
using UnityEditor;
using System.Collections;

namespace AlpacaSound
{
    [CustomEditor(typeof(RetroPixel))]
    public class RetroPixelEditor : Editor
    {
        SerializedObject serObj;

        SerializedProperty horizontalResolution;
        SerializedProperty verticalResolution;
        SerializedProperty numColors;

        SerializedProperty color0;
        SerializedProperty color1;
        SerializedProperty color2;
        SerializedProperty color3;
        SerializedProperty color4;
        SerializedProperty color5;
        SerializedProperty color6;
        SerializedProperty color7;
        SerializedProperty color8;
        SerializedProperty color9;
        SerializedProperty color10;
        SerializedProperty color11;
        SerializedProperty color12;
        SerializedProperty color13;
        SerializedProperty color14;
        SerializedProperty color15;

        Texture2D image;

        void OnEnable()
        {
            serObj = new SerializedObject(target);

            horizontalResolution = serObj.FindProperty("horizontalResolution");
            verticalResolution = serObj.FindProperty("verticalResolution");
            numColors = serObj.FindProperty("numColors");
            color0 = serObj.FindProperty("color0");
            color1 = serObj.FindProperty("color1");
            color2 = serObj.FindProperty("color2");
            color3 = serObj.FindProperty("color3");
            color4 = serObj.FindProperty("color4");
            color5 = serObj.FindProperty("color5");
            color6 = serObj.FindProperty("color6");
            color7 = serObj.FindProperty("color7");
            color8 = serObj.FindProperty("color8");
            color9 = serObj.FindProperty("color9");
            color10 = serObj.FindProperty("color10");
            color11 = serObj.FindProperty("color11");
            color12 = serObj.FindProperty("color12");
            color13 = serObj.FindProperty("color13");
            color14 = serObj.FindProperty("color14");
            color15 = serObj.FindProperty("color15");
        }

        override public void OnInspectorGUI()
        {
            serObj.Update();

            //RetroPixel myTarget = (RetroPixel) target;

            horizontalResolution.intValue = EditorGUILayout.IntField("Horizontal Resolution", horizontalResolution.intValue);
            verticalResolution.intValue = EditorGUILayout.IntField("Vertical Resolution", verticalResolution.intValue);

            image = (Texture2D)EditorGUILayout.ObjectField("Image", image, typeof(Texture2D), false);
            numColors.intValue = image.width;

            for (int i = 0; i < image.width; i++)
            {
                if (i == 0)
                {
                    color0.colorValue = image.GetPixel(i, 0);
                }
                if (i == 1)
                {
                    color1.colorValue = image.GetPixel(i, 0);
                }
                if (i == 2)
                {
                    color2.colorValue = image.GetPixel(i, 0);
                }
                if (i == 3)
                {
                    color3.colorValue = image.GetPixel(i, 0);
                }
                if (i == 4)
                {
                    color4.colorValue = image.GetPixel(i, 0);
                }
                if (i == 5)
                {
                    color5.colorValue = image.GetPixel(i, 0);
                }
                if (i == 6)
                {
                    color6.colorValue = image.GetPixel(i, 0);
                }
                if (i == 7)
                {
                    color7.colorValue = image.GetPixel(i, 0);
                }
                if (i == 8)
                {
                    color8.colorValue = image.GetPixel(i, 0);
                }
                if (i == 9)
                {
                    color9.colorValue = image.GetPixel(i, 0);
                }
                if (i == 10)
                {
                    color10.colorValue = image.GetPixel(i, 0);
                }
                if (i == 11)
                {
                    color11.colorValue = image.GetPixel(i, 0);
                }
                if (i == 12)
                {
                    color12.colorValue = image.GetPixel(i, 0);
                }
                if (i == 13)
                {
                    color13.colorValue = image.GetPixel(i, 0);
                }
                if (i == 14)
                {
                    color14.colorValue = image.GetPixel(i, 0);
                }
                if (i == 15)
                {
                    color15.colorValue = image.GetPixel(i, 0);
                }
            }

            serObj.ApplyModifiedProperties();
        }
    }
}
