﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shooter.Contracts;
using Shooter.Controllers;
using Shooter.Heroes;
using Shooter.Models.Ammunition;
using UnityEngine;

namespace Shooter.Controllers.Ammunition
{
    public class BulletController : ControllerBase< Bullet >
    {
        private float _currentDamage;
        protected override void Awake()
        {
            base.Awake();

            // 	self-destructing
            Destroy( GameObject, Model.TimeToDestruct );

            _currentDamage = Model.Damage;
            Rigidbody.mass = Model.Mass;
        }

        private void OnCollisionEnter( Collision collision )
        {
            if ( collision.gameObject.tag == "Player" ) return;

            var comp = collision.gameObject.GetComponent< ISetDamage >();
            if ( comp == null ) {
                comp = collision.gameObject.GetComponentInParent< ISetDamage >();
            }

            if ( comp != null ) {
                _setDamage( comp );
            }

            Destroy( GameObject, 1 );
            return;
        }

        private void _setDamage( ISetDamage obj )
        {
            obj?.ApplyDamage( _currentDamage );
        }
    }
}
