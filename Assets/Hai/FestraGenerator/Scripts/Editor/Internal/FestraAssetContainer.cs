using System;
using Hai.FestraGenerator.Scripts.Editor.Internal.FestraAac;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using VRC.SDK3.Avatars.Components;

namespace Hai.FestraGenerator.Scripts.Editor.Internal
{
    internal class FestraAssetContainer
    {
        private readonly AnimatorController _holder;
        private readonly FestraAacFlBase _aac;

        private FestraAssetContainer(AnimatorController holder, VRCAvatarDescriptor avatarDescriptor)
        {
            _holder = holder;

            var root = avatarDescriptor != null ? avatarDescriptor.transform : null;
            _aac = FestraAacV0.Create(new FestraAacConfiguration
            {
                AnimatorRoot = root,
                AssetContainer = _holder,
                AssetKey = "GeneratedFESTRA",
                AvatarDescriptor = avatarDescriptor,
                DefaultsProvider = new FestraDefaultsProvider(true),
                SystemName = "FESTRA",
                DefaultValueRoot = root
            });
        }

        public static FestraAssetContainer CreateNew(string folderToCreateAssetIn, VRCAvatarDescriptor avatarDescriptor)
        {
            var holder = new AnimatorController();
            var container = new FestraAssetContainer(holder, avatarDescriptor);
            AssetDatabase.CreateAsset(holder, folderToCreateAssetIn + "/GeneratedFESTRA__" + DateTime.Now.ToString("yyyy'-'MM'-'dd'_'HHmmss") + ".asset");
            return container;
        }

        public static FestraAssetContainer FromExisting(RuntimeAnimatorController existingContainer, VRCAvatarDescriptor avatarDescriptor)
        {
            var assetContainer = (AnimatorController) existingContainer;
            if (assetContainer == null)
            {
                throw new ArgumentException("An asset container must not be null.");
            }
            if (assetContainer.layers.Length != 0)
            {
                throw new ArgumentException("An asset container must have 0 layers to be safely used.");
            }
            if (!AssetDatabase.Contains(assetContainer))
            {
                throw new ArgumentException("The asset container must already be an asset");
            }

            return new FestraAssetContainer(assetContainer, avatarDescriptor);
        }

        public void AddAnimation(AnimationClip animation)
        {
            _aac.FESTRA_StoringMotion(animation);
        }

        public void AddBlendTree(BlendTree blendTree)
        {
            _aac.FESTRA_StoringMotion(blendTree);
        }

        public void AddAvatarMask(AvatarMask mask)
        {
            _aac.FESTRA_StoringAsset(mask);
        }

        public RuntimeAnimatorController ExposeContainerAsset()
        {
            return _holder;
        }

        public static void GlobalSave()
        {
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public FestraAacFlBase ExposeFestraAac()
        {
            return _aac;
        }
    }

    public sealed class FestraDefaultsProvider : IFestraAacDefaultsProvider
    {
        private readonly bool _writeDefaults;

        public FestraDefaultsProvider(bool writeDefaults = false)
        {
            _writeDefaults = writeDefaults;
        }

        public void ConfigureState(AnimatorState state, AnimationClip emptyClip)
        {
            state.motion = emptyClip;
            state.writeDefaultValues = _writeDefaults;
        }

        public void ConfigureTransition(AnimatorStateTransition transition)
        {
            transition.duration = 0;
            transition.hasExitTime = false;
            transition.exitTime = 0;
            transition.hasFixedDuration = true;
            transition.offset = 0;
            transition.interruptionSource = TransitionInterruptionSource.None;
            transition.orderedInterruption = true;
            transition.canTransitionToSelf = false;
        }

        public string ConvertLayerName(string systemName)
        {
            return systemName;
        }

        public string ConvertLayerNameWithSuffix(string systemName, string suffix)
        {
            return suffix;
        }

        public Vector2 Grid()
        {
            return new Vector2(250, 70);
        }

        public void ConfigureStateMachine(AnimatorStateMachine stateMachine)
        {
            var grid = Grid();
            stateMachine.anyStatePosition = grid * new Vector2(0, 7);
            stateMachine.entryPosition = grid * new Vector2(0, -1);
            stateMachine.exitPosition = grid * new Vector2(7, -1);
            stateMachine.parentStateMachinePosition = grid * new Vector2(3, -1);
        }
    }
}
