using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssemblyCSharp.Assets.Scripts.Views;
using Shooter.Controllers;
using Shooter.Models;
using UnityEngine;

namespace Shooter.Views
{
    [RequireComponent(typeof(Animator))]
    public class FlashlightView : ViewBase< Flashlight >
    {
        private Animator _animator;

        // ReSharper disable once ConvertToNullCoalescingCompoundAssignment
        public Animator Animator => _animator ?? (_animator = GetComponent< Animator >() );

        void Update()
        {
        }
    }
}
