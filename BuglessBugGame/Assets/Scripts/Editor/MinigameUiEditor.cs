using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MiniGameUI))]
public class MiniGameUiEditor : Editor
{
    private SerializedProperty miniGameType;
    private SerializedProperty catchType;

    private SerializedProperty tryCountTxt;
    private SerializedProperty insectIconRect;
    private SerializedProperty lineDrawer;
    private SerializedProperty drawingArea;
    private SerializedProperty canvas;
    private SerializedProperty loseInsectGo;

    private SerializedProperty hideSpot;

    private SerializedProperty swipeHitRadius;

    private SerializedProperty circleClosureThreshold;
    private SerializedProperty minCirclePoints;

    private void OnEnable()
    {
        miniGameType = serializedObject.FindProperty("miniGameType");
        catchType = serializedObject.FindProperty("catchType");

        tryCountTxt = serializedObject.FindProperty("tryCountTxt");
        insectIconRect = serializedObject.FindProperty("insectIconRect");
        lineDrawer = serializedObject.FindProperty("lineDrawer");
        drawingArea = serializedObject.FindProperty("drawingArea");
        canvas = serializedObject.FindProperty("canvas");
        loseInsectGo = serializedObject.FindProperty("loseInsectGo");

        hideSpot = serializedObject.FindProperty("hideSpot");

        swipeHitRadius = serializedObject.FindProperty("swipeHitRadius");

        circleClosureThreshold = serializedObject.FindProperty("circleClosureThreshold");
        minCirclePoints = serializedObject.FindProperty("minCirclePoints");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("Insect Type", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(miniGameType, new GUIContent("Mini-Game Type"));
        EditorGUILayout.PropertyField(catchType, new GUIContent("Catch Type"));

        EditorGUILayout.Space(6);
        EditorGUILayout.LabelField("References", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(tryCountTxt);
        EditorGUILayout.PropertyField(insectIconRect);
        EditorGUILayout.PropertyField(lineDrawer);
        EditorGUILayout.PropertyField(drawingArea);
        EditorGUILayout.PropertyField(canvas);
        EditorGUILayout.PropertyField(loseInsectGo);

        DrawMiniGameTypeFields();
        DrawCatchTypeFields();

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawMiniGameTypeFields()
    {
        if (IsEnum(miniGameType, nameof(InsectData.TypeMiniGame.Hider)))
        {
            EditorGUILayout.Space(6);
            EditorGUILayout.LabelField("Hiding Spots (Hider)", EditorStyles.boldLabel);
            EditorGUILayout.HelpBox(
                "Areas where the insect can hide. While it is inside one, the player cannot catch it.",
                MessageType.None);
            EditorGUILayout.PropertyField(hideSpot, true);

            if (hideSpot.arraySize == 0)
                EditorGUILayout.HelpBox("No hiding spot assigned: the insect will never hide.", MessageType.Warning);
        }
        else if (IsEnum(miniGameType, nameof(InsectData.TypeMiniGame.Line)))
        {
            EditorGUILayout.Space(6);
            EditorGUILayout.HelpBox(
                "Line type: the insect follows the line drawn by the MiniGameEditor. No hiding spots to configure.",
                MessageType.Info);
        }
    }

    private void DrawCatchTypeFields()
    {
        if (IsEnum(catchType, nameof(InsectData.TypeCatch.Swipe)))
        {
            EditorGUILayout.Space(6);
            EditorGUILayout.LabelField("Swipe Settings", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(swipeHitRadius);
        }
        else if (IsEnum(catchType, nameof(InsectData.TypeCatch.DrawCircle)))
        {
            EditorGUILayout.Space(6);
            EditorGUILayout.LabelField("Circle Settings", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(circleClosureThreshold);
            EditorGUILayout.PropertyField(minCirclePoints);
        }
    }

    private static bool IsEnum(SerializedProperty property, string valueName)
    {
        int index = property.enumValueIndex;
        return index >= 0 && index < property.enumNames.Length && property.enumNames[index] == valueName;
    }
}