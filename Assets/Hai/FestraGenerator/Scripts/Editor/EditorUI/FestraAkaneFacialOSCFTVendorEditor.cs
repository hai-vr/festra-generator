using System;
using Hai.FestraGenerator.Scripts.Components;
using UnityEditor;
using UnityEngine;

namespace Hai.FestraGenerator.Scripts.Editor.EditorUI
{
    [CustomEditor(typeof(FestraAkaneFacialOSCFTVendor))]
    public class FestraAkaneFacialOSCFTVendorEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DisplayGroupFor(
                nameof(FestraAkaneFacialOSCFTVendor.GROUP_目の周りのデータ__SDKの計算方法と同様に計算した値),
                nameof(FestraAkaneFacialOSCFTVendor.Eye_Left_Blink),
                nameof(FestraAkaneFacialOSCFTVendor.Eye_Left_Wide),
                nameof(FestraAkaneFacialOSCFTVendor.Eye_Left_Right),
                nameof(FestraAkaneFacialOSCFTVendor.Eye_Left_Left),
                nameof(FestraAkaneFacialOSCFTVendor.Eye_Left_Up),
                nameof(FestraAkaneFacialOSCFTVendor.Eye_Left_Down),
                nameof(FestraAkaneFacialOSCFTVendor.Eye_Right_Blink),
                nameof(FestraAkaneFacialOSCFTVendor.Eye_Right_Wide),
                nameof(FestraAkaneFacialOSCFTVendor.Eye_Right_Right),
                nameof(FestraAkaneFacialOSCFTVendor.Eye_Right_Left),
                nameof(FestraAkaneFacialOSCFTVendor.Eye_Right_Up),
                nameof(FestraAkaneFacialOSCFTVendor.Eye_Right_Down),
                nameof(FestraAkaneFacialOSCFTVendor.Eye_Left_Frown),
                nameof(FestraAkaneFacialOSCFTVendor.Eye_Right_Frown),
                nameof(FestraAkaneFacialOSCFTVendor.Eye_Left_Squeeze),
                nameof(FestraAkaneFacialOSCFTVendor.Eye_Right_Squeeze));

            DisplayGroupFor(
                nameof(FestraAkaneFacialOSCFTVendor.GROUP_視線__アプリ内で計算された値),
                nameof(FestraAkaneFacialOSCFTVendor.Gaze_Left_Vertical),
                nameof(FestraAkaneFacialOSCFTVendor.Gaze_Left_Horizontal),
                nameof(FestraAkaneFacialOSCFTVendor.Gaze_Right_Vertical),
                nameof(FestraAkaneFacialOSCFTVendor.Gaze_Right_Horizontal),
                nameof(FestraAkaneFacialOSCFTVendor.Gaze_Vertical),
                nameof(FestraAkaneFacialOSCFTVendor.Gaze_Horizontal));

            DisplayGroupFor(
                nameof(FestraAkaneFacialOSCFTVendor.GROUP_目__計算処理済みの値),
                nameof(FestraAkaneFacialOSCFTVendor.Eye_Blink),
                nameof(FestraAkaneFacialOSCFTVendor.Eye_Wide),
                nameof(FestraAkaneFacialOSCFTVendor.Eye_Right),
                nameof(FestraAkaneFacialOSCFTVendor.Eye_Left),
                nameof(FestraAkaneFacialOSCFTVendor.Eye_Up),
                nameof(FestraAkaneFacialOSCFTVendor.Eye_Down),
                nameof(FestraAkaneFacialOSCFTVendor.Eye_Frown),
                nameof(FestraAkaneFacialOSCFTVendor.Eye_Squeeze));

