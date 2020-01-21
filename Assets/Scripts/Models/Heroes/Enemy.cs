using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shooter;
using Shooter.Contracts;
using UnityEngine;

namespace Shooter.Models.Heroes
{
    [Serializable]
    public class Enemy : IModel
    {
        [SerializeField] private float _hp = 100;

        private bool _isDead = false;
        private float _step = 2f;


        public float Hp
        {
            get => _hp;
            internal set => _hp = value;
        }

        public float Step => _step;

        public bool IsDead
        {
            get => _isDead;
            internal set => _isDead = value;
        }
    }
}
