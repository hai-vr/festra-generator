using Hai.FestraGenerator.Scripts.Editor.Internal.FestraAac;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using VRC.SDKBase;

namespace Hai.FestraGenerator.Scripts.Editor.Internal
{
    internal class FestraLayerForBlinkingOverrideView
    {
        private const string AnimBlinkParam = "_Hai_GestureAnimBlink";
        
        private readonly float _analogBlinkingUpperThreshold;
        private readonly AvatarMask _logicalAvatarMask;
        private readonly AnimatorController _animatorController;
        private readonly FestraAssetContainer _assetContainer;
        private readonly bool _writeDefaultsForLogicalStates;

        public FestraLayerForBlinkingOverrideView(float analogBlinkingUpperThreshold, AvatarMask logicalAvatarMask, AnimatorController animatorController, FestraAssetContainer assetContainer, bool writeDefaults)
        {
            _analogBlinkingUpperThreshold = analogBlinkingUpperThreshold;
            _logicalAvatarMask = logicalAvatarMask;
            _animatorController = animatorController;
            _assetContainer = assetContainer;
            _writeDefaultsForLogicalStates = writeDefaults;
        }

        public void Create()
        {
            EditorUtility.DisplayProgressBar("FestraExpressions", "Clearing eyes blinking override layer", 0f);
            var layer = ReinitializeLayer();

            var enableBlinking = CreateBlinkingState(layer, VRC_AnimatorTrackingControl.TrackingType.Tracking);
            var disableBlinking = CreateBlinkingState(layer, VRC_AnimatorTrackingControl.TrackingType.Animation);

            enableBlinking.TransitionsTo(disableBlinking)
                .When(layer.FloatParameter(AnimBlinkParam).IsGreaterThan(_analogBlinkingUpperThreshold))
                .Or().When(layer.FloatParameter(FestraGeneratorInternal.FTInfluenceParam).IsGreaterThan(_analogBlinkingUpperThreshold));
            disableBlinking.TransitionsTo(enableBlinking)
                .When(layer.FloatParameter(AnimBlinkParam).IsLessThan(_analogBlinkingUpperThreshold))
                .And(layer.FloatParameter(FestraGeneratorInternal.FTInfluenceParam).IsLessThan(_analogBlinkingUpperThreshold));
        }

        private FestraAacFlState CreateBlinkingState(FestraAacFlLayer layer, VRC_AnimatorTrackingControl.TrackingType type)
        {
            return layer.NewState(type == VRC_AnimatorTrackingControl.TrackingType.Tracking ? "EnableBlinking" : "DisableBlinking", type == VRC_AnimatorTrackingControl.TrackingType.Tracking ? 0 : 2, 3)
                .WithWriteDefaultsSetTo(_writeDefaultsForLogicalStates)
                .TrackingSets(FestraAacFlState.TrackingElement.Eyes, type);
        }

        private FestraAacFlLayer ReinitializeLayer()
        {
            return _assetContainer.ExposeFestraAac().CreateSupportingArbitraryControllerLayer(_animatorController, "Hai_GestureBlinking")
                .WithAvatarMask(_logicalAvatarMask)
                .FESTRA_WithLayerWeight(0f);
        }
    }
}
