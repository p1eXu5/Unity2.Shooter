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
    public class PlayerController : ControllerBase< PlayerView, Player >
    {
        private FlashlightController2 _flashlightController2;
        private WeaponSwitcherController _weaponSwitcherController;


        // called right after been instantiated before View assignment
        void Awake()
        { }

        void Start()
        {
            //var fview = View.GetComponentInChildren< FlashlightView >();
            //_flashlightController2 = gameObject.AddComponent<FlashlightController2>();
            //_flashlightController2.SetView( fview );

            _flashlightController2 = _GetController< FlashlightView, FlashlightController2 >( true );



            //var wview = View.GetComponentInChildren< WeaponSwitcherView >();
            //_weaponSwitcherController = gameObject.AddComponent <WeaponSwitcherController >();
            //_weaponSwitcherController.SetView( wview );

            _weaponSwitcherController = _GetController< WeaponSwitcherView, WeaponSwitcherController >( true );
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
                _weaponSwitcherController.NextWeapon();
            }

            if ( Input.GetAxis( "Mouse ScrollWheel" ) < 0 ) {
                _weaponSwitcherController.PrevWeapon();
            }


        }

    }
}
