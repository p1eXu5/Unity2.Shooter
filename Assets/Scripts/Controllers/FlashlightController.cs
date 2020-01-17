using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Shooter.Controllers
{
    public class FlashlightController : BaseController
    {
        private Light _light;
        private float _timeout = 10;
        private float _currTime;
        private float _currReloadTime;

        public bool IsEnabled => _light?.enabled == true;


        #region activities

        void Awake()
        {
            _light = GameObject.Find( "Flashlight" ).GetComponentInChildren<Light>();
        }

        void Start()
        {
            _setActiveFlashlight( false );
        }

        void Update()
        {
            if (!IsEnabled) return;

            // light behavior starts here
        }

        #endregion


        #region overrides

        public override void On()
        {
            if ( IsEnabled ) return;

            _setActiveFlashlight( true );
        }

        public override void Off()
        {
            if ( !IsEnabled ) return;

            _setActiveFlashlight( false );
        }


        #endregion
        
        
        private void _setActiveFlashlight( bool value )
        {
            _light.enabled = value;
        }
    }
}
