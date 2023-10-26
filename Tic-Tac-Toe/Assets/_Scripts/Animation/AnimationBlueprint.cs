using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Anim", menuName = "Animation/Blueprint")]
public class AnimationBlueprint : ScriptableObject
{
    [Range(0.01f, 10f)]
    public float duration = 1;

    [Range(0f, 10f)]
    public float startDelay = 0;

    public bool loop = false;
    public bool playOnEnable = false;

    public AnimationBlueprintParamaters[] animationComponent;

    [HideInInspector]
    public bool customStartPos;
    [HideInInspector]
    public Vector3 startPos;

    [HideInInspector]
    public bool customEndPos;
    [HideInInspector]
    public Vector3 endPos;
}

#if UNITY_EDITOR
[CustomEditor(typeof(AnimationBlueprint))]
[CanEditMultipleObjects]
class AnimationBlueprintEditor : Editor
{
    SerializedProperty animationComponentArray;

    SerializedProperty customStartPos;
    SerializedProperty startPos;

    SerializedProperty customEndPos;
    SerializedProperty endPos;

    void OnEnable()
    {
        animationComponentArray = serializedObject.FindProperty("animationComponent");

        customStartPos = serializedObject.FindProperty("customStartPos");
        startPos = serializedObject.FindProperty("startPos");

        customEndPos = serializedObject.FindProperty("customEndPos");
        endPos = serializedObject.FindProperty("endPos");
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        serializedObject.Update();

        for (int i = 0; i < animationComponentArray.arraySize; i++)
        {
            SerializedProperty animationComponent = animationComponentArray.GetArrayElementAtIndex(i);
            SerializedProperty animElement = animationComponent.FindPropertyRelative("animElement");

            if (animElement.enumValueIndex == (int)Utilities.AnimElement.Position)
            {
                customStartPos.boolValue = EditorGUILayout.ToggleLeft("Custom Start Position", customStartPos.boolValue);
                if (customStartPos.boolValue)
                    startPos.vector3Value = EditorGUILayout.Vector3Field("Start Position", startPos.vector3Value);

                customEndPos.boolValue = EditorGUILayout.ToggleLeft("Custom End Position", customEndPos.boolValue);
                if (customEndPos.boolValue)
                    endPos.vector3Value = EditorGUILayout.Vector3Field("End Position", endPos.vector3Value);

                break;
            }
        }
        serializedObject.ApplyModifiedProperties();
    }
}
#endif