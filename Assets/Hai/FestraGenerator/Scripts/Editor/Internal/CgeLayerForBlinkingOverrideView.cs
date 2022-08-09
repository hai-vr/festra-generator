﻿using Hai.FestraGenerator.Scripts.Editor.Internal.CgeAac;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using VRC.SDKBase;

namespace Hai.ComboGesture.Scripts.Editor.Internal
{
    internal class CgeLayerForBlinkingOverrideView
    {
        private const string AnimBlinkParam = "_Hai_GestureAnimBlink";
        
        private readonly float _analogBlinkingUpperThreshold;
        private readonly AvatarMask _logicalAvatarMask;
        private readonly AnimatorController _animatorController;
        private readonly CgeAssetContainer _assetContainer;
        private readonly bool _writeDefaultsForLogicalStates;

        public CgeLayerForBlinkingOverrideView(float analogBlinkingUpperThreshold, AvatarMask logicalAvatarMask, AnimatorController animatorController, CgeAssetContainer assetContainer, bool writeDefaults)
        {
            _analogBlinkingUpperThreshold = analogBlinkingUpperThreshold;
            _logicalAvatarMask = logicalAvatarMask;
            _animatorController = animatorController;
            _assetContainer = assetContainer;
            _writeDefaultsForLogicalStates = writeDefaults;
        }

        public void Create()
        {
            EditorUtility.DisplayProgressBar("ComboGestureExpressions", "Clearing eyes blinking override layer", 0f);
            var layer = ReinitializeLayer();

            var enableBlinking = CreateBlinkingState(layer, VRC_AnimatorTrackingControl.TrackingType.Tracking);
            var disableBlinking = CreateBlinkingState(layer, VRC_AnimatorTrackingControl.TrackingType.Animation);

            enableBlinking.TransitionsTo(disableBlinking)
                .When(layer.FloatParameter(AnimBlinkParam).IsGreaterThan(_analogBlinkingUpperThreshold))
                .Or().When(layer.FloatParameter(CgeFaceTracking.FTInfluenceParam).IsGreaterThan(_analogBlinkingUpperThreshold));
            disableBlinking.TransitionsTo(enableBlinking)
                .When(layer.FloatParameter(AnimBlinkParam).IsLessThan(_analogBlinkingUpperThreshold))
                .And(layer.FloatParameter(CgeFaceTracking.FTInfluenceParam).IsLessThan(_analogBlinkingUpperThreshold));
        }

        private CgeAacFlState CreateBlinkingState(CgeAacFlLayer layer, VRC_AnimatorTrackingControl.TrackingType type)
        {
            return layer.NewState(type == VRC_AnimatorTrackingControl.TrackingType.Tracking ? "EnableBlinking" : "DisableBlinking", type == VRC_AnimatorTrackingControl.TrackingType.Tracking ? 0 : 2, 3)
                .WithWriteDefaultsSetTo(_writeDefaultsForLogicalStates)
                .TrackingSets(CgeAacFlState.TrackingElement.Eyes, type);
        }

        private CgeAacFlLayer ReinitializeLayer()
        {
            return _assetContainer.ExposeCgeAac().CreateSupportingArbitraryControllerLayer(_animatorController, "Hai_GestureBlinking")
                .WithAvatarMask(_logicalAvatarMask)
                .CGE_WithLayerWeight(0f);
        }
    }
}
