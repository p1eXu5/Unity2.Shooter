using Shooter.Models.Weapons;
using Shooter.Views;
using UnityEngine;

namespace Shooter.Controllers
{
    public class WeaponController : ControllerBase< WeaponView, Weapon >
    {
        private float _delay;
        private Timer _rechargeTimer;
        
        private Transform _gunElement;
        
        void Start()
        {
            _delay = Model.RechargeTime;
            _rechargeTimer = new Timer( Model.RechargeTime );
            _gunElement = View.Transform.Find( "GunT" );
        }
        
        public bool CanFire() => Model.Armo > 0 && _rechargeTimer.IsStopped;
        
        public void Fire( Fire fire = Shooter.Fire.PrimaryFire )
        {
            if (!CanFire() ) return;

            BulletController bullet = _getAmmunition( fire );

            if ( bullet != null ) 
            {
                var newBullet = Instantiate( _getAmmunition(fire), _gunElement.transform.position, View.Transform.rotation );
            
                if ( newBullet != null ) {

                    Model.Armo--;
                    newBullet.Rigidbody.AddForce( _gunElement.forward * Model.Force );
                    newBullet.Name = "Bullet";

                    _rechargeTimer.Restart();
                }
            }
        }

        void Update()
        {
            if ( _rechargeTimer.IsStopped ) return;
            _rechargeTimer.Update();
        }


        private BulletController _getAmmunition( Fire fire)
        {
            var ind = (int)fire;
            if ( Model.ammunition == null || Model.ammunition.Length == 0 || ind >= Model.ammunition.Length ) {
                return null;
            }
            
            return Model.ammunition[ind];
        }
    }   
}
