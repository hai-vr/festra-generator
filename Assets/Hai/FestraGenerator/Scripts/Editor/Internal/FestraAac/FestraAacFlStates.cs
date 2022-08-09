using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using VRC.SDKBase;

namespace Hai.FestraGenerator.Scripts.Editor.Internal.FestraAac
{
    internal class FestraAacBackingAnimator
    {
        private readonly FestraAacAnimatorGenerator _generator;

        public FestraAacBackingAnimator(FestraAacAnimatorGenerator animatorGenerator)
        {
            _generator = animatorGenerator;
        }

        public FestraAacFlBoolParameter BoolParameter(string parameterName)
        {
            var result = FestraAacFlBoolParameter.Internally(parameterName);
            _generator.CreateParamsAsNeeded(result);
            return result;
        }

        public FestraAacFlBoolParameter TriggerParameter(string parameterName)
        {
            var result = FestraAacFlBoolParameter.Internally(parameterName);
            _generator.CreateTriggerParamsAsNeeded(result);
            return result;
        }

        public FestraAacFlFloatParameter FloatParameter(string parameterName)
        {
            var result = FestraAacFlFloatParameter.Internally(parameterName);
            _generator.CreateParamsAsNeeded(result);
            return result;
        }

        public FestraAacFlIntParameter IntParameter(string parameterName)
        {
            var result = FestraAacFlIntParameter.Internally(parameterName);
            _generator.CreateParamsAsNeeded(result);
            return result;
        }

        public FestraAacFlEnumIntParameter<TEnum> EnumParameter<TEnum>(string parameterName) where TEnum : Enum
        {
            var result = FestraAacFlEnumIntParameter<TEnum>.Internally<TEnum>(parameterName);
            _generator.CreateParamsAsNeeded(result);
            return result;
        }

        public FestraAacFlBoolParameterGroup BoolParameters(params string[] parameterNames)
        {
            var result = FestraAacFlBoolParameterGroup.Internally(parameterNames);
            _generator.CreateParamsAsNeeded(result.ToList().ToArray());
            return result;
        }

        public FestraAacFlBoolParameterGroup TriggerParameters(params string[] parameterNames)
        {
            var result = FestraAacFlBoolParameterGroup.Internally(parameterNames);
            _generator.CreateTriggerParamsAsNeeded(result.ToList().ToArray());
            return result;
        }

        public FestraAacFlFloatParameterGroup FloatParameters(params string[] parameterNames)
        {
            var result = FestraAacFlFloatParameterGroup.Internally(parameterNames);
            _generator.CreateParamsAsNeeded(result.ToList().ToArray());
            return result;
        }

        public FestraAacFlIntParameterGroup IntParameters(params string[] parameterNames)
        {
            var result = FestraAacFlIntParameterGroup.Internally(parameterNames);
            _generator.CreateParamsAsNeeded(result.ToList().ToArray());
            return result;
        }

        public FestraAacFlBoolParameterGroup BoolParameters(params FestraAacFlBoolParameter[] parameters)
        {
            var result = FestraAacFlBoolParameterGroup.Internally(parameters.Select(parameter => parameter.Name).ToArray());
            _generator.CreateParamsAsNeeded(parameters);
            return result;
        }

        public FestraAacFlBoolParameterGroup TriggerParameters(params FestraAacFlBoolParameter[] parameters)
        {
            var result = FestraAacFlBoolParameterGroup.Internally(parameters.Select(parameter => parameter.Name).ToArray());
            _generator.CreateTriggerParamsAsNeeded(parameters);
            return result;
        }

        public FestraAacFlFloatParameterGroup FloatParameters(params FestraAacFlFloatParameter[] parameters)
        {
            var result = FestraAacFlFloatParameterGroup.Internally(parameters.Select(parameter => parameter.Name).ToArray());
            _generator.CreateParamsAsNeeded(parameters);
            return result;
        }

        public FestraAacFlIntParameterGroup IntParameters(params FestraAacFlIntParameter[] parameters)
        {
            var result = FestraAacFlIntParameterGroup.Internally(parameters.Select(parameter => parameter.Name).ToArray());
            _generator.CreateParamsAsNeeded(parameters);
            return result;
        }
    }

    public class FestraAacFlStateMachine : FestraAacAnimatorNode<FestraAacFlStateMachine>
    {
        public readonly AnimatorStateMachine Machine;
        private readonly AnimationClip _emptyClip;
        private readonly FestraAacBackingAnimator _backingAnimator;
        private readonly IFestraAacDefaultsProvider _defaultsProvider;
        private readonly float _gridShiftX;
        private readonly float _gridShiftY;

        private readonly List<FestraAacAnimatorNode> _childNodes;

        internal FestraAacFlStateMachine(AnimatorStateMachine machine, AnimationClip emptyClip, FestraAacBackingAnimator backingAnimator, IFestraAacDefaultsProvider defaultsProvider, FestraAacFlStateMachine parent = null)
            : base(parent, defaultsProvider)
        {
            Machine = machine;
            _emptyClip = emptyClip;
            _backingAnimator = backingAnimator;
            _defaultsProvider = defaultsProvider;

            var grid = defaultsProvider.Grid();
            _gridShiftX = grid.x;
            _gridShiftY = grid.y;

            _childNodes = new List<FestraAacAnimatorNode>();
        }

        internal FestraAacBackingAnimator BackingAnimator()
        {
            return _backingAnimator;
        }

        public FestraAacFlStateMachine NewSubStateMachine(string name)
        {
            var lastState = LastNodePosition();
            return NewSubStateMachine(name, 0, 0).Shift(lastState, 0, 1);
        }

        public FestraAacFlStateMachine NewSubStateMachine(string name, int x, int y)
        {
            var stateMachine = Machine.AddStateMachine(name, GridPosition(x, y));
            FestraAacV0.UndoDisable(stateMachine);
            var aacMachine = new FestraAacFlStateMachine(stateMachine, _emptyClip, _backingAnimator, DefaultsProvider, this);
            _defaultsProvider.ConfigureStateMachine(stateMachine);
            _childNodes.Add(aacMachine);
            return aacMachine;
        }

        public FestraAacFlStateMachine WithEntryPosition(int x, int y)
        {
            Machine.entryPosition = GridPosition(x, y);
            return this;
        }

        public FestraAacFlStateMachine WithExitPosition(int x, int y)
        {
            Machine.exitPosition = GridPosition(x, y);
            return this;
        }

        public FestraAacFlStateMachine WithAnyStatePosition(int x, int y)
        {
            Machine.anyStatePosition = GridPosition(x, y);
            return this;
        }

        public FestraAacFlStateMachine WithParentStateMachinePosition(int x, int y)
        {
            Machine.parentStateMachinePosition = GridPosition(x, y);
            return this;
        }

        public FestraAacFlState NewState(string name)
        {
            var lastState = LastNodePosition();
            return NewState(name, 0, 0).Shift(lastState, 0, 1);
        }

        public FestraAacFlState NewState(string name, int x, int y)
        {
            var state = Machine.AddState(name, GridPosition(x, y));
            FestraAacV0.UndoDisable(state);
            DefaultsProvider.ConfigureState(state, _emptyClip);
            var aacState = new FestraAacFlState(state, this, DefaultsProvider);
            _childNodes.Add(aacState);
            return aacState;
        }

        public FestraAacFlTransition AnyTransitionsTo(FestraAacFlState destination)
        {
            return AnyTransition(destination, Machine);
        }

        public FestraAacFlEntryTransition EntryTransitionsTo(FestraAacFlState destination)
        {
            return EntryTransition(destination, Machine);
        }

        public FestraAacFlEntryTransition EntryTransitionsTo(FestraAacFlStateMachine destination)
        {
            return EntryTransition(destination, Machine);
        }

        public FestraAacFlEntryTransition TransitionsFromEntry()
        {
            return EntryTransition(this, ParentMachine.Machine);
        }

        public FestraAacFlNewTransitionContinuation TransitionsTo(FestraAacFlState destination)
        {
            var transition = ParentMachine.Machine.AddStateMachineTransition(Machine, destination.State);
            FestraAacV0.UndoDisable(transition);
            return new FestraAacFlNewTransitionContinuation(transition, ParentMachine.Machine, Machine, destination.State);
        }

        public FestraAacFlNewTransitionContinuation TransitionsTo(FestraAacFlStateMachine destination)
        {
            var transition = ParentMachine.Machine.AddStateMachineTransition(Machine, destination.Machine);
            FestraAacV0.UndoDisable(transition);
            return new FestraAacFlNewTransitionContinuation(transition, ParentMachine.Machine, Machine, destination.Machine);
        }

        public FestraAacFlNewTransitionContinuation Restarts()
        {
            var transition = ParentMachine.Machine.AddStateMachineTransition(Machine, Machine);
            FestraAacV0.UndoDisable(transition);
            return new FestraAacFlNewTransitionContinuation(transition, ParentMachine.Machine, Machine, Machine);
        }

        public FestraAacFlNewTransitionContinuation Exits()
        {
            var transition = ParentMachine.Machine.AddStateMachineExitTransition(Machine);
            FestraAacV0.UndoDisable(transition);
            return new FestraAacFlNewTransitionContinuation(transition, ParentMachine.Machine, Machine, null);
        }

        private FestraAacFlTransition AnyTransition(FestraAacFlState destination, AnimatorStateMachine animatorStateMachine)
        {
            var transition = animatorStateMachine.AddAnyStateTransition(destination.State);
            FestraAacV0.UndoDisable(transition);
            return new FestraAacFlTransition(ConfigureTransition(transition), animatorStateMachine, null, destination.State);
        }

        private AnimatorStateTransition ConfigureTransition(AnimatorStateTransition transition)
        {
            DefaultsProvider.ConfigureTransition(transition);
            return transition;
        }

        private FestraAacFlEntryTransition EntryTransition(FestraAacFlState destination, AnimatorStateMachine animatorStateMachine)
        {
            var transition = animatorStateMachine.AddEntryTransition(destination.State);
            FestraAacV0.UndoDisable(transition);
            return new FestraAacFlEntryTransition(transition, animatorStateMachine, null, destination.State);
        }

        private FestraAacFlEntryTransition EntryTransition(FestraAacFlStateMachine destination, AnimatorStateMachine animatorStateMachine)
        {
            var transition = animatorStateMachine.AddEntryTransition(destination.Machine);
            FestraAacV0.UndoDisable(transition);
            return new FestraAacFlEntryTransition(transition, animatorStateMachine, null, destination.Machine);
        }

        internal Vector3 LastNodePosition()
        {
            return _childNodes.LastOrDefault()?.GetPosition() ?? Vector3.right * _gridShiftX * 2;
        }

        private Vector3 GridPosition(int x, int y)
        {
            return new Vector3(x * _gridShiftX, y * _gridShiftY, 0);
        }

        internal IReadOnlyList<FestraAacAnimatorNode> GetChildNodes()
        {
            return _childNodes;
        }

        protected internal override Vector3 GetPosition()
        {
            return ParentMachine.Machine.stateMachines.First(x => x.stateMachine == Machine).position;
        }

        protected internal override void SetPosition(Vector3 position)
        {
            var stateMachines = ParentMachine.Machine.stateMachines;
            for (var i = 0; i < stateMachines.Length; i++)
            {
                var m = stateMachines[i];
                if (m.stateMachine == Machine)
                {
                    m.position = position;
                    stateMachines[i] = m;
                    break;
                }
            }
            ParentMachine.Machine.stateMachines = stateMachines;
        }

