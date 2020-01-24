using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Shooter.Contracts;
using Shooter.Models.Ammunition;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Shooter.Models.Ammunition
{
    [Serializable]
    public class Bullet : AmmunitionBase
    {
        [SerializeField] private float _timeToDestruct = 10;
        [SerializeField] private float _damage = 20;
        [SerializeField] private float _mass = 0.01f;

        private float _currentDamage;

        
        public float TimeToDestruct => _timeToDestruct;
        public float Damage => _damage;

        public float Mass => _mass;
    }
}
