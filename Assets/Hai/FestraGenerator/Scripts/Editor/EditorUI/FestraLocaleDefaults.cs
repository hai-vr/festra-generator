
// ReSharper disable InconsistentNaming

namespace Hai.FestraGenerator.Scripts.Editor.EditorUI
{
    public class FestraLocaleDefaults
    {
        // 1.4
        internal static string OfficialDocumentationUrlAsPrefix = "https://hai-vr.github.io/combo-gesture-expressions-av3/";
        internal static string FESTRA_Documentation_URL = OfficialDocumentationUrlAsPrefix;
        internal static string FESTRA_PermutationsDocumentation_URL = OfficialDocumentationUrlAsPrefix + "#permutations";
        internal static string FESTRA_IntegratorDocumentation_URL = OfficialDocumentationUrlAsPrefix + "integrator.html";
        internal static string FESTRAE_Open_editor = "Open editor";
        internal static string FESTRAE_Additional_editors = "Additional editors";
        internal static string FESTRAE_All_combos = "All combos";
        internal static string FESTRAE_Analog_Fist = "Analog Fist";
        internal static string FESTRAE_Combine_expressions = "Combine expressions";
        internal static string FESTRAE_Combos = "Combos";
        internal static string FESTRAE_Complete_view = "Complete view";
        internal static string FESTRAE_Create_blend_trees = "Create blend trees";
        internal static string FESTRAE_Manipulate_trees = "Manipulate trees";
        internal static string FESTRAE_Other_options = "Other options";
        internal static string FESTRAE_Permutations = "Permutations";
        internal static string FESTRAE_Prevent_eyes_blinking = "Prevent eyes blinking";
        internal static string FESTRAE_Set_face_expressions = "Set face expressions";
        internal static string FESTRAE_Simplified_view = "Simplified view";
        internal static string FESTRAE_Singles = "Singles";
        internal static string FESTRAE_Tutorials = "Tutorials";
        internal static string FESTRAE_View_blend_trees = "View blend trees";
        internal static string FESTRAE_Open_Documentation_and_tutorials = "Open documentation and tutorials";
        internal static string FESTRAE_PermutationsIntro = @"Permutations allows animations to depend on which hand side is doing the gesture.
It is significantly harder to create and use an Activity with permutations.
Consider using multiple Activities instead before deciding to use permutations.
When a permutation is not defined, the other side will be used.";
        internal static string FESTRAE_ConfirmUsePermutations = @"Do you really want to use permutations?";
        internal static string FESTRAE_Enable_permutations_for_this_Activity = @"Enable permutations for this Activity";
        internal static string FESTRAE_PermutationsFootnote = @"Permutations can be disabled later. Permutations are saved even after disabling permutations.
Compiling an Activity with permutations disabled will not take any saved permutation into account.";
        internal static string FESTRAE_GeneratePreview = "Generate\npreview";
        internal static string FESTRAE_SetupPreview = "Setup\npreview";
        internal static string FESTRAE_SelectFaceExpressionsWithBothEyesClosed = "Select face expressions with <b>both eyes closed</b>.";
        internal static string FESTRAE_Blinking = "Blinking";
        internal static string FESTRAE_Transition_duration_in_seconds = "Transition duration\n(in seconds)";
        internal static string FESTRAE_IncompletePreviewSetup = "A preview setup was found but it is incomplete or invalid.";
        internal static string FESTRAE_Automatically_setup_preview = "Automatically setup preview!";
        internal static string FESTRAE_AutoSetupReused = "The scene already contains a preview setup. It has been reused here.";
        internal static string FESTRAE_AutoSetupNoActiveAvatarDescriptor = "No active avatar descriptor was found in the root objects of the scene.";
        internal static string FESTRAE_AutoSetupCreated = "A new preview setup was created.";
        internal static string FESTRAE_Transition_duration = "Transition duration (s)";
        internal static string FESTRAE_Preview_setup = "Preview setup";
        internal static string FESTRAE_Generate_missing_previews = "Generate missing previews";
        internal static string FESTRAE_Regenerate_all_previews = "Regenerate all previews";
        internal static string FESTRAE_Stop_generating_previews = "Stop generating previews";
        internal static string FESTRAE_ExplainFourDirections = "Joystick with 4 directions: Up, down, left, right.";
        internal static string FESTRAE_ExplainEightDirections = "Joystick with 8 directions: Up, down, left, right, and all of the diagonals in a circle shape.";
        internal static string FESTRAE_ExplainSixDirectionsPointingForward = "Joystick with 6 directions in a hexagon shape. The up and down directions are lined up.";
        internal static string FESTRAE_ExplainSixDirectionsPointingSideways = "Joystick with 6 directions in a hexagon shape. The left and right directions are lined up.";
        internal static string FESTRAE_ExplainSingleAnalogFistWithHairTrigger = "One-handed analog Fist.\nThe parameter _AutoGestureWeight will be automatically replaced with the appropriate hand weight parameter.";
        internal static string FESTRAE_ExplainSingleAnalogFistAndTwoDirections = "One-handed analog Fist with an option to combine it with one Joystick direction.\nThe parameter _AutoGestureWeight will be automatically replaced with the appropriate hand weight parameter.";
        internal static string FESTRAE_ExplainDualAnalogFist = "Two-handed analog Fist.\nThe parameter GestureRightWeight is on the X axis to better visualize the blend tree directions.";
        internal static string FESTRAE_Create_a_new_blend_tree = "Create a new blend tree";
        internal static string FESTRAE_Blend_tree_asset = "Blend tree asset";
        //
        internal static string FESTRAC_Documentation_and_tutorials = @"Documentation and tutorials";
        internal static string FESTRAC_BackupFX = @"Make backups! The FX Animator Controller will be modified directly.";
        internal static string FESTRAC_FX_Playable_Layer = @"FX Playable Layer";
        internal static string FESTRAC_Gesture_Playable_Layer = @"Gesture Playable Layer";
        internal static string FESTRAC_FX_Animator_Controller = @"FX Animator Controller";
        internal static string FESTRAC_Parameter_Mode = @"Parameter Type";
        internal static string FESTRAC_Parameter_Name = @"Parameter Name";
        internal static string FESTRAC_Parameter_Value = @"Parameter Value";
        internal static string FESTRAC_Mood_sets = @"Mood sets";
        internal static string FESTRAC_HelpExpressionParameterOptimize = @"Parameter Type is set to Single Int. This will cost 8 bits of memory in your Expression Parameters even though you're not using a large amount of mood sets.

Usually, you should switch to Multiple Bools instead especially if you're short on Expression Parameters.";
        internal static string FESTRAC_WarnValuesOverlap = @"Some Parameters Values are overlapping.";
        internal static string FESTRAC_WarnNamesOverlap = @"Some Parameter Names are overlapping.";
        internal static string FESTRAC_WarnNoBlendTree = @"One of the puppets has no blend tree defined inside it.";
        internal static string FESTRAC_WarnNoActivity = @"One of the mood sets is missing.";
        internal static string FESTRAC_WarnNoActivityName = @"Parameter Name is missing.";
        internal static string FESTRAC_HelpWhenAllParameterNamesDefined = @"The first Mood set in the list ""{0}"" having Parameter name ""{1}"" will be active by default whenever none of the others are active.
You may choose to leave one of the mood set Parameter Name blank and it will become the default instead.";
        internal static string FESTRAC_HintDefaultMood = @"The mood set ""{0}"" is the default mood because it has a blank Parameter Name.";
        internal static string FESTRAC_GestureWeight_correction = @"GestureWeight correction";
        internal static string FESTRAC_Avatar_descriptor = @"Avatar descriptor";
        internal static string FESTRAC_Support_for_other_transforms = @"Enable this if you need support for ears/wings/tail/other transforms.";
        internal static string FESTRAC_Gesture_playable_layer_support = @"Gesture Playable Layer support";
        internal static string FESTRAC_BackupGesture = @"Make backups! The Gesture Animator Controller will be modified directly.";
        internal static string FESTRAC_Gesture_Animator_Controller = @"Gesture Animator Controller";
        internal static string FESTRAC_MusclesUnsupported = @"Finger positions or other muscles are not supported.";
        internal static string FESTRAC_Synchronization = @"Synchronization";
        internal static string FESTRAC_Synchronize_Animator_FX_and_Gesture_layers = @"Synchronize Animator FX and Gesture layers";
        internal static string FESTRAC_Synchronize_Animator_FX_layers = @"Synchronize Animator FX layers";
        internal static string FESTRAC_Asset_generation = @"Asset generation";
        internal static string FESTRAC_Asset_container = @"Asset container";
        internal static string FESTRAC_FX_Playable_Mode = @"FX Playable Mode";
        internal static string FESTRAC_WarnCautiousWriteDefaultsChosenOff = @"Write Defaults is ON for the Gesture layer. You can choose to switch to Write Defaults OFF to follow VRChat documentation, but be aware it is more difficult to do.";
        internal static string FESTRAC_WarnWriteDefaultsChosenOff = @"You have chosen to use Write Defaults ON. This goes against VRChat recommendation.";
        internal static string FESTRAC_AndMoreOnly15FirstResults = @"
... and more (only first 15 results shown).";
        internal static string FESTRAC_WarnWriteDefaultsOnStatesFound = @"Some states have Write Defaults ON:";
        internal static string FESTRAC_Gesture_Playable_Mode = @"Gesture Playable Mode";
        internal static string FESTRAC_Other_tweaks = @"Other tweaks";
        internal static string FESTRAC_Analog_fist_blinking_threshold = @"Analog fist blinking threshold";
        internal static string FESTRAC_AnalogFist_Popup = @"(0: Eyes are open, 1: Eyes are closed)";
        internal static string FESTRAC_Advanced = @"Advanced";
        internal static string FESTRAC_Capture_Transforms_Mode = @"Capture Transforms Mode";
        internal static string FESTRAC_MissingFxMask = @"There are {0} layers of your FX animator that have a missing mask. This will usually prevent Transform animations of the Gesture Playable Layer from functioning properly.

You can try to fix this by clicking ""Add missing masks"" which will modify your existing FX animator. However, be aware that masks might cause conflicts with other functions of your avatar.";
        internal static string FESTRAC_Add_missing_masks = "Add missing masks";
        internal static string FESTRAC_Remove_applied_masks = "Remove applied masks";
        internal static string FESTRAC_Unbind_Asset_container = "Unbind Asset container";

