using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
/*
[CustomPropertyDrawer(typeof(AttackOption))]
public class AttackOptionDrawer : PropertyDrawer
{
   public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        
     /*   
        EditorGUI.DrawRect(new Rect(position.x + position.width * .5f,position.y ,100,100),Color.green);
        EditorGUI.FloatField(new Rect(position.x, position.y, 30, position.height), 1f);
        EditorGUI.FloatField(new Rect(position.x + 100, position.y + 100, 50, position.height), 0);
   
     EditorGUI.BeginProperty(position, label, property);
     
     Rect typeRect = new Rect(position.x,position.y,position.width,15);

     EditorGUI.PropertyField(typeRect, property.FindPropertyRelative("attackType"));

     Rect[][] GridButtons =  new Rect[3][];
     for(int i = 0; i < )
     
     
     
     EditorGUI.EndProperty();

    }

    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        var container = new VisualElement();
        container.Add(new PropertyField(property.FindPropertyRelative("attackType")));
        return container;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 1000;
    }
}
*/