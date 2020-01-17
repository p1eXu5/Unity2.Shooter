using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shooter.Controllers;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Shooter
{
    public sealed class Main : MonoBehaviour
    {
        private ControllerDirector _controllerDirector;

        private static Main _instance;

        public static Main Instance => _instance == null ? (_instance = new Main()) : _instance;

        private GameObject _controllersGameObject;
        private InputController _inputController;

        [SerializeField]
        private FlashlightController _flashlightController;


        public FlashlightController FlashlightController => _flashlightController;
        public InputController InputController => _inputController;


        #region activities

        void Awake()
        {
            _controllerDirector = new ControllerDirector( this.gameObject );

            //_instance = this;

            //_controllersGameObject = new GameObject( "Controllers" );

            //if ( !_flashlightController ) {
            //    _flashlightController = _controllersGameObject.AddComponent<FlashlightController>();
            //}
        }

        #endregion
    }
}