        internal static string FESTRAI_BackupAnimator = @"Make backups! The Animator Controller will be modified directly.";
        internal static string FESTRAI_Animator_Controller = @"Animator Controller";
        internal static string FESTRAI_Info = @"FestraIntegrator is a module that lets you integrate the Weight Correction layers onto your Animator controller even if you don't use Festra face expression system.

After synchronizing, you will need to edit your Normalized Time or Blend Tree parameters manually:
- Replace GestureLeftWeight with _Hai_GestureLWSmoothing
- Replace GestureRightWeight with _Hai_GestureRWSmoothing

You need one FestraIntegrator per animator layer that depends on it (Gesture, FX...).
You usually need to synchronize only once.";
        internal static string FESTRAI_Synchronize_Animator_layers = @"Synchronize Animator layers";
        internal static string FESTRAI_Documentation = @"Integrator Documentation";
        // 1.5
        internal static string FESTRAE_EyesAreClosed = "Eyes are closed";
        // 1.6.0
        internal static string FESTRAE_OneHandMode = "One Hand Mode";
        internal static string FESTRAE_OneHandModeIntro = @"When using One Hand Mode, you will not use combos at all. Only one hand will be used, and the other hand will control nothing.";
        internal static string FESTRAC_WarnNoMassiveBlend = @"One of the massive blends has an incorrect configuration.";
        internal static string FESTRAE_Combine = "Combine";
        internal static string FESTRAE_CombineAcross = "Combine Across";
        internal static string FESTRAE_Create = "Create";
        internal static string FESTRAE_Simplify = "Simplify";
        internal static string FESTRAE_SwapToFix = "Swap to Fix";
        internal static string FESTRAE_AutoSet = "Auto-set";
        internal static string FESTRAE_EnablePermutations = "Enable permutations";
        internal static string FESTRAE_CombinerShowHidden = "Show hidden";
        internal static string FESTRAE_CombinerShowFullPaths = "Show full paths";
        internal static string FESTRAE_TreeAnimationAtRest = "Animation at rest";
        internal static string FESTRAE_TreeJoystickCenterAnimation = "Joystick center animation";
        internal static string FESTRAE_TreeFixJoystickSnapping = "Fix joystick snapping";
        internal static string FESTRAE_TreeJoystickMaximumTilt = "Joystick maximum tilt";
        internal static string FESTRAE_TreeCreateAsset = "Create a new blend tree asset";
        internal static string FESTRAE_TreeFileCreate = "Create...";
        internal static string FESTRAE_TreeFileInvalidSavePath = "Invalid save path";
        internal static string FESTRAE_TreeFileInvalidSavePathMessage = "Save path must be in the project's /Assets path.";
        internal static string FESTRAC_ViveAdvancedControlsWarning = @"Vive Advanced Controls Analog is enabled, which will significantly change the way the Animator is generated.
The animations may look different when you are not using Vive controllers (such as Index controllers or Oculus controllers).

You should disable Vive Advanced Controls Analog if you are not sure of what you're doing.";

