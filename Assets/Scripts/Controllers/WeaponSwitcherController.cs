using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shooter.Controllers;
using Shooter.Models;
using Shooter.Views;

namespace Shooter.Controllers
{
    public class WeaponSwitcherController : ControllerBase< WeaponSwitcherView >
    {
        
        private int _selectedWeapon;

        private WeaponController[] _weaponControllers;

        private WeaponController _selectedWeaponController => _weaponControllers[_selectedWeapon];


        public int Count => _weaponControllers.Length;

        void Start()
        {
            _weaponControllers = View.GetComponentsInChildren< WeaponView >( true ).Select( v => {
                var contr = gameObject.AddComponent<WeaponController>();
                contr.SetView( v );
                contr.Disable();
                return contr;
            } ).ToArray();

            _selectedWeaponController.Enable();
        }

        public void NextWeapon()
        {
            if ( _selectedWeapon == Count - 1 ) {
                _changeWeapon( 0 );
                return;
            }

            _changeWeapon( _selectedWeapon + 1 );
        }

        public void PrevWeapon()
        {
            if ( _selectedWeapon == 0 ) {
                _changeWeapon( Count - 1 );
            }

            _changeWeapon( _selectedWeapon - 1 );
        }

        public void ChooseWeapon( int index )
        {
            var nextWeapon = _selectedWeapon;

            if ( index < 0 && nextWeapon != 0 ) {
                nextWeapon = 0;
            }
            else if ( index >= Count && nextWeapon != Count - 1 ) {
                nextWeapon = Count - 1;
            }
            else if ( nextWeapon != index ) {
                nextWeapon = index;
            }
            else {
                return;
            }

            _changeWeapon( nextWeapon );
        }

        private void _changeWeapon( int nextIndex )
        {
            _selectedWeaponController.Disable();
            _selectedWeapon = nextIndex;
            _selectedWeaponController.Enable();
        }


    }
}
