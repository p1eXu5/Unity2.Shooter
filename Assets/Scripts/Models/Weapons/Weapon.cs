using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shooter.Helpers;
using Shooter.Models.Ammunition;
using Shooter.Views;
using UnityEngine;

// ReSharper disable CheckNamespace

namespace Shooter.Models.Weapons
{
    [Serializable]
    public class Weapon : IModel
    {
        [RenameProperty("Init Recharge time in sec.")]
        public float RechargeTime = 1;
        public float Armo;

        public BulletController[] ammunition;

        [SerializeField] protected Transform _GunTransform;
        [SerializeField] protected float _Force = 500;

        [SerializeField] protected ParticleSystem _ParticleSystem;
        [SerializeField] protected GameObject _HitParticle;

        public float Force => _Force;
    }
}
