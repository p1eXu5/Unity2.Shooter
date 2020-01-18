using System.Collections;
using System.Collections.Generic;
using Shooter.Weapons;
using UnityEngine;


namespace Shooter
{
    public class ObjectManager : MonoBehaviour
    {
        [SerializeField] private Weapon[] _weapons = new Weapon[5];
        [SerializeField] private Ammunition[] _ammunitionList = new Ammunition[5];

        public Weapon[] Weapons => _weapons;
        public Ammunition[] AmmunitionList => _ammunitionList;
    }

}