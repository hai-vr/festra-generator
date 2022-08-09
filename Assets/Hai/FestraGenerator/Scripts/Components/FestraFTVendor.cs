using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using VRC.SDK3.Avatars.ScriptableObjects;

namespace Hai.FestraGenerator.Scripts.Components
{
    public abstract class FestraFTVendor : MonoBehaviour
    {
        public VRCExpressionParameters expressionParameters;
        public FestraDebugInfluence debugShowInfluences;

        public abstract Dictionary<string, FestraElementActuator[]> ExposeMap();

        public virtual FestraElementActuator[] ToElementActuators()
        {
            var map = ExposeMap();
            return GetType()
                .GetFields(BindingFlags.Public | BindingFlags.Instance)
                .Where(info => info.FieldType == typeof(bool))
                .Where(info => (bool)info.GetValue(this))
                .SelectMany(info => map[info.Name])
                .ToArray();
        }

        public enum FestraDebugInfluence
        {
            None, All, OnlyActive
        }
    }

    [Serializable]
    public struct FestraElementActuator
    {
        public string element;
        public FestraActuator actuator;
    }

    [Serializable]
    public struct FestraActuator
    {
        public string sensorParameterName;
        public float neutral;
        public float actuated;
    }

    // SRAnipal Naming Convention
    public enum FestraSRAnipalConvention
    {
        Jaw_Right,
        Jaw_Left,
        Jaw_Forward,
        Jaw_Open,
        Mouth_Ape_Shape,
        Mouth_Upper_Right,
        Mouth_Upper_Left,
        Mouth_Lower_Right,
        Mouth_Lower_Left,
        Mouth_Upper_Overturn,
        Mouth_Lower_Overturn,
        Mouth_Pout,
        Mouth_Smile_Right,
        Mouth_Smile_Left,
        Mouth_Sad_Right,
        Mouth_Sad_Left,
        Cheek_Puff_Right,
        Cheek_Puff_Left,
        Cheek_Suck,
        Mouth_Upper_UpRight,
        Mouth_Upper_UpLeft,
        Mouth_Lower_DownRight,
        Mouth_Lower_DownLeft,
        Mouth_Upper_Inside,
        Mouth_Lower_Inside,
        Mouth_Lower_Overlay,
        Tongue_LongStep1,
        Tongue_LongStep2,
        Tongue_Down,
        Tongue_Up,
        Tongue_Right,
        Tongue_Left,
        Tongue_Roll,
        Tongue_UpLeft_Morph,
        Tongue_UpRight_Morph,
        Tongue_DownLeft_Morph,
        Tongue_DownRight_Morph,
        Eye_Left_Blink,
        Eye_Left_Wide,
        Eye_Left_Right,
        Eye_Left_Left,
        Eye_Left_Up,
        Eye_Left_Down,
        Eye_Right_Blink,
        Eye_Right_Wide,
        Eye_Right_Right,
        Eye_Right_Left,
        Eye_Right_Up,
        Eye_Right_Down,
        Eye_Frown, // SRAnipal doesn't seem to actuate this
        // Eye_Left_Frown,
        // Eye_Right_Frown,
        Eye_Left_Squeeze,
        Eye_Right_Squeeze,
        // VRCFaceTracking additional conventions
        Eye_Left_Dilation,
        Eye_Left_Constrict,
        Eye_Right_Dilation,
        Eye_Right_Constrict,
        NOT_APPLICABLE,
        NOT_IMPLEMENTED,
        FestraElementTODO
    }
    
    internal class FestraInternalVRCFTContinuation
    {
        private readonly List<FestraElementActuator> _result;
        private readonly string _parameter;

        public FestraInternalVRCFTContinuation(string parameter)
        {
            _result = new List<FestraElementActuator>();
            _parameter = parameter;
        }

        internal FestraElementActuator[] ToArray()
        {
            return _result.ToArray();
        }
        
        internal FestraInternalVRCFTContinuation P01(FestraSRAnipalConvention element)
        {
            _result.Add(new FestraElementActuator
            {
                element = Enum.GetName(typeof(FestraSRAnipalConvention), element),
                actuator = new FestraActuator
                {
                    sensorParameterName = _parameter,
                    neutral = 0f,
                    actuated = 1f
                }
            });
            return this;
        }

        public FestraInternalVRCFTContinuation Decay(FestraSRAnipalConvention element)
        {
            _result.Add(new FestraElementActuator
            {
                element = Enum.GetName(typeof(FestraSRAnipalConvention), element),
                actuator = new FestraActuator
                {
                    sensorParameterName = _parameter,
                    neutral = 1f,
                    actuated = 0f
                }
            });
            return this;
        }
        
        internal FestraInternalVRCFTContinuation Negative(FestraSRAnipalConvention element)
        {
            _result.Add(new FestraElementActuator
            {
                element = Enum.GetName(typeof(FestraSRAnipalConvention), element),
                actuator = new FestraActuator
                {
                    sensorParameterName = _parameter,
                    neutral = 0f,
                    actuated = -1f
                }
            });
            return this;
        }

        internal FestraInternalVRCFTContinuation Joystick(FestraSRAnipalConvention negativeLeftDown, FestraSRAnipalConvention positiveUpRight)
        {
            _result.Add(new FestraElementActuator
            {
                element = Enum.GetName(typeof(FestraSRAnipalConvention), negativeLeftDown),
                actuator = new FestraActuator
                {
                    sensorParameterName = _parameter,
                    neutral = 0f,
                    actuated = -1f
                }
            });
            _result.Add(new FestraElementActuator
            {
                element = Enum.GetName(typeof(FestraSRAnipalConvention), positiveUpRight),
                actuator = new FestraActuator
                {
                    sensorParameterName = _parameter,
                    neutral = 0f,
                    actuated = 1f
                }
            });
            return this;
        }

        internal FestraInternalVRCFTContinuation Stepped(FestraSRAnipalConvention neutral, FestraSRAnipalConvention positive)
        {
            _result.Add(new FestraElementActuator
            {
                element = Enum.GetName(typeof(FestraSRAnipalConvention), neutral),
                actuator = new FestraActuator
                {
                    sensorParameterName = _parameter,
                    neutral = -1f,
                    actuated = 0f
                }
            });
            _result.Add(new FestraElementActuator
            {
                element = Enum.GetName(typeof(FestraSRAnipalConvention), positive),
                actuator = new FestraActuator
                {
                    sensorParameterName = _parameter,
                    neutral = 0f,
                    actuated = 1f
                }
            });
            return this;
        }

        public FestraInternalVRCFTContinuation Aperture(FestraSRAnipalConvention zero, FestraSRAnipalConvention one)
        {
            _result.Add(new FestraElementActuator
            {
                element = Enum.GetName(typeof(FestraSRAnipalConvention), zero),
                actuator = new FestraActuator
                {
                    sensorParameterName = _parameter,
                    neutral = 0.5f,
                    actuated = 0f
                }
            });
            _result.Add(new FestraElementActuator
            {
                element = Enum.GetName(typeof(FestraSRAnipalConvention), one),
                actuator = new FestraActuator
                {
                    sensorParameterName = _parameter,
                    neutral = 0.5f,
                    actuated = 1f
                }
            });
            return this;
        }
    }
}