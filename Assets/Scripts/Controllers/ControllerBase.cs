using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shooter.Controllers;
using Shooter.Models;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Shooter.Views
{
    public class ControllerBase< TModel > : ControllerBase
        where TModel : IModel, new()
    {
        [SerializeField]
        private TModel _model;

        public TModel Model => _model;
    }


    public class ControllerBase : BaseObject
    {

        public virtual void Enable()
        {
            gameObject.SetActive( true );
        }

        public virtual void Disable()
        {
            gameObject.SetActive( false );
        }
    }
}
