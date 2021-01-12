using System.Collections;
using System.Collections.Generic;
using RC3.GameOfLifeStack;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(LuRule2))]
public class TabInstEditor : Editor
{
    private LuRule2 _rule;
    private SerializedObject _soRule;

    // take inst from Rules
    private SerializedProperty instSetMO1;
    private SerializedProperty instSetMO2;
    private SerializedProperty instSetMO3;

    
   private void OnEnable()
    {
        _rule = (LuRule2) target;
        _soRule = new SerializedObject(target);

        instSetMO1 = _soRule.FindProperty("instSetMO1");
        instSetMO2 = _soRule.FindProperty("instSetMO2");
        instSetMO3 = _soRule.FindProperty("instSetMO3");
        }

    public override void OnInspectorGUI()
    {
        _rule.instTab = GUILayout.Toolbar(_rule.instTab, new string[]{ "Inst01", "Inst02", "Inst03" });

        EditorGUILayout.PropertyField(instSetMO1);
        EditorGUILayout.PropertyField(instSetMO2);
        EditorGUILayout.PropertyField(instSetMO3);
        
    }
}
