using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shooter.Controllers;
using Shooter.Models;
using Shooter.Views;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Shooter.Controllers
{
    public class PlayerController_ : ControllerBase< PlayerView, Player >
    {
        private FlashlightController2 _flashlightController2;
        private WeaponInStoreController _weaponsInStoreController;


        // called right after been instantiated before View assignment
        void Awake()
        { }

        void Start()
        {
            //var fview = View.GetComponentInChildren< FlashlightController >();
            //_flashlightController2 = gameObject.AddComponent<FlashlightController2>();
            //_flashlightController2.SetView( fview );

            _flashlightController2 = _GetController< Views.FlashlightController, FlashlightController2 >( true );



            //var wview = View.GetComponentInChildren< WeaponSwitcherView >();
            //_weaponsInStoreController = gameObject.AddComponent <WeaponInStoreController >();
            //_weaponsInStoreController.SetView( wview );

            _weaponsInStoreController = _GetController< WeaponSwitcherView, WeaponInStoreController >( true );
        }

        

        void Update()
        {
            if ( Input.GetKeyDown( KeyCode.F )) {
                _flashlightController2.StartRecharge();
            }
            if ( Input.GetKeyUp( KeyCode.F )) {
                _flashlightController2.ResetCharging();
                _flashlightController2.Toggle();
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
