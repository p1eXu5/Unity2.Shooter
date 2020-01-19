using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shooter.Models;
using Shooter.Views;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Scripting;

namespace Shooter.Controllers
{
    public interface IFlashlightMessageTarget : IEventSystemHandler
    {
        void SwitchOn();
        void SwitchOff();

        void StartRecharge();
    }

    public class FlashlightController2 : ControllerBase< FlashlightView, Flashlight >
    {
        private bool _isCharging;
        private float _chargingTime;

        public float Energy
        {
            get => Model.Energy;
            set {
                Model.Energy =  value < 0.0f ? 0.0f : value;
                View.Animator.SetFloat( "energy", Model.Energy );
            }
        }

        public void Toggle()
        {
            if (View.Animator.GetBool("isOn")) {
                View.Animator.SetBool("isOn", false);
            }
            else {
                View.Animator.SetBool("isOn", true);
            }
            
        }

        public void SwitchOff()
        {
            View.Animator.SetBool("isOn", false);
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

            if ( View.Animator.GetBool( "isOn" ) ) {
                Energy = Model.Energy - Model.Power * Time.deltaTime;
            }
        }

    }
}