            DisplayGroupFor(
                nameof(FestraAkaneFacialOSCFTVendor.GROUP_顔__トラッカで取得した生の値),
                nameof(FestraAkaneFacialOSCFTVendor.Jaw_Right),
                nameof(FestraAkaneFacialOSCFTVendor.Jaw_Left),
                nameof(FestraAkaneFacialOSCFTVendor.Jaw_Forward),
                nameof(FestraAkaneFacialOSCFTVendor.Jaw_Open),
                nameof(FestraAkaneFacialOSCFTVendor.Mouth_Ape_Shape),
                nameof(FestraAkaneFacialOSCFTVendor.Mouth_Upper_Right),
                nameof(FestraAkaneFacialOSCFTVendor.Mouth_Upper_Left),
                nameof(FestraAkaneFacialOSCFTVendor.Mouth_Lower_Right),
                nameof(FestraAkaneFacialOSCFTVendor.Mouth_Lower_Left),
                nameof(FestraAkaneFacialOSCFTVendor.Mouth_Upper_Overturn),
                nameof(FestraAkaneFacialOSCFTVendor.Mouth_Lower_Overturn),
                nameof(FestraAkaneFacialOSCFTVendor.Mouth_Pout),
                nameof(FestraAkaneFacialOSCFTVendor.Mouth_Smile_Right),
                nameof(FestraAkaneFacialOSCFTVendor.Mouth_Smile_Left),
                nameof(FestraAkaneFacialOSCFTVendor.Mouth_Sad_Right),
                nameof(FestraAkaneFacialOSCFTVendor.Mouth_Sad_Left),
                nameof(FestraAkaneFacialOSCFTVendor.Cheek_Puff_Right),
                nameof(FestraAkaneFacialOSCFTVendor.Cheek_Puff_Left),
                nameof(FestraAkaneFacialOSCFTVendor.Cheek_Suck),
                nameof(FestraAkaneFacialOSCFTVendor.Mouth_Upper_UpRight),
                nameof(FestraAkaneFacialOSCFTVendor.Mouth_Upper_UpLeft),
                nameof(FestraAkaneFacialOSCFTVendor.Mouth_Lower_DownRight),
                nameof(FestraAkaneFacialOSCFTVendor.Mouth_Lower_DownLeft),
                nameof(FestraAkaneFacialOSCFTVendor.Mouth_Upper_Inside),
                nameof(FestraAkaneFacialOSCFTVendor.Mouth_Lower_Inside),
                nameof(FestraAkaneFacialOSCFTVendor.Mouth_Lower_Overlay),
                nameof(FestraAkaneFacialOSCFTVendor.Tongue_LongStep1),
                nameof(FestraAkaneFacialOSCFTVendor.Tongue_LongStep2),
                nameof(FestraAkaneFacialOSCFTVendor.Tongue_Down),
                nameof(FestraAkaneFacialOSCFTVendor.Tongue_Up),
                nameof(FestraAkaneFacialOSCFTVendor.Tongue_Right),
                nameof(FestraAkaneFacialOSCFTVendor.Tongue_Left),
                nameof(FestraAkaneFacialOSCFTVendor.Tongue_Roll),
                nameof(FestraAkaneFacialOSCFTVendor.Tongue_UpLeft_Morph),
                nameof(FestraAkaneFacialOSCFTVendor.Tongue_UpRight_Morph),
                nameof(FestraAkaneFacialOSCFTVendor.Tongue_DownLeft_Morph),
                nameof(FestraAkaneFacialOSCFTVendor.Tongue_DownRight_Morph));

            DisplayGroupFor(
                nameof(FestraAkaneFacialOSCFTVendor.GROUP_顔__アプリ内で計算_統合したデータ),
                nameof(FestraAkaneFacialOSCFTVendor.Jaw_Left_Right),
                nameof(FestraAkaneFacialOSCFTVendor.Mouth_Sad_Smile_Right),
                nameof(FestraAkaneFacialOSCFTVendor.Mouth_Sad_Smile_Left),
                nameof(FestraAkaneFacialOSCFTVendor.Mouth_Smile),
                nameof(FestraAkaneFacialOSCFTVendor.Mouth_Sad),
                nameof(FestraAkaneFacialOSCFTVendor.Mouth_Sad_Smile),
                nameof(FestraAkaneFacialOSCFTVendor.Mouth_Upper_Left_Right),
                nameof(FestraAkaneFacialOSCFTVendor.Mouth_Lower_Left_Right),
                nameof(FestraAkaneFacialOSCFTVendor.Mouth_Left_Right),
                nameof(FestraAkaneFacialOSCFTVendor.Mouth_Upper_Inside_Overturn),
                nameof(FestraAkaneFacialOSCFTVendor.Mouth_Lower_Inside_Overturn),
                nameof(FestraAkaneFacialOSCFTVendor.Cheek_Puff),
                nameof(FestraAkaneFacialOSCFTVendor.Cheek_Suck_Puff),
                nameof(FestraAkaneFacialOSCFTVendor.Mouth_Upper_Up),
                nameof(FestraAkaneFacialOSCFTVendor.Mouth_Lower_Down),
                nameof(FestraAkaneFacialOSCFTVendor.Tongue_Left_Right),
                nameof(FestraAkaneFacialOSCFTVendor.Tongue_Down_Up));
            
            serializedObject.ApplyModifiedProperties();
        }

        private void DisplayGroupFor(string groupPropertyName, params string[] constituents)
        {
            var group = serializedObject.FindProperty(groupPropertyName);
            EditorGUILayout.LabelField(groupPropertyName.Replace("GROUP_", "").Replace("_", " "), EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(group, new GUIContent("Group"));
            var groupValue = (FestraVendorGroup)group.intValue;
            var isSome = groupValue == FestraVendorGroup.Some;
            EditorGUI.BeginDisabledGroup(!isSome);
            EditorGUILayout.BeginVertical("GroupBox");
            foreach (var constituent in constituents)
            {
                FieldFor(constituent, groupValue);
            }
            EditorGUILayout.EndVertical();
            EditorGUI.EndDisabledGroup();
        }
        
        private void FieldFor(string propertyName, FestraVendorGroup groupValue)
        {
            switch (groupValue)
            {
                case FestraVendorGroup.None:
                    EditorGUILayout.Toggle(propertyName, false);
                    break;
                case FestraVendorGroup.All:
                    EditorGUILayout.Toggle(propertyName, true);
                    break;
                case FestraVendorGroup.Some:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(propertyName), new GUIContent(propertyName));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(groupValue), groupValue, null);
            }
        }
    }
}