using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shooter.Controllers;
using Shooter.Models;
using Shooter.Views;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Shooter
{
    public sealed class Main : MonoBehaviour
    {
        //private ControllerDirector _controllerDirector;

        //private static Main _instance;

        //public static Main GameObject => _instance == null ? (_instance = new Main()) : _instance;

        //private GameObject _controllersGameObject;
        //private InputController _inputController;

        [SerializeField] private PlayerController _playerController;

        //[SerializeField]
        //private FlashlightController _flashlightController;


        //public FlashlightController FlashlightController => _flashlightController;
        //public InputController InputController => _inputController;


        #region activities

        void Awake()
        {
            //_controllerDirector = new ControllerDirector( this.gameObject );
            if ( !_playerController ) {
                var views = FindObjectsOfType< PlayerController >();
                if ( views.Any() ) {
                    _playerController = views[0];
                }
                else {
                    Debug.Log( "no PlayerView's in scene." );
                }
            }



            //_instance = this;

            //_controllersGameObject = new GameObject( "Controllers" );

            //if ( !_flashlightController ) {
            //    _flashlightController = _controllersGameObject.AddComponent<FlashlightController>();
            //}
        }

        #endregion
    }
}
