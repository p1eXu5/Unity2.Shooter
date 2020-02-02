using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shooter.Controllers;
using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts.Controllers
{
    public class MiniMapController : MonoBehaviour
    {
        private Transform _player;

        void Start()
        {
            _player = FindObjectOfType<PlayerController>().transform;
        }

        void LateUpdate()
        {
            Vector3 newPosition = _player.position;
            newPosition.y = transform.position.y;
            transform.position = newPosition;

            transform.rotation = Quaternion.Euler( 90f, _player.eulerAngles.y, 0 );
        }
    }
}
