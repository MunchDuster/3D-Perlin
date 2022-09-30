using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapController))]
[CanEditMultipleObjects]
public class MapControllerEditor : Editor 
{    
    public override void OnInspectorGUI()
    {
        MapController mapController = (MapController)target;
        
        DrawDefaultInspector();
        
        if(GUILayout.Button("MyButton"))
        {
        	mapController.InitializeMap();
        }
    }
}