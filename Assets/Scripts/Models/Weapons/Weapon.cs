using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shooter.Models.Ammunition;
using UnityEngine;

// ReSharper disable CheckNamespace

namespace Shooter.Models.Weapons
{
    public class Weapon : IModel
    {
        [SerializeField] protected Transform _GunTransform;
        [SerializeField] protected float _Force = 500;

        [SerializeField] protected float _RechargeTime = 500;
        protected Timer _RechargeTimer = new Timer();
        
        [SerializeField] protected ParticleSystem _ParticleSystem;
        [SerializeField] protected GameObject _HitParticle;

        protected bool _CanFire = true;



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
