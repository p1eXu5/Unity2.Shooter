using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Shooter.Controllers
{
    public class ChangeWeapon : Controller
    {
        private int _weaponId = 0;


        void Start()
        {

        }

        void Update()
        {
            var previousSelectWeapon = _weaponId;

            if ( Input.GetAxis( "Mouse ScrollWheel" ) < 0 ) {
                if ( _weaponId <= 0 ) {
                    _weaponId = ChildCount - 1;
                }
                else {
                    _weaponId--;
                }
            }

            if ( Input.GetAxis( "Mouse ScrollWheel" ) < 0 ) {
                if ( _weaponId >= ChildCount - 1 ) {
                    _weaponId = 0;
                }
                else {
                    _weaponId++;
                }
            }

            if ( Input.GetKeyDown( KeyCode.Alpha1 )) {
                _weaponId = 0;
            }

            if ( Input.GetKeyDown( KeyCode.Alpha2 ) && ChildCount > 2 ) {
                _weaponId = 1;
            }

            if ( Input.GetKeyDown( KeyCode.Alpha3 ) && ChildCount > 3 ) {
                _weaponId = 2;
            }

            if ( Input.GetKeyDown( KeyCode.Alpha4 ) && ChildCount > 4 ) {
                _weaponId = 3;
            }

            if ( Input.GetKeyDown( KeyCode.Alpha5 ) && ChildCount > 5 ) {
                _weaponId = 4;
            }

            if ( previousSelectWeapon != _weaponId ) {
                _selectWeapon();
            }
        }

        private void _selectWeapon()
        {
            int i = 0;
            foreach ( Transform weapon in gameObject.transform ) {
                if (i == _weaponId) {
                    weapon.gameObject.SetActive( true );
                }
                else {
                    weapon.gameObject.SetActive( false );
                }
                ++i;
            }
        }
    }
}
