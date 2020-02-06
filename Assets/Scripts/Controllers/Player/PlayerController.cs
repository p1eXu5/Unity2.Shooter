using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssemblyCSharp.Assets.Scripts.Contracts;
using AssemblyCSharp.Assets.Scripts.Serialization;
using Shooter.Contracts;
using Shooter.Controllers;
using Shooter.Controllers.Weapons.Messages;
using Shooter.Models;
using Shooter.Views;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Shooter.Controllers
{
    public class PlayerController : ControllerBase< Player >, ISetDamage
    {
        private FlashlightController _flashlightController;
        private WeaponInStoreController _weaponsInStoreController;

        private IDataSaver _dataSaver;


        // called right after been instantiated before View assignment
        protected override void Awake()
        {
            base.Awake();

            _flashlightController = GetComponentInChildren< FlashlightController >( true) ;
            _weaponsInStoreController = GetComponentInChildren< WeaponInStoreController >();
        }

        void Start()
        {
            _dataSaver = new JsonData();

            Model.Health = 100;
            Model.Visible = IsVisible;
            Model.Name = name;

            _dataSaver.Save( Model );

            Player player = _dataSaver.Load();
            Debug.Log( player );

            //PlayerPrefs.SetString( "Name", name );
            //PlayerPrefs.SetInt( "Health", Model.Health );
            //PlayerPrefs.Save();

            //Debug.Log( PlayerPrefs.GetString( "Name" ) );

            //PlayerPrefs.DeleteAll();

            //Debug.Log( PlayerPrefs.GetString( "Name" ) );
        }

        

        void Update()
        {
            if ( Input.GetKeyDown( KeyCode.F )) {
                _flashlightController.StartRecharging();
            }
            if ( Input.GetKeyUp( KeyCode.F )) {
                _flashlightController.ResetCharging();
                _flashlightController.Toggle();
            }

            if ( Input.GetKeyDown( KeyCode.R )) {
                BroadcastMessage( nameof(IWeaponControllerMessageTarget.Reload) );
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

        public void ApplyDamage( float damage )
        {
            if ( Model.Hp > 0 )
            {
                Model.Hp -= damage;

                if ( Model.Hp <= 0 ) {
                    Model.IsDead = true;
                    Model.Hp = 0;
                }
            }
        }
    }
}
