using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using static Hai.FestraGenerator.Scripts.Editor.Internal.FestraAac.FestraAacFlConditionSimple;
using static UnityEditor.Animations.AnimatorConditionMode;

namespace Hai.FestraGenerator.Scripts.Editor.Internal.FestraAac
{
    class FestraAacFlConditionSimple : IFestraAacFlCondition
    {
        private readonly Action<FestraAacFlCondition> _action;

        public FestraAacFlConditionSimple(Action<FestraAacFlCondition> action)
        {
            _action = action;
        }

        public static FestraAacFlConditionSimple Just(Action<FestraAacFlCondition> action)
        {
            return new FestraAacFlConditionSimple(action);
        }

        public static FestraAacFlConditionSimple ForEach(string[] subjects, Action<string, FestraAacFlCondition> action)
        {
            return new FestraAacFlConditionSimple(condition =>
            {
                foreach (var subject in subjects)
                {
                    action.Invoke(subject, condition);
                }
            });
        }

        public void ApplyTo(FestraAacFlCondition appender)
        {
            _action.Invoke(appender);
        }
    }

    public abstract class FestraAacFlParameter
    {
        public string Name { get; }

        protected FestraAacFlParameter(string name)
        {
            Name = name;
        }
    }

    public class FestraAacFlFloatParameter : FestraAacFlParameter
    {
        internal static FestraAacFlFloatParameter Internally(string name) => new FestraAacFlFloatParameter(name);
        protected FestraAacFlFloatParameter(string name) : base(name) { }
        public IFestraAacFlCondition IsGreaterThan(float other) => Just(condition => condition.Add(Name, Greater, other));
        public IFestraAacFlCondition IsLessThan(float other) => Just(condition => condition.Add(Name, Less, other));
    }

    public class FestraAacFlIntParameter : FestraAacFlParameter
    {
        internal static FestraAacFlIntParameter Internally(string name) => new FestraAacFlIntParameter(name);
        protected FestraAacFlIntParameter(string name) : base(name) { }
        public IFestraAacFlCondition IsGreaterThan(int other) => Just(condition => condition.Add(Name, Greater, other));
        public IFestraAacFlCondition IsLessThan(int other) => Just(condition => condition.Add(Name, Less, other));
        public IFestraAacFlCondition IsEqualTo(int other) => Just(condition => condition.Add(Name, AnimatorConditionMode.Equals, other));
        public IFestraAacFlCondition IsNotEqualTo(int other) => Just(condition => condition.Add(Name, NotEqual, other));
    }

    public class FestraAacFlEnumIntParameter<TEnum> : FestraAacFlIntParameter where TEnum : Enum
    {
        internal static FestraAacFlEnumIntParameter<TInEnum> Internally<TInEnum>(string name) where TInEnum : Enum => new FestraAacFlEnumIntParameter<TInEnum>(name);
        protected FestraAacFlEnumIntParameter(string name) : base(name)
        {
        }

        public IFestraAacFlCondition IsEqualTo(TEnum other) => IsEqualTo((int)(object)other);
        public IFestraAacFlCondition IsNotEqualTo(TEnum other) => IsNotEqualTo((int)(object)other);
    }

    public class FestraAacFlBoolParameter : FestraAacFlParameter
    {
        internal static FestraAacFlBoolParameter Internally(string name) => new FestraAacFlBoolParameter(name);
        protected FestraAacFlBoolParameter(string name) : base(name) { }
        public IFestraAacFlCondition IsTrue() => Just(condition => condition.Add(Name, If, 0));
        public IFestraAacFlCondition IsFalse() => Just(condition => condition.Add(Name, IfNot, 0));
        public IFestraAacFlCondition IsEqualTo(bool other) => Just(condition => condition.Add(Name, other ? If : IfNot, 0));
        public IFestraAacFlCondition IsNotEqualTo(bool other) => Just(condition => condition.Add(Name, other ? IfNot : If, 0));
    }

    public class FestraAacFlFloatParameterGroup
    {
        internal static FestraAacFlFloatParameterGroup Internally(params string[] names) => new FestraAacFlFloatParameterGroup(names);
        private readonly string[] _names;
        private FestraAacFlFloatParameterGroup(params string[] names) { _names = names; }
        public List<FestraAacFlBoolParameter> ToList() => _names.Select(FestraAacFlBoolParameter.Internally).ToList();

        public IFestraAacFlCondition AreGreaterThan(float other) => ForEach(_names, (name, condition) => condition.Add(name, Greater, other));
        public IFestraAacFlCondition AreLessThan(float other) => ForEach(_names, (name, condition) => condition.Add(name, Less, other));
    }

    public class FestraAacFlIntParameterGroup
    {
        internal static FestraAacFlIntParameterGroup Internally(params string[] names) => new FestraAacFlIntParameterGroup(names);
        private readonly string[] _names;
        private FestraAacFlIntParameterGroup(params string[] names) { _names = names; }
        public List<FestraAacFlBoolParameter> ToList() => _names.Select(FestraAacFlBoolParameter.Internally).ToList();

