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
    public sealed class InputController : BaseController
    {
        private bool _isActiveFlashlight = false;

        void Update()
        {
            if ( Input.GetKeyDown( KeyCode.F ) ) {
                _isActiveFlashlight = !_isActiveFlashlight;
                if ( _isActiveFlashlight ) {
                    // Вызов функции On() класса FlashlightController
                    Main.Instance.FlashlightController.On();
                }
                else {
                    // Вызов функции Off() класса FlashlightController
                    Main.Instance.FlashlightController.Off();
                }
            }
        }

        #region overrides

        public override void On()
        { }

        public override void Off()
        { }

        #endregion
    }
}
