using UnityEditor;

[CustomEditor(typeof(CookieCloudAnimation))]
public class CookieCloudAnimationEditor : Editor
{
    SerializedProperty cloudTexture1;
    SerializedProperty cloudTexture2;
    SerializedProperty speed1;
    SerializedProperty speed2;

    // Start is called before the first frame update
    void OnEnable()
    {
        cloudTexture1 = serializedObject.FindProperty("cloudLayer1");
        cloudTexture2 = serializedObject.FindProperty("cloudLayer2");
        speed1 = serializedObject.FindProperty("speedLayer1");
        speed2 = serializedObject.FindProperty("speedLayer2");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        CookieCloudAnimation editorTarget = this.target as CookieCloudAnimation;
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.Space();
        EditorGUILayout.BeginVertical("box");
		EditorGUILayout.PropertyField(cloudTexture1);
		EditorGUI.indentLevel++;
		EditorGUILayout.PropertyField(speed1);
		EditorGUI.indentLevel--;         
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space();
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.PropertyField(cloudTexture2);
        EditorGUI.indentLevel++;
        EditorGUILayout.PropertyField(speed2);
        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space();
        if(EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
            editorTarget.SetShaderProperties();
        }
    }
}