        public FestraAacFlStateMachine WithDefaultState(FestraAacFlState newDefaultState)
        {
            Machine.defaultState = newDefaultState.State;
            return this;
        }
    }

    public class FestraAacFlState : FestraAacAnimatorNode<FestraAacFlState>
    {
        public readonly AnimatorState State;
        private readonly AnimatorStateMachine _machine;
        private VRCAvatarParameterDriver _driver;
        private VRCAnimatorTrackingControl _tracking;
        private VRCAnimatorLocomotionControl _locomotionControl;
        private VRCAnimatorTemporaryPoseSpace _temporaryPoseSpace;

        public FestraAacFlState(AnimatorState state, FestraAacFlStateMachine parentMachine, IFestraAacDefaultsProvider defaultsProvider) : base(parentMachine, defaultsProvider)
        {
            State = state;
            _machine = parentMachine.Machine;
        }

        public FestraAacFlState WithAnimation(Motion clip)
        {
            State.motion = clip;
            return this;
        }

        public FestraAacFlState WithAnimation(FestraAacFlClip clip)
        {
            State.motion = clip.Clip;
            return this;
        }

        public FestraAacFlTransition TransitionsTo(FestraAacFlState destination)
        {
            var internalTransition = State.AddTransition(destination.State);
            FestraAacV0.UndoDisable(internalTransition);
            return new FestraAacFlTransition(ConfigureTransition(internalTransition), _machine, State, destination.State);
        }

        public FestraAacFlTransition TransitionsTo(FestraAacFlStateMachine destination)
        {
            var internalTransition = State.AddTransition(destination.Machine);
            FestraAacV0.UndoDisable(internalTransition);
            return new FestraAacFlTransition(internalTransition, _machine, State, destination.Machine);
        }

        public FestraAacFlTransition TransitionsFromAny()
        {
            var internalTransition = _machine.AddAnyStateTransition(State);
            FestraAacV0.UndoDisable(internalTransition);
            return new FestraAacFlTransition(ConfigureTransition(internalTransition), _machine, null, State);
        }

        public FestraAacFlEntryTransition TransitionsFromEntry()
        {
            var internalTransition = _machine.AddEntryTransition(State);
            FestraAacV0.UndoDisable(internalTransition);
            return new FestraAacFlEntryTransition(internalTransition, _machine, null, State);
        }

        public FestraAacFlState AutomaticallyMovesTo(FestraAacFlState destination)
        {
            var internalTransition = State.AddTransition(destination.State);
            FestraAacV0.UndoDisable(internalTransition);
            var transition = ConfigureTransition(internalTransition);
            transition.hasExitTime = true;
            return this;
        }

        public FestraAacFlState FESTRA_AutomaticallyMovesTo(FestraAacFlStateMachine destination)
        {
            var internalTransition = State.AddTransition(destination.Machine);
            FestraAacV0.UndoDisable(internalTransition);
            var transition = ConfigureTransition(internalTransition);
            transition.hasExitTime = true;
            return this;
        }

        public FestraAacFlTransition Exits()
        {
            var transition = State.AddExitTransition();
            FestraAacV0.UndoDisable(transition);
            return new FestraAacFlTransition(ConfigureTransition(transition), _machine, State, null);
        }

        private AnimatorStateTransition ConfigureTransition(AnimatorStateTransition transition)
        {
            DefaultsProvider.ConfigureTransition(transition);
            return transition;
        }

        public FestraAacFlState Drives(FestraAacFlIntParameter parameter, int value)
        {
            CreateDriverBehaviorIfNotExists();
            _driver.parameters.Add(new VRC_AvatarParameterDriver.Parameter
            {
                type = VRC_AvatarParameterDriver.ChangeType.Set,
                name = parameter.Name, value = value
            });
            return this;
        }

        public FestraAacFlState Drives(FestraAacFlFloatParameter parameter, float value)
        {
            CreateDriverBehaviorIfNotExists();
            _driver.parameters.Add(new VRC_AvatarParameterDriver.Parameter
            {
                type = VRC_AvatarParameterDriver.ChangeType.Set,
                name = parameter.Name, value = value
            });
            return this;
        }

        public FestraAacFlState DrivingIncreases(FestraAacFlFloatParameter parameter, float additiveValue)
        {
            CreateDriverBehaviorIfNotExists();
            _driver.parameters.Add(new VRC_AvatarParameterDriver.Parameter
            {
                type = VRC_AvatarParameterDriver.ChangeType.Add,
                name = parameter.Name, value = additiveValue
            });
            return this;
        }

        public FestraAacFlState DrivingDecreases(FestraAacFlFloatParameter parameter, float positiveValueToDecreaseBy)
        {
            CreateDriverBehaviorIfNotExists();
            _driver.parameters.Add(new VRC_AvatarParameterDriver.Parameter
            {
                type = VRC_AvatarParameterDriver.ChangeType.Add,
                name = parameter.Name, value = -positiveValueToDecreaseBy
            });
            return this;
        }

        public FestraAacFlState DrivingIncreases(FestraAacFlIntParameter parameter, int additiveValue)
        {
            CreateDriverBehaviorIfNotExists();
            _driver.parameters.Add(new VRC_AvatarParameterDriver.Parameter
            {
                type = VRC_AvatarParameterDriver.ChangeType.Add,
                name = parameter.Name, value = additiveValue
            });
            return this;
        }

