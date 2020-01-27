using Shooter.Models;
using UnityEngine;

namespace Shooter.Controllers
{
    public class ControllerBase : BaseObject
    { }

    public class ControllerBase< TModel > : ControllerBase
        where TModel : IModel, new()
    {
        [SerializeField]
        private TModel _model;

        public TModel Model => _model;
    }


}
