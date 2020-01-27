using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shooter.Controllers.Heroes;
using Shooter.Models.Heroes;
using UnityEngine;

namespace Shooter.Controllers.Heroes
{
    public class StaticEnemyController : EnemyController< Enemy >
    {
        public void Update()
        {
            if ( Model.IsDead ) {
                Color color = Color;


                if ( color.a > 0 ) {
                    color.a -= Model.Step / 100; 
                    Color = color;
                }
            }
        }

        protected override void OnHasDead()
        {
            Color = Color.red;
        }

        
    }
}
