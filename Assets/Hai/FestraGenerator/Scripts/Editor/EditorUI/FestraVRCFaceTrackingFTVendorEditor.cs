using System;
using System.Linq;
using Hai.FestraGenerator.Scripts.Components;
using UnityEditor;
using UnityEngine;
using VRC.SDK3.Avatars.ScriptableObjects;

namespace Hai.FestraGenerator.Scripts.Editor.EditorUI
{
    [CustomEditor(typeof(FestraVRCFaceTrackingFTVendor))]
    public class FestraVRCFaceTrackingFTVendorEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(FestraFTVendor.expressionParameters)));
            EditorGUILayout.LabelField("VRCFaceTracking vendor", EditorStyles.boldLabel);
            EditorGUILayout.TextField("https://github.com/benaclejames/VRCFaceTracking/wiki/Parameters");
            EditorGUILayout.HelpBox(@"This is NOT an endorsement.

It is INHERENTLY DANGEROUS to run code that someone else has written. It is your responsibility to exercise caution when running projects.", MessageType.Warning);
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(FestraFTVendor.debugShowInfluences)));
            
            DisplayGroupFor(nameof(FestraVRCFaceTrackingFTVendor.GROUP_Eye_Tracking_Parameters),
                nameof(FestraVRCFaceTrackingFTVendor.EyesX),
                nameof(FestraVRCFaceTrackingFTVendor.EyesY),
                nameof(FestraVRCFaceTrackingFTVendor.LeftEyeLid),
                nameof(FestraVRCFaceTrackingFTVendor.RightEyeLid),
                nameof(FestraVRCFaceTrackingFTVendor.CombinedEyeLid),
                nameof(FestraVRCFaceTrackingFTVendor.EyesWiden),
                nameof(FestraVRCFaceTrackingFTVendor.EyesDilation),
                nameof(FestraVRCFaceTrackingFTVendor.EyesSqueeze),
                nameof(FestraVRCFaceTrackingFTVendor.LeftEyeX),
                nameof(FestraVRCFaceTrackingFTVendor.LeftEyeY),
                nameof(FestraVRCFaceTrackingFTVendor.RightEyeX),
                nameof(FestraVRCFaceTrackingFTVendor.RightEyeY),
                nameof(FestraVRCFaceTrackingFTVendor.LeftEyeWiden),
                nameof(FestraVRCFaceTrackingFTVendor.RightEyeWiden),
                nameof(FestraVRCFaceTrackingFTVendor.LeftEyeSqueeze),
                nameof(FestraVRCFaceTrackingFTVendor.RightEyeSqueeze),
                nameof(FestraVRCFaceTrackingFTVendor.LeftEyeLidExpanded),
                nameof(FestraVRCFaceTrackingFTVendor.RightEyeLidExpanded),
                nameof(FestraVRCFaceTrackingFTVendor.CombinedEyeLidExpanded),
                nameof(FestraVRCFaceTrackingFTVendor.LeftEyeLidExpandedSqueeze),
                nameof(FestraVRCFaceTrackingFTVendor.RightEyeLidExpandedSqueeze),
                nameof(FestraVRCFaceTrackingFTVendor.CombinedEyeLidExpandedSqueeze));

            DisplayGroupFor(nameof(FestraVRCFaceTrackingFTVendor.GROUP_Lip_Tracking_Parameters),
                nameof(FestraVRCFaceTrackingFTVendor.JawRight),
                nameof(FestraVRCFaceTrackingFTVendor.JawLeft),
                nameof(FestraVRCFaceTrackingFTVendor.JawForward),
                nameof(FestraVRCFaceTrackingFTVendor.JawOpen),
                nameof(FestraVRCFaceTrackingFTVendor.MouthApeShape),
                nameof(FestraVRCFaceTrackingFTVendor.MouthUpperRight),
                nameof(FestraVRCFaceTrackingFTVendor.MouthUpperLeft),
                nameof(FestraVRCFaceTrackingFTVendor.MouthLowerRight),
                nameof(FestraVRCFaceTrackingFTVendor.MouthLowerLeft),
                nameof(FestraVRCFaceTrackingFTVendor.MouthUpperOverturn),
                nameof(FestraVRCFaceTrackingFTVendor.MouthLowerOverturn),
                nameof(FestraVRCFaceTrackingFTVendor.MouthPout),
                nameof(FestraVRCFaceTrackingFTVendor.MouthSmileRight),
                nameof(FestraVRCFaceTrackingFTVendor.MouthSmileLeft),
                nameof(FestraVRCFaceTrackingFTVendor.MouthSadRight),
                nameof(FestraVRCFaceTrackingFTVendor.MouthSadLeft),
                nameof(FestraVRCFaceTrackingFTVendor.CheekPuffRight),
                nameof(FestraVRCFaceTrackingFTVendor.CheekPuffLeft),
                nameof(FestraVRCFaceTrackingFTVendor.CheekSuck),
                nameof(FestraVRCFaceTrackingFTVendor.MouthUpperUpRight),
                nameof(FestraVRCFaceTrackingFTVendor.MouthUpperUpLeft),
                nameof(FestraVRCFaceTrackingFTVendor.MouthLowerDownRight),
                nameof(FestraVRCFaceTrackingFTVendor.MouthLowerDownLeft),
                nameof(FestraVRCFaceTrackingFTVendor.MouthUpperInside),
                nameof(FestraVRCFaceTrackingFTVendor.MouthLowerInside),
                nameof(FestraVRCFaceTrackingFTVendor.MouthLowerOverlay),
                nameof(FestraVRCFaceTrackingFTVendor.TongueLongStep1),
                nameof(FestraVRCFaceTrackingFTVendor.TongueLongStep2),
                nameof(FestraVRCFaceTrackingFTVendor.TongueDown),
                nameof(FestraVRCFaceTrackingFTVendor.TongueUp),
                nameof(FestraVRCFaceTrackingFTVendor.TongueRight),
                nameof(FestraVRCFaceTrackingFTVendor.TongueLeft),
                nameof(FestraVRCFaceTrackingFTVendor.TongueRoll),
                nameof(FestraVRCFaceTrackingFTVendor.TongueUpLeftMorph),
                nameof(FestraVRCFaceTrackingFTVendor.TongueUpRightMorph),
                nameof(FestraVRCFaceTrackingFTVendor.TongueDownLeftMorph),
                nameof(FestraVRCFaceTrackingFTVendor.TongueDownRightMorph));

            DisplayGroupFor(nameof(FestraVRCFaceTrackingFTVendor.GROUP_General_Combined_Lip_Parameters),
                nameof(FestraVRCFaceTrackingFTVendor.JawX),
                nameof(FestraVRCFaceTrackingFTVendor.MouthUpper),
                nameof(FestraVRCFaceTrackingFTVendor.MouthLower),
                nameof(FestraVRCFaceTrackingFTVendor.MouthX),
                nameof(FestraVRCFaceTrackingFTVendor.SmileSadRight),
                nameof(FestraVRCFaceTrackingFTVendor.SmileSadLeft),
                nameof(FestraVRCFaceTrackingFTVendor.SmileSad),
                nameof(FestraVRCFaceTrackingFTVendor.TongueY),
                nameof(FestraVRCFaceTrackingFTVendor.TongueX),
                nameof(FestraVRCFaceTrackingFTVendor.TongueSteps),
                nameof(FestraVRCFaceTrackingFTVendor.PuffSuckRight),
                nameof(FestraVRCFaceTrackingFTVendor.PuffSuckLeft),
                nameof(FestraVRCFaceTrackingFTVendor.PuffSuck));

            DisplayGroupFor(nameof(FestraVRCFaceTrackingFTVendor.GROUP_Jaw_Open_Combined_Lip_Parameters),
                nameof(FestraVRCFaceTrackingFTVendor.JawOpenApe),
                nameof(FestraVRCFaceTrackingFTVendor.JawOpenPuff),
                nameof(FestraVRCFaceTrackingFTVendor.JawOpenPuffRight),
                nameof(FestraVRCFaceTrackingFTVendor.JawOpenPuffLeft),
                nameof(FestraVRCFaceTrackingFTVendor.JawOpenSuck),
                nameof(FestraVRCFaceTrackingFTVendor.JawOpenForward));

            DisplayGroupFor(nameof(FestraVRCFaceTrackingFTVendor.GROUP_Mouth_Upper_Up_Right_Combined_Lip_Parameters),
                nameof(FestraVRCFaceTrackingFTVendor.MouthUpperUpRightUpperInside),
                nameof(FestraVRCFaceTrackingFTVendor.MouthUpperUpRightPuffRight),
                nameof(FestraVRCFaceTrackingFTVendor.MouthUpperUpRightApe),
                nameof(FestraVRCFaceTrackingFTVendor.MouthUpperUpRightPout),
                nameof(FestraVRCFaceTrackingFTVendor.MouthUpperUpRightOverlay));

            DisplayGroupFor(nameof(FestraVRCFaceTrackingFTVendor.GROUP_Mouth_Upper_Up_Left_Combined_Parameters),
                nameof(FestraVRCFaceTrackingFTVendor.MouthUpperUpLeftUpperInside),
                nameof(FestraVRCFaceTrackingFTVendor.MouthUpperUpLeftPuffLeft),
                nameof(FestraVRCFaceTrackingFTVendor.MouthUpperUpLeftApe),
                nameof(FestraVRCFaceTrackingFTVendor.MouthUpperUpLeftPout),
                nameof(FestraVRCFaceTrackingFTVendor.MouthUpperUpLeftOverlay));

            DisplayGroupFor(nameof(FestraVRCFaceTrackingFTVendor.GROUP_Mouth_Upper_Up_Combined_Parameters),
                nameof(FestraVRCFaceTrackingFTVendor.MouthUpperUpUpperInside),
                nameof(FestraVRCFaceTrackingFTVendor.MouthUpperUpInside),
                nameof(FestraVRCFaceTrackingFTVendor.MouthUpperUpPuff),
                nameof(FestraVRCFaceTrackingFTVendor.MouthUpperUpPuffLeft),
                nameof(FestraVRCFaceTrackingFTVendor.MouthUpperUpPuffRight),
                nameof(FestraVRCFaceTrackingFTVendor.MouthUpperUpApe),
                nameof(FestraVRCFaceTrackingFTVendor.MouthUpperUpPout),
                nameof(FestraVRCFaceTrackingFTVendor.MouthUpperUpOverlay));

            DisplayGroupFor(nameof(FestraVRCFaceTrackingFTVendor.GROUP_Mouth_Lower_Down_Right_Combined_Parameters),
                nameof(FestraVRCFaceTrackingFTVendor.MouthLowerDownRightLowerInside),
                nameof(FestraVRCFaceTrackingFTVendor.MouthLowerDownRightPuffRight),
                nameof(FestraVRCFaceTrackingFTVendor.MouthLowerDownRightApe),
                nameof(FestraVRCFaceTrackingFTVendor.MouthLowerDownRightPout),
                nameof(FestraVRCFaceTrackingFTVendor.MouthLowerDownRightOverlay));

            DisplayGroupFor(nameof(FestraVRCFaceTrackingFTVendor.GROUP_Mouth_Lower_Down_Left_Combined_Parameters),
                nameof(FestraVRCFaceTrackingFTVendor.MouthLowerDownLeftLowerInside),
                nameof(FestraVRCFaceTrackingFTVendor.MouthLowerDownLeftPuffLeft),
                nameof(FestraVRCFaceTrackingFTVendor.MouthLowerDownLeftApe),
                nameof(FestraVRCFaceTrackingFTVendor.MouthLowerDownLeftPout),
                nameof(FestraVRCFaceTrackingFTVendor.MouthLowerDownLeftOverlay));

            DisplayGroupFor(nameof(FestraVRCFaceTrackingFTVendor.GROUP_Mouth_Lower_Down_Combined_Parameters),
                nameof(FestraVRCFaceTrackingFTVendor.MouthLowerDownLowerInside),
                nameof(FestraVRCFaceTrackingFTVendor.MouthLowerDownInside),
                nameof(FestraVRCFaceTrackingFTVendor.MouthLowerDownPuff),
                nameof(FestraVRCFaceTrackingFTVendor.MouthLowerDownPuffLeft),
                nameof(FestraVRCFaceTrackingFTVendor.MouthLowerDownPuffRight),
                nameof(FestraVRCFaceTrackingFTVendor.MouthLowerDownApe),
                nameof(FestraVRCFaceTrackingFTVendor.MouthLowerDownPout),
                nameof(FestraVRCFaceTrackingFTVendor.MouthLowerDownOverlay));

            DisplayGroupFor(nameof(FestraVRCFaceTrackingFTVendor.GROUP_Smile_Right_Combined_Parameters),
                nameof(FestraVRCFaceTrackingFTVendor.SmileRightUpperOverturn),
                nameof(FestraVRCFaceTrackingFTVendor.SmileRightLowerOverturn),
                nameof(FestraVRCFaceTrackingFTVendor.SmileRightOverturn),
                nameof(FestraVRCFaceTrackingFTVendor.SmileRightApe),
                nameof(FestraVRCFaceTrackingFTVendor.SmileRightOverlay),
                nameof(FestraVRCFaceTrackingFTVendor.SmileRightPout));

            DisplayGroupFor(nameof(FestraVRCFaceTrackingFTVendor.GROUP_Smile_Left_Combined_Parameters),
                nameof(FestraVRCFaceTrackingFTVendor.SmileLeftUpperOverturn),
                nameof(FestraVRCFaceTrackingFTVendor.SmileLeftLowerOverturn),
                nameof(FestraVRCFaceTrackingFTVendor.SmileLeftOverturn),
                nameof(FestraVRCFaceTrackingFTVendor.SmileLeftApe),
                nameof(FestraVRCFaceTrackingFTVendor.SmileLeftOverlay),
                nameof(FestraVRCFaceTrackingFTVendor.SmileLeftPout));

            DisplayGroupFor(nameof(FestraVRCFaceTrackingFTVendor.GROUP_Smile_Combined_Parameters),
                nameof(FestraVRCFaceTrackingFTVendor.SmileUpperOverturn),
                nameof(FestraVRCFaceTrackingFTVendor.SmileLowerOverturn),
                nameof(FestraVRCFaceTrackingFTVendor.SmileApe),
                nameof(FestraVRCFaceTrackingFTVendor.SmileOverlay),
                nameof(FestraVRCFaceTrackingFTVendor.SmilePout));

            DisplayGroupFor(nameof(FestraVRCFaceTrackingFTVendor.GROUP_Cheek_Puff_Right_Combined_Parameters),
                nameof(FestraVRCFaceTrackingFTVendor.PuffRightUpperOverturn),
                nameof(FestraVRCFaceTrackingFTVendor.PuffRightLowerOverturn),
                nameof(FestraVRCFaceTrackingFTVendor.PuffRightOverturn));

            DisplayGroupFor(nameof(FestraVRCFaceTrackingFTVendor.GROUP_Cheek_Puff_Left_Combined_Parameters),
                nameof(FestraVRCFaceTrackingFTVendor.PuffLeftUpperOverturn),
                nameof(FestraVRCFaceTrackingFTVendor.PuffLeftLowerOverturn),
                nameof(FestraVRCFaceTrackingFTVendor.PuffLeftOverturn));

            DisplayGroupFor(nameof(FestraVRCFaceTrackingFTVendor.GROUP_Cheek_Puff_Combined_Parameters),
                nameof(FestraVRCFaceTrackingFTVendor.PuffUpperOverturn),
                nameof(FestraVRCFaceTrackingFTVendor.PuffLowerOverturn),
                nameof(FestraVRCFaceTrackingFTVendor.PuffOverturn));
                
            serializedObject.ApplyModifiedProperties();
        }

        private void DisplayGroupFor(string groupPropertyName, params string[] constituents)
        {
            var vendor = ((FestraFTVendor)target);
            var expressionParametersNullable = vendor.expressionParameters;
            // var group = serializedObject.FindProperty(groupPropertyName);
            var map = ((FestraVRCFaceTrackingFTVendor)target).ExposeMap();
            EditorGUILayout.LabelField(groupPropertyName.Replace("GROUP_", "").Replace("_", " "), EditorStyles.boldLabel);
            // EditorGUILayout.PropertyField(group, new GUIContent("Group"));
            // var groupValue = (FestraVendorGroup)group.intValue;
            // var isSome = groupValue == FestraVendorGroup.Some;
            // EditorGUI.BeginDisabledGroup(!isSome);
            EditorGUILayout.BeginVertical("GroupBox");
            foreach (var constituent in constituents)
            {
                // FieldFor(constituent, groupValue);
                EditorGUILayout.BeginHorizontal();
                var sp = serializedObject.FindProperty(constituent);
                EditorGUILayout.PropertyField(sp, new GUIContent(constituent));
                if (!serializedObject.isEditingMultipleObjects && expressionParametersNullable != null)
                {
                    var isOn = sp.boolValue;
                    var contains = expressionParametersNullable.parameters.Any(parameter => parameter.name == constituent);
                    if (isOn && !contains)
                    {
                        if (GUILayout.Button("Update params"))
                        {
                            var newArray = expressionParametersNullable.parameters.Concat(new[]
                            {
                                new VRCExpressionParameters.Parameter
                                {
                                    name = constituent,
                                    defaultValue = 0f,
                                    saved = false,
                                    valueType = VRCExpressionParameters.ValueType.Float
                                }
                            }).ToArray();
                            expressionParametersNullable.parameters = newArray;
                            EditorUtility.SetDirty(expressionParametersNullable);
                        }
                    }
                    if (!isOn && contains)
                    {
                        if (GUILayout.Button("Update params"))
                        {
                            var newArray = expressionParametersNullable.parameters
                                .Where(parameter => parameter.name != constituent)
                                .ToArray();
                            expressionParametersNullable.parameters = newArray;
                            EditorUtility.SetDirty(expressionParametersNullable);
                        }
                    }
                }
                EditorGUILayout.EndHorizontal();
                if (vendor.debugShowInfluences == FestraFTVendor.FestraDebugInfluence.All || vendor.debugShowInfluences == FestraFTVendor.FestraDebugInfluence.OnlyActive && sp.boolValue)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.TextField(string.Join(", ", map[constituent].Select(actuator => actuator.element + $"[{actuator.actuator.neutral}:{actuator.actuator.actuated}]").ToArray()));
                    EditorGUILayout.EndHorizontal();
                }
            }
            EditorGUILayout.EndVertical();
            // EditorGUI.EndDisabledGroup();
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