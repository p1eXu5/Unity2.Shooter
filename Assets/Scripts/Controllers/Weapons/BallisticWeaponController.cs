using Shooter.Controllers.Ammunition;
using Shooter.Controllers.Weapons.Messages;
using Shooter.Models.Weapons;
using Shooter.Views;
using UnityEngine;

namespace Shooter.Controllers.Weapons
{
    public class BallisticWeaponController : WeaponController< BallisticWeapon >
    {
        private Transform _artilleryTube;
        
        protected override void Awake()
        {
            base.Awake();

            _artilleryTube = Transform.Find( "GunT" );
        }

        
        protected override bool _Fire( Fire fire )
        {
            BulletController bullet = _getAmmunition( fire );

            if ( bullet != null ) {
                var res = Instantiate( bullet, _artilleryTube.transform.position, Transform.rotation )
                    .TryGetComponent( typeof( Rigidbody ), out var newBullet );

                if ( res ) {
                    Model.Armo--;
                    (( Rigidbody )newBullet).AddForce( _artilleryTube.forward * Model.Force );
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
