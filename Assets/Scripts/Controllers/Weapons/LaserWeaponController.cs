using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shooter.Contracts;
using Shooter.Controllers.Weapons.Messages;
using Shooter.Models.Ammunition;
using Shooter.Models.Weapons;
using UnityEngine;
// ReSharper disable ParameterHidesMember

namespace Shooter.Controllers.Weapons
{
    public class LaserWeaponController : WeaponController< LaserWeapon >
    {
        public GameObject hitParticle;

        protected override bool TryFire( Fire fire )
        {
            RaycastHit hit;
            Ray ray = new Ray( Camera.transform.position, Camera.transform.forward );

            if ( Physics.Raycast( ray, out hit )) {

                var collider = hit.collider;

                if ( collider.tag == "Player" ) {
                    return true;
                }
                else {
                    // wall
                    _trySetDamage( collider );
                    _createHitParticle( hit );
                }
            }

            return true;
        }

        private bool _trySetDamage( Collider collider )
        {
            var damageable = collider.GetComponent<ISetDamage>();

            if ( damageable != null) {
                damageable.ApplyDamage( Model.Damage );
                return true;
            }

            return  false;
        }

        private void _createHitParticle( RaycastHit hit )
        {
            GameObject particle = Instantiate( hitParticle, hit.point, Quaternion.identity );
            particle.transform.parent = hit.transform;
            Destroy( particle, 0.5f );
        }

        


    }
}