        public FestraAacFlState DrivingDecreases(FestraAacFlIntParameter parameter, int positiveValueToDecreaseBy)
        {
            CreateDriverBehaviorIfNotExists();
            _driver.parameters.Add(new VRC_AvatarParameterDriver.Parameter
            {
                type = VRC_AvatarParameterDriver.ChangeType.Add,
                name = parameter.Name, value = -positiveValueToDecreaseBy
            });
            return this;
        }

        public FestraAacFlState DrivingRandomizesLocally(FestraAacFlFloatParameter parameter, float min, float max)
        {
            CreateDriverBehaviorIfNotExists();
            _driver.parameters.Add(new VRC_AvatarParameterDriver.Parameter
            {
                type = VRC_AvatarParameterDriver.ChangeType.Random,
                name = parameter.Name, valueMin = min, valueMax = max
            });
            _driver.localOnly = true;
            return this;
        }

        public FestraAacFlState DrivingRandomizesLocally(FestraAacFlBoolParameter parameter, float chance)
        {
            CreateDriverBehaviorIfNotExists();
            _driver.parameters.Add(new VRC_AvatarParameterDriver.Parameter
            {
                type = VRC_AvatarParameterDriver.ChangeType.Random,
                name = parameter.Name, chance = chance
            });
            _driver.localOnly = true;
            return this;
        }

        public FestraAacFlState DrivingRandomizesLocally(FestraAacFlIntParameter parameter, int min, int max)
        {
            CreateDriverBehaviorIfNotExists();
            _driver.parameters.Add(new VRC_AvatarParameterDriver.Parameter
            {
                type = VRC_AvatarParameterDriver.ChangeType.Random,
                name = parameter.Name, valueMin = min, valueMax = max
            });
            _driver.localOnly = true;
            return this;
        }

        public FestraAacFlState Drives(FestraAacFlBoolParameter parameter, bool value)
        {
            CreateDriverBehaviorIfNotExists();
            _driver.parameters.Add(new VRC_AvatarParameterDriver.Parameter
            {
                name = parameter.Name, value = value ? 1 : 0
            });
            return this;
        }

        public FestraAacFlState Drives(FestraAacFlBoolParameterGroup parameters, bool value)
        {
            CreateDriverBehaviorIfNotExists();
            foreach (var parameter in parameters.ToList())
            {
                _driver.parameters.Add(new VRC_AvatarParameterDriver.Parameter
                {
                    name = parameter.Name, value = value ? 1 : 0
                });
            }
            return this;
        }

        public FestraAacFlState DrivingLocally()
        {
            CreateDriverBehaviorIfNotExists();
            _driver.localOnly = true;
            return this;
        }

        private void CreateDriverBehaviorIfNotExists()
        {
            if (_driver != null) return;
            _driver = State.AddStateMachineBehaviour<VRCAvatarParameterDriver>();
            _driver.parameters = new List<VRC_AvatarParameterDriver.Parameter>();
        }

        public FestraAacFlState WithWriteDefaultsSetTo(bool shouldWriteDefaults)
        {
            State.writeDefaultValues = shouldWriteDefaults;
            return this;
        }

        public FestraAacFlState PrintsToLogUsingTrackingBehaviour(string value)
        {
            CreateTrackingBehaviorIfNotExists();
            _tracking.debugString = value;

            return this;
        }

        public FestraAacFlState TrackingTracks(TrackingElement element)
        {
            CreateTrackingBehaviorIfNotExists();
            SettingElementTo(element, VRC_AnimatorTrackingControl.TrackingType.Tracking);

            return this;
        }

        public FestraAacFlState TrackingAnimates(TrackingElement element)
        {
            CreateTrackingBehaviorIfNotExists();
            SettingElementTo(element, VRC_AnimatorTrackingControl.TrackingType.Animation);

            return this;
        }

        public FestraAacFlState TrackingSets(TrackingElement element, VRC_AnimatorTrackingControl.TrackingType trackingType)
        {
            CreateTrackingBehaviorIfNotExists();
            SettingElementTo(element, trackingType);

            return this;
        }

        public FestraAacFlState LocomotionEnabled()
        {
            CreateLocomotionBehaviorIfNotExists();
            _locomotionControl.disableLocomotion = false;

            return this;
        }

        public FestraAacFlState LocomotionDisabled()
        {
            CreateLocomotionBehaviorIfNotExists();
            _locomotionControl.disableLocomotion = true;

            return this;
        }

        public FestraAacFlState PlayableEnables(VRC_PlayableLayerControl.BlendableLayer blendable, float blendDurationSeconds = 0f)
        {
            return PlayableSets(blendable, blendDurationSeconds, 1.0f);
        }

        public FestraAacFlState PlayableDisables(VRC_PlayableLayerControl.BlendableLayer blendable, float blendDurationSeconds = 0f)
        {
            return PlayableSets(blendable, blendDurationSeconds, 0.0f);
        }

        public FestraAacFlState PlayableSets(VRC_PlayableLayerControl.BlendableLayer blendable, float blendDurationSeconds, float weight)
        {
            var playable = State.AddStateMachineBehaviour<VRCPlayableLayerControl>();
            playable.layer = blendable;
            playable.goalWeight = weight;
            playable.blendDuration = blendDurationSeconds;

            return this;
        }

        public FestraAacFlState PoseSpaceEntered(float delaySeconds = 0f)
        {
            CreateTemporaryPoseSpaceBehaviorIfNotExists();
            _temporaryPoseSpace.enterPoseSpace = true;
            _temporaryPoseSpace.fixedDelay = true;
            _temporaryPoseSpace.delayTime = delaySeconds;

            return this;
        }

        public FestraAacFlState PoseSpaceExited(float delaySeconds = 0f)
        {
            CreateTemporaryPoseSpaceBehaviorIfNotExists();
            _temporaryPoseSpace.enterPoseSpace = false;
            _temporaryPoseSpace.fixedDelay = true;
            _temporaryPoseSpace.delayTime = delaySeconds;

            return this;
        }

