using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shooter.Models;
using UnityEngine;

namespace Shooter.Models.Weapons
{
    public class LaserWeapon : WeaponBase
    {
        

        [SerializeField] protected float _damage;

        public float Damage => _damage;

    }
}
