using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shooter.Controllers;
using Shooter.Views;
using UnityEngine;

namespace Shooter
{
    public class ControllerDirector : Shooter.Contracts.IControllerDirector
    {
        private readonly Dictionary<Type, Controller> _map;


        public ControllerDirector( GameObject mainContainer )
        {
            _map = new Dictionary< Type, Controller > {
                //[typeof(InputController)] = mainContainer.GetComponent< InputController >() 
                //                            ?? mainContainer.AddComponent< InputController >(),

                //[typeof(FlashlightController)] = mainContainer.GetComponent< FlashlightController >() 
                //                                 ?? mainContainer.AddComponent< FlashlightController >(),

                [typeof(GuiController)] = mainContainer.GetComponent< GuiController >() 
                                          ?? mainContainer.AddComponent< GuiController >()
            };

            foreach ( var kvp in _map ) {
                kvp.Value.Director = this;
            }
        }

        private void FlashlightControllerChanged( FlashlightController controller )
        {

        }

        public IDictionary< Type, Controller > ControllerMap => _map;
    }
}
