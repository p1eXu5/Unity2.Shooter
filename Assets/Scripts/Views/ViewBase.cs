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
    public class ViewBase< TModel > : ViewBase
        where TModel : IModel, new()
    {
        [SerializeField]
        private TModel _model;

        public TModel Model => _model;
    }

    public class ViewBase : BaseObject
    {

        public virtual void Enable()
        {
            gameObject.SetActive( true );
        }

        public virtual void Disable()
        {
            gameObject.SetActive( false );
        }

        protected TC _GetController<TV, TC>( bool includeInactive = false )
            where TC : ControllerBase<TV>
            where TV : ViewBase
        {
            TV view = GetComponentInChildren<TV>( includeInactive );
            TC controller = gameObject.AddComponent<TC>();
            controller.SetView( view );

            return controller;
        }
    }
}
