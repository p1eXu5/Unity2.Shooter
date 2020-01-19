using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Shooter.Helpers
{
    public class RenamePropertyAttribute : PropertyAttribute
    {
        public RenamePropertyAttribute( string name )
        {
            Name = name;
        }

        public string Name { get; }
    }

    [CustomPropertyDrawer( typeof(RenamePropertyAttribute))]
    public class RenamePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI( Rect position, SerializedProperty property, GUIContent label )
        {
            EditorGUI.PropertyField( position, property, new GUIContent( ( attribute as RenamePropertyAttribute ).Name ) );
        }
    }

}
