using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

// ReSharper disable CheckNamespace
namespace Shooter.Controllers
{
    public abstract class BaseController : MonoBehaviour
    {
        public abstract void On();
        public abstract void Off();
    }
}
