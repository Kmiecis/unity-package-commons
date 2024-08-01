using System;
using System.Collections.Generic;
using UnityEditor;

namespace CommonEditor.Extensions
{
    public static class SerializedObjectExtensions
    {
        public static SerializedProperty FindPropertyField(this SerializedObject self, string name)
        {
            return self.FindProperty($"<{name}>k__BackingField");
        }

        public static IEnumerable<SerializedProperty> GetChildren(this SerializedObject self)
        {
            var iterator = self.GetIterator();
            if (iterator.NextVisible(true))
            {
                while (iterator.NextVisible(false))
                {
                    yield return iterator;
                }
            }
        }

        public static Type GetTargetType(this SerializedObject self)
        {
            if (self.isEditingMultipleObjects)
            {
                var single = self.targetObjects[0];
                return single.GetType();
            }
            return self.targetObject.GetType();
        }
    }
}