using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shooter.Controllers;
using Shooter.Controllers.Gui;
using UnityEngine;
using UnityEngine.UI;

namespace Shooter.Controllers
{
    public class GuiController : ControllerBase
    {
        [SerializeField]
        private ProgressBarController _batteryIndicator;


        #region properties



        #endregion


        #region initialization



        #endregion


        #region activities

        protected override void Awake()
        {
            base.Awake();

            if ( !_batteryIndicator ) {
                FindOrCreateComponent< ProgressBarController >( "BatteryProgressBar", pbc => _batteryIndicator = pbc );
            }

            Messenger<float>.AddListener( "battery", f => { _batteryIndicator.Progress = f; } );
        }

        #endregion


        #region methods



        #endregion
    }
}
