﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shooter.Helpers;
using UnityEngine;

namespace Shooter.Models.Weapons
{
    [ Serializable ]
    public class Weapon : IModel
    {
        [RenameProperty("Init Reload time in sec.")]
        public float RechargeTime = 1;
        public int Magazine;
        public Texture reticle;
        public int claimSize = 50;
    }
}
