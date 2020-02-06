﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Shooter.Controllers.Weapons
{
    public class EnemyLaserWeaponController : LaserWeaponController
    {

        [SerializeField] private LayerMask _targetMask;

        protected override void Awake()
        {
            base.Awake();


        }

        protected override bool TryFire( Fire fire )
        {
            Ray ray = new Ray( transform.position, transform.forward );

            if ( Physics.Raycast( ray, out var hit, _targetMask )) {

                Debug.Log( hit.collider.name );

                _createHitParticle( hit );

                if ( hit.collider.tag == "Player" ) {

                    _trySetDamage( hit.collider );
                }
            }

            return true;
        }
    }
}
