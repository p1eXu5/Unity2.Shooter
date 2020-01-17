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
        private static Main _instance;

        public static Main Instance => _instance == null ? (_instance = new Main()) : _instance;

        private GameObject _controllersGameObject;
        private InputController _inputController;
        private FlashlightController _flashlightController;


        public FlashlightController FlashlightController => _flashlightController;
        public InputController InputController => _inputController;


        #region activities

        void Start()
        {
            _instance = this;

            _controllersGameObject = new GameObject( "Controllers" );
            _inputController = _controllersGameObject.AddComponent<InputController>();
            _flashlightController = _controllersGameObject.AddComponent<FlashlightController>();
        }

        #endregion
    }
}
