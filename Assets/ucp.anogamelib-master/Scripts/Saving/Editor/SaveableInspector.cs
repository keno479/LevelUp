using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

namespace anogamelib
{
#if UNITY_EDITOR
    [CustomEditor(typeof(Saveable), true)]
    public class SaveableInspector : Editor
    {
        private Saveable targetComponent;

        private void OnEnable()
        {
            targetComponent = target as Saveable;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var serializedObject = new SerializedObject(targetComponent);
            var property = serializedObject.FindProperty("saveableComponents");

            SerializedProperty arraySizeProp = property.FindPropertyRelative("Array.size");

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.BeginHorizontal();
            EditorGUIUtility.labelWidth = 15;
            EditorGUILayout.TextField($"Component ({arraySizeProp.intValue})", EditorStyles.boldLabel);
            EditorGUIUtility.labelWidth = 0;
            EditorGUILayout.TextField("Save Identifier", EditorStyles.boldLabel);
            EditorGUILayout.EndHorizontal();

            for (int i = 0; i < arraySizeProp.intValue; i++)
            {
                var subProperty = property.GetArrayElementAtIndex(i);
                var identifier = subProperty.FindPropertyRelative("identifier");
                var monoBehaviour = subProperty.FindPropertyRelative("monoBehaviour");

                EditorGUILayout.BeginHorizontal();
                GUI.enabled = false;
                EditorGUIUtility.labelWidth = 25;
                EditorGUILayout.PropertyField(monoBehaviour, new GUIContent());
                EditorGUIUtility.labelWidth = 0;
                GUI.enabled = true;
                EditorGUILayout.PropertyField(identifier, new GUIContent());
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndVertical();

            if (GUILayout.Button("Refresh"))
            {
                targetComponent.Refresh();
            }
        }
    }
#endif
}
