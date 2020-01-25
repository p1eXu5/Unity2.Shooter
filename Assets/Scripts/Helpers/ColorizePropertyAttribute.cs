using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Shooter.Helpers
{
    public class Colors
    {
        public const string DEFAULT = "default";

        private static Colors _colors;


        // ReSharper disable once ConvertToNullCoalescingCompoundAssignment
        public static Colors Instance => _colors ?? ( _colors = new Colors() );


        private readonly Dictionary< string, Color > _map
            = new Dictionary< string, Color >() {
                [Colors.DEFAULT] = new Color(0,0,0,1),
                ["running"] = new Color(0,0,1,1),
            }; 


        protected Colors()
        { }


        public Color this[ string color ]
        {
            get {
                if ( _map.TryGetValue( color, out var unityColor ) ) {
                    return unityColor;
                }

                return _map[Colors.DEFAULT];
            }
        }
    }

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
