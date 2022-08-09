
// ReSharper disable InconsistentNaming

using System.Linq;
using System.Reflection;
using System.Text;

namespace Hai.FestraGenerator.Scripts.Editor.EditorUI
{
    public class FestraLocale
    {
        private static string FESTRA_Documentation_URL => LocalizeOrElse("FESTRA_Documentation_URL", FestraLocaleDefaults.FESTRA_Documentation_URL);
        private static string FESTRA_PermutationsDocumentation_URL => LocalizeOrElse("FESTRA_PermutationsDocumentation_URL", FestraLocaleDefaults.FESTRA_PermutationsDocumentation_URL);
        private static string FESTRA_IntegratorDocumentation_URL => LocalizeOrElse("FESTRA_IntegratorDocumentation_URL", FestraLocaleDefaults.FESTRA_IntegratorDocumentation_URL);
        // 1.4
        internal static string FESTRAE_Open_editor => LocalizeOrElse("FESTRAE_Open_editor", FestraLocaleDefaults.FESTRAE_Open_editor);
        internal static string FESTRAE_Additional_editors => LocalizeOrElse("FESTRAE_Additional_editors", FestraLocaleDefaults.FESTRAE_Additional_editors);
        internal static string FESTRAE_All_combos => LocalizeOrElse("FESTRAE_All_combos", FestraLocaleDefaults.FESTRAE_All_combos);
        internal static string FESTRAE_Analog_Fist => LocalizeOrElse("FESTRAE_Analog_Fist", FestraLocaleDefaults.FESTRAE_Analog_Fist);
        internal static string FESTRAE_Combine_expressions => LocalizeOrElse("FESTRAE_Combine_expressions", FestraLocaleDefaults.FESTRAE_Combine_expressions);
        internal static string FESTRAE_Combos => LocalizeOrElse("FESTRAE_Combos", FestraLocaleDefaults.FESTRAE_Combos);
        internal static string FESTRAE_Complete_view => LocalizeOrElse("FESTRAE_Complete_view", FestraLocaleDefaults.FESTRAE_Complete_view);
        internal static string FESTRAE_Create_blend_trees => LocalizeOrElse("FESTRAE_Create_blend_trees", FestraLocaleDefaults.FESTRAE_Create_blend_trees);
        internal static string FESTRAE_Manipulate_trees => LocalizeOrElse("FESTRAE_Manipulate_trees", FestraLocaleDefaults.FESTRAE_Manipulate_trees);
        internal static string FESTRAE_Other_options => LocalizeOrElse("FESTRAE_Other_options", FestraLocaleDefaults.FESTRAE_Other_options);
        internal static string FESTRAE_Permutations => LocalizeOrElse("FESTRAE_Permutations", FestraLocaleDefaults.FESTRAE_Permutations);
        internal static string FESTRAE_Prevent_eyes_blinking => LocalizeOrElse("FESTRAE_Prevent_eyes_blinking", FestraLocaleDefaults.FESTRAE_Prevent_eyes_blinking);
        internal static string FESTRAE_Set_face_expressions => LocalizeOrElse("FESTRAE_Set_face_expressions", FestraLocaleDefaults.FESTRAE_Set_face_expressions);
        internal static string FESTRAE_Simplified_view => LocalizeOrElse("FESTRAE_Simplified_view", FestraLocaleDefaults.FESTRAE_Simplified_view);
        internal static string FESTRAE_Singles => LocalizeOrElse("FESTRAE_Singles", FestraLocaleDefaults.FESTRAE_Singles);
        internal static string FESTRAE_Tutorials => LocalizeOrElse("FESTRAE_Tutorials", FestraLocaleDefaults.FESTRAE_Tutorials);
        internal static string FESTRAE_View_blend_trees => LocalizeOrElse("FESTRAE_View_blend_trees", FestraLocaleDefaults.FESTRAE_View_blend_trees);
        internal static string FESTRAE_Open_Documentation_and_tutorials => LocalizeOrElse("FESTRAE_Open_Documentation_and_tutorials", FestraLocaleDefaults.FESTRAE_Open_Documentation_and_tutorials);
        internal static string FESTRAE_PermutationsIntro => LocalizeOrElse("FESTRAE_PermutationsIntro", FestraLocaleDefaults.FESTRAE_PermutationsIntro);
        internal static string FESTRAE_ConfirmUsePermutations => LocalizeOrElse("FESTRAE_ConfirmUsePermutations", FestraLocaleDefaults.FESTRAE_ConfirmUsePermutations);
        internal static string FESTRAE_Enable_permutations_for_this_Activity => LocalizeOrElse("FESTRAE_Enable_permutations_for_this_Activity", FestraLocaleDefaults.FESTRAE_Enable_permutations_for_this_Activity);
        internal static string FESTRAE_PermutationsFootnote => LocalizeOrElse("FESTRAE_PermutationsFootnote", FestraLocaleDefaults.FESTRAE_PermutationsFootnote);
        internal static string FESTRAE_GeneratePreview => LocalizeOrElse("FESTRAE_GeneratePreview", FestraLocaleDefaults.FESTRAE_GeneratePreview);
        internal static string FESTRAE_SetupPreview => LocalizeOrElse("FESTRAE_SetupPreview", FestraLocaleDefaults.FESTRAE_SetupPreview);
        internal static string FESTRAE_SelectFaceExpressionsWithBothEyesClosed => LocalizeOrElse("FESTRAE_SelectFaceExpressionsWithBothEyesClosed", FestraLocaleDefaults.FESTRAE_SelectFaceExpressionsWithBothEyesClosed);
        internal static string FESTRAE_Blinking => LocalizeOrElse("FESTRAE_Blinking", FestraLocaleDefaults.FESTRAE_Blinking);
        internal static string FESTRAE_Transition_duration_in_seconds => LocalizeOrElse("FESTRAE_Transition_duration_in_seconds", FestraLocaleDefaults.FESTRAE_Transition_duration_in_seconds);
        internal static string FESTRAE_IncompletePreviewSetup => LocalizeOrElse("FESTRAE_IncompletePreviewSetup", FestraLocaleDefaults.FESTRAE_IncompletePreviewSetup);
        internal static string FESTRAE_Automatically_setup_preview => LocalizeOrElse("FESTRAE_Automatically_setup_preview", FestraLocaleDefaults.FESTRAE_Automatically_setup_preview);
        internal static string FESTRAE_AutoSetupReused => LocalizeOrElse("FESTRAE_AutoSetupReused", FestraLocaleDefaults.FESTRAE_AutoSetupReused);
        internal static string FESTRAE_AutoSetupNoActiveAvatarDescriptor => LocalizeOrElse("FESTRAE_AutoSetupNoActiveAvatarDescriptor", FestraLocaleDefaults.FESTRAE_AutoSetupNoActiveAvatarDescriptor);
        internal static string FESTRAE_AutoSetupCreated => LocalizeOrElse("FESTRAE_AutoSetupCreated", FestraLocaleDefaults.FESTRAE_AutoSetupCreated);
        internal static string FESTRAE_Transition_duration => LocalizeOrElse("FESTRAE_Transition_duration", FestraLocaleDefaults.FESTRAE_Transition_duration);
        internal static string FESTRAE_Preview_setup => LocalizeOrElse("FESTRAE_Preview_setup", FestraLocaleDefaults.FESTRAE_Preview_setup);
        internal static string FESTRAE_Generate_missing_previews => LocalizeOrElse("FESTRAE_Generate_missing_previews", FestraLocaleDefaults.FESTRAE_Generate_missing_previews);
        internal static string FESTRAE_Regenerate_all_previews => LocalizeOrElse("FESTRAE_Regenerate_all_previews", FestraLocaleDefaults.FESTRAE_Regenerate_all_previews);
        internal static string FESTRAE_Stop_generating_previews => LocalizeOrElse("FESTRAE_Stop_generating_previews", FestraLocaleDefaults.FESTRAE_Stop_generating_previews);
        internal static string FESTRAE_ExplainFourDirections => LocalizeOrElse("FESTRAE_ExplainFourDirections", FestraLocaleDefaults.FESTRAE_ExplainFourDirections);
        internal static string FESTRAE_ExplainEightDirections => LocalizeOrElse("FESTRAE_ExplainEightDirections", FestraLocaleDefaults.FESTRAE_ExplainEightDirections);
        internal static string FESTRAE_ExplainSixDirectionsPointingForward => LocalizeOrElse("FESTRAE_ExplainSixDirectionsPointingForward", FestraLocaleDefaults.FESTRAE_ExplainSixDirectionsPointingForward);
        internal static string FESTRAE_ExplainSixDirectionsPointingSideways => LocalizeOrElse("FESTRAE_ExplainSixDirectionsPointingSideways", FestraLocaleDefaults.FESTRAE_ExplainSixDirectionsPointingSideways);
        internal static string FESTRAE_ExplainSingleAnalogFistWithHairTrigger => LocalizeOrElse("FESTRAE_ExplainSingleAnalogFistWithHairTrigger", FestraLocaleDefaults.FESTRAE_ExplainSingleAnalogFistWithHairTrigger);
        internal static string FESTRAE_ExplainSingleAnalogFistAndTwoDirections => LocalizeOrElse("FESTRAE_ExplainSingleAnalogFistAndTwoDirections", FestraLocaleDefaults.FESTRAE_ExplainSingleAnalogFistAndTwoDirections);
        internal static string FESTRAE_ExplainDualAnalogFist => LocalizeOrElse("FESTRAE_ExplainDualAnalogFist", FestraLocaleDefaults.FESTRAE_ExplainDualAnalogFist);
        internal static string FESTRAE_Create_a_new_blend_tree => LocalizeOrElse("FESTRAE_Create_a_new_blend_tree", FestraLocaleDefaults.FESTRAE_Create_a_new_blend_tree);
        internal static string FESTRAE_Blend_tree_asset => LocalizeOrElse("FESTRAE_Blend_tree_asset", FestraLocaleDefaults.FESTRAE_Blend_tree_asset);
        //
        internal static string FESTRAC_Documentation_and_tutorials => LocalizeOrElse("FESTRAC_Documentation_and_tutorials", FestraLocaleDefaults.FESTRAC_Documentation_and_tutorials);
        internal static string FESTRAC_BackupFX => LocalizeOrElse("FESTRAC_BackupFX", FestraLocaleDefaults.FESTRAC_BackupFX);
        internal static string FESTRAC_FX_Animator_Controller => LocalizeOrElse("FESTRAC_FX_Animator_Controller", FestraLocaleDefaults.FESTRAC_FX_Animator_Controller);
        internal static string FESTRAC_FX_Playable_Layer => LocalizeOrElse("FESTRAC_FX_Playable_Layer", FestraLocaleDefaults.FESTRAC_FX_Playable_Layer);
        internal static string FESTRAC_Gesture_Playable_Layer => LocalizeOrElse("FESTRAC_Gesture_Playable_Layer", FestraLocaleDefaults.FESTRAC_Gesture_Playable_Layer);
        internal static string FESTRAC_Parameter_Mode => LocalizeOrElse("FESTRAC_Parameter_Mode", FestraLocaleDefaults.FESTRAC_Parameter_Mode);
        internal static string FESTRAC_Parameter_Name => LocalizeOrElse("FESTRAC_Parameter_Name", FestraLocaleDefaults.FESTRAC_Parameter_Name);
        internal static string FESTRAC_Parameter_Value => LocalizeOrElse("FESTRAC_Parameter_Value", FestraLocaleDefaults.FESTRAC_Parameter_Value);
        internal static string FESTRAC_Mood_sets => LocalizeOrElse("FESTRAC_Mood_sets", FestraLocaleDefaults.FESTRAC_Mood_sets);
        internal static string FESTRAC_HelpExpressionParameterOptimize => LocalizeOrElse("FESTRAC_HelpExpressionParameterOptimize", FestraLocaleDefaults.FESTRAC_HelpExpressionParameterOptimize);
        internal static string FESTRAC_WarnValuesOverlap => LocalizeOrElse("FESTRAC_WarnValuesOverlap", FestraLocaleDefaults.FESTRAC_WarnValuesOverlap);
        internal static string FESTRAC_WarnNamesOverlap => LocalizeOrElse("FESTRAC_WarnNamesOverlap", FestraLocaleDefaults.FESTRAC_WarnNamesOverlap);
        internal static string FESTRAC_WarnNoBlendTree => LocalizeOrElse("FESTRAC_WarnNoBlendTree", FestraLocaleDefaults.FESTRAC_WarnNoBlendTree);
        internal static string FESTRAC_WarnNoActivity => LocalizeOrElse("FESTRAC_WarnNoActivity", FestraLocaleDefaults.FESTRAC_WarnNoActivity);
        internal static string FESTRAC_HelpWhenAllParameterNamesDefined => LocalizeOrElse("FESTRAC_HelpWhenAllParameterNamesDefined", FestraLocaleDefaults.FESTRAC_HelpWhenAllParameterNamesDefined);
        internal static string FESTRAC_HintDefaultMood => LocalizeOrElse("FESTRAC_HintDefaultMood", FestraLocaleDefaults.FESTRAC_HintDefaultMood);
        internal static string FESTRAC_GestureWeight_correction => LocalizeOrElse("FESTRAC_GestureWeight_correction", FestraLocaleDefaults.FESTRAC_GestureWeight_correction);
        internal static string FESTRAC_Avatar_descriptor => LocalizeOrElse("FESTRAC_Avatar_descriptor", FestraLocaleDefaults.FESTRAC_Avatar_descriptor);
        internal static string FESTRAC_Support_for_other_transforms => LocalizeOrElse("FESTRAC_Support_for_other_transforms", FestraLocaleDefaults.FESTRAC_Support_for_other_transforms);
        internal static string FESTRAC_Gesture_playable_layer_support => LocalizeOrElse("FESTRAC_Gesture_playable_layer_support", FestraLocaleDefaults.FESTRAC_Gesture_playable_layer_support);
        internal static string FESTRAC_BackupGesture => LocalizeOrElse("FESTRAC_BackupGesture", FestraLocaleDefaults.FESTRAC_BackupGesture);
        internal static string FESTRAC_Gesture_Animator_Controller => LocalizeOrElse("FESTRAC_Gesture_Animator_Controller", FestraLocaleDefaults.FESTRAC_Gesture_Animator_Controller);
        internal static string FESTRAC_MusclesUnsupported => LocalizeOrElse("FESTRAC_MusclesUnsupported", FestraLocaleDefaults.FESTRAC_MusclesUnsupported);
        internal static string FESTRAC_Synchronization => LocalizeOrElse("FESTRAC_Synchronization", FestraLocaleDefaults.FESTRAC_Synchronization);
        internal static string FESTRAC_Synchronize_Animator_FX_and_Gesture_layers => LocalizeOrElse("FESTRAC_Synchronize_Animator_FX_and_Gesture_layers", FestraLocaleDefaults.FESTRAC_Synchronize_Animator_FX_and_Gesture_layers);
        internal static string FESTRAC_Synchronize_Animator_FX_layers => LocalizeOrElse("FESTRAC_Synchronize_Animator_FX_layers", FestraLocaleDefaults.FESTRAC_Synchronize_Animator_FX_layers);
        internal static string FESTRAC_SynchronizationConditionsV2 => LocalizeOrElse("FESTRAC_SynchronizationConditionsV2", FestraLocaleDefaults.FESTRAC_SynchronizationConditionsV2);
        internal static string FESTRAC_Asset_generation => LocalizeOrElse("FESTRAC_Asset_generation", FestraLocaleDefaults.FESTRAC_Asset_generation);
        internal static string FESTRAC_Asset_container => LocalizeOrElse("FESTRAC_Asset_container", FestraLocaleDefaults.FESTRAC_Asset_container);
        internal static string FESTRAC_FX_Playable_Mode => LocalizeOrElse("FESTRAC_FX_Playable_Mode", FestraLocaleDefaults.FESTRAC_FX_Playable_Mode);
        internal static string FESTRAC_WarnCautiousWriteDefaultsChosenOff => LocalizeOrElse("FESTRAC_WarnCautiousWriteDefaultsChosenOff", FestraLocaleDefaults.FESTRAC_WarnCautiousWriteDefaultsChosenOff);
        internal static string FESTRAC_WarnWriteDefaultsChosenOff => LocalizeOrElse("FESTRAC_WarnWriteDefaultsChosenOff", FestraLocaleDefaults.FESTRAC_WarnWriteDefaultsChosenOff);
        internal static string FESTRAC_AndMoreOnly15FirstResults => LocalizeOrElse("FESTRAC_AndMoreOnly15FirstResults", FestraLocaleDefaults.FESTRAC_AndMoreOnly15FirstResults);
        internal static string FESTRAC_WarnWriteDefaultsOnStatesFound => LocalizeOrElse("FESTRAC_WarnWriteDefaultsOnStatesFound", FestraLocaleDefaults.FESTRAC_WarnWriteDefaultsOnStatesFound);
        internal static string FESTRAC_Gesture_Playable_Mode => LocalizeOrElse("FESTRAC_Gesture_Playable_Mode", FestraLocaleDefaults.FESTRAC_Gesture_Playable_Mode);
        internal static string FESTRAC_Other_tweaks => LocalizeOrElse("FESTRAC_Other_tweaks", FestraLocaleDefaults.FESTRAC_Other_tweaks);
        internal static string FESTRAC_Analog_fist_blinking_threshold => LocalizeOrElse("FESTRAC_Analog_fist_blinking_threshold", FestraLocaleDefaults.FESTRAC_Analog_fist_blinking_threshold);
        internal static string FESTRAC_AnalogFist_Popup => LocalizeOrElse("FESTRAC_AnalogFist_Popup", FestraLocaleDefaults.FESTRAC_AnalogFist_Popup);
        internal static string FESTRAC_Advanced => LocalizeOrElse("FESTRAC_Advanced", FestraLocaleDefaults.FESTRAC_Advanced);
        internal static string FESTRAC_WarnNoActivityName => LocalizeOrElse("FESTRAC_WarnNoActivityName", FestraLocaleDefaults.FESTRAC_WarnNoActivityName);
        internal static string FESTRAC_Capture_Transforms_Mode => LocalizeOrElse("FESTRAC_Capture_Transforms_Mode", FestraLocaleDefaults.FESTRAC_Capture_Transforms_Mode);
        internal static string FESTRAC_MissingFxMask => LocalizeOrElse("FESTRAC_MissingFxMask", FestraLocaleDefaults.FESTRAC_MissingFxMask);
        internal static string FESTRAC_Add_missing_masks => LocalizeOrElse("FESTRAC_Add_missing_masks", FestraLocaleDefaults.FESTRAC_Add_missing_masks);
        internal static string FESTRAC_Remove_applied_masks => LocalizeOrElse("FESTRAC_Remove_applied_masks", FestraLocaleDefaults.FESTRAC_Remove_applied_masks);
        internal static string FESTRAC_Unbind_Asset_container => LocalizeOrElse("FESTRAC_Unbind_Asset_container", FestraLocaleDefaults.FESTRAC_Unbind_Asset_container);
        //
        internal static string FESTRAI_Documentation => LocalizeOrElse("FESTRAI_Documentation", FestraLocaleDefaults.FESTRAI_Documentation);
        internal static string FESTRAI_BackupAnimator => LocalizeOrElse("FESTRAI_BackupAnimator", FestraLocaleDefaults.FESTRAI_BackupAnimator);
        internal static string FESTRAI_Animator_Controller => LocalizeOrElse("FESTRAI_Animator_Controller", FestraLocaleDefaults.FESTRAI_Animator_Controller);
        internal static string FESTRAI_Info => LocalizeOrElse("FESTRAI_Info", FestraLocaleDefaults.FESTRAI_Info);
        internal static string FESTRAI_Synchronize_Animator_layers => LocalizeOrElse("FESTRAI_Synchronize_Animator_layers", FestraLocaleDefaults.FESTRAI_Synchronize_Animator_layers);
        // 1.5
        internal static string FESTRAE_EyesAreClosed => LocalizeOrElse("FESTRAE_EyesAreClosed", FestraLocaleDefaults.FESTRAE_EyesAreClosed);
        // 1.6.0
        internal static string FESTRAC_WarnNoMassiveBlend => LocalizeOrElse("FESTRAC_WarnNoMassiveBlend", FestraLocaleDefaults.FESTRAC_WarnNoMassiveBlend);
        //
        internal static string FESTRAE_OneHandMode => LocalizeOrElse("FESTRAE_OneHandMode", FestraLocaleDefaults.FESTRAE_OneHandMode);
        internal static string FESTRAE_OneHandModeIntro => LocalizeOrElse("FESTRAE_OneHandModeIntro", FestraLocaleDefaults.FESTRAE_OneHandModeIntro);
        internal static string FESTRAE_Combine => LocalizeOrElse("FESTRAE_Combine", FestraLocaleDefaults.FESTRAE_Combine);
        internal static string FESTRAE_CombineAcross => LocalizeOrElse("FESTRAE_CombineAcross", FestraLocaleDefaults.FESTRAE_CombineAcross);
        internal static string FESTRAE_Create => LocalizeOrElse("FESTRAE_Create", FestraLocaleDefaults.FESTRAE_Create);
        internal static string FESTRAE_Simplify => LocalizeOrElse("FESTRAE_Simplify", FestraLocaleDefaults.FESTRAE_Simplify);
        internal static string FESTRAE_SwapToFix => LocalizeOrElse("FESTRAE_SwapToFix", FestraLocaleDefaults.FESTRAE_SwapToFix);
        internal static string FESTRAE_AutoSet => LocalizeOrElse("FESTRAE_AutoSet", FestraLocaleDefaults.FESTRAE_AutoSet);
        internal static string FESTRAE_EnablePermutations => LocalizeOrElse("FESTRAE_EnablePermutations", FestraLocaleDefaults.FESTRAE_EnablePermutations);
        internal static string FESTRAE_CombinerShowHidden => LocalizeOrElse("FESTRAE_CombinerShowHidden", FestraLocaleDefaults.FESTRAE_CombinerShowHidden);
        internal static string FESTRAE_CombinerShowFullPaths => LocalizeOrElse("FESTRAE_CombinerShowFullPaths", FestraLocaleDefaults.FESTRAE_CombinerShowFullPaths);
        internal static string FESTRAE_TreeAnimationAtRest => LocalizeOrElse("FESTRAE_TreeAnimationAtRest", FestraLocaleDefaults.FESTRAE_TreeAnimationAtRest);
        internal static string FESTRAE_TreeJoystickCenterAnimation => LocalizeOrElse("FESTRAE_TreeJoystickCenterAnimation", FestraLocaleDefaults.FESTRAE_TreeJoystickCenterAnimation);
        internal static string FESTRAE_TreeFixJoystickSnapping => LocalizeOrElse("FESTRAE_TreeFixJoystickSnapping", FestraLocaleDefaults.FESTRAE_TreeFixJoystickSnapping);
        internal static string FESTRAE_TreeJoystickMaximumTilt => LocalizeOrElse("FESTRAE_TreeJoystickMaximumTilt", FestraLocaleDefaults.FESTRAE_TreeJoystickMaximumTilt);
        internal static string FESTRAE_TreeCreateAsset => LocalizeOrElse("FESTRAE_TreeCreateAsset", FestraLocaleDefaults.FESTRAE_TreeCreateAsset);
        internal static string FESTRAE_TreeFileCreate => LocalizeOrElse("FESTRAE_TreeFileCreate", FestraLocaleDefaults.FESTRAE_TreeFileCreate);
        internal static string FESTRAE_TreeFileInvalidSavePath => LocalizeOrElse("FESTRAE_TreeFileInvalidSavePath", FestraLocaleDefaults.FESTRAE_TreeFileInvalidSavePath);
        internal static string FESTRAE_TreeFileInvalidSavePathMessage => LocalizeOrElse("FESTRAE_TreeFileInvalidSavePathMessage", FestraLocaleDefaults.FESTRAE_TreeFileInvalidSavePathMessage);
        // 2.0.0
        internal static string FESTRAC_ViveAdvancedControlsWarning => LocalizeOrElse("FESTRAC_ViveAdvancedControlsWarning", FestraLocaleDefaults.FESTRAC_ViveAdvancedControlsWarning);
        //
        internal static string FESTRAC_Avatar_Dynamics => LocalizeOrElse("FESTRAC_Avatar_Dynamics", FestraLocaleDefaults.FESTRAC_Avatar_Dynamics);
        internal static string FESTRAC_Dynamics => LocalizeOrElse("FESTRAC_Dynamics", FestraLocaleDefaults.FESTRAC_Dynamics);
        internal static string FESTRAC_DoNotForceBlinkBlendshapes => LocalizeOrElse("FESTRAC_DoNotForceBlinkBlendshapes", FestraLocaleDefaults.FESTRAC_DoNotForceBlinkBlendshapes);
        internal static string FESTRAD_DynamicExpression => LocalizeOrElse("FESTRAD_DynamicExpression", FestraLocaleDefaults.FESTRAD_DynamicExpression);
        internal static string FESTRAD_DynamicCondition => LocalizeOrElse("FESTRAD_DynamicCondition", FestraLocaleDefaults.FESTRAD_DynamicCondition);
        internal static string FESTRAD_Clip => LocalizeOrElse("FESTRAD_Clip", FestraLocaleDefaults.FESTRAD_Clip);
        internal static string FESTRAD_Condition => LocalizeOrElse("FESTRAD_Condition", FestraLocaleDefaults.FESTRAD_Condition);
        internal static string FESTRAD_ContactReceiver => LocalizeOrElse("FESTRAD_ContactReceiver", FestraLocaleDefaults.FESTRAD_ContactReceiver);
        internal static string FESTRAD_Effect => LocalizeOrElse("FESTRAD_Effect", FestraLocaleDefaults.FESTRAD_Effect);
        internal static string FESTRAD_IsHardThreshold => LocalizeOrElse("FESTRAD_IsHardThreshold", FestraLocaleDefaults.FESTRAD_IsHardThreshold);
        internal static string FESTRAD_MoodSet => LocalizeOrElse("FESTRAD_MoodSet", FestraLocaleDefaults.FESTRAD_MoodSet);
        internal static string FESTRAD_ParameterName => LocalizeOrElse("FESTRAD_ParameterName", FestraLocaleDefaults.FESTRAD_ParameterName);
        internal static string FESTRAD_ParameterType => LocalizeOrElse("FESTRAD_ParameterType", FestraLocaleDefaults.FESTRAD_ParameterType);
        internal static string FESTRAD_PhysBone => LocalizeOrElse("FESTRAD_PhysBone", FestraLocaleDefaults.FESTRAD_PhysBone);
        internal static string FESTRAD_PhysBoneSource => LocalizeOrElse("FESTRAD_PhysBoneSource", FestraLocaleDefaults.FESTRAD_PhysBoneSource);
        internal static string FESTRAD_Source => LocalizeOrElse("FESTRAD_Source", FestraLocaleDefaults.FESTRAD_Source);
        internal static string FESTRAD_Threshold => LocalizeOrElse("FESTRAD_Threshold", FestraLocaleDefaults.FESTRAD_Threshold);
        internal static string FESTRAD_Higher_priority => LocalizeOrElse("FESTRAD_Higher_priority", FestraLocaleDefaults.FESTRAD_Higher_priority);
        public static string FESTRAC_MMD_compatibility_toggle_parameter => LocalizeOrElse("FESTRAC_MMD_compatibility_toggle_parameter", FestraLocaleDefaults.FESTRAC_MMD_compatibility_toggle_parameter);
        public static string FESTRAE_Mode => LocalizeOrElse("FESTRAE_Mode", FestraLocaleDefaults.FESTRAE_Mode);
        public static string FESTRAC_Slowness_warning => LocalizeOrElse("FESTRAC_Slowness_warning", FestraLocaleDefaults.FESTRAC_Slowness_warning);
        public static string FESTRAD_MissingParameterOnContact => LocalizeOrElse("FESTRAD_MissingParameterOnContact", FestraLocaleDefaults.FESTRAD_MissingParameterOnContact);
        public static string FESTRAD_MissingParameterOnPhysBone => LocalizeOrElse("FESTRAD_MissingParameterOnPhysBone", FestraLocaleDefaults.FESTRAD_MissingParameterOnPhysBone);
        public static string FESTRAC_MainDynamics => LocalizeOrElse("FESTRAC_MainDynamics", FestraLocaleDefaults.FESTRAC_MainDynamics);
        public static string FESTRAD_EnterTransitionDuration => LocalizeOrElse("FESTRAD_EnterTransitionDuration", FestraLocaleDefaults.FESTRAD_EnterTransitionDuration);
        public static string FESTRAD_OnEnterCurve => LocalizeOrElse("FESTRAD_OnEnterCurve", FestraLocaleDefaults.FESTRAD_OnEnterCurve);
        public static string FESTRAD_OnEnterDuration => LocalizeOrElse("FESTRAD_OnEnterDuration", FestraLocaleDefaults.FESTRAD_OnEnterDuration);
        public static string FESTRAD_BehavesLikeOnEnter => LocalizeOrElse("FESTRAD_BehavesLikeOnEnter", FestraLocaleDefaults.FESTRAD_BehavesLikeOnEnter);
        public static string FESTRAD_UpperBound => LocalizeOrElse("FESTRAD_UpperBound", FestraLocaleDefaults.FESTRAD_UpperBound);
        // 2.1.0
        public static string FESTRAC_FaceTracking => LocalizeOrElse("FESTRAC_FaceTracking", FestraLocaleDefaults.FESTRAC_FaceTracking);
        public static string FESTRAC_Synchronize_Face_Tracking_Layers => LocalizeOrElse("FESTRAC_Synchronize_Face_Tracking_Layers", FestraLocaleDefaults.FESTRAC_Synchronize_Face_Tracking_Layers);



