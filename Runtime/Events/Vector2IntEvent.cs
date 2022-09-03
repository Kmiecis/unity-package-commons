using System;
using UnityEngine;
using UnityEngine.Events;

namespace Common.Events
{
    [Serializable]
    public sealed class Vector2IntEvent : UnityEvent<Vector2Int>
    {
    }
}
