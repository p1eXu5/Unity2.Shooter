using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.EventSystems;

namespace Shooter.Controllers.Weapons.Messages
{
    public interface IWeaponControllerMessageTarget : IEventSystemHandler
    {
        void ShowAway();
        void PullOut();
        void Fire( Fire fire );
        void Reload();
    }
}
