using Shooter.Controllers.Ammunition;
using Shooter.Controllers.Weapons.Messages;
using Shooter.Models.Weapons;
using Shooter.Views;
using UnityEngine;

namespace Shooter.Controllers.Weapons
{
    public class BallisticWeaponController : WeaponController< BallisticWeapon >
    {

        protected override bool TryFire( Fire fire )
        {
            BulletController bullet = _getAmmunition( fire );

            if ( bullet != null ) {
                var res = Instantiate( bullet, artilleryTube.transform.position, Transform.rotation )
                    .TryGetComponent( typeof( Rigidbody ), out var newBullet );

                if ( res ) {
                    (( Rigidbody )newBullet).AddForce( artilleryTube.forward * Model.Force, ForceMode.Impulse );
                    newBullet.gameObject.name = "Bullet";

                    return true;
                }
            }

            return false;
        }

        
        private BulletController _getAmmunition( Fire fire)
        {
            var ind = (int)fire;
            if ( Model.ammunition == null || Model.ammunition.Length == 0 || ind < 0 || ind >= Model.ammunition.Length ) {
                return null;
            }
            
            return Model.ammunition[ind];
        }
    }   
}
