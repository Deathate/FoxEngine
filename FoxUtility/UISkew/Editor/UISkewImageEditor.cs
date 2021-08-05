
using UnityEngine;
using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(UISkewImage), true)]
[CanEditMultipleObjects]
public class UISkewImageEditor: ImageEditor {
	SerializedProperty m_OffsetLeftTop;
	SerializedProperty m_OffsetRightTop;
	SerializedProperty m_OffsetLeftButtom;
	SerializedProperty m_OffsetRightButtom;
	GUIContent m_LTContent;
	GUIContent m_RTContent;
	GUIContent m_LBContent;
	GUIContent m_RBContent;
	protected override void OnEnable()
	{
		base.OnEnable();
		m_OffsetLeftTop = serializedObject.FindProperty("offsetLeftTop");
		m_OffsetRightTop = serializedObject.FindProperty("offsetRightTop");
		m_OffsetLeftButtom = serializedObject.FindProperty("offsetLeftButtom");
		m_OffsetRightButtom = serializedObject.FindProperty("offsetRightButtom");
		m_LTContent = new GUIContent("左上");
		m_RTContent = new GUIContent("右上");
		m_LBContent = new GUIContent("左下");
		m_RBContent = new GUIContent("右下");
	}
	public override void OnInspectorGUI(){
		base.OnInspectorGUI();
		serializedObject.Update();
		EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
		if (GUILayout.Button("重置", GUILayout.Width(40))){
			m_OffsetLeftTop.vector3Value = Vector3.zero;
		}
		EditorGUILayout.PropertyField(m_OffsetLeftTop, m_LTContent);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
		if (GUILayout.Button("重置", GUILayout.Width(40))){
			m_OffsetRightTop.vector3Value = Vector3.zero;
		}
		EditorGUILayout.PropertyField(m_OffsetRightTop, m_RTContent);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
		if (GUILayout.Button("重置", GUILayout.Width(40))){
			m_OffsetLeftButtom.vector3Value = Vector3.zero;
		}
		EditorGUILayout.PropertyField(m_OffsetLeftButtom, m_LBContent);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
		if (GUILayout.Button("重置", GUILayout.Width(40))){
			m_OffsetRightButtom.vector3Value = Vector3.zero;
		}
		EditorGUILayout.PropertyField(m_OffsetRightButtom, m_RBContent);
		EditorGUILayout.EndHorizontal();
		
		serializedObject.ApplyModifiedProperties();
	}
}