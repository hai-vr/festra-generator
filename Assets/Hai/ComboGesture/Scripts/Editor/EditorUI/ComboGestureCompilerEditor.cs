using System;
using System.IO;
using System.Linq;
using Hai.ComboGesture.Scripts.Components;
using Hai.ComboGesture.Scripts.Editor.Internal;
using UnityEditor;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using AnimatorController = UnityEditor.Animations.AnimatorController;

namespace Hai.ComboGesture.Scripts.Editor.EditorUI
{
    [CustomEditor(typeof(ComboGestureCompiler))]
    public class ComboGestureCompilerEditor : UnityEditor.Editor
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
            animatorController = serializedObject.FindProperty(nameof(ComboGestureCompiler.animatorController));
            useGesturePlayableLayer = serializedObject.FindProperty(nameof(ComboGestureCompiler.useGesturePlayableLayer));
            gesturePlayableLayerController = serializedObject.FindProperty(nameof(ComboGestureCompiler.gesturePlayableLayerController));
            customEmptyClip = serializedObject.FindProperty(nameof(ComboGestureCompiler.customEmptyClip));
            analogBlinkingUpperThreshold = serializedObject.FindProperty(nameof(ComboGestureCompiler.analogBlinkingUpperThreshold));

            doNotGenerateBlinkingOverrideLayer = serializedObject.FindProperty(nameof(ComboGestureCompiler.doNotGenerateBlinkingOverrideLayer));

            writeDefaultsRecommendationMode = serializedObject.FindProperty(nameof(ComboGestureCompiler.writeDefaultsRecommendationMode));
            writeDefaultsRecommendationModeGesture = serializedObject.FindProperty(nameof(ComboGestureCompiler.writeDefaultsRecommendationModeGesture));
            generatedAvatarMask = serializedObject.FindProperty(nameof(ComboGestureCompiler.generatedAvatarMask));

            avatarDescriptor = serializedObject.FindProperty(nameof(ComboGestureCompiler.avatarDescriptor));

            assetContainer = serializedObject.FindProperty(nameof(ComboGestureCompiler.assetContainer));
            generateNewContainerEveryTime = serializedObject.FindProperty(nameof(ComboGestureCompiler.generateNewContainerEveryTime));

            doNotForceBlinkBlendshapes = serializedObject.FindProperty(nameof(ComboGestureCompiler.doNotForceBlinkBlendshapes));
            mmdCompatibilityToggleParameter = serializedObject.FindProperty(nameof(ComboGestureCompiler.mmdCompatibilityToggleParameter));
            faceTracking = serializedObject.FindProperty(nameof(ComboGestureCompiler.faceTracking));

            // reference: https://blog.terresquall.com/2020/03/creating-reorderable-lists-in-the-unity-inspector/

