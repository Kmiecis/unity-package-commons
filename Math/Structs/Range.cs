using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Common.Mathematics
{
    [Serializable]
    public struct Range : IEquatable<Range>
    {
        public float min;
        public float max;

        public static readonly Range Zero;
        public static readonly Range One = new Range(0.0f, 1.0f);
        public static readonly Range Max = new Range(float.MinValue, float.MaxValue);
        public static readonly Range Empty = new Range(float.MaxValue, float.MinValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Range(float min, float max)
        {
            this.min = min;
            this.max = max;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator RangeInt(Range r)
        {
            return new RangeInt(
                Mathf.RoundToInt(r.min),
                Mathf.RoundToInt(r.max)
            );
        }

        public float Center
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return (max + min) * 0.5f; }
        }

        public float Length
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return max - min; }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Include(float f)
        {
            min = Math.Min(min, f);
            max = Math.Max(max, f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Include(float otherMin, float otherMax)
        {
            min = Math.Min(min, otherMin);
            max = Math.Max(max, otherMax);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Include(Range other)
        {
            Include(other.min, other.max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(float f)
        {
            return min <= f && f <= max;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(float otherMin, float otherMax)
        {
            return min <= otherMin && otherMax <= max;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(Range other)
        {
            return Contains(other.min, other.max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Overlaps(Range other)
        {
            return min <= other.max && other.min <= max;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Range Intersection(Range other)
        {
            return new Range(
                Math.Max(min, other.min),
                Math.Min(max, other.max)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Range other)
        {
            return (
                Mathx.IsEqual(min, other.min) &&
                Mathx.IsEqual(max, other.max)
            );
        }

        public override bool Equals(object obj)
        {
            return obj is Range && Equals((Range)obj);
        }

        public override int GetHashCode()
        {
            return (
                min.GetHashCode() ^
                (max.GetHashCode() << 2)
            );
        }

        public override string ToString()
        {
            return ToString("F2");
        }

        public string ToString(string format)
        {
            return string.Format("({0}, {1})", min.ToString(format), max.ToString(format));
        }
    }
}
