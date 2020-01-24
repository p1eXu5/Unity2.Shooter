using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shooter.Controllers.Weapons.Messages;
using Shooter.Models;
using Shooter.Models.Weapons;
using Shooter.Views;

namespace Shooter.Controllers.Weapons
{
    public abstract class WeaponControllerBase< TModel > : ControllerBase< TModel >, IWeaponControllerMessageTarget
        where TModel : WeaponBase, new()
    {
        private float _delay;
        private Timer _rechargeTimer;

        public bool CanFire() => Model.Armo > 0 && _rechargeTimer.IsStopped;
        
        public void Fire( Fire fire = Shooter.Fire.PrimaryFire )
        {
            if (!CanFire() ) return;

            if (_Fire( fire ) ) _rechargeTimer.Restart();;
        }

        protected abstract bool _Fire( Fire fire );

        protected override void Awake()
        {
            base.Awake();

            _rechargeTimer = new Timer( Model.RechargeTime );
            _delay = Model.RechargeTime;
        }

        void Update()
        {
            if ( _rechargeTimer.IsStopped ) return;
            _rechargeTimer.Update();
        }

        public void ShowAway()
        {
            Disable();
        }

        public void PullOut()
        {
            Enable();
        }
    }
}
