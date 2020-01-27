using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Shooter.Controllers.Heroes
{
    public class WaypointPathController : ControllerBase
    {
        #region fields

        public List< Transform > nodes = new List< Transform >();

        public Vector3 currNode;
        public Vector3 prevNode;


        #endregion


        #region properties



        #endregion


        #region initialization



        #endregion


        #region activities
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            foreach ( var childrenTransform in GetChildrenTransforms() ) {
                if ( !nodes.Contains( childrenTransform ) ) {
                    nodes.Add( childrenTransform );
                }
            }

            for (int i = 0; i < nodes.Count; i++) 
            {
                currNode = nodes[i].position;

                if ( i > 0 ) {
                    prevNode = nodes[ i - 1 ].position;
                }
                else if ( i == 0 ) {
                    prevNode = nodes[nodes.Count - 1].position;
                }

                Gizmos.color = Color.red;
                Gizmos.DrawLine( prevNode, currNode );
                Gizmos.color = Color.blue;
                Gizmos.DrawCube( currNode, Vector3.one );
            }

        }

        #endregion


        #region methods



        #endregion
    }
}
