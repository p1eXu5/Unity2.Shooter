using Shooter.Contracts;
using Shooter.Models.Heroes;
using UnityEngine;

namespace Shooter.Controllers.Heroes
{
    public class EnemyController : ControllerBase< Enemy >, ISetDamage
    {
        public void Update()
        {
            if ( Model.IsDead ) {
                Color color = Material.color;

                if ( color.a > 0 ) {
                    color.a -= Model.Step / 100; 
                    Color = color;
                }

                if (color.a < 1) {
                    Destroy( Instance.GetComponent< Collider >() );
                    Destroy( Instance, 5f );
                }
            }
        }


        #region ISetDamage implementation

        public virtual void ApplyDamage( float damage )
        {
            if ( Model.Hp > 0 )
            {
                Model.Hp -= damage;
            }

            if ( Model.Hp <= 0 ) {
                Model.Hp = 0;
                Color = Color.red;
                Model.IsDead = true;
            }
        }

        #endregion
    }
}
