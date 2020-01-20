using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shooter.Contracts;
using UnityEngine;

namespace Shooter.Heroes
{
    public class Box : BaseObject, ISetDamage
    {
        [SerializeField] private float _hp = 100;
        private bool _isDead = false;
        private float _step = 2f;


        public void Update()
        {
            if ( _isDead ) {
                Color color = Material.color;

                if ( color.a > 0 ) {
                    color.a -= _step / 100;
                    Color = color;
                }

                if (color.a < 1) {
                    Destroy( Instance.GetComponent< Collider >() );
                    Destroy( Instance, 5f );
                }
            }
        }



        #region ISetDamage implementation

        public void ApplyDamage( float damage )
        {
            if ( _hp > 0 )
            {
                _hp -= damage;
            }

            if ( _hp <= 0 ) {
                _hp = 0;
                Color = Color.red;
                _isDead = true;
            }
        }

        #endregion
    }
}
