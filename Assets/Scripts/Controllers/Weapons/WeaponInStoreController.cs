using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssemblyCSharp.Assets.Scripts;
using Shooter.Controllers;
using Shooter.Controllers.Weapons;
using Shooter.Controllers.Weapons.Messages;
using Shooter.Models;
using Shooter.Models.Weapons;
using Shooter.Views;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Shooter.Controllers
{
    public class WeaponInStoreController : ControllerBase
    {
        
        private int _selectedWeapon;

        private IWeaponControllerMessageTarget[] _weaponControllers;

        private IWeaponControllerMessageTarget SelectedWeaponController => _weaponControllers[_selectedWeapon];


        public int Count => _weaponControllers.Length;

        void Start()
        {
            _weaponControllers = 
                GetInChildren< IWeaponControllerMessageTarget >()
                    .Select( c => { c.ShowAway(); return c; } )
                    .ToArray();

            //BroadcastMessage( Messages.WeaponController.ShowAway );

            var sw = SelectedWeaponController;
            SelectedWeaponController.PullOut();
        }

        public void NextWeapon()
        {
            if ( _selectedWeapon == Count - 1 ) {
                ChangeWeapon( 0 );
                return;
            }

            ChangeWeapon( _selectedWeapon + 1 );
        }

        public void PrevWeapon()
        {
            if ( _selectedWeapon == 0 ) {
                ChangeWeapon( Count - 1 );
                return;
            }

            ChangeWeapon( _selectedWeapon - 1 );
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

            this.

            ChangeWeapon( nextWeapon );
        }

        private void ChangeWeapon( int nextIndex )
        {
            SelectedWeaponController.ShowAway();
            Debug.Log( $"ShowAway #{_selectedWeapon}" );
            _selectedWeapon = nextIndex;
            SelectedWeaponController.PullOut();
            Debug.Log( $"PullOut #{_selectedWeapon}" );
        }

        public void FireSelected( Fire fire = Fire.PrimaryFire )
        {
            SelectedWeaponController.Fire( fire );
        }
    }
}
