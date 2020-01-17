using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shooter.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace Shooter.Controllers
{
    public class GuiController : Controller
    {
        [SerializeField]
        private Image _image;
        public float Battery
        {
            get => _image.fillAmount; 
            set => _image.fillAmount = value;
        }

        void Awake()
        {

        }


    }
}
