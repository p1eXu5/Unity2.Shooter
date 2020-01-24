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
    public class ControllerBase< TModel > : BaseObject
        where TModel : IModel, new()
    {
        [SerializeField]
        private TModel _model;

        public TModel Model => _model;
    }

}
