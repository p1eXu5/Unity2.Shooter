using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Shooter.Helpers;
using UnityEngine;
// ReSharper disable CheckNamespace

namespace Shooter
{
    public abstract class BaseObject : MonoBehaviour
    {
        #region fields


        private bool _isVisible;

        #endregion


        #region properties

        public GameObject GameObject { get; private set; }
        public Camera Camera { get; private set; }
        public Animator Animator { get; private set; }


        public Transform Transform => GameObject.transform;
        public Vector3 Position
        {
            get => Transform.position;
            set => Transform.position = value;
        }
        public Vector3 Scale
        {
            get => Transform.localScale;
            set => Transform.localScale = value;
        }
        public Quaternion Rotation
        {
            get => Transform.rotation;
            set => Transform.rotation = value;
        }



        public Rigidbody Rigidbody { get; private set; }
        public Renderer Renderer { get; private set; }
        public Material Material => Renderer?.material;
        public Color Color
        {
            get => Material?.color ?? Colors.Default;
            set {
                var material = Material;
                if ( material ) {
                    material.color = value;
                }
            }
        }


        public string Name
        {
            get => GameObject.name;
            set => GameObject.name = value;
        }
        public int Layer
        {
            get => GameObject.layer;
            set {
                var inst = GameObject;
                inst.layer = value;
                AskLayer( inst.transform, inst.layer );
            }
        }





        private void AskLayer( Transform obj, int layer )
        {
            foreach ( Transform child in obj ) {
                AskLayer( child, layer );
            }
        }




        private void AskColor( Transform obj, Color color )
        {
            // ReSharper disable once LocalVariableHidesMember
            var renderer = obj.GetComponent<Renderer>();
            if ( renderer ) {
                if ( renderer.material ) {
                    renderer.material.color = color;
                }
            }
            foreach ( Transform child in obj ) {
                AskColor( child, color );
            }
        }




        public bool IsVisible
        {
            get => _isVisible;
            set {
                _isVisible = value;

                if ( Renderer ) {
                    Renderer.enabled = value;
                }
            }
        }

        public IEnumerable<Transform> GetChildrenTransforms()
        {
            for (int i = 0; i < transform.childCount; i++) { 
                yield return transform.GetChild( i );
            }
        }

        public IEnumerable<T> GetInChildren<T>()
        {
            for (int i = 0; i < transform.childCount; i++) { 
                yield return transform.GetChild( i ).gameObject.GetComponent<T>();
            }
        }

         public virtual void Activate()
        {
            gameObject.SetActive( true );
        }

        public virtual void Deactivate()
        {
            gameObject.SetActive( false );
        }

        #endregion


        #region activities

        protected virtual void Awake()
        {
            GameObject = gameObject;
            Camera = Camera.main;

            TryCacheComponent< Rigidbody >( c => Rigidbody = c );
            TryCacheComponent< Animator >( c => Animator = c );
            TryCacheComponent< Renderer >( c => Renderer = c );
        }

        private void TryCacheComponent<T>( Action< T > propertySetter )
            where T : Component
        {
            T comp = GetComponent< T >();
            propertySetter( comp ? comp : null );
        }

        #endregion
    }
}
