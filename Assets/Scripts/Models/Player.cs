using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shooter.Models;

namespace Shooter.Models
{
    [Serializable]
    public class Player : IModel
    {
        public Flashlight Flashlight { get; set; }
    }
}
