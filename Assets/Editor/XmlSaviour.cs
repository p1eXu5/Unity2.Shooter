using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;


[Serializable]
public struct SVector3
{
    public float X;
    public float Y;
    public float Z;

    public SVector3( float x, float y, float z)
    {
        X = x; Y = y; Z = z;
    }

    public static implicit operator SVector3( Vector3 val )
    {
        return new SVector3( val.x, val.y, val.z );
    }

    public static implicit operator Vector3( SVector3 val )
    {
        return new Vector3( val.X, val.Y, val.Z );
    }
}

[Serializable]
public struct SQuaternion
{
    public float X;
    public float Y;
    public float Z;
    public float W;

    public SQuaternion( float x, float y, float z, float w)
    {
        X = x; Y = y; Z = z; W = w;
    }

    public static implicit operator SQuaternion( Quaternion val )
    {
        return new SQuaternion( val.x, val.y, val.z, val.w );
    }

    public static implicit operator Quaternion( SQuaternion val )
    {
        return new Quaternion( val.X, val.Y, val.Z, val.W );
    }
}

public struct SGameObject
{
    public string Name;
    public SVector3 Position;
    public SVector3 Scale;
    public SQuaternion Rotation;
}


public class SaveLevel
{
    [MenuItem("Инструменты/ Сохранение шаблонов/ Сохранить уровень", false, 1)]
    private static void SaveScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        List< GameObject > rootObjects = new List< GameObject >();
        scene.GetRootGameObjects(rootObjects);
        List< SGameObject > levelObjects = new List< SGameObject >();

        string savePath = Path.Combine( Application.dataPath, "EditorDataXml.xml" );

        foreach ( var obj in rootObjects ) {
            var temp = obj.transform;
            levelObjects.Add( 
                new SGameObject {
                    Name = temp.name,
                    Position = temp.position,
                    Rotation = temp.rotation,
                    Scale = temp.localScale
                });
        }
        // XML save
        XmlSaviour.Save( levelObjects.ToArray(), savePath );
    }
}

 
public class XmlSaviour : MonoBehaviour
{
    private static XmlSerializer _xmlSerializer;

    static XmlSaviour()
    {
        _xmlSerializer = new XmlSerializer( typeof( SGameObject[] ) );
    }

    public static void Save( SGameObject[] levelObj, string path )
    {
        if ( levelObj == null && !String.IsNullOrWhiteSpace( path ) )
        {
            Debug.Log( "Не задан путь или массив пуст" );
            return;
        }

        if ( levelObj.Length <= 0) {
            return;
        }

        using ( FileStream fs = new FileStream( path, FileMode.Create ) )
        {
            _xmlSerializer.Serialize( fs, (SGameObject[])levelObj );
        }
    }

    [ MenuItem("Инструменты/ Сохранение шаблонов/ Загрузить уровень") ]
    private static void Load()
    {
        SGameObject[] result;

        using( FileStream fs = new FileStream( Path.Combine( Application.dataPath, "EditorDataXml.xml" ), FileMode.Open ) )
        {
            result = _xmlSerializer.Deserialize( fs ) as SGameObject[];
        }

        if ( result != null ) {
            foreach ( var o in result ) 
            {
                var prefab = Resources.Load< GameObject >( o.Name );
                if ( prefab )
                {
                    GameObject temp = Instantiate( prefab, o.Position, o.Rotation );
                    temp.name = o.Name;
                }
            }
        }
    }
}
