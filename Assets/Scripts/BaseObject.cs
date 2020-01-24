using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
// ReSharper disable CheckNamespace

namespace Shooter
{
    public abstract class BaseObject : MonoBehaviour
    {
        #region fields

        protected Camera _MainCamera;
        protected Animator _Animator;

        private bool _isVisible;

        #endregion


        #region properties

        public GameObject Instance { get; protected set; }

        public Transform Transform => Instance.transform;


        public Rigidbody Rigidbody { get; protected set; }

        public string Name
        {
            get => Instance.name;
            set => Instance.name = value;
        }

        public int Layer
        {
            get => Instance.layer;
            set {
                var inst = Instance;
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


        public Material Material => Instance.GetComponent<Renderer>().material;

        public Color Color
        {
            get => Material.color;
            set {
                var material = Material;
                if ( material ) {
                    material.color = value;
                }
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

        public bool IsVisible
        {
            get => _isVisible;
            set {
                _isVisible = value;
                var inst = Instance;
                MeshRenderer r;
                if ( (r = inst.GetComponent<MeshRenderer>()) != null ) {
                    r.enabled = _isVisible;
                }

                SkinnedMeshRenderer s;
                if ( (s = inst.GetComponent<SkinnedMeshRenderer>()) != null ) {
                    s.enabled = _isVisible;
                }
            }
        }

        public IEnumerable<Transform> GetChildrenTransforms()
        {
            for (int i = 0; i < transform.childCount; i++) { 
                yield return transform.GetChild( i );
            }
        }

         public virtual void Enable()
        {
            gameObject.SetActive( true );
        }

        public virtual void Disable()
        {
            gameObject.SetActive( false );
        }

        #endregion


        #region activities

        protected virtual void Awake()
        {
            Instance = gameObject;
            _MainCamera = Camera.main;

            if ( GetComponent< Rigidbody >() ) {
                Rigidbody = GetComponent< Rigidbody >();
            }

            if ( GetComponent< Animator>() ) {
                _Animator = GetComponent< Animator>();
            }
        }

        #endregion
    }
}
