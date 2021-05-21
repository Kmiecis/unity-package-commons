using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Common
{
    [Serializable]
    public struct Range3Int : IEquatable<Range3Int>
    {
        public Vector3Int min;
        public Vector3Int max;

        public static readonly Range3Int Zero;
        public static readonly Range3Int Full = new Range3Int(Vector3Int.one * int.MinValue, Vector3Int.one * int.MaxValue);
        public static readonly Range3Int Empty = new Range3Int(Vector3Int.one * int.MaxValue, Vector3Int.one * int.MinValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Range3Int(Vector3Int min, Vector3Int max)
        {
            this.min = min;
            this.max = max;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Range3Int(int minX, int minY, int minZ, int maxX, int maxY, int maxZ)
        {
            this.min = new Vector3Int(minX, minY, minZ);
            this.max = new Vector3Int(maxX, maxY, maxZ);
        }

        public Vector3 Center
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return Mathx.Multiply((max + min), 0.5f); }
        }

        public Vector3Int Extents
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return max - min; }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Include(int x, int y, int z)
        {
            min.x = Math.Min(min.x, x);
            min.y = Math.Min(min.y, y);
            min.z = Math.Min(min.z, z);

            max.x = Math.Max(max.x, x);
            max.y = Math.Max(max.y, y);
            max.z = Math.Max(max.z, z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Include(Vector3Int v)
        {
            Include(v.x, v.y, v.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Include(Vector3Int otherMin, Vector3Int otherMax)
        {
            min = Mathx.Min(min, otherMin);
            max = Mathx.Max(max, otherMax);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Include(Range3Int other)
        {
            Include(other.min, other.max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(int x, int y, int z)
        {
            return (
                min.x <= x &&
                min.y <= y &&
                min.z <= z &&
                x <= max.x &&
                y <= max.y &&
                z <= max.z
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(Vector3Int v)
        {
            return Contains(v.x, v.y, v.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(Vector3Int otherMin, Vector3Int otherMax)
        {
            return (
                Mathx.AreLesserOrEqual(min, otherMin) &&
                Mathx.AreLesserOrEqual(otherMax, max)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(Range3Int other)
        {
            return Contains(other.min, other.max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Overlaps(Range3Int other)
        {
            return (
                Mathx.AreLesserOrEqual(min, other.max) &&
                Mathx.AreLesserOrEqual(other.min, max)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Range3Int Intersection(Range3Int other)
        {
            return new Range3Int(
                Mathx.Max(min, other.min),
                Mathx.Min(max, other.max)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Range3Int other)
        {
            return (
                Mathx.AreEqual(min, other.min) &&
                Mathx.AreEqual(max, other.max)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
        {
            return obj is Range3Int converted && Equals(converted);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return min.GetHashCode() ^ (max.GetHashCode() << 2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return string.Format("Range3Int({0}, {1})", min, max);
        }
    }
}