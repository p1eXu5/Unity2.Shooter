using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shooter.Contracts;
using UnityEngine;

// ReSharper disable CheckNamespace
namespace Shooter.Controllers
{
    public abstract class Controller : MonoBehaviour
    {
        public IControllerDirector Director { get; set; }
    }
}
