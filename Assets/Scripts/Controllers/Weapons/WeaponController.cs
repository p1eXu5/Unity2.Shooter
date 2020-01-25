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
using UnityEngine.UI;

namespace Shooter.Controllers.Weapons
{
    public abstract class WeaponController< TModel > : ControllerBase< TModel >, IWeaponControllerMessageTarget
        where TModel : Weapon, new()
    {
        public ParticleSystem gunshotFlash;
        public GameObject hitParticle;
        

        protected Transform artilleryTube;


        [SerializeField]
        private int _runningArmo;
        
        private float _delay;
        private Timer _rechargeTimer;
        private bool _isFire;


        #region initialization
        protected override void Awake()
        {
            base.Awake();

            artilleryTube = Transform.Find( "GunT" );
        }

        protected virtual void Start()
        {
            _rechargeTimer = new Timer( Model.RechargeTime );
            _delay = Model.RechargeTime;
            _runningArmo = Model.Magazine;

            gunshotFlash = Instantiate( gunshotFlash, this.artilleryTube );
        }

        #endregion


        #region properties

        public bool CanFire() => _runningArmo > 0 && _rechargeTimer.IsStopped;

        protected bool IsFire
        {
            get => _isFire;
            set {
                _isFire = value;
                Animator.SetBool( "shoot", value );
            }
        }

        #endregion


        #region update

        void Update()
        {
            if (_rechargeTimer.IsStopped) {
                // default behavior here:
                
                return;
            }

            // fire
            _rechargeTimer.Update();

            if ( _rechargeTimer.IsDingDong ) {
                if ( IsFire ) {
                    IsFire = false;
                }
            }
        }

        protected virtual void OnGUI()
        {
            if ( Model.reticle == null ) return;

            int width = Model.reticle.width;
            int height = Model.reticle.height;

            float posX = Camera.pixelWidth / 2 - width / 2;
            float posY = Camera.pixelHeight / 2 - height / 2;

            GUI.Label( new Rect( posX, posY, width, height ), Model.reticle );
        }


        #endregion


        #region IWeaponControllerMessageTarget implementation

        public void Fire( Fire fire = Shooter.Fire.PrimaryFire )
        {
            if (!CanFire() ) return;

            if ( TryFire( fire ) ) 
            {
                if ( !IsFire ) { IsFire = true; }

                gunshotFlash?.Play(true);
                
                --_runningArmo;
                _rechargeTimer.Restart();
            };
        }

        public void ShowAway()
        {
            Deactivate();
        }

        public void PullOut()
        {
            Activate();
        }

        public void Recharge()
        {
            _runningArmo = Model.Magazine;
        }

        #endregion


        protected abstract bool TryFire( Fire fire );
    }
}
