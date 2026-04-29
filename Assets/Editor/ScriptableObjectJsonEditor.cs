#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;


public class ScriptableObjectJsonEditor : EditorWindow
{
    private ScriptableObject targetObject;
    private string jsonText = "";
    private Vector2 scrollPosition;

    [MenuItem("Tools/ScriptableObject JSON Editor")]
    public static void ShowWindow()
    {
        var window = GetWindow<ScriptableObjectJsonEditor>();
        window.titleContent = new GUIContent("SO JSON Editor");
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Target ScriptableObject", EditorStyles.boldLabel);
        var newTarget = (ScriptableObject)EditorGUILayout.ObjectField(targetObject, typeof(ScriptableObject), false);
        if (newTarget != targetObject)
        {
            targetObject = newTarget;
            LoadJsonFromTarget();
        }

        if (targetObject == null)
        {
            GUILayout.Label("Выберите ScriptableObject для редактирования.");
            return;
        }

        GUILayout.Space(10);
        GUILayout.Label("JSON (можно редактировать)", EditorStyles.boldLabel);
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Height(position.height - 150));
        jsonText = EditorGUILayout.TextArea(jsonText, GUILayout.ExpandHeight(true));
        GUILayout.EndScrollView();

        GUILayout.Space(5);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Load from ScriptableObject"))
        {
            LoadJsonFromTarget();
        }
        if (GUILayout.Button("Apply JSON to ScriptableObject"))
        {
            ApplyJsonToTarget();
        }
        if (GUILayout.Button("Copy JSON to Clipboard"))
        {
            EditorGUIUtility.systemCopyBuffer = jsonText;
            Debug.Log("JSON скопирован в буфер обмена.");
        }
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Paste from Clipboard"))
        {
            jsonText = EditorGUIUtility.systemCopyBuffer;
        }
    }

    private void LoadJsonFromTarget()
    {
        if (targetObject == null) return;
        jsonText = JsonUtility.ToJson(targetObject, true);
        Debug.Log($"JSON загружен из {targetObject.name}");
    }

    private void ApplyJsonToTarget()
    {
        if (targetObject == null) return;
        if (string.IsNullOrEmpty(jsonText))
        {
            Debug.LogWarning("JSON пуст. Применение отменено.");
            return;
        }
        try
        {
            JsonUtility.FromJsonOverwrite(jsonText, targetObject);
            EditorUtility.SetDirty(targetObject);
            Debug.Log($"JSON успешно применён к {targetObject.name}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Ошибка применения JSON: {e.Message}\n{e.StackTrace}");
        }
    }
}
#endif