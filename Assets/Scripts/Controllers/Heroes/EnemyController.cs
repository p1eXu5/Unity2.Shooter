using System.Collections;
using Shooter.Contracts;
using Shooter.Models.Heroes;
using UnityEngine;

namespace Shooter.Controllers.Heroes
{
    public class EnemyController< T > : ControllerBase< T >, ISetDamage
        where T : Enemy, new()
    {

        [ SerializeField ]
        protected float timeToDisappearance = 5f;



        #region ISetDamage implementation

        public virtual void ApplyDamage( float damage )
        {
            if ( Model.Hp > 0 )
            {
                Model.Hp -= damage;

                if ( Model.Hp <= 0 ) {
                    Model.IsDead = true;
                    OnHasDead();
                    Model.Hp = 0;
                }
            }
        }

        #endregion

        /// <summary>
        /// Do nothing.
        /// </summary>
        protected virtual void OnHasDead()
        { }

        
    }
}
