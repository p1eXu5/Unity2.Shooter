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

namespace Shooter.Controllers.Weapons
{
    public class LaserWeaponController : WeaponController< LaserWeapon >
    {
        public ParticleSystem _particleSystem;
        public GameObject _hitParticle;
        protected override bool _Fire( Fire fire )
        {
            Model.Armo--;

            RaycastHit hit;
            Ray ray = new Ray( _MainCamera.transform.position, _MainCamera.transform.forward );

            if ( Physics.Raycast( ray, out hit )) {
                if ( hit.collider.tag == "Player" ) {
                    return true;
                }
                else {
                    // wall
                    _SetDamage( hit.collider.GetComponent<ISetDamage>() );
                    _CreateHitParticle( hit );
                }
            }

            return true;
        }

        private void _SetDamage( ISetDamage obj )
        {
            obj?.ApplyDamage( Model.Damage );
        }

        private void _CreateHitParticle( RaycastHit hit )
        {
            GameObject particle = Instantiate( _hitParticle, hit.point, Quaternion.identity );
            particle.transform.parent = hit.transform;
            Destroy( particle, 0.5f );
        }
    }
}
