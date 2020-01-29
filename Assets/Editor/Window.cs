using Shooter;
using UnityEngine;
using UnityEditor;

public class Window : EditorWindow
{
    public GameObject botPref;
    public string name = "bot";
    public int objectCount;
    public float radius = 20;
    public float y;

    [MenuItem("Инструменты/ Создание префабов/ Генератор Ботов")]
    public static void ShowWindow()
    {
        GetWindow( typeof( Window ) );
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField( "Настройки", EditorStyles.boldLabel );
        botPref = EditorGUILayout.ObjectField( "Префаб бота", botPref, typeof( GameObject ), true ) as GameObject;
        objectCount = EditorGUILayout.IntSlider( "Количество объектов", objectCount, 1, 200 );

        radius = EditorGUILayout.Slider( "Радиус", radius, 10, 100 );

        y = EditorGUILayout.Slider( "Высота", y, 0, 3 );

        if ( GUILayout.Button( "Сгенерировать ботов" ))
        {
            if ( botPref ) {
                GameObject main = new GameObject("MainBot");
                for (int i = 0; i < objectCount; i++) {
                    float angle = i * Mathf.PI * 2 / objectCount;
                    Vector3 position = new Vector3( Mathf.Cos( angle ), y, Mathf.Sin( angle )) * radius;
                    GameObject temp = Instantiate( botPref, position, Quaternion.identity );
                    temp.transform.parent = main.transform;
                    temp.name += "(" + i + ")";
                }
            }
        }
    }
}
