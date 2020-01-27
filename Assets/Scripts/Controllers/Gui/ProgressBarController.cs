using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Shooter.Controllers.Gui
{
    public class ProgressBarController : ControllerBase
    {
        /// <summary>
        /// Image component for gun reticle.
        /// </summary>
        [SerializeField] 
        private Image _maskImage;


        #region properties
        public float Progress
        {
            get => _maskImage.fillAmount;
            set => _maskImage.fillAmount = Mathf.Clamp( value, 0.0f, 1.0f );
        }

        #endregion


        #region initialization

        protected override void Awake()
        {
            base.Awake();


            _maskImage = GetInChildren<Image>().First();
        }

        #endregion


        #region activities



        #endregion


        #region methods



        #endregion
    }
}
