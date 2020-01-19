using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shooter.Helpers;
using Shooter.Models;
using UnityEngine;

namespace Shooter.Models
{
    [Serializable]
    public class Flashlight : IModel
    {
        [SerializeField]
        private float _capacitance;

        [RenameProperty("Power (I)")]
        public float Power;

        [RenameProperty("Energy (C)")]
        public float Energy;
        
        public float ChargingDuration = 2f;

        [HideInInspector]
        public bool IsCharging = false;

        public float Capacitance => _capacitance;
    }
}
