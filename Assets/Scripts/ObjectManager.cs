using System.Collections;
using System.Collections.Generic;
using Shooter.Models.Ammunition;
using Shooter.Models.Weapons;
using Shooter.Weapons;
using UnityEngine;


namespace Shooter
{
    public class ObjectManager : MonoBehaviour
    {
        [SerializeField] private BallisticWeapon[] _weapons = new BallisticWeapon[5];
        [SerializeField] private AmmunitionBase[] _ammunitionList = new AmmunitionBase[5];

        public BallisticWeapon[] Weapons => _weapons;
        public AmmunitionBase[] AmmunitionList => _ammunitionList;
    }

}