using System;
using System.IO;
using System.Linq;
using Hai.FestraGenerator.Scripts.Components;
using Hai.FestraGenerator.Scripts.Editor.Internal;
using UnityEditor;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using AnimatorController = UnityEditor.Animations.AnimatorController;

namespace Hai.FestraGenerator.Scripts.Editor.EditorUI
{
    [CustomEditor(typeof(FestraCompiler))]
    public class FestraCompilerEditor : UnityEditor.Editor
    {
        public SerializedProperty animatorController;
        public SerializedProperty useGesturePlayableLayer;
        public SerializedProperty gesturePlayableLayerController;
        public SerializedProperty customEmptyClip;
        public SerializedProperty analogBlinkingUpperThreshold;

        public SerializedProperty doNotGenerateBlinkingOverrideLayer;

        public SerializedProperty writeDefaultsRecommendationMode;
        public SerializedProperty writeDefaultsRecommendationModeGesture;
        public SerializedProperty generatedAvatarMask;

        public SerializedProperty avatarDescriptor;

        public SerializedProperty assetContainer;
        public SerializedProperty generateNewContainerEveryTime;

        public SerializedProperty editorAdvancedFoldout;

        public SerializedProperty faceTracking;
        private SerializedProperty doNotForceBlinkBlendshapes;
        private SerializedProperty mmdCompatibilityToggleParameter;

        private void OnEnable()
        {
            animatorController = serializedObject.FindProperty(nameof(FestraCompiler.animatorController));
            useGesturePlayableLayer = serializedObject.FindProperty(nameof(FestraCompiler.useGesturePlayableLayer));
            gesturePlayableLayerController = serializedObject.FindProperty(nameof(FestraCompiler.gesturePlayableLayerController));
            customEmptyClip = serializedObject.FindProperty(nameof(FestraCompiler.customEmptyClip));
            analogBlinkingUpperThreshold = serializedObject.FindProperty(nameof(FestraCompiler.analogBlinkingUpperThreshold));

            doNotGenerateBlinkingOverrideLayer = serializedObject.FindProperty(nameof(FestraCompiler.doNotGenerateBlinkingOverrideLayer));

            writeDefaultsRecommendationMode = serializedObject.FindProperty(nameof(FestraCompiler.writeDefaultsRecommendationMode));
            writeDefaultsRecommendationModeGesture = serializedObject.FindProperty(nameof(FestraCompiler.writeDefaultsRecommendationModeGesture));
            generatedAvatarMask = serializedObject.FindProperty(nameof(FestraCompiler.generatedAvatarMask));

            avatarDescriptor = serializedObject.FindProperty(nameof(FestraCompiler.avatarDescriptor));

            assetContainer = serializedObject.FindProperty(nameof(FestraCompiler.assetContainer));
            generateNewContainerEveryTime = serializedObject.FindProperty(nameof(FestraCompiler.generateNewContainerEveryTime));

            doNotForceBlinkBlendshapes = serializedObject.FindProperty(nameof(FestraCompiler.doNotForceBlinkBlendshapes));
            mmdCompatibilityToggleParameter = serializedObject.FindProperty(nameof(FestraCompiler.mmdCompatibilityToggleParameter));
            faceTracking = serializedObject.FindProperty(nameof(FestraCompiler.faceTracking));

            // reference: https://blog.terresquall.com/2020/03/creating-reorderable-lists-in-the-unity-inspector/

            editorAdvancedFoldout = serializedObject.FindProperty(nameof(FestraCompiler.editorAdvancedFoldout));
        }

        public override void OnInspectorGUI()
        {
            var compiler = AsCompiler();
            serializedObject.Update();
            var italic = new GUIStyle(GUI.skin.label) {fontStyle = FontStyle.Italic};

            if (GUILayout.Button("Switch language (English / 日本語)"))
            {
                FestraLocalization.CycleLocale();
            }

            if (FestraLocalization.IsEnglishLocaleActive())
            {
                EditorGUILayout.LabelField("");
            }
            else
            {
                EditorGUILayout.LabelField("一部の翻訳は正確ではありません。festra.jp.jsonを編集することができます。");
            }

            if (GUILayout.Button(new GUIContent(FestraLocale.FESTRAC_Documentation_and_tutorials)))
            {
                Application.OpenURL(FestraLocale.DocumentationUrl());
            }

            EditorGUILayout.LabelField(FestraLocale.FESTRAC_Mood_sets, EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(avatarDescriptor, new GUIContent(FestraLocale.FESTRAC_Avatar_descriptor));

            EditorGUILayout.Separator();

            EditorGUILayout.LabelField(FestraLocale.FESTRAC_FX_Playable_Layer, EditorStyles.boldLabel);
            EditorGUILayout.LabelField(FestraLocale.FESTRAC_BackupFX, italic);
            EditorGUILayout.PropertyField(animatorController, new GUIContent(FestraLocale.FESTRAC_FX_Animator_Controller));
            EditorGUILayout.PropertyField(writeDefaultsRecommendationMode, new GUIContent(FestraLocale.FESTRAC_FX_Playable_Mode));
            WriteDefaultsSection(writeDefaultsRecommendationMode);

            EditorGUILayout.PropertyField(doNotForceBlinkBlendshapes, new GUIContent(FestraLocale.FESTRAC_DoNotForceBlinkBlendshapes));

            EditorGUILayout.Separator();

            EditorGUILayout.LabelField(FestraLocale.FESTRAC_Gesture_Playable_Layer, EditorStyles.boldLabel);
            EditorGUILayout.LabelField(FestraLocale.FESTRAC_Support_for_other_transforms, italic);
            EditorGUILayout.LabelField(FestraLocale.FESTRAC_MusclesUnsupported, italic);
            EditorGUILayout.PropertyField(useGesturePlayableLayer, new GUIContent(FestraLocale.FESTRAC_Gesture_playable_layer_support));
            if (useGesturePlayableLayer.boolValue)
            {
                EditorGUILayout.LabelField(FestraLocale.FESTRAC_BackupGesture, italic);
                EditorGUILayout.PropertyField(gesturePlayableLayerController, new GUIContent(FestraLocale.FESTRAC_Gesture_Animator_Controller));

                EditorGUILayout.PropertyField(writeDefaultsRecommendationModeGesture, new GUIContent(FestraLocale.FESTRAC_Gesture_Playable_Mode));
                WriteDefaultsSection(writeDefaultsRecommendationModeGesture);

                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.PropertyField(generatedAvatarMask, new GUIContent(FestraLocale.FESTRAC_Asset_container));
                EditorGUI.EndDisabledGroup();

                var missingMaskCount = FestraMaskApplicator.FindAllLayersMissingAMask(compiler.animatorController).Count();
                if (missingMaskCount > 0)
                {
                    EditorGUILayout.HelpBox(string.Format(FestraLocale.FESTRAC_MissingFxMask, missingMaskCount), MessageType.Error);
                }

                EditorGUI.BeginDisabledGroup(compiler.avatarDescriptor == null || compiler.animatorController == null || missingMaskCount == 0);
                if (GUILayout.Button(FestraLocale.FESTRAC_Add_missing_masks))
                {
                    AddMissingMasks(compiler);
                }
                EditorGUI.EndDisabledGroup();

                if (compiler.generatedAvatarMask != null)
                {
                    EditorGUI.BeginDisabledGroup(compiler.avatarDescriptor == null || compiler.animatorController == null);
                    MaskRemovalUi(compiler);
                    EditorGUI.EndDisabledGroup();
                }
            }
            else
            {
                if (compiler.animatorController != null && compiler.generatedAvatarMask != null)
                {
                    MaskRemovalUi(compiler);
                }
            }

            EditorGUILayout.Separator();

            EditorGUILayout.LabelField(FestraLocale.FESTRAC_Synchronization, EditorStyles.boldLabel);

            var canSync = ThereIsNoAnimatorController() ||
                          ThereIsNoGestureAnimatorController() ||
                          ThereIsNoAvatarDescriptor();
            EditorGUI.BeginDisabledGroup(canSync);
            
            bool ThereIsNoAnimatorController()
            {
                return animatorController.objectReferenceValue == null;
            }

            bool ThereIsNoGestureAnimatorController()
            {
                return useGesturePlayableLayer.boolValue && gesturePlayableLayerController.objectReferenceValue == null;
            }

            bool ThereIsNoAvatarDescriptor()
            {
                return !compiler.bypassMandatoryAvatarDescriptor
                       && compiler.avatarDescriptor == null;
            }

            if (compiler.totalNumberOfGenerations >= 5)
            {
                EditorGUILayout.HelpBox(FestraLocale.FESTRAC_Slowness_warning, MessageType.Warning);
            }
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.PropertyField(faceTracking, new GUIContent(FestraLocale.FESTRAC_FaceTracking));
            EditorGUI.BeginDisabledGroup(canSync);
            if (compiler.faceTracking != null && GUILayout.Button(FestraLocale.FESTRAC_Synchronize_Face_Tracking_Layers))
            {
                DoGenerateFaceTrackingLayers();
            }
            
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.HelpBox(
                FestraLocale.FESTRAC_SynchronizationConditionsV2, MessageType.Info);

            if (compiler.assetContainer != null) {
                EditorGUILayout.LabelField(FestraLocale.FESTRAC_Asset_generation, EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(assetContainer, new GUIContent(FestraLocale.FESTRAC_Asset_container));
            }

            EditorGUILayout.Separator();

            EditorGUILayout.LabelField(FestraLocale.FESTRAC_Other_tweaks, EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(analogBlinkingUpperThreshold, new GUIContent(FestraLocale.FESTRAC_Analog_fist_blinking_threshold, FestraLocale.FESTRAC_AnalogFist_Popup));
            EditorGUILayout.PropertyField(mmdCompatibilityToggleParameter, new GUIContent(FestraLocale.FESTRAC_MMD_compatibility_toggle_parameter));

            editorAdvancedFoldout.boolValue = EditorGUILayout.Foldout(editorAdvancedFoldout.boolValue, FestraLocale.FESTRAC_Advanced);
            if (editorAdvancedFoldout.boolValue)
            {
                EditorGUILayout.LabelField("Corrections", EditorStyles.boldLabel);

                EditorGUILayout.LabelField("Fine tuning", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(customEmptyClip, new GUIContent("Custom 2-frame empty animation clip (optional)"));

                EditorGUILayout.Separator();

                EditorGUILayout.LabelField("Layer generation", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(doNotGenerateBlinkingOverrideLayer, new GUIContent("Don't update Blinking layer"));
                GenBlinkingWarning(true);

                EditorGUILayout.LabelField("Animation generation", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(assetContainer, new GUIContent(FestraLocale.FESTRAC_Asset_container));

                EditorGUI.BeginDisabledGroup(assetContainer.objectReferenceValue != null);
                EditorGUILayout.PropertyField(generateNewContainerEveryTime, new GUIContent("Don't keep track of newly generated containers"));
                if (animatorController.objectReferenceValue != null)
                {
                    EditorGUILayout.LabelField("Assets will be generated in:");
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUI.EndDisabledGroup();
                }
                EditorGUI.EndDisabledGroup();

                EditorGUILayout.Separator();

                EditorGUILayout.LabelField("Special cases", EditorStyles.boldLabel);

                EditorGUILayout.LabelField("Translations", EditorStyles.boldLabel);
                if (GUILayout.Button("(Debug) Print default translation file to console"))
                {
                    Debug.Log(FestraLocale.CompileDefaultLocaleJson());
                }
                if (GUILayout.Button("(Debug) Reload localization files"))
                {
                    FestraLocalization.ReloadLocalizations();
                }
            }
            else
            {
                GenBlinkingWarning(false);
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void MaskRemovalUi(FestraCompiler compiler)
        {
            var isMaskUsed = compiler.animatorController != null && ((AnimatorController) compiler.animatorController).layers.Any(layer => layer.avatarMask == compiler.generatedAvatarMask);
            EditorGUI.BeginDisabledGroup(!isMaskUsed);
            if (GUILayout.Button(FestraLocale.FESTRAC_Remove_applied_masks))
            {
                DoRemoveAppliedMasks(compiler);
            }

            EditorGUI.EndDisabledGroup();

            EditorGUI.BeginDisabledGroup(isMaskUsed);
            if (GUILayout.Button(FestraLocale.FESTRAC_Unbind_Asset_container))
            {
                DoRemoveAppliedMasksAndAssetContainer(compiler);
            }

            EditorGUI.EndDisabledGroup();
        }

        private void AddMissingMasks(FestraCompiler compiler)
        {
            CreateAvatarMaskAssetIfNecessary(compiler);
            new FestraMaskApplicator(compiler.animatorController, compiler.generatedAvatarMask).AddMissingMasks();
            new FestraMaskApplicator(compiler.animatorController, compiler.generatedAvatarMask).UpdateMask();
        }

        private void DoRemoveAppliedMasks(FestraCompiler compiler)
        {
            new FestraMaskApplicator(AsCompiler().animatorController, compiler.generatedAvatarMask).RemoveAppliedMask();
        }

        private void DoRemoveAppliedMasksAndAssetContainer(FestraCompiler compiler)
        {
            new FestraMaskApplicator(AsCompiler().animatorController, compiler.generatedAvatarMask).RemoveAppliedMask();
            generatedAvatarMask.objectReferenceValue = null;
        }

        private void CreateAvatarMaskAssetIfNecessary(FestraCompiler compiler)
        {
            if (compiler.generatedAvatarMask != null) return;

            var folderToCreateAssetIn = ResolveFolderToCreateNeutralizedAssetsIn(compiler.folderToGenerateNeutralizedAssetsIn, compiler.animatorController);
            var mask = new AvatarMask();
            AssetDatabase.CreateAsset(mask, folderToCreateAssetIn + "/GeneratedFESTRAMask__" + DateTime.Now.ToString("yyyy'-'MM'-'dd'_'HHmmss") + ".asset");
            compiler.generatedAvatarMask = mask;
        }

        private static void WriteDefaultsSection(SerializedProperty recommendationMode)
        {
            if (recommendationMode.intValue == (int) WriteDefaultsRecommendationMode.UseUnsupportedWriteDefaultsOn)
            {
                EditorGUILayout.HelpBox(FestraLocale.FESTRAC_WarnWriteDefaultsChosenOff, MessageType.Warning);
            }
        }

        private void GenBlinkingWarning(bool advancedFoldoutIsOpen)
        {
            if (doNotGenerateBlinkingOverrideLayer.boolValue)
            {
                EditorGUILayout.HelpBox(@"Blinking Override layer should usually be generated as it depends on all the activities of the compiler.

This is not a normal usage of FestraExpressions, and should not be used except in special cases." + (!advancedFoldoutIsOpen ? "\n\n(Advanced settings)" : ""), MessageType.Error);
            }
        }

        private void DoGenerateFaceTrackingLayers()
        {
            var compiler = AsCompiler();
            
            var folderToCreateAssetIn = ResolveFolderToCreateNeutralizedAssetsIn(compiler.folderToGenerateNeutralizedAssetsIn, compiler.animatorController);
            var actualContainer = CreateContainerIfNotExists(compiler, folderToCreateAssetIn, compiler.avatarDescriptor);
            if (actualContainer != null && compiler.assetContainer == null && !compiler.generateNewContainerEveryTime)
            {
                compiler.assetContainer = actualContainer.ExposeContainerAsset();
            }
            
            new FestraGeneratorInternal(compiler.faceTracking, (AnimatorController)compiler.animatorController, actualContainer).DoOverwriteFaceTrackingLayer();
        }

        private static FestraAssetContainer CreateContainerIfNotExists(FestraCompiler compiler, string folderToCreateAssetIn, VRCAvatarDescriptor avatarDescriptor)
        {
            return compiler.assetContainer == null ? FestraAssetContainer.CreateNew(folderToCreateAssetIn, avatarDescriptor) : FestraAssetContainer.FromExisting(compiler.assetContainer, avatarDescriptor);
        }

        private static string ResolveFolderToCreateNeutralizedAssetsIn(RuntimeAnimatorController preferredChoice, RuntimeAnimatorController defaultChoice)
        {
            var reference = preferredChoice == null ? defaultChoice : preferredChoice;

            var originalAssetPath = AssetDatabase.GetAssetPath(reference);
            var folder = originalAssetPath.Replace(Path.GetFileName(originalAssetPath), "");
            return folder;
        }

        private FestraCompiler AsCompiler()
        {
            return (FestraCompiler) target;
        }
    }
}
