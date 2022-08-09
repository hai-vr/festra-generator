﻿using UnityEngine;

namespace Hai.ComboGesture.Scripts.Editor.Internal
{
        /**
         * A qualified animation is an animation clip with expression metadata: Are eyes blinking? What shape is the mouth?
         * A single animation file may be qualified differently in different places.
         * Usually, the animation file qualification is consistent within a single Manifest, and may vary across other Manifests.
         */
        public readonly struct CgeQualifiedAnimation
        {
            public CgeQualifiedAnimation(AnimationClip clip, Qualification qualification)
            {
                Clip = clip;
                Qualification = qualification;
            }

            public AnimationClip Clip { get; }
            public Qualification Qualification { get; }

            public bool Equals(CgeQualifiedAnimation other)
            {
                return Equals(Clip, other.Clip) && Qualification.Equals(other.Qualification);
            }

            public override bool Equals(object obj)
            {
                return obj is CgeQualifiedAnimation other && Equals(other);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return ((Clip != null ? Clip.GetHashCode() : 0) * 397) ^ Qualification.GetHashCode();
                }
            }

            public static bool operator ==(CgeQualifiedAnimation left, CgeQualifiedAnimation right)
            {
                return left.Equals(right);
            }

            public static bool operator !=(CgeQualifiedAnimation left, CgeQualifiedAnimation right)
            {
                return !left.Equals(right);
            }

            public CgeQualifiedAnimation NewInstanceWithClip(AnimationClip clip)
            {
                return new CgeQualifiedAnimation(clip, Qualification);
            }

            public override string ToString()
            {
                return $"{nameof(Clip)}: {Clip}, {nameof(Qualification)}: {Qualification}";
            }
        }

        public readonly struct Qualification
        {
            public Qualification(bool isBlinking)
            {
                IsBlinking = isBlinking;
            }

            public bool IsBlinking { get; }

            public bool Equals(Qualification other)
            {
                return IsBlinking == other.IsBlinking;
            }

            public override bool Equals(object obj)
            {
                return obj is Qualification other && Equals(other);
            }

            public override int GetHashCode()
            {
                return IsBlinking.GetHashCode();
            }

            public static bool operator ==(Qualification left, Qualification right)
            {
                return left.Equals(right);
            }

            public static bool operator !=(Qualification left, Qualification right)
            {
                return !left.Equals(right);
            }
        }

        public enum QualifiedLimitation
        {
            None, Wide
        }

}
