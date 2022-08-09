using UnityEngine;
using VRC.SDK3.Avatars.Components;

namespace Hai.FestraGenerator.Scripts.Components
{
    public class FestraCompiler : MonoBehaviour
    {
        public RuntimeAnimatorController animatorController;
        public bool useGesturePlayableLayer;
        public RuntimeAnimatorController gesturePlayableLayerController;
        public RuntimeAnimatorController folderToGenerateNeutralizedAssetsIn;
        public RuntimeAnimatorController assetContainer;
        public bool generateNewContainerEveryTime;

        public AnimationClip customEmptyClip;
        public float analogBlinkingUpperThreshold = 0.95f;

        public bool doNotGenerateBlinkingOverrideLayer;
        public bool doNotGenerateWeightCorrectionLayer;

        public WriteDefaultsRecommendationMode writeDefaultsRecommendationMode = WriteDefaultsRecommendationMode.FollowVrChatRecommendationWriteDefaultsOff;
        public WriteDefaultsRecommendationMode writeDefaultsRecommendationModeGesture = WriteDefaultsRecommendationMode.UseUnsupportedWriteDefaultsOn;

        public AvatarMask generatedAvatarMask;

        public bool editorAdvancedFoldout;

        public AnimationClip ignoreParamList;
        public AnimationClip fallbackParamList;
        public bool doNotFixSingleKeyframes;

        public VRCAvatarDescriptor avatarDescriptor;
        public bool bypassMandatoryAvatarDescriptor;

        public bool doNotForceBlinkBlendshapes;

        public string mmdCompatibilityToggleParameter;
        public int totalNumberOfGenerations;

        public FestraAvatar faceTracking;
    }

    [System.Serializable]
    public enum GestureComboStageKind
    {
        Activity, Puppet, Massive
    }

    [System.Serializable]
    public enum WriteDefaultsRecommendationMode
    {
        FollowVrChatRecommendationWriteDefaultsOff, UseUnsupportedWriteDefaultsOn
    }
}