            editorAdvancedFoldout = serializedObject.FindProperty(nameof(ComboGestureCompiler.editorAdvancedFoldout));
        }

        public override void OnInspectorGUI()
        {
            var compiler = AsCompiler();
            serializedObject.Update();
            var italic = new GUIStyle(GUI.skin.label) {fontStyle = FontStyle.Italic};

            if (GUILayout.Button("Switch language (English / 日本語)"))
            {
                CgeLocalization.CycleLocale();
            }

            if (CgeLocalization.IsEnglishLocaleActive())
            {
                EditorGUILayout.LabelField("");
            }
            else
            {
                EditorGUILayout.LabelField("一部の翻訳は正確ではありません。cge.jp.jsonを編集することができます。");
            }

            if (GUILayout.Button(new GUIContent(CgeLocale.CGEC_Documentation_and_tutorials)))
            {
                Application.OpenURL(CgeLocale.DocumentationUrl());
            }

            EditorGUILayout.LabelField(CgeLocale.CGEC_Mood_sets, EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(avatarDescriptor, new GUIContent(CgeLocale.CGEC_Avatar_descriptor));

            EditorGUILayout.Separator();

            EditorGUILayout.LabelField(CgeLocale.CGEC_FX_Playable_Layer, EditorStyles.boldLabel);
            EditorGUILayout.LabelField(CgeLocale.CGEC_BackupFX, italic);
            EditorGUILayout.PropertyField(animatorController, new GUIContent(CgeLocale.CGEC_FX_Animator_Controller));
            EditorGUILayout.PropertyField(writeDefaultsRecommendationMode, new GUIContent(CgeLocale.CGEC_FX_Playable_Mode));
            WriteDefaultsSection(writeDefaultsRecommendationMode);

            EditorGUILayout.PropertyField(doNotForceBlinkBlendshapes, new GUIContent(CgeLocale.CGEC_DoNotForceBlinkBlendshapes));

            EditorGUILayout.Separator();

            EditorGUILayout.LabelField(CgeLocale.CGEC_Gesture_Playable_Layer, EditorStyles.boldLabel);
            EditorGUILayout.LabelField(CgeLocale.CGEC_Support_for_other_transforms, italic);
            EditorGUILayout.LabelField(CgeLocale.CGEC_MusclesUnsupported, italic);
            EditorGUILayout.PropertyField(useGesturePlayableLayer, new GUIContent(CgeLocale.CGEC_Gesture_playable_layer_support));
            if (useGesturePlayableLayer.boolValue)
            {
                EditorGUILayout.LabelField(CgeLocale.CGEC_BackupGesture, italic);
                EditorGUILayout.PropertyField(gesturePlayableLayerController, new GUIContent(CgeLocale.CGEC_Gesture_Animator_Controller));

                EditorGUILayout.PropertyField(writeDefaultsRecommendationModeGesture, new GUIContent(CgeLocale.CGEC_Gesture_Playable_Mode));
                WriteDefaultsSection(writeDefaultsRecommendationModeGesture);

                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.PropertyField(generatedAvatarMask, new GUIContent(CgeLocale.CGEC_Asset_container));
                EditorGUI.EndDisabledGroup();

                var missingMaskCount = CgeMaskApplicator.FindAllLayersMissingAMask(compiler.animatorController).Count();
                if (missingMaskCount > 0)
                {
                    EditorGUILayout.HelpBox(string.Format(CgeLocale.CGEC_MissingFxMask, missingMaskCount), MessageType.Error);
                }

                EditorGUI.BeginDisabledGroup(compiler.avatarDescriptor == null || compiler.animatorController == null || missingMaskCount == 0);
                if (GUILayout.Button(CgeLocale.CGEC_Add_missing_masks))
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

            EditorGUILayout.LabelField(CgeLocale.CGEC_Synchronization, EditorStyles.boldLabel);

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
                EditorGUILayout.HelpBox(CgeLocale.CGEC_Slowness_warning, MessageType.Warning);
            }
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.PropertyField(faceTracking, new GUIContent(CgeLocale.CGEC_FaceTracking));
            EditorGUI.BeginDisabledGroup(canSync);
            if (compiler.faceTracking != null && GUILayout.Button(CgeLocale.CGEC_Synchronize_Face_Tracking_Layers))
            {
                DoGenerateFaceTrackingLayers();
            }
            
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.HelpBox(
                CgeLocale.CGEC_SynchronizationConditionsV2, MessageType.Info);

            if (compiler.assetContainer != null) {
                EditorGUILayout.LabelField(CgeLocale.CGEC_Asset_generation, EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(assetContainer, new GUIContent(CgeLocale.CGEC_Asset_container));
            }

            EditorGUILayout.Separator();

            EditorGUILayout.LabelField(CgeLocale.CGEC_Other_tweaks, EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(analogBlinkingUpperThreshold, new GUIContent(CgeLocale.CGEC_Analog_fist_blinking_threshold, CgeLocale.CGEC_AnalogFist_Popup));
            EditorGUILayout.PropertyField(mmdCompatibilityToggleParameter, new GUIContent(CgeLocale.CGEC_MMD_compatibility_toggle_parameter));

            editorAdvancedFoldout.boolValue = EditorGUILayout.Foldout(editorAdvancedFoldout.boolValue, CgeLocale.CGEC_Advanced);
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
                EditorGUILayout.PropertyField(assetContainer, new GUIContent(CgeLocale.CGEC_Asset_container));

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
                    Debug.Log(CgeLocale.CompileDefaultLocaleJson());
                }
                if (GUILayout.Button("(Debug) Reload localization files"))
                {
                    CgeLocalization.ReloadLocalizations();
                }
            }
            else
            {
                GenBlinkingWarning(false);
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void MaskRemovalUi(ComboGestureCompiler compiler)
        {
            var isMaskUsed = compiler.animatorController != null && ((AnimatorController) compiler.animatorController).layers.Any(layer => layer.avatarMask == compiler.generatedAvatarMask);
            EditorGUI.BeginDisabledGroup(!isMaskUsed);
            if (GUILayout.Button(CgeLocale.CGEC_Remove_applied_masks))
            {
                DoRemoveAppliedMasks(compiler);
            }

            EditorGUI.EndDisabledGroup();

            EditorGUI.BeginDisabledGroup(isMaskUsed);
            if (GUILayout.Button(CgeLocale.CGEC_Unbind_Asset_container))
            {
                DoRemoveAppliedMasksAndAssetContainer(compiler);
            }

            EditorGUI.EndDisabledGroup();
        }

        private void AddMissingMasks(ComboGestureCompiler compiler)
        {
            CreateAvatarMaskAssetIfNecessary(compiler);
            new CgeMaskApplicator(compiler.animatorController, compiler.generatedAvatarMask).AddMissingMasks();
            new CgeMaskApplicator(compiler.animatorController, compiler.generatedAvatarMask).UpdateMask();
        }

        private void DoRemoveAppliedMasks(ComboGestureCompiler compiler)
        {
            new CgeMaskApplicator(AsCompiler().animatorController, compiler.generatedAvatarMask).RemoveAppliedMask();
        }

        private void DoRemoveAppliedMasksAndAssetContainer(ComboGestureCompiler compiler)
        {
            new CgeMaskApplicator(AsCompiler().animatorController, compiler.generatedAvatarMask).RemoveAppliedMask();
            generatedAvatarMask.objectReferenceValue = null;
        }

        private void CreateAvatarMaskAssetIfNecessary(ComboGestureCompiler compiler)
        {
            if (compiler.generatedAvatarMask != null) return;

            var folderToCreateAssetIn = ResolveFolderToCreateNeutralizedAssetsIn(compiler.folderToGenerateNeutralizedAssetsIn, compiler.animatorController);
            var mask = new AvatarMask();
            AssetDatabase.CreateAsset(mask, folderToCreateAssetIn + "/GeneratedCGEMask__" + DateTime.Now.ToString("yyyy'-'MM'-'dd'_'HHmmss") + ".asset");
            compiler.generatedAvatarMask = mask;
        }

        private static void WriteDefaultsSection(SerializedProperty recommendationMode)
        {
            if (recommendationMode.intValue == (int) WriteDefaultsRecommendationMode.UseUnsupportedWriteDefaultsOn)
            {
                EditorGUILayout.HelpBox(CgeLocale.CGEC_WarnWriteDefaultsChosenOff, MessageType.Warning);
            }
        }

        private void GenBlinkingWarning(bool advancedFoldoutIsOpen)
        {
            if (doNotGenerateBlinkingOverrideLayer.boolValue)
            {
                EditorGUILayout.HelpBox(@"Blinking Override layer should usually be generated as it depends on all the activities of the compiler.

This is not a normal usage of ComboGestureExpressions, and should not be used except in special cases." + (!advancedFoldoutIsOpen ? "\n\n(Advanced settings)" : ""), MessageType.Error);
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
            
            new CgeFaceTracking(compiler.faceTracking, (AnimatorController)compiler.animatorController, actualContainer).DoOverwriteFaceTrackingLayer();
        }

        private static CgeAssetContainer CreateContainerIfNotExists(ComboGestureCompiler compiler, string folderToCreateAssetIn, VRCAvatarDescriptor avatarDescriptor)
        {
            return compiler.assetContainer == null ? CgeAssetContainer.CreateNew(folderToCreateAssetIn, avatarDescriptor) : CgeAssetContainer.FromExisting(compiler.assetContainer, avatarDescriptor);
        }

        private static string ResolveFolderToCreateNeutralizedAssetsIn(RuntimeAnimatorController preferredChoice, RuntimeAnimatorController defaultChoice)
        {
            var reference = preferredChoice == null ? defaultChoice : preferredChoice;

            var originalAssetPath = AssetDatabase.GetAssetPath(reference);
            var folder = originalAssetPath.Replace(Path.GetFileName(originalAssetPath), "");
            return folder;
        }

        private ComboGestureCompiler AsCompiler()
        {
            return (ComboGestureCompiler) target;
        }
    }
}
