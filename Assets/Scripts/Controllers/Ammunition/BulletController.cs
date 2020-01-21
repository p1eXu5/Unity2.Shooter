using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssemblyCSharp.Assets.Scripts.Controllers.Heroes;
using Shooter.Contracts;
using Shooter.Heroes;
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
            var comp = collision.gameObject.GetComponent< ISetDamage >();
            if ( comp == null ) {
                comp = collision.gameObject.GetComponentInParent< ISetDamage >();
            }

            _setDamage( comp );

            Destroy( Instance );
        }

        private void _setDamage( ISetDamage obj )
        {
            obj?.ApplyDamage( _currentDamage );
        }
    }
}
