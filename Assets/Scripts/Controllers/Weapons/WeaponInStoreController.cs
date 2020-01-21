using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssemblyCSharp.Assets.Scripts;
using Shooter.Controllers;
using Shooter.Models;
using Shooter.Views;

namespace Shooter.Controllers
{
    public class WeaponInStoreController : ControllerBase
    {
        
        private int _selectedWeapon;

        private WeaponController[] _weaponControllers;

        private WeaponController _selectedWeaponController => _weaponControllers[_selectedWeapon];


        public int Count => _weaponControllers.Length;

        void Start()
        {
            _weaponControllers = GetComponentsInChildren< WeaponController >( true ).Select( c => {
                c.Disable(); return c;
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
                return;
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

        public void FireSelected( Fire fire = Fire.PrimaryFire )
        {
            _selectedWeaponController.Fire( fire );
        }
    }
}
