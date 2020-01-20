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
    public class FlashlightController : ViewBase< Flashlight >
    {
        private Animator _animator;

        // ReSharper disable once ConvertToNullCoalescingCompoundAssignment
        public Animator Animator => _animator ?? (_animator = GetComponent< Animator >() );

        private bool _isCharging;
        private float _chargingTime;

        public float Energy
        {
            get => Model.Energy;
            set {
                Model.Energy =  value < 0.0f ? 0.0f : value;
                Animator.SetFloat( "energy", Model.Energy );
            }
        }

        public void Toggle()
        {
            if (Animator.GetBool("isOn")) {
                Animator.SetBool("isOn", false);
            }
            else {
                Animator.SetBool("isOn", true);
            }
            
        }

        public void SwitchOff()
        {
            Animator.SetBool("isOn", false);
        }

        public void StartRecharge()
        {
            _isCharging = true;
        }



        public void ResetCharging()
        {
            _isCharging = false;
            _chargingTime = 0.0f;
        }

        public void Charge()
        {
            Energy = Model.Capacitance;
        }


        void Start()
        {
            if ( Model.Energy > Model.Capacitance ) {
                Model.Energy = Model.Capacitance;
            }
        }

        void Update()
        {
            if ( _isCharging) {
                _chargingTime += Time.deltaTime;

                if ( _chargingTime >= Model.ChargingDuration ) {
                    Charge();
                    ResetCharging();
                }
            }

            if ( Animator.GetBool( "isOn" ) ) {
                Energy = Model.Energy - Model.Power * Time.deltaTime;
            }
        }
    }
}
