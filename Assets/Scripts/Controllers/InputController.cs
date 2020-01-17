using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Shooter.Controllers
{
    /// <summary>
    /// Hotkeys controller.
    /// </summary>
    public sealed class InputController : Controller
    {
        public float chargingDuration = 1.0f;

        private bool _isActiveFlashlight = false;

        private bool _isCharging;
        private float _chargingDuration;


        private FlashlightController FlashlightController => (FlashlightController)Director?.ControllerMap[ typeof(FlashlightController) ];

        void Awake()
        {
            _chargingDuration = chargingDuration;
        }

        void Update()
        {
            if ( Input.GetKeyDown( KeyCode.F ) ) {
                _isCharging = true;
            }
            else if ( Input.GetKeyUp( KeyCode.F ) )
            {

                _isCharging = false;
                
                if ( _chargingDuration < 0.0 ) {
                    _chargingDuration = chargingDuration;
                    return;
                }

                _chargingDuration = chargingDuration;


                    _isActiveFlashlight = !_isActiveFlashlight;
                var flashlightController = FlashlightController;

                if ( _isActiveFlashlight ) {
                    // Вызов функции On() класса FlashlightController

                    flashlightController.On();
                }
                else {
                    // Вызов функции Off() класса FlashlightController
                    flashlightController.Off();
                }
            }

            if ( _isCharging) {
                _chargingDuration -= Time.deltaTime;

                Debug.Log( _chargingDuration );

                if ( _chargingDuration < 0.0 ) {
                    var flashlightController = FlashlightController;
                    flashlightController.ResetBattery();
                }
            }
        }
    }
}