        private static string LocalizeOrElse(string key, string defaultCultureLocalization)
        {
            return FestraLocalization.LocalizeOrElse(key, defaultCultureLocalization);
        }

        public static string DocumentationUrl()
        {
            var localizedUrl = FESTRA_Documentation_URL;
            return localizedUrl.StartsWith(FestraLocaleDefaults.OfficialDocumentationUrlAsPrefix) ? localizedUrl : FestraLocaleDefaults.FESTRA_Documentation_URL;
        }

        public static string PermutationsDocumentationUrl()
        {
            var localizedUrl = FESTRA_PermutationsDocumentation_URL;
            return localizedUrl.StartsWith(FestraLocaleDefaults.OfficialDocumentationUrlAsPrefix) ? localizedUrl : FestraLocaleDefaults.FESTRA_PermutationsDocumentation_URL;
        }

        public static string IntegratorDocumentationUrl()
        {
            var localizedUrl = FESTRA_IntegratorDocumentation_URL;
            return localizedUrl.StartsWith(FestraLocaleDefaults.OfficialDocumentationUrlAsPrefix) ? localizedUrl : FestraLocaleDefaults.FESTRA_IntegratorDocumentation_URL;
        }

        public static string CompileDefaultLocaleJson()
        {
            var fields = typeof(FestraLocaleDefaults).GetFields(BindingFlags.NonPublic | BindingFlags.Static);
            var jsonObject = new JSONObject();
            foreach (var field in fields.Where(info => info.Name.StartsWith("FESTRA")))
            {
                jsonObject[field.Name] = new JSONString((string) field.GetValue(null));
            }

            var sb = new StringBuilder();
            jsonObject.WriteToStringBuilder(sb, 0, 0, JSONTextMode.Indent);
            return sb.ToString();
        }
    }
}
