using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

// ReSharper disable CheckNamespace

namespace Shooter.Weapons
{
    public abstract class Weapon : BaseObject
    {
        [SerializeField] protected Transform _GunTransform;
        [SerializeField] protected float _Force = 500;

        [SerializeField] protected float _RechargeTime = 500;
        protected Timer _RechargeTimer = new Timer();
        
        [SerializeField] protected ParticleSystem _ParticleSystem;
        [SerializeField] protected GameObject _HitParticle;

        protected bool _CanFire = true;



        public abstract void Fire( Ammunition ammunition );


        void Update()
        {
            _RechargeTimer.Update();

            if (_RechargeTimer.IsEvent())
            {
                _CanFire = true;
            }
        }
    }
}
