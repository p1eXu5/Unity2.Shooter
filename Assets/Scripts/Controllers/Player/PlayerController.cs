using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shooter.Controllers;
using Shooter.Controllers.Weapons.Messages;
using Shooter.Models;
using Shooter.Views;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Shooter.Controllers
{
    public class PlayerController : ControllerBase< Player >
    {
        private FlashlightController _flashlightController;
        private WeaponInStoreController _weaponsInStoreController;


        // called right after been instantiated before View assignment
        protected override void Awake()
        {
            base.Awake();

            _flashlightController = GetComponentInChildren< FlashlightController >();
            _weaponsInStoreController = GetComponentInChildren< WeaponInStoreController >();
        }

        void Start()
        {

        }

        

        void Update()
        {
            if ( Input.GetKeyDown( KeyCode.F )) {
                _flashlightController.StartRecharge();
            }
            if ( Input.GetKeyUp( KeyCode.F )) {
                _flashlightController.ResetCharging();
                _flashlightController.Toggle();
            }

            if ( Input.GetKeyDown( KeyCode.R )) {
                BroadcastMessage( nameof(IWeaponControllerMessageTarget.Recharge) );
            }

            if ( Input.GetAxis( "Mouse ScrollWheel" ) > 0 ) {
                _weaponsInStoreController.NextWeapon();
            }

            if ( Input.GetAxis( "Mouse ScrollWheel" ) < 0 ) {
                _weaponsInStoreController.PrevWeapon();
            }

            if ( Input.GetButton( "Fire1" )) {
                _weaponsInStoreController.FireSelected();
            }

        }

    }
}