        internal static string FESTRAC_Avatar_Dynamics = "Avatar Dynamics";
        internal static string FESTRAC_Dynamics = "Dynamics";
        internal static string FESTRAC_DoNotForceBlinkBlendshapes = "Do not force blink blendshapes";
        internal static string FESTRAD_DynamicExpression = "Dynamic Expression";
        internal static string FESTRAD_DynamicCondition = "Dynamic Condition";
        internal static string FESTRAD_Clip = "Clip";
        internal static string FESTRAD_Condition = "Condition";
        internal static string FESTRAD_ContactReceiver = "Contact Receiver";
        internal static string FESTRAD_Effect = "Effect";
        internal static string FESTRAD_IsHardThreshold = "Is hard threshold";
        internal static string FESTRAD_MoodSet = "Mood";
        internal static string FESTRAD_ParameterName = "Parameter name";
        internal static string FESTRAD_ParameterType = "Parameter type";
        internal static string FESTRAD_PhysBone = "PhysBone";
        internal static string FESTRAD_PhysBoneSource = "PhysBone Source";
        internal static string FESTRAD_Source = "Source";
        internal static string FESTRAD_Threshold = "Threshold";
        internal static string FESTRAD_Higher_priority = "Dynamic Expressions at the top have a higher priority than the Dynamic Expressions at the bottom.";
        internal static string FESTRAC_MMD_compatibility_toggle_parameter = "MMD compatibility toggle parameter";
        internal static string FESTRAE_Mode = "Mode";
        internal static string FESTRAC_Slowness_warning = "Synchronization can become slower after generating multiple times in a single Unity session.\nIf synchronization becomes too slow, please restart Unity.";
        internal static string FESTRAC_SynchronizationConditionsV2 = @"Synchronization will regenerate FESTRA's animator layers and generate animations.
- Only layers starting with 'Hai_Gesture' will be affected.
- The avatar descriptor will not be modified.

You should press synchronize when any of the following happens:
- this Compiler is modified,
- an Activity, Puppet, Massive, or Dynamics is modified,
- an animation or a blend tree or avatar mask is modified,
- the avatar descriptor Eyelids is modified,
- the avatar transforms are modified,
- if MMD compatibility is enabled and the animator layer order is modified.";

        internal static string FESTRAD_MissingParameterOnContact = "This Contact has no parameter name. You must define it within the Contact component.";
        internal static string FESTRAD_MissingParameterOnPhysBone = "This PhysBone has no parameter name. You must define it within the PhysBone component.";
        internal static string FESTRAC_MainDynamics = "Main Dynamics";
        internal static string FESTRAD_EnterTransitionDuration = "Enter transition duration (s)";
        internal static string FESTRAD_OnEnterCurve = "OnEnter Curve";
        internal static string FESTRAD_OnEnterDuration = "Duration (s)";
        internal static string FESTRAD_BehavesLikeOnEnter = "Behaves like OnEnter";
        internal static string FESTRAD_UpperBound = "Upper bound";
        internal static string FESTRAC_Synchronize_Face_Tracking_Layers = "Synchronize Face Tracking Layers";
        internal static string FESTRAC_FaceTracking = "Face Tracking";
    }
}
