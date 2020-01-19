using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssemblyCSharp.Assets.Scripts.Views;
using Shooter.Controllers;
using Shooter.Models;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Shooter.Views
{
    public class ViewBase< TModel > : ViewBase
        where TModel : IModel, new()
    {
        [SerializeField]
        private TModel _model;

        public TModel Model => _model;
    }

    public class ViewBase : BaseObject
    { }
}
