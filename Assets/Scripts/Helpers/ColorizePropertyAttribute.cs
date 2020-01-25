using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Shooter.Helpers
{
    public class ColorizePropertyAttribute : PropertyAttribute
    {
        public ColorizePropertyAttribute( string color )
        {
            Color = color;
        }


        public string Color { get; }
    }

    [CustomPropertyDrawer(typeof( ColorizePropertyAttribute ))]
    public class ColorizePropertyDrawer : PropertyDrawer
    {
        ColorizePropertyAttribute attr { get { return ((ColorizePropertyAttribute)attribute); } }
         public float GetHeight() { return 0; }
     
         public void OnGUI(Rect position)
         {
             GUI.backgroundColor = Colors.Instance[attr.Color];
         }
        //public override void OnGUI( Rect position, SerializedProperty property, GUIContent label )
        //{
        //    base.OnGUI( position, property, label );
        //}
    }
}
