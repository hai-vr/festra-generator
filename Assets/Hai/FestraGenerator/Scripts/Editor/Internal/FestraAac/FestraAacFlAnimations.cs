using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Hai.FestraGenerator.Scripts.Editor.Internal.FestraAac
{
    public readonly struct FestraAacFlClip
    {
        private readonly FestraAacConfiguration _component;
        public AnimationClip Clip { get; }

        public FestraAacFlClip(FestraAacConfiguration component, AnimationClip clip)
        {
            _component = component;
            Clip = clip;
        }

        public FestraAacFlClip Looping()
        {
            var settings = AnimationUtility.GetAnimationClipSettings(Clip);
            settings.loopTime = true;
            AnimationUtility.SetAnimationClipSettings(Clip, settings);

            return this;
        }

        public FestraAacFlClip NonLooping()
        {
            var settings = AnimationUtility.GetAnimationClipSettings(Clip);
            settings.loopTime = false;
            AnimationUtility.SetAnimationClipSettings(Clip, settings);

            return this;
        }

        public FestraAacFlClip Animating(Action<FestraAacFlEditClip> action)
        {
            action.Invoke(new FestraAacFlEditClip(_component, Clip));
            return this;
        }

        public FestraAacFlClip Toggling(GameObject[] gameObjectsWithNulls, bool value)
        {
            var defensiveObjects = gameObjectsWithNulls.Where(o => o != null); // Allow users to remove an item in the middle of the array
            foreach (var component in defensiveObjects)
            {
                var binding = FestraAacV0.Binding(_component, typeof(GameObject), component.transform, "m_IsActive");

                AnimationUtility.SetEditorCurve(Clip, binding, FestraAacV0.OneFrame(value ? 1f : 0f));
            }

            return this;
        }

        public FestraAacFlClip BlendShape(SkinnedMeshRenderer renderer, string blendShapeName, float value)
        {
            var binding = FestraAacV0.Binding(_component, typeof(SkinnedMeshRenderer), renderer.transform, $"blendShape.{blendShapeName}");

            AnimationUtility.SetEditorCurve(Clip, binding, FestraAacV0.OneFrame(value));

            return this;
        }

        public FestraAacFlClip BlendShape(SkinnedMeshRenderer[] rendererWithNulls, string blendShapeName, float value)
        {
            var defensiveObjects = rendererWithNulls.Where(o => o != null); // Allow users to remove an item in the middle of the array
            foreach (var component in defensiveObjects)
            {
                var binding = FestraAacV0.Binding(_component, typeof(SkinnedMeshRenderer), component.transform, $"blendShape.{blendShapeName}");

                AnimationUtility.SetEditorCurve(Clip, binding, FestraAacV0.OneFrame(value));
            }

            return this;
        }

        public FestraAacFlClip Scaling(GameObject[] gameObjectsWithNulls, Vector3 scale)
        {
            var defensiveObjects = gameObjectsWithNulls.Where(o => o != null); // Allow users to remove an item in the middle of the array
            foreach (var component in defensiveObjects)
            {
                AnimationUtility.SetEditorCurve(Clip, FestraAacV0.Binding(_component, typeof(Transform), component.transform, "m_LocalScale.x"), FestraAacV0.OneFrame(scale.x));
                AnimationUtility.SetEditorCurve(Clip, FestraAacV0.Binding(_component, typeof(Transform), component.transform, "m_LocalScale.y"), FestraAacV0.OneFrame(scale.y));
                AnimationUtility.SetEditorCurve(Clip, FestraAacV0.Binding(_component, typeof(Transform), component.transform, "m_LocalScale.z"), FestraAacV0.OneFrame(scale.z));
            }

            return this;
        }

        public FestraAacFlClip Toggling(GameObject gameObject, bool value)
        {
            var binding = FestraAacV0.Binding(_component, typeof(GameObject), gameObject.transform, "m_IsActive");

            AnimationUtility.SetEditorCurve(Clip, binding, FestraAacV0.OneFrame(value ? 1f : 0f));

            return this;
        }

        public FestraAacFlClip TogglingComponent(Component[] componentsWithNulls, bool value)
        {
            var defensiveComponents = componentsWithNulls.Where(o => o != null); // Allow users to remove an item in the middle of the array
            foreach (var component in defensiveComponents)
            {
                var binding = FestraAacV0.Binding(_component, component.GetType(), component.transform, "m_Enabled");

                AnimationUtility.SetEditorCurve(Clip, binding, FestraAacV0.OneFrame(value ? 1f : 0f));
            }

            return this;
        }

        public FestraAacFlClip TogglingComponent(Component component, bool value)
        {
            var binding = FestraAacV0.Binding(_component, component.GetType(), component.transform, "m_Enabled");

            AnimationUtility.SetEditorCurve(Clip, binding, FestraAacV0.OneFrame(value ? 1f : 0f));

            return this;
        }

        public FestraAacFlClip SwappingMaterial(Renderer renderer, int slot, Material material)
        {
            var binding = FestraAacV0.Binding(_component, renderer.GetType(), renderer.transform, $"m_Materials.Array.data[{slot}]");

            AnimationUtility.SetObjectReferenceCurve(Clip, binding, new[] {
                new ObjectReferenceKeyframe { time = 0f, value = material },
                new ObjectReferenceKeyframe { time = 1/60f, value = material }
            });

            return this;
        }

        public FestraAacFlClip SwappingMaterial(ParticleSystem particleSystem, int slot, Material material)
        {
            var binding = FestraAacV0.Binding(_component, typeof(ParticleSystemRenderer), particleSystem.transform, $"m_Materials.Array.data[{slot}]");

            AnimationUtility.SetObjectReferenceCurve(Clip, binding, new[] {
                new ObjectReferenceKeyframe { time = 0f, value = material },
                new ObjectReferenceKeyframe { time = 1/60f, value = material }
            });

            return this;
        }
    }

    public readonly struct FestraAacFlEditClip
    {
        private readonly FestraAacConfiguration _component;
        public AnimationClip Clip { get; }

        public FestraAacFlEditClip(FestraAacConfiguration component, AnimationClip clip)
        {
            _component = component;
            Clip = clip;
        }

        public FestraAacFlSettingCurve Animates(string path, Type type, string propertyName)
        {
            var binding = new EditorCurveBinding
            {
                path = path,
                type = type,
                propertyName = propertyName
            };
            return new FestraAacFlSettingCurve(Clip, new[] {binding});
        }

        public FestraAacFlSettingCurve Animates(Transform transform, Type type, string propertyName)
        {
            var binding = FestraAacV0.Binding(_component, type, transform, propertyName);

            return new FestraAacFlSettingCurve(Clip, new[] {binding});
        }

        public FestraAacFlSettingCurve Animates(GameObject gameObject)
        {
            var binding = FestraAacV0.Binding(_component, typeof(GameObject), gameObject.transform, "m_IsActive");

            return new FestraAacFlSettingCurve(Clip, new[] {binding});
        }

        public FestraAacFlSettingCurve Animates(Component anyComponent, string property)
        {
            var binding = Internal_BindingFromComponent(anyComponent, property);

            return new FestraAacFlSettingCurve(Clip, new[] {binding});
        }

        public FestraAacFlSettingCurve Animates(Component[] anyComponents, string property)
        {
            var that = this;
            var bindings = anyComponents
                .Select(anyComponent => that.Internal_BindingFromComponent(anyComponent, property))
                .ToArray();

            return new FestraAacFlSettingCurve(Clip, bindings);
        }

        public FestraAacFlSettingCurve AnimatesAnimator(FestraAacFlParameter floatParameter)
        {
            var binding = new EditorCurveBinding
            {
                path = "",
                type = typeof(Animator),
                propertyName = floatParameter.Name
            };
            return new FestraAacFlSettingCurve(Clip, new[] {binding});
        }

        public FestraAacFlSettingCurveColor AnimatesColor(Component anyComponent, string property)
        {
            var binding = Internal_BindingFromComponent(anyComponent, property);
            return new FestraAacFlSettingCurveColor(Clip, new[] {binding});
        }

        public FestraAacFlSettingCurveColor AnimatesColor(Component[] anyComponents, string property)
        {
            var that = this;
            var bindings = anyComponents
                .Select(anyComponent => that.Internal_BindingFromComponent(anyComponent, property))
                .ToArray();

            return new FestraAacFlSettingCurveColor(Clip, bindings);
        }

        public EditorCurveBinding BindingFromComponent(Component anyComponent, string propertyName)
        {
            return Internal_BindingFromComponent(anyComponent, propertyName);
        }

        private EditorCurveBinding Internal_BindingFromComponent(Component anyComponent, string propertyName)
        {
            return FestraAacV0.Binding(_component, anyComponent.GetType(), anyComponent.transform, propertyName);
        }
    }

    public class FestraAacFlSettingCurve
    {
        private readonly AnimationClip _clip;
        private readonly EditorCurveBinding[] _bindings;

        public FestraAacFlSettingCurve(AnimationClip clip, EditorCurveBinding[] bindings)
        {
            _clip = clip;
            _bindings = bindings;
        }

        public void WithOneFrame(float desiredValue)
        {
            foreach (var binding in _bindings)
            {
                AnimationUtility.SetEditorCurve(_clip, binding, FestraAacV0.OneFrame(desiredValue));
            }
        }

        public void WithFixedSeconds(float seconds, float desiredValue)
        {
            foreach (var binding in _bindings)
            {
                AnimationUtility.SetEditorCurve(_clip, binding, FestraAacV0.ConstantSeconds(seconds, desiredValue));
            }
        }

        public void WithSecondsUnit(Action<FestraAacFlSettingKeyframes> action)
        {
            InternalWithUnit(FestraAacFlUnit.Seconds, action);
        }

        public void WithFrameCountUnit(Action<FestraAacFlSettingKeyframes> action)
        {
            InternalWithUnit(FestraAacFlUnit.Frames, action);
        }

        public void WithUnit(FestraAacFlUnit unit, Action<FestraAacFlSettingKeyframes> action)
        {
            InternalWithUnit(unit, action);
        }

        private void InternalWithUnit(FestraAacFlUnit unit, Action<FestraAacFlSettingKeyframes> action)
        {
            var mutatedKeyframes = new List<Keyframe>();
            var builder = new FestraAacFlSettingKeyframes(unit, mutatedKeyframes);
            action.Invoke(builder);

            foreach (var binding in _bindings)
            {
                AnimationUtility.SetEditorCurve(_clip, binding, new AnimationCurve(mutatedKeyframes.ToArray()));
            }
        }

        public void WithAnimationCurve(AnimationCurve animationCurve)
        {
            foreach (var binding in _bindings)
            {
                AnimationUtility.SetEditorCurve(_clip, binding, animationCurve);
            }
        }
    }

    public class FestraAacFlSettingCurveColor
    {
        private readonly AnimationClip _clip;
        private readonly EditorCurveBinding[] _bindings;

        public FestraAacFlSettingCurveColor(AnimationClip clip, EditorCurveBinding[] bindings)
        {
            _clip = clip;
            _bindings = bindings;
        }

        public void WithOneFrame(Color desiredValue)
        {
            foreach (var binding in _bindings)
            {
                AnimationUtility.SetEditorCurve(_clip, FestraAacV0.ToSubBinding(binding, "r"), FestraAacV0.OneFrame(desiredValue.r));
                AnimationUtility.SetEditorCurve(_clip, FestraAacV0.ToSubBinding(binding, "g"), FestraAacV0.OneFrame(desiredValue.g));
                AnimationUtility.SetEditorCurve(_clip, FestraAacV0.ToSubBinding(binding, "b"), FestraAacV0.OneFrame(desiredValue.b));
                AnimationUtility.SetEditorCurve(_clip, FestraAacV0.ToSubBinding(binding, "a"), FestraAacV0.OneFrame(desiredValue.a));
            }
        }

        public void WithKeyframes(FestraAacFlUnit unit, Action<FestraAacFlSettingKeyframesColor> action)
        {
            var mutatedKeyframesR = new List<Keyframe>();
            var mutatedKeyframesG = new List<Keyframe>();
            var mutatedKeyframesB = new List<Keyframe>();
            var mutatedKeyframesA = new List<Keyframe>();
            var builder = new FestraAacFlSettingKeyframesColor(unit, mutatedKeyframesR, mutatedKeyframesG, mutatedKeyframesB, mutatedKeyframesA);
            action.Invoke(builder);

            foreach (var binding in _bindings)
            {
                AnimationUtility.SetEditorCurve(_clip, FestraAacV0.ToSubBinding(binding, "r"), new AnimationCurve(mutatedKeyframesR.ToArray()));
                AnimationUtility.SetEditorCurve(_clip, FestraAacV0.ToSubBinding(binding, "g"), new AnimationCurve(mutatedKeyframesG.ToArray()));
                AnimationUtility.SetEditorCurve(_clip, FestraAacV0.ToSubBinding(binding, "b"), new AnimationCurve(mutatedKeyframesB.ToArray()));
                AnimationUtility.SetEditorCurve(_clip, FestraAacV0.ToSubBinding(binding, "a"), new AnimationCurve(mutatedKeyframesA.ToArray()));
            }
        }
    }

    public class FestraAacFlSettingKeyframes
    {
        private readonly FestraAacFlUnit _unit;
        private readonly List<Keyframe> _mutatedKeyframes;

        public FestraAacFlSettingKeyframes(FestraAacFlUnit unit, List<Keyframe> mutatedKeyframes)
        {
            _unit = unit;
            _mutatedKeyframes = mutatedKeyframes;
        }

        public FestraAacFlSettingKeyframes Easing(float timeInUnit, float value)
        {
            _mutatedKeyframes.Add(new Keyframe(AsSeconds(timeInUnit), value, 0, 0));

            return this;
        }

        public FestraAacFlSettingKeyframes Constant(float timeInUnit, float value)
        {
            _mutatedKeyframes.Add(new Keyframe(AsSeconds(timeInUnit), value, 0, float.PositiveInfinity));

            return this;
        }

        public FestraAacFlSettingKeyframes Linear(float timeInUnit, float value)
        {
            float valueEnd = value;
            float valueStart = _mutatedKeyframes.Count == 0 ? value : _mutatedKeyframes.Last().value;
            float timeEnd = AsSeconds(timeInUnit);
            float timeStart = _mutatedKeyframes.Count == 0 ? value : _mutatedKeyframes.Last().time;
            float num = (float) (((double) valueEnd - (double) valueStart) / ((double) timeEnd - (double) timeStart));
            // FIXME: This can cause NaN tangents which messes everything

            // return new AnimationCurve(new Keyframe[2]
            // {
                // new Keyframe(timeStart, valueStart, 0.0f, num),
                // new Keyframe(timeEnd, valueEnd, num, 0.0f)
            // });

            if (_mutatedKeyframes.Count > 0)
            {
                var lastKeyframe = _mutatedKeyframes.Last();
                lastKeyframe.outTangent = num;
                _mutatedKeyframes[_mutatedKeyframes.Count - 1] = lastKeyframe;
            }
            _mutatedKeyframes.Add(new Keyframe(AsSeconds(timeInUnit), value, num, 0.0f));

            return this;
        }

        private float AsSeconds(float timeInUnit)
        {
            switch (_unit)
            {
                case FestraAacFlUnit.Frames:
                    return timeInUnit / 60f;
                case FestraAacFlUnit.Seconds:
                    return timeInUnit;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public class FestraAacFlSettingKeyframesColor
    {
        private FestraAacFlSettingKeyframes _r;
        private FestraAacFlSettingKeyframes _g;
        private FestraAacFlSettingKeyframes _b;
        private FestraAacFlSettingKeyframes _a;

        public FestraAacFlSettingKeyframesColor(FestraAacFlUnit unit, List<Keyframe> mutatedKeyframesR, List<Keyframe> mutatedKeyframesG, List<Keyframe> mutatedKeyframesB, List<Keyframe> mutatedKeyframesA)
        {
            _r = new FestraAacFlSettingKeyframes(unit, mutatedKeyframesR);
            _g = new FestraAacFlSettingKeyframes(unit, mutatedKeyframesG);
            _b = new FestraAacFlSettingKeyframes(unit, mutatedKeyframesB);
            _a = new FestraAacFlSettingKeyframes(unit, mutatedKeyframesA);
        }

        public FestraAacFlSettingKeyframesColor Easing(int frame, Color value)
        {
            _r.Easing(frame, value.r);
            _g.Easing(frame, value.g);
            _b.Easing(frame, value.b);
            _a.Easing(frame, value.a);

            return this;
        }

        public FestraAacFlSettingKeyframesColor Linear(float frame, Color value)
        {
            _r.Linear(frame, value.r);
            _g.Linear(frame, value.g);
            _b.Linear(frame, value.b);
            _a.Linear(frame, value.a);

            return this;
        }

        public FestraAacFlSettingKeyframesColor Constant(int frame, Color value)
        {
            _r.Constant(frame, value.r);
            _g.Constant(frame, value.g);
            _b.Constant(frame, value.b);
            _a.Constant(frame, value.a);

            return this;
        }
    }

    public enum FestraAacFlUnit
    {
        Frames, Seconds
    }
}
