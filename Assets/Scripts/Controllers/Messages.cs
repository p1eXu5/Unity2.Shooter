using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shooter.Controllers.Weapons.Messages;

namespace Shooter.Controllers
{
    public class Messages
    {
        public static class WeaponController
        {
            public static string ShowAway => nameof( IWeaponControllerMessageTarget.ShowAway );
            public static string PullOut => nameof( IWeaponControllerMessageTarget.PullOut );
            public static string Fire => nameof( IWeaponControllerMessageTarget.Fire );
        }
    }
}