        public FestraAacFlState PoseSpaceEnteredPercent(float delayNormalized)
        {
            CreateTemporaryPoseSpaceBehaviorIfNotExists();
            _temporaryPoseSpace.enterPoseSpace = true;
            _temporaryPoseSpace.fixedDelay = false;
            _temporaryPoseSpace.delayTime = delayNormalized;

            return this;
        }

        public FestraAacFlState PoseSpaceExitedPercent(float delayNormalized)
        {
            CreateTemporaryPoseSpaceBehaviorIfNotExists();
            _temporaryPoseSpace.enterPoseSpace = false;
            _temporaryPoseSpace.fixedDelay = false;
            _temporaryPoseSpace.delayTime = delayNormalized;

            return this;
        }

        public FestraAacFlState MotionTime(FestraAacFlFloatParameter floatParam)
        {
            State.timeParameterActive = true;
            State.timeParameter = floatParam.Name;

            return this;
        }

        public FestraAacFlState WithCycleOffset(FestraAacFlFloatParameter floatParam)
        {
            State.cycleOffsetParameterActive = false;
            State.cycleOffsetParameter = floatParam.Name;

            return this;
        }

        public FestraAacFlState WithCycleOffsetSetTo(float cycleOffset)
        {
            State.cycleOffsetParameterActive = false;
            State.cycleOffset = cycleOffset;

            return this;
        }

        private void SettingElementTo(TrackingElement element, VRC_AnimatorTrackingControl.TrackingType target)
        {
            switch (element)
            {
                case TrackingElement.Head:
                    _tracking.trackingHead = target;
                    break;
                case TrackingElement.LeftHand:
                    _tracking.trackingLeftHand = target;
                    break;
                case TrackingElement.RightHand:
                    _tracking.trackingRightHand = target;
                    break;
                case TrackingElement.Hip:
                    _tracking.trackingHip = target;
                    break;
                case TrackingElement.LeftFoot:
                    _tracking.trackingLeftFoot = target;
                    break;
                case TrackingElement.RightFoot:
                    _tracking.trackingRightFoot = target;
                    break;
                case TrackingElement.LeftFingers:
                    _tracking.trackingLeftFingers = target;
                    break;
                case TrackingElement.RightFingers:
                    _tracking.trackingRightFingers = target;
                    break;
                case TrackingElement.Eyes:
                    _tracking.trackingEyes = target;
                    break;
                case TrackingElement.Mouth:
                    _tracking.trackingMouth = target;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(element), element, null);
            }
        }

        private void CreateTrackingBehaviorIfNotExists()
        {
            if (_tracking != null) return;
            _tracking = State.AddStateMachineBehaviour<VRCAnimatorTrackingControl>();
        }

        private void CreateLocomotionBehaviorIfNotExists()
        {
            if (_locomotionControl != null) return;
            _locomotionControl = State.AddStateMachineBehaviour<VRCAnimatorLocomotionControl>();
        }

        private void CreateTemporaryPoseSpaceBehaviorIfNotExists()
        {
            if (_temporaryPoseSpace != null) return;
            _temporaryPoseSpace = State.AddStateMachineBehaviour<VRCAnimatorTemporaryPoseSpace>();
        }

        public enum TrackingElement
        {
            Head,
            LeftHand,
            RightHand,
            Hip,
            LeftFoot,
            RightFoot,
            LeftFingers,
            RightFingers,
            Eyes,
            Mouth
        }

        public FestraAacFlState WithSpeed(FestraAacFlFloatParameter parameter)
        {
            State.speedParameterActive = true;
            State.speedParameter = parameter.Name;

            return this;
        }

        public FestraAacFlState WithSpeedSetTo(float speed)
        {
            State.speedParameterActive = false;
            State.speed = speed;

            return this;
        }

        protected internal override Vector3 GetPosition()
        {
            return _machine.states.First(x => x.state == State).position;
        }

