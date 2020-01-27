using System.Collections.Generic;
using UnityEngine;

namespace Shooter
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

        public static Color Default => Instance[ DEFAULT ];
    }
}