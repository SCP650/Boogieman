using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer((typeof(PolarAttribute)))]
public class PolarPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var polar_attrib = attribute as PolarAttribute;
        
        
        //EditorGUI.DrawRect(new Rect(position.x + position.width * .5f,position.y ,100,100),Color.green);
        EditorGUI.FloatField(new Rect(position.x, position.y, 30, position.height), 1f);
        EditorGUI.FloatField(new Rect(position.x, position.y + 35, 50, position.height), 0);

    }

    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        return base.CreatePropertyGUI(property);
    }
}
