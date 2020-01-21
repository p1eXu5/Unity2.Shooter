using Shooter.Models.Weapons;
using Shooter.Views;
using UnityEngine;

namespace Shooter.Controllers
{
    public class WeaponController : ControllerBase< Weapon >
    {
        private float _delay;
        private Timer _rechargeTimer;
        
        private Transform _gunElement;
        
        protected override void Awake()
        {
            base.Awake();

            _rechargeTimer = new Timer( Model.RechargeTime );
            _delay = Model.RechargeTime;
            _gunElement = Transform.Find( "GunT" );
        }

        void Start()
        {
        }
        
        public bool CanFire() => Model.Armo > 0 && _rechargeTimer.IsStopped;
        
        public void Fire( Fire fire = Shooter.Fire.PrimaryFire )
        {
            if (!CanFire() ) return;

            BulletController bullet = _getAmmunition( fire );

            if ( bullet != null ) 
            {
                var res = Instantiate( bullet, _gunElement.transform.position, Transform.rotation ).TryGetComponent(typeof(Rigidbody), out var newBullet);
            
                if ( res ) {

                    Model.Armo--;
                    ((Rigidbody)newBullet).AddForce( _gunElement.forward * Model.Force );
                    newBullet.gameObject.name = "Bullet";

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
            if ( Model.ammunition == null || Model.ammunition.Length == 0 || ind < 0 || ind >= Model.ammunition.Length ) {
                return null;
            }
            
            return Model.ammunition[ind];
        }
    }   
}
