using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shooter.Controllers;
using Shooter.Models;
using UnityEngine;

namespace Shooter.Views
{

    [RequireComponent(typeof(Animator))]
    public class FlashlightController : ControllerBase< Flashlight >
    {
        private float _runningEnergy;
        private bool _isCharging;
        private float _chargingTime;

        #region properties
        private Light _Light => gameObject.GetComponentInChildren<Light>();
        
        public float Energy
        {
            get => Model.energy;
            set {
                Model.energy =  value < 0.0f ? 0.0f : value;

                float energy = Model.energy / Model.Capacitance;

                Animator.SetFloat( "energy", energy * 10 );
                
            }
        }

        #endregion


        #region initialization
        void Start()
        {
            if ( Model.energy > Model.Capacitance ) {
                Model.energy = Model.Capacitance;
            }
            Energy = Model.energy;
        }

        #endregion


        #region activities
        void Update()
        {
            if ( _isCharging) {
                _chargingTime += Time.deltaTime;

                if ( _chargingTime >= Model.rechargingDuration ) {
                    Recharge();
                    ResetCharging();
                }
            }

            if ( Animator.GetBool( "isOn" ) ) {
                Energy = Model.energy - Model.power * Time.deltaTime;
            }
        }

        void FixedUpdate()
        {
            if ( Animator.GetBool( "isOn" ) ) {
                float energy = Model.energy / Model.Capacitance;
                Messenger<float>.Broadcast( "battery", energy );
            }
        }

        private void OnDisable()
        {
            Messenger<float>.Broadcast( "battery", 0.0f );
        }

        private void OnEnable()
        {
            float energy = Model.energy / Model.Capacitance;
            Messenger<float>.Broadcast( "battery", energy );
        }






        #endregion


        #region methods
        public void Toggle()
        {
            if (Animator.GetBool("isOn")) {
                Animator.SetBool("isOn", false);
            }
            else {
                Animator.SetBool("isOn", true);
            }
            
        }

        public void Recharge()
        {
            Energy = Model.Capacitance;
        }

        public void StartRecharging()
        {
            _isCharging = true;
        }

        public void ResetCharging()
        {
            _isCharging = false;
            _chargingTime = 0.0f;
        }
        
        #endregion
    }
}
