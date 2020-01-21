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

        [SerializeField] protected Transform _gunTransform;
        [SerializeField] protected float _force = 500;

        [SerializeField] protected ParticleSystem _particleSystem;
        [SerializeField] protected GameObject _hitParticle;

        [SerializeField] protected float _damage;

        public float Force => _force;
        public float Damage => _damage;

        public GameObject HitParticle => _hitParticle;
    }
}
