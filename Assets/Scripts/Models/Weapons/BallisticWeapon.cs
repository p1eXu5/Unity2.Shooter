using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shooter.Controllers.Ammunition;
using Shooter.Helpers;
using Shooter.Models.Ammunition;
using Shooter.Views;
using UnityEngine;

// ReSharper disable CheckNamespace

namespace Shooter.Models.Weapons
{
    [Serializable]
    public class BallisticWeapon : Weapon
    {
        

        public BulletController[] ammunition;

        [SerializeField] protected float _force = 500;

        

        public float Force => _force;
    }
}
