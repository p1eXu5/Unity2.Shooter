using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shooter.Controllers.Heroes;
using Shooter.Controllers.Weapons.Messages;
using Shooter.Models.Heroes;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

namespace Shooter.Controllers.Heroes
{
    [ RequireComponent( typeof(NavMeshAgent))]
    public class MobileEnemyController : EnemyController< Enemy >
    {
        #region fields

        private NavMeshAgent _agent;
        private Transform _playerPos;
        private float _groundChkDistance = 0.1f;

        private bool _grounded;
        private int _stoppingDistance = 5;
        private int _activeDistance = 10;

        [SerializeField] List<Vector3> _wayPoints = new List<Vector3>();
        private int _wayCounter;
        private GameObject _wayPointMain;

        [SerializeField] private float _timeWait = 2f;
        [SerializeField] private float _timeOut;

        [SerializeField] private bool patrol;
        [SerializeField] private bool shooting;
        [SerializeField] private bool isTarget;
        [SerializeField] private float _shootDistance = 1000f;


        // gun
        [SerializeField] private List<Transform> _visibleTargets = new List<Transform>();

        [SerializeField] private float _maxAngle = 30;
        [SerializeField] private float _maxRadius = 20;

        [SerializeField] private LayerMask _targetMask;
        [SerializeField] private LayerMask _obstacleMask;

        [SerializeField] private int _dalay = 1;

        #endregion


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

            _wayPointMain = GameObject.FindWithTag( "Waypoints" );

            foreach ( Transform trfm in _wayPointMain.transform ) {
                _wayPoints.Add( trfm.position );
            }

            patrol = true;

            StartCoroutine( nameof(FindTargets), 0.1f );
        }

        #endregion


        #region activities

        void Update()
        {
            if ( _visibleTargets.Count > 0 ) {
                patrol = false;
            }
            else {
                patrol = true;
            }

            if ( !Model.IsDead ) {

                //_agent.SetDestination( _playerPos.position );
                //_agent.stoppingDistance = _stoppingDistance;

                if ( _agent.remainingDistance > _agent.stoppingDistance ) {
                    //Move( _agent.desiredVelocity );
                    Animator.SetBool( "move", true );
                }
                else {
                    //Move( Vector3.zero );
                    Animator.SetBool( "move", false );
                }
            }

            if ( patrol ) 
            {
                if ( _wayPoints.Count >= 2 )
                {
                    _agent.stoppingDistance = 0;
                    _agent.SetDestination( _wayPoints[_wayCounter] );

                    if ( !_agent.hasPath )
                    {
                        _timeOut += 0.1f;
                        if ( _timeOut > _timeWait ) {
                            _timeOut = 0;
                            ChangeWaypoint();
                        }
                    }
                }
                else {
                    _agent.stoppingDistance = _activeDistance;
                    _agent.SetDestination( _playerPos.position );
                }
            }
            else {
                _agent.stoppingDistance = _activeDistance;

                // shoot
                Vector3 pos = transform.position + Vector3.up;
                Ray ray = new Ray(pos, transform.forward);
                RaycastHit hit;
                Debug.DrawRay( ray.origin, ray.direction * _shootDistance, Color.green );

                if ( Physics.Raycast( ray, out hit, _shootDistance, _targetMask ))
                {
                    if ( hit.collider.tag == "Player" && !shooting )
                    {
                        // Shoot; -> hit
                        shooting = true;
                        StartCoroutine( nameof( Shoot ) );
                    }
                }
                else {
                    return;
                }
            }
        }


        void OnDrawGizmos()
        {
            Vector3 pos = transform.position + Vector3.up;

            Handles.color = new Color(1, 0, 1, 0.1f);
            Handles.DrawSolidArc( pos, Vector3.up, transform.forward,  _maxAngle, _maxRadius );
            Handles.DrawSolidArc( pos, Vector3.up, transform.forward,  -_maxAngle, _maxRadius );
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

        private void FindVisibleTarget()
        {
            _visibleTargets.Clear();
            Collider[] targetsInRadius = Physics.OverlapSphere( Position, _maxRadius, _targetMask );

            for (int i = 0; i < targetsInRadius.Length; i++) 
            {
                Transform target = targetsInRadius[i].transform;
                Vector3 dirToTarget = (target.position - Position).normalized;
                float targetAngle = Vector3.Angle( transform.forward, dirToTarget );

                if ( -_maxAngle < targetAngle && targetAngle < _maxAngle )
                {
                    float distToTarget = Vector3.Distance( Position, target.position );

                    if ( !Physics.Raycast( (transform.position + Vector3.up), dirToTarget, _obstacleMask ))
                    {
                        if ( !_visibleTargets.Contains( target ) ) {
                            _visibleTargets.Add( target );
                        }
                    }
                }
            }
        }

        IEnumerator Shoot()
        {
            yield return new WaitForSeconds( 0.5f );
            Animator.SetBool( "shoot", shooting );
            BroadcastMessage( nameof( IWeaponControllerMessageTarget.Fire ), Fire.PrimaryFire );
            shooting = false;
            Animator.SetBool( "shoot", shooting );
        }

        private IEnumerator FindTargets( float delay )
        {
            Debug.Log( "coroutine" );
            while ( true )
            {
                yield return new WaitForSeconds( _dalay );
                FindVisibleTarget();
            }
        }

        private void ChangeWaypoint()
        {
            ++_wayCounter;
            if ( _wayCounter >= _wayPoints.Count ) {
                _wayCounter = 0;
            }
        }

        #endregion
    }
}
