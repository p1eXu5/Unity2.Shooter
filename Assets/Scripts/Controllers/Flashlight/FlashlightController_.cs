//using System;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Shooter.Controllers
{
    [Obsolete]
    public class FlashlightController_ : Controller
    {
        private Light _light;
        private Animator _animator;

        [SerializeField]
        private float _power = 0.1f;
        private float _currentEnergy = 1.0f;
        private float _reserveEnergy;


        public bool IsEnabled => _light?.enabled == true;


        #region activities

        void Awake()
        {
            Debug.Log( "Flashlight awake" );
            var flashlight = GameObject.Find( "Flashlight" );
            if ( flashlight ) {
                _light = flashlight.GetComponentInChildren< Light >();
                _animator = flashlight.GetComponentInChildren< Animator >();
            }

            _animator.SetBool( "isOn", false );
            _animator.SetFloat( "energy", _currentEnergy );
        }

        void Start()
        {
        }

        void Update()
        {
            // light behavior starts here
            var isOn = _animator.GetBool( "isOn" );

            var battery = 0.0f;

            if ( isOn && _currentEnergy > 0.0 ) {
                _currentEnergy -= _power * Time.deltaTime;
                _animator.SetFloat( "energy", _currentEnergy );

                battery = _currentEnergy < 0.0f ? 0.0f : _currentEnergy;
            }

            var guiController = (GuiController)Director.ControllerMap[typeof( GuiController )];
            guiController.Battery = battery;
        }

        #endregion


        public void On()
        {
            //if ( IsEnabled ) return;

            //_setActiveFlashlight( true );
            _animator.SetBool( "isOn", true );
        }

        public void Off()
        {
            //if ( !IsEnabled ) return;

            //_setActiveFlashlight( false );
            _animator.SetBool( "isOn", false );
        }

        public void ResetBattery()
        {
            _currentEnergy = 1.0f;
        }
        
        
        private void _setActiveFlashlight( bool value )
        {
            //_light.enabled = value;
        }
    }
}
