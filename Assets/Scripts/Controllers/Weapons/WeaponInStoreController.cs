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

        private Transform[] _weaponControllers;

        private Transform _SelectedWeaponController => _weaponControllers[_selectedWeapon];


        public int Count => _weaponControllers.Length;

        void Start()
        {
            _weaponControllers = GetChildrenTransforms().ToArray();
            BroadcastMessage( Messages.WeaponController.ShowAway );

            _SelectedWeaponController.BroadcastMessage( Messages.WeaponController.PullOut );
        }

        public void NextWeapon()
        {
            if ( _selectedWeapon == Count - 1 ) {
                _ChangeWeapon( 0 );
                return;
            }

            _ChangeWeapon( _selectedWeapon + 1 );
        }

        public void PrevWeapon()
        {
            if ( _selectedWeapon == 0 ) {
                _ChangeWeapon( Count - 1 );
                return;
            }

            _ChangeWeapon( _selectedWeapon - 1 );
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

            _ChangeWeapon( nextWeapon );
        }

        private void _ChangeWeapon( int nextIndex )
        {
            _SelectedWeaponController.BroadcastMessage( Messages.WeaponController.ShowAway);
            _selectedWeapon = nextIndex;
            _SelectedWeaponController.BroadcastMessage( Messages.WeaponController.PullOut );
        }

        public void FireSelected( Fire fire = Fire.PrimaryFire )
        {
            _SelectedWeaponController.BroadcastMessage( Messages.WeaponController.Fire, fire );
        }
    }
}
