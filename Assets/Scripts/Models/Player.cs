using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shooter.Models;
using UnityEngine;

namespace Shooter.Models
{
    [Serializable]
    public class Player : IModel
    {
        [SerializeField] private float _hp = 100;
        private bool _isDead = false;

        public Flashlight Flashlight { get; set; }

        public float Hp
        {
            get => _hp;
            internal set => _hp = value;
        }

        public bool IsDead
        {
            get => _isDead;
            internal set => _isDead = value;
        }
    }
}
