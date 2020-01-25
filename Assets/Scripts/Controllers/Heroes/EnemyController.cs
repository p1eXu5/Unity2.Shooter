using System.Collections;
using Shooter.Contracts;
using Shooter.Models.Heroes;
using UnityEngine;

namespace Shooter.Controllers.Heroes
{
    public class EnemyController : ControllerBase< Enemy >, ISetDamage
    {

        [ SerializeField ]
        private float _timeToDisappearance = 5f;

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
                StartCoroutine( Die() );
            }
        }

        #endregion

        private IEnumerator Die()
        {
            Destroy( GameObject.GetComponent< Collider >() );
            Animator?.SetBool( "isDead", true );

            yield return new WaitForSeconds( _timeToDisappearance );

            Destroy( GameObject, 5f );
        }
    }
}
