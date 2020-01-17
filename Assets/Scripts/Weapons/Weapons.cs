using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

// ReSharper disable CheckNamespace

namespace Shooter.Weapons
{
    public abstract class Weapons : BaseObject
    {
        [SerializeField] protected Transform _Gun;
        [SerializeField] protected float _Force = 500;
        [SerializeField] protected float _RechargeTime = 500;
        [SerializeField] protected ParticleSystem _ParticleSystem;
        [SerializeField] protected GameObject _hitParticle;



        
        protected bool _Fire = true;


        public abstract void Fire();

        protected override void Awake()
        {
            base.Awake();
        }

        void Update()
        {

        }
    }
}
