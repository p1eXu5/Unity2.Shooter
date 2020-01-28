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
        private bool _isRecharging;

        #region properties

        public bool CanFire() => _runningArmo > 0 && _rechargeTimer.IsStopped && !_isRecharging;

        public bool NeedToReload => _runningArmo < Model.Magazine;

        protected bool IsFire
        {
            get => _isFire;
            set {
                _isFire = value;
                Animator?.SetBool( "shoot", value );
            }
        }

        #endregion


        #region initialization
        protected override void Awake()
        {
            base.Awake();

            artilleryTube = Transform.Find( "GunT" );
            var clip = Animator?.runtimeAnimatorController.animationClips.FirstOrDefault( c => c.name == "Reload" );
            if ( clip != null ) {
                var animEvent = new AnimationEvent() { functionName = nameof( OnReloaded ) };
                clip.AddEvent( animEvent );
            }
        }

        protected virtual void Start()
        {
            _rechargeTimer = new Timer( Model.RechargeTime );
            _delay = Model.RechargeTime;
            _runningArmo = Model.Magazine;

            //gunshotFlash = Instantiate( gunshotFlash, this.artilleryTube );
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

            int width = Mathf.Clamp(Model.reticle.width, 0, Model.claimSize );
            int height = Mathf.Clamp(Model.reticle.height, 0, Model.claimSize );

            int maxHalfSize = Model.claimSize / 2;
            float posX = Camera.pixelWidth / 2 - maxHalfSize;
            float posY = Camera.pixelHeight / 2 - maxHalfSize;

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

        public void Reload()
        {
            if ( NeedToReload ) {
                _runningArmo = Model.Magazine;
                Animator.SetTrigger( "reload" );
                _isRecharging = true;
            }
        }

        #endregion


        protected abstract bool TryFire( Fire fire );

        protected virtual void OnReloaded()
        {
            _isRecharging = false;
        }
    }
}
