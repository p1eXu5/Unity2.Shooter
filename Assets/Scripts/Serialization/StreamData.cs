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
    public class StreamData : IDataSaver
    {
        string _path = Path.Combine( Application.dataPath, "JsonData.xml" );


        public void Save( Player player )
        {
            using (StreamWriter writer = new StreamWriter( _path )) {
                writer.WriteLine( player.Name );
                writer.WriteLine( player.Health );
                writer.WriteLine( player.Visible );
            }
        }

        public Player Load()
        {
            var result = new Player();

            if ( File.Exists( _path ) ) {
                using (StreamReader reader = new StreamReader( _path )) {

                    result.Name = reader.ReadLine();
                    Int32.TryParse( reader.ReadLine(), out result.Health );
                    Boolean.TryParse( reader.ReadLine(), out result.Visible );
                }
            }
            else {
                Debug.Log( "file does not exists" );
            }

            return result;
        }
    }
}
