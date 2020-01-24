using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shooter.Helpers;

namespace Shooter.Models.Weapons
{
    [ Serializable ]
    public class Weapon : IModel
    {
        [RenameProperty("Init Recharge time in sec.")]
        public float RechargeTime = 1;
        public float Armo;
    }
}
