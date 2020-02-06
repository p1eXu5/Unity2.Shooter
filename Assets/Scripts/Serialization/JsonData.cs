using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssemblyCSharp.Assets.Scripts.Contracts;
using Shooter.Models;
using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts.Serialization
{
    public class JsonData : IDataSaver
    {
        string _path = Path.Combine( Application.dataPath, "JsonData.xml" );

        public void Save( Player player )
        {
            string jsonFile = JsonUtility.ToJson( player );
            File.WriteAllText( _path, jsonFile );
        }

        public Player Load()
        {
            string temp = File.ReadAllText( _path );
            return JsonUtility.FromJson< Player >( temp );
        }
    }
}
