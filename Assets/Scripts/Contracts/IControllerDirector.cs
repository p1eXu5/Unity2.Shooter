using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shooter.Controllers;

namespace Shooter.Contracts
{
    public interface IControllerDirector
    {
        IDictionary<Type, Controller> ControllerMap { get; }
    }
}
