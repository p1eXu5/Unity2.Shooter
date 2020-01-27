using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shooter.Controllers.Heroes;
using Shooter.Models.Heroes;
using UnityEngine;
using UnityEngine.AI;

namespace Shooter.Controllers.Heroes
{
    [ RequireComponent( typeof(NavMeshAgent))]
    public class MobileEnemyController : EnemyController< Enemy >
    {
        private NavMeshAgent _agent;
        private Transform _playerPos;
        private float _groundChkDistance = 0.1f;

        private bool _grounded;
        private int _stoppingDistance = 3;


        #region properties



        #endregion


        #region initialization

        protected override void Awake()
        {
            base.Awake();

            _agent = GetComponent<NavMeshAgent>();
            _agent.updatePosition = true;
            _agent.updateRotation = true;

            _playerPos = GameObject.FindGameObjectWithTag( "Player" ).transform;
        }

        #endregion


        #region activities

        void Update()
        {
            if ( !Model.IsDead ) {

                _agent.SetDestination( _playerPos.position );
                _agent.stoppingDistance = _stoppingDistance;

                if ( _agent.remainingDistance > _agent.stoppingDistance ) {
                    //Move( _agent.desiredVelocity );
                    Animator.SetBool( "move", true );
                }
                else {
                    //Move( Vector3.zero );
                    Animator.SetBool( "move", false );
                }
            }
        }

        #endregion


        #region methods

        public void Move( Vector3 move )
        {
            if ( move.magnitude > 1f ) {
                move.Normalize();
            }

            move = transform.InverseTransformDirection( move );
            CheckGroundStatus();
        }

        protected override void OnHasDead()
        {
            base.OnHasDead();

           StartCoroutine( Die() );

            if ( _agent ) {
                _agent.ResetPath();
               // Move( Vector3.zero );
            }

            if ( Rigidbody ) Rigidbody.isKinematic = true;


        }

        private IEnumerator Die()
        {
            Destroy( GameObject.GetComponent< Collider >() );
            Animator?.SetBool( "isDead", true );

            yield return new WaitForSeconds( timeToDisappearance );

            Destroy( GameObject, 5f );
        }

        private void CheckGroundStatus()
        {
            RaycastHit hit;

            if ( Physics.Raycast( transform.position + (Vector3.up * .2f), Vector3.down, out hit, _groundChkDistance ))
            {
                _grounded = true;
            }
            else {
                _grounded = false;
            }
        }

        #endregion
    }
}
