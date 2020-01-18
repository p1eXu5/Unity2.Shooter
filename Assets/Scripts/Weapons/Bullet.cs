using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Shooter.Contracts;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Shooter.Weapons
{
    public class Bullet : Ammunition
    {
        [SerializeField] private float _timeToDestruct = 10;
        [SerializeField] private float _damage = 20;
        [SerializeField] private float _mass = 0.01f;

        private float _currentDamage;

        protected override void Awake()
        {
            base.Awake();

            // 	self-destructing
            Destroy( Instance, _timeToDestruct );

            _currentDamage = _damage;
            Rigidbody.mass = _mass;
        }

        private void OnCollisionEnter( Collision collision )
        {
            if ( collision.collider.tag == "Bullet" ) return;

            _setDamage( collision.gameObject.GetComponent<ISetDamage>() );

            Destroy( Instance );
        }

        private void _setDamage( ISetDamage obj )
        {
            obj?.ApplyDamage( _currentDamage );
        }
    }
}
