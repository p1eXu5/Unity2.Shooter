using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shooter.Contracts;
using Shooter.Models.Ammunition;
using UnityEngine;

namespace Shooter.Views
{
    public class BulletController : ControllerBase< Bullet >
    {
        private float _currentDamage;
        protected override void Awake()
        {
            base.Awake();

            // 	self-destructing
            Destroy( Instance, Model.TimeToDestruct );

            _currentDamage = Model.Damage;
            Rigidbody.mass = Model.Mass;
        }

        private void OnCollisionEnter( Collision collision )
        {
            if ( collision.collider.tag == "Bullet" ) return;

            _setDamage( collision.gameObject.GetComponent< ISetDamage >() );

            Destroy( Instance );
        }

        private void _setDamage( ISetDamage obj )
        {
            obj?.ApplyDamage( _currentDamage );
        }
    }
}
