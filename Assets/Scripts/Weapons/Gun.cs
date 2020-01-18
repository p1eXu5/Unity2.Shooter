using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Shooter.Weapons
{
    public class Gun : Weapon
    {
        [SerializeField] private int _bulletCount = 30;
        [SerializeField] private float _shootDistance = 1000f;
        [SerializeField] int _damage = 20;

        public  KeyCode Reload = KeyCode.R;

        public override void Fire( Ammunition ammunition )
        {
            if ( _bulletCount > 0 && _CanFire && ammunition ) {
                // animator
                // audio
                // muzzle
                _bulletCount--;

                Bullet tempBullet = Instantiate( ammunition, _GunTransform.position, _GunTransform.rotation ) as Bullet;

                if ( tempBullet != null ) {
                    tempBullet.Rigidbody.AddForce( _GunTransform.forward * _Force );
                    tempBullet.Name = "Bullet";

                    _CanFire = false;

                    _RechargeTimer.Start( _RechargeTime );
                }
            }
        }
    }
}
