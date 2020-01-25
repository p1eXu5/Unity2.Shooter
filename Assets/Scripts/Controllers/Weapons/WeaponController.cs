using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shooter.Controllers.Weapons.Messages;
using Shooter.Helpers;
using Shooter.Models;
using Shooter.Models.Weapons;
using Shooter.Views;
using UnityEngine;

namespace Shooter.Controllers.Weapons
{
    public abstract class WeaponController< TModel > : ControllerBase< TModel >, IWeaponControllerMessageTarget
        where TModel : Weapon, new()
    {
        public ParticleSystem gunshotFlash;
        protected Transform artilleryTube;
        
        protected override void Awake()
        {
            base.Awake();

            artilleryTube = Transform.Find( "GunT" );
        }
        

        private float _delay;
        private Timer _rechargeTimer;
        
        [SerializeField]
        private int _runningArmo;

        public bool CanFire() => _runningArmo > 0 && _rechargeTimer.IsStopped;
        
        public void Fire( Fire fire = Shooter.Fire.PrimaryFire )
        {
            if (!CanFire() ) return;

            if ( TryFire( fire ) ) {
                // TODO: need to update package with weapons (idle position animation was deleted)
                //Animator.SetBool( "shoot", true );
                gunshotFlash?.Play(true);
                --_runningArmo;
                _rechargeTimer.Restart();
            };
        }

        protected abstract bool TryFire( Fire fire );

        protected virtual void Start()
        {
            _rechargeTimer = new Timer( Model.RechargeTime );
            _delay = Model.RechargeTime;
            _runningArmo = Model.Magazine;

            gunshotFlash = Instantiate( gunshotFlash, this.artilleryTube );
        }

        void Update()
        {
            if ( _rechargeTimer.IsStopped ) return;
            _rechargeTimer.Update();
            if ( _rechargeTimer.IsDingDong ) {
                //Animator.SetBool( "shoot", false );
            }
        }

        protected virtual void OnGUI()
        {
            int size = 12;
            float posX = Camera.pixelWidth / 2 - size / 2;
            float posY = Camera.pixelHeight/ 2 - size / 2;

            GUI.Label( new Rect( posX, posY, size, size ), "*" );
        }

        public void ShowAway()
        {
            Disable();
        }

        public void PullOut()
        {
            Enable();
        }

        public void Recharge()
        {
            _runningArmo = Model.Magazine;
        }
    }
}
