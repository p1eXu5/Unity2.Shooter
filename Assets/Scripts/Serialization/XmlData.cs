using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using AssemblyCSharp.Assets.Scripts.Contracts;
using Shooter.Models;
using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts.Serialization
{
    public class XmlData : IDataSaver
    {
        string _path = Path.Combine( Application.dataPath, "XmlData.xml" );


        public void Save( Player player )
        {
            XmlDocument xmlDocument = new XmlDocument();

            XmlNode rootNode = xmlDocument.CreateElement( "Player" );
            xmlDocument.AppendChild( rootNode );

            XmlElement element = xmlDocument.CreateElement( "Name" );
            element.SetAttribute( "value", player.Name );
            rootNode.AppendChild( element );

            element = xmlDocument.CreateElement( "Health" );
            element.SetAttribute( "value", player.Health.ToString() );
            rootNode.AppendChild( element );

            element = xmlDocument.CreateElement( "Visible" );
            element.SetAttribute( "value", player.Visible.ToString() );
            rootNode.AppendChild( element );

            xmlDocument.Save( _path );
        }

        public Player Load()
        {
            var result = new Player();

            if ( File.Exists( _path ) ) {
                using (XmlTextReader reader = new XmlTextReader( _path )) 
                {
                    string key = "Name";
                    
                    while ( reader.Read() ) 
                    {
                        if ( reader.IsStartElement( key ) ) {
                            result.Name = reader.GetAttribute( "value" );
                        }

                        key = "Health";
                            
                        if ( reader.IsStartElement( key ) ) {
                            Int32.TryParse( reader.GetAttribute( "value" ), out result.Health );
                        }

                        key = "Visible";
                            
                        if ( reader.IsStartElement( key ) ) {
                            Boolean.TryParse( reader.GetAttribute( "value" ), out result.Visible );
                        }
                    }
                }
            }
            else {
                Debug.Log( "File does nor exists" );
            }

            return result;
        }
    }
}
