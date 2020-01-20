using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shooter.Models;
using Shooter.Views;
using UnityEngine;

namespace Shooter.Controllers
{



    public class ControllerBase<TView> : MonoBehaviour
        where TView : ViewBase
    {
        public TView View { get; private set; }


        public void SetView( TView view )
        {
            View = view;
        }

        public virtual void Enable()
        {
            View.Instance.SetActive( true );
        }

        public virtual void Disable()
        {
            View.Instance.SetActive( false );
        }

        protected TC _GetController<TV, TC>( bool includeInactive = false )
            where TC : ControllerBase<TV>
            where TV : ViewBase
        {
            TV view = View.GetComponentInChildren<TV>( includeInactive );
            TC controller = gameObject.AddComponent<TC>();
            controller.SetView( view );

            return controller;
        }
    }



    public class ControllerBase<TView, TModel > : ControllerBase<TView>
        where TModel : IModel, new()
        where TView : ViewBase< TModel >
    {
        public TModel Model => View.Model;
    }
}