        public IFestraAacFlCondition AreGreaterThan(float other) => ForEach(_names, (name, condition) => condition.Add(name, Greater, other));
        public IFestraAacFlCondition AreLessThan(float other) => ForEach(_names, (name, condition) => condition.Add(name, Less, other));
        public IFestraAacFlCondition AreEqualTo(float other) => ForEach(_names, (name, condition) => condition.Add(name, AnimatorConditionMode.Equals, other));
        public IFestraAacFlCondition AreNotEqualTo(float other) => ForEach(_names, (name, condition) => condition.Add(name, NotEqual, other));
    }

    public class FestraAacFlBoolParameterGroup
    {
        internal static FestraAacFlBoolParameterGroup Internally(params string[] names) => new FestraAacFlBoolParameterGroup(names);
        private readonly string[] _names;
        private FestraAacFlBoolParameterGroup(params string[] names) { _names = names; }
        public List<FestraAacFlBoolParameter> ToList() => _names.Select(FestraAacFlBoolParameter.Internally).ToList();

        public IFestraAacFlCondition AreTrue() => ForEach(_names, (name, condition) => condition.Add(name, If, 0));
        public IFestraAacFlCondition AreFalse() => ForEach(_names, (name, condition) => condition.Add(name, IfNot, 0));
        public IFestraAacFlCondition AreEqualTo(bool other) => ForEach(_names, (name, condition) => condition.Add(name, other ? If : IfNot, 0));

/// is true when all of the following conditions are met:
/// <ul>
/// <li>all of the parameters in the group must be false except for the parameter defined in exceptThisMustBeTrue if it is present in the group.</li>
/// <li>the parameter defined in exceptThisMustBeTrue must be true.</li>
/// </ul>
        public IFestraAacFlCondition AreFalseExcept(FestraAacFlBoolParameter exceptThisMustBeTrue)
        {
            var group = new FestraAacFlBoolParameterGroup(exceptThisMustBeTrue.Name);
            return AreFalseExcept(group);
        }

        public IFestraAacFlCondition AreFalseExcept(params FestraAacFlBoolParameter[] exceptTheseMustBeTrue)
        {
            var group = new FestraAacFlBoolParameterGroup(exceptTheseMustBeTrue.Select(parameter => parameter.Name).ToArray());
            return AreFalseExcept(group);
        }

        public IFestraAacFlCondition AreFalseExcept(FestraAacFlBoolParameterGroup exceptTheseMustBeTrue) => Just(condition =>
        {
            foreach (var name in _names.Where(name => !exceptTheseMustBeTrue._names.Contains(name)))
            {
                condition.Add(name, IfNot, 0);
            }
            foreach (var name in exceptTheseMustBeTrue._names)
            {
                condition.Add(name, If, 0);
            }
        });

        public IFestraAacFlCondition AreTrueExcept(FestraAacFlBoolParameter exceptThisMustBeFalse)
        {
            var group = new FestraAacFlBoolParameterGroup(exceptThisMustBeFalse.Name);
            return AreTrueExcept(group);
        }

        public IFestraAacFlCondition AreTrueExcept(params FestraAacFlBoolParameter[] exceptTheseMustBeFalse)
        {
            var group = new FestraAacFlBoolParameterGroup(exceptTheseMustBeFalse.Select(parameter => parameter.Name).ToArray());
            return AreTrueExcept(group);
        }

        public IFestraAacFlCondition AreTrueExcept(FestraAacFlBoolParameterGroup exceptTheseMustBeFalse) => Just(condition =>
        {
            foreach (var name in _names.Where(name => !exceptTheseMustBeFalse._names.Contains(name)))
            {
                condition.Add(name, If, 0);
            }
            foreach (var name in exceptTheseMustBeFalse._names)
            {
                condition.Add(name, IfNot, 0);
            }
        });

        public IFestraAacFlOrCondition IsAnyTrue()
        {
            return IsAnyEqualTo(true);
        }

        public IFestraAacFlOrCondition IsAnyFalse()
        {
            return IsAnyEqualTo(false);
        }

        private IFestraAacFlOrCondition IsAnyEqualTo(bool value)
        {
            return new FestraAacFlBoolParameterIsAnyOrCondition(_names, value);
        }
    }

    internal class FestraAacFlBoolParameterIsAnyOrCondition : IFestraAacFlOrCondition
    {
        private readonly string[] _names;
        private readonly bool _value;

        public FestraAacFlBoolParameterIsAnyOrCondition(string[] names, bool value)
        {
            _names = names;
            _value = value;
        }

        public List<FestraAacFlTransitionContinuation> ApplyTo(FestraAacFlNewTransitionContinuation firstContinuation)
        {
            var pendingContinuations = new List<FestraAacFlTransitionContinuation>();

            var newContinuation = firstContinuation;
            for (var index = 0; index < _names.Length; index++)
            {
                var name = _names[index];
                var pendingContinuation = newContinuation.When(FestraAacFlBoolParameter.Internally(name).IsEqualTo(_value));
                pendingContinuations.Add(pendingContinuation);
                if (index < _names.Length - 1)
                {
                    newContinuation = pendingContinuation.Or();
                }
            }

            return pendingContinuations;
        }
    }
}