        protected internal override void SetPosition(Vector3 position)
        {
            var states = _machine.states;
            for (var i = 0; i < states.Length; i++)
            {
                var m = states[i];
                if (m.state == State)
                {
                    m.position = position;
                    states[i] = m;
                    break;
                }
            }
            _machine.states = states;
        }
    }

    public class FestraAacFlTransition : FestraAacFlNewTransitionContinuation
    {
        private readonly AnimatorStateTransition _transition;

        public FestraAacFlTransition(AnimatorStateTransition transition, AnimatorStateMachine machine, FestraAacTransitionEndpoint sourceNullableIfAny, FestraAacTransitionEndpoint destinationNullableIfExits) : base(transition, machine, sourceNullableIfAny, destinationNullableIfExits)
        {
            _transition = transition;
        }

        public FestraAacFlTransition WithSourceInterruption()
        {
            _transition.interruptionSource = TransitionInterruptionSource.Source;
            return this;
        }

        public FestraAacFlTransition WithInterruption(TransitionInterruptionSource interruptionSource)
        {
            _transition.interruptionSource = interruptionSource;
            return this;
        }

        public FestraAacFlTransition WithTransitionDurationSeconds(float transitionDuration)
        {
            _transition.duration = transitionDuration;
            return this;
        }

        public FestraAacFlTransition WithOrderedInterruption()
        {
            _transition.orderedInterruption = true;
            return this;
        }

        public FestraAacFlTransition WithNoOrderedInterruption()
        {
            _transition.orderedInterruption = false;
            return this;
        }

        public FestraAacFlTransition WithTransitionToSelf()
        {
            _transition.canTransitionToSelf = true;
            return this;
        }

        public FestraAacFlTransition WithNoTransitionToSelf()
        {
            _transition.canTransitionToSelf = false;
            return this;
        }

        public FestraAacFlTransition AfterAnimationFinishes()
        {
            _transition.hasExitTime = true;
            _transition.exitTime = 1;

            return this;
        }

        public FestraAacFlTransition AfterAnimationIsAtLeastAtPercent(float exitTimeNormalized)
        {
            _transition.hasExitTime = true;
            _transition.exitTime = exitTimeNormalized;

            return this;
        }

        public FestraAacFlTransition WithTransitionDurationPercent(float transitionDurationNormalized)
        {
            _transition.hasFixedDuration = false;
            _transition.duration = transitionDurationNormalized;

            return this;
        }
    }

    public class FestraAacFlEntryTransition : FestraAacFlNewTransitionContinuation
    {
        public FestraAacFlEntryTransition(AnimatorTransition transition, AnimatorStateMachine machine, AnimatorState sourceNullableIfAny, FestraAacTransitionEndpoint destinationNullableIfExits) : base(transition, machine, sourceNullableIfAny, destinationNullableIfExits)
        {
        }
    }

    public interface IFestraAacFlCondition
    {
        void ApplyTo(FestraAacFlCondition appender);
    }

    public interface IFestraAacFlOrCondition
    {
        List<FestraAacFlTransitionContinuation> ApplyTo(FestraAacFlNewTransitionContinuation firstContinuation);
    }

    public class FestraAacFlCondition
    {
        private readonly AnimatorTransitionBase _transition;

        public FestraAacFlCondition(AnimatorTransitionBase transition)
        {
            _transition = transition;
        }

        public FestraAacFlCondition Add(string parameter, AnimatorConditionMode mode, float threshold)
        {
            _transition.AddCondition(mode, threshold, parameter);
            return this;
        }
    }

    public class FestraAacFlNewTransitionContinuation
    {
        public readonly AnimatorTransitionBase Transition;
        private readonly AnimatorStateMachine _machine;
        private readonly FestraAacTransitionEndpoint _sourceNullableIfAny;
        private readonly FestraAacTransitionEndpoint _destinationNullableIfExits;

        public FestraAacFlNewTransitionContinuation(AnimatorTransitionBase transition, AnimatorStateMachine machine, FestraAacTransitionEndpoint sourceNullableIfAny, FestraAacTransitionEndpoint destinationNullableIfExits)
        {
            Transition = transition;
            _machine = machine;
            _sourceNullableIfAny = sourceNullableIfAny;
            _destinationNullableIfExits = destinationNullableIfExits;
        }

        /// Adds a condition to the transition.
        ///
        /// The settings of the transition can no longer be modified after this point.
        /// <example>
        /// <code>
        /// .When(_aac.BoolParameter(my.myBoolParameterName).IsTrue())
        /// .And(_aac.BoolParameter(my.myIntParameterName).IsGreaterThan(2))
        /// .And(FestraAacAv3.ItIsLocal())
        /// .Or()
        /// .When(_aac.BoolParameters(
        ///     my.myBoolParameterName,
        ///     my.myOtherBoolParameterName
        /// ).AreTrue())
        /// .And(FestraAacAv3.ItIsRemote());
        /// </code>
        /// </example>
        public FestraAacFlTransitionContinuation When(IFestraAacFlCondition action)
        {
            action.ApplyTo(new FestraAacFlCondition(Transition));
            return AsContinuationWithOr();
        }

        /// <summary>
        /// Applies a series of conditions to this transition, but this series of conditions cannot include an Or operator.
        /// </summary>
        /// <param name="actionsWithoutOr"></param>
        /// <returns></returns>
        public FestraAacFlTransitionContinuation When(Action<FestraAacFlTransitionContinuationWithoutOr> actionsWithoutOr)
        {
            actionsWithoutOr(new FestraAacFlTransitionContinuationWithoutOr(Transition));
            return AsContinuationWithOr();
        }

        /// <summary>
        /// Applies a series of conditions, and this series may contain Or operators. However, the result can not be followed by an And operator. It can only be an Or operator.
        /// </summary>
        /// <param name="actionsWithOr"></param>
        /// <returns></returns>
        public FestraAacFlTransitionContinuationOnlyOr When(Action<FestraAacFlNewTransitionContinuation> actionsWithOr)
        {
            actionsWithOr(this);
            return AsContinuationOnlyOr();
        }

        /// <summary>
        /// Applies a series of conditions, and this series may contain Or operators. All And operators that follow will apply to all the conditions generated by this series, until the next Or operator.
        /// </summary>
        /// <param name="actionsWithOr"></param>
        /// <returns></returns>
        public FestraAacFlMultiTransitionContinuation When(IFestraAacFlOrCondition actionsWithOr)
        {
            var pendingContinuations = actionsWithOr.ApplyTo(this);
            return new FestraAacFlMultiTransitionContinuation(Transition, _machine, _sourceNullableIfAny, _destinationNullableIfExits, pendingContinuations);
        }

        public FestraAacFlTransitionContinuation WhenConditions()
        {
            return AsContinuationWithOr();
        }

        private FestraAacFlTransitionContinuation AsContinuationWithOr()
        {
            return new FestraAacFlTransitionContinuation(Transition, _machine, _sourceNullableIfAny, _destinationNullableIfExits);
        }

        private FestraAacFlTransitionContinuationOnlyOr AsContinuationOnlyOr()
        {
            return new FestraAacFlTransitionContinuationOnlyOr(Transition, _machine, _sourceNullableIfAny, _destinationNullableIfExits);
        }
    }

    public class FestraAacFlTransitionContinuation : FestraAacFlTransitionContinuationAbstractWithOr
    {
        public FestraAacFlTransitionContinuation(AnimatorTransitionBase transition, AnimatorStateMachine machine, FestraAacTransitionEndpoint sourceNullableIfAny, FestraAacTransitionEndpoint destinationNullableIfExits) : base(transition, machine, sourceNullableIfAny, destinationNullableIfExits)
        {
        }

        /// Adds an additional condition to the transition that requires all preceding conditions to be true.
        /// <example>
        /// <code>
        /// .When(_aac.BoolParameter(my.myBoolParameterName).IsTrue())
        /// .And(_aac.BoolParameter(my.myIntParameterName).IsGreaterThan(2))
        /// .And(FestraAacAv3.ItIsLocal())
        /// .Or()
        /// .When(_aac.BoolParameters(
        ///     my.myBoolParameterName,
        ///     my.myOtherBoolParameterName
        /// ).AreTrue())
        /// .And(FestraAacAv3.ItIsRemote());
        /// </code>
        /// </example>
        public FestraAacFlTransitionContinuation And(IFestraAacFlCondition action)
        {
            action.ApplyTo(new FestraAacFlCondition(Transition));
            return this;
        }

        /// <summary>
        /// Applies a series of conditions to this transition. The conditions cannot include an Or operator.
        /// </summary>
        /// <param name="actionsWithoutOr"></param>
        /// <returns></returns>
        public FestraAacFlTransitionContinuation And(Action<FestraAacFlTransitionContinuationWithoutOr> actionsWithoutOr)
        {
            actionsWithoutOr(new FestraAacFlTransitionContinuationWithoutOr(Transition));
            return this;
        }
    }

    public class FestraAacFlMultiTransitionContinuation : FestraAacFlTransitionContinuationAbstractWithOr
    {
        private readonly List<FestraAacFlTransitionContinuation> _pendingContinuations;

        public FestraAacFlMultiTransitionContinuation(AnimatorTransitionBase transition, AnimatorStateMachine machine, FestraAacTransitionEndpoint sourceNullableIfAny, FestraAacTransitionEndpoint destinationNullableIfExits, List<FestraAacFlTransitionContinuation> pendingContinuations) : base(transition, machine, sourceNullableIfAny, destinationNullableIfExits)
        {
            _pendingContinuations = pendingContinuations;
        }

        /// Adds an additional condition to these transitions that requires all preceding conditions to be true.
        /// <example>
        /// <code>
        /// .When(_aac.BoolParameter(my.myBoolParameterName).IsTrue())
        /// .And(_aac.BoolParameter(my.myIntParameterName).IsGreaterThan(2))
        /// .And(FestraAacAv3.ItIsLocal())
        /// .Or()
        /// .When(_aac.BoolParameters(
        ///     my.myBoolParameterName,
        ///     my.myOtherBoolParameterName
        /// ).AreTrue())
        /// .And(FestraAacAv3.ItIsRemote());
        /// </code>
        /// </example>
        public FestraAacFlMultiTransitionContinuation And(IFestraAacFlCondition action)
        {
            foreach (var pendingContinuation in _pendingContinuations)
            {
                pendingContinuation.And(action);
            }

            return this;
        }

        /// <summary>
        /// Applies a series of conditions to these transitions. The conditions cannot include an Or operator.
        /// </summary>
        /// <param name="actionsWithoutOr"></param>
        /// <returns></returns>
        public FestraAacFlMultiTransitionContinuation And(Action<FestraAacFlTransitionContinuationWithoutOr> actionsWithoutOr)
        {
            foreach (var pendingContinuation in _pendingContinuations)
            {
                pendingContinuation.And(actionsWithoutOr);
            }

            return this;
        }
    }

    public class FestraAacFlTransitionContinuationOnlyOr : FestraAacFlTransitionContinuationAbstractWithOr
    {
        public FestraAacFlTransitionContinuationOnlyOr(AnimatorTransitionBase transition, AnimatorStateMachine machine, FestraAacTransitionEndpoint sourceNullableIfAny, FestraAacTransitionEndpoint destinationNullableIfExits) : base(transition, machine, sourceNullableIfAny, destinationNullableIfExits)
        {
        }
    }

    public abstract class FestraAacFlTransitionContinuationAbstractWithOr
    {
        protected readonly AnimatorTransitionBase Transition;
        private readonly AnimatorStateMachine _machine;
        private readonly FestraAacTransitionEndpoint _sourceNullableIfAny;
        private readonly FestraAacTransitionEndpoint _destinationNullableIfExits;

        public FestraAacFlTransitionContinuationAbstractWithOr(AnimatorTransitionBase transition, AnimatorStateMachine machine, FestraAacTransitionEndpoint sourceNullableIfAny, FestraAacTransitionEndpoint destinationNullableIfExits)
        {
            Transition = transition;
            _machine = machine;
            _sourceNullableIfAny = sourceNullableIfAny;
            _destinationNullableIfExits = destinationNullableIfExits;
        }

        /// <summary>
        /// Creates a new transition with identical settings but having no conditions defined yet.
        /// </summary>
        /// <example>
        /// <code>
        /// .When(_aac.BoolParameter(my.myBoolParameterName).IsTrue())
        /// .And(_aac.BoolParameter(my.myIntParameterName).IsGreaterThan(2))
        /// .And(FestraAacAv3.ItIsLocal())
        /// .Or()
        /// .When(_aac.BoolParameters(
        ///     my.myBoolParameterName,
        ///     my.myOtherBoolParameterName
        /// ).AreTrue())
        /// .And(FestraAacAv3.ItIsRemote());
        /// </code>
        /// </example>
        public FestraAacFlNewTransitionContinuation Or()
        {
            return new FestraAacFlNewTransitionContinuation(NewTransitionFromTemplate(), _machine, _sourceNullableIfAny, _destinationNullableIfExits);
        }

        private AnimatorTransitionBase NewTransitionFromTemplate()
        {
            AnimatorTransitionBase newTransition;
            if (Transition is AnimatorStateTransition templateStateTransition)
            {
                var stateTransition = NewTransition();
                stateTransition.duration = templateStateTransition.duration;
                stateTransition.offset = templateStateTransition.offset;
                stateTransition.interruptionSource = templateStateTransition.interruptionSource;
                stateTransition.orderedInterruption = templateStateTransition.orderedInterruption;
                stateTransition.exitTime = templateStateTransition.exitTime;
                stateTransition.hasExitTime = templateStateTransition.hasExitTime;
                stateTransition.hasFixedDuration = templateStateTransition.hasFixedDuration;
                stateTransition.canTransitionToSelf = templateStateTransition.canTransitionToSelf;
                newTransition = stateTransition;
            }
            else
            {
                if (_sourceNullableIfAny == null)
                {
                    if (_destinationNullableIfExits.TryGetState(out var state))
                        newTransition = _machine.AddEntryTransition(state);
                    else if (_destinationNullableIfExits.TryGetStateMachine(out var stateMachine))
                        newTransition = _machine.AddEntryTransition(stateMachine);
                    else
                        throw new InvalidOperationException("_destinationNullableIfExits is not null but does not contain an AnimatorState or AnimatorStateMachine");
                }
                // source will never be a state if we're cloning an AnimatorTransition
                else if (_sourceNullableIfAny.TryGetStateMachine(out var stateMachine))
                {
                    if (_destinationNullableIfExits == null)
                        newTransition = _machine.AddStateMachineExitTransition(stateMachine);
                    else if (_destinationNullableIfExits.TryGetState(out var destinationState))
                        newTransition = _machine.AddStateMachineTransition(stateMachine, destinationState);
                    else if (_destinationNullableIfExits.TryGetStateMachine(out var destinationStateMachine))
                        newTransition = _machine.AddStateMachineTransition(stateMachine, destinationStateMachine);
                    else
                        throw new InvalidOperationException("_destinationNullableIfExits is not null but does not contain an AnimatorState or AnimatorStateMachine");

                    FestraAacV0.UndoDisable(newTransition);
                }
                else
                    throw new InvalidOperationException("_sourceNullableIfAny is not null but does not contain an AnimatorStateMachine");
            }
            return newTransition;
        }

        private AnimatorStateTransition NewTransition()
        {
            AnimatorState state;
            AnimatorStateMachine stateMachine;

            if (_sourceNullableIfAny == null)
            {
                if (_destinationNullableIfExits.TryGetState(out state))
                {
                    var transition = _machine.AddAnyStateTransition(state);
                    FestraAacV0.UndoDisable(transition);
                    return transition;
                }

                if (_destinationNullableIfExits.TryGetStateMachine(out stateMachine))
                {
                    var transition = _machine.AddAnyStateTransition(stateMachine);
                    FestraAacV0.UndoDisable(transition);
                    return transition;
                }

                throw new InvalidOperationException("Transition has no source nor destination.");
            }

            // source will never be a state machine if we're cloning an AnimatorStateTransition
            if (_sourceNullableIfAny.TryGetState(out var sourceState))
            {
                if (_destinationNullableIfExits == null)
                {
                    var transition = sourceState.AddExitTransition();
                    FestraAacV0.UndoDisable(transition);
                    return transition;
                }

                if (_destinationNullableIfExits.TryGetState(out state))
                {
                    var transition = sourceState.AddTransition(state);
                    FestraAacV0.UndoDisable(transition);
                    return transition;
                }

                if (_destinationNullableIfExits.TryGetStateMachine(out stateMachine))
                {
                    var transition = sourceState.AddTransition(stateMachine);
                    FestraAacV0.UndoDisable(transition);
                    return transition;
                }

                throw new InvalidOperationException("_destinationNullableIfExits is not null but does not contain an AnimatorState or AnimatorStateMachine");
            }
            throw new InvalidOperationException("_sourceNullableIfAny is not null but does not contain an AnimatorState");
        }
    }

    public class FestraAacFlTransitionContinuationWithoutOr
    {
        private readonly AnimatorTransitionBase _transition;

        public FestraAacFlTransitionContinuationWithoutOr(AnimatorTransitionBase transition)
        {
            _transition = transition;
        }

        public FestraAacFlTransitionContinuationWithoutOr And(IFestraAacFlCondition action)
        {
            action.ApplyTo(new FestraAacFlCondition(_transition));
            return this;
        }

        /// <summary>
        /// Applies a series of conditions to this transition. The conditions cannot include an Or operator.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public FestraAacFlTransitionContinuationWithoutOr AndWhenever(Action<FestraAacFlTransitionContinuationWithoutOr> action)
        {
            action(this);
            return this;
        }
    }

    public class FestraAacTransitionEndpoint
    {
        private readonly AnimatorState _state;
        private readonly AnimatorStateMachine _stateMachine;

        public FestraAacTransitionEndpoint(AnimatorState state)
        {
            _state = state;
        }

        public FestraAacTransitionEndpoint(AnimatorStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public static implicit operator FestraAacTransitionEndpoint(AnimatorState state)
        {
            return new FestraAacTransitionEndpoint(state);
        }

        public static implicit operator FestraAacTransitionEndpoint(AnimatorStateMachine stateMachine)
        {
            return new FestraAacTransitionEndpoint(stateMachine);
        }

        public bool TryGetState(out AnimatorState state)
        {
            state = _state;
            return _state != null;
        }

        public bool TryGetStateMachine(out AnimatorStateMachine stateMachine)
        {
            stateMachine = _stateMachine;
            return _stateMachine != null;
        }
    }
}
