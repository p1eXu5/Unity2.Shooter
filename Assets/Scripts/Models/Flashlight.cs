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

        /// <summary>
        /// How fast the battery runs out.
        /// </summary>
        [RenameProperty("power (I)")]
        public float power;

        /// <summary>
        /// Battery charge in the flashlight.
        /// </summary>
        [RenameProperty("energy (C)")]
        public float energy;
        
        public float rechargingDuration = 2f;



        public float Capacitance => _capacitance;
    }
}
