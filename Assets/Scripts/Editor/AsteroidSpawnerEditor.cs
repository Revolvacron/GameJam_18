using UnityEngine;
using UnityEditor;

namespace EVE.SOF
{
    [CustomEditor(typeof(AsteroidSpawner))]
    public class AsteroidSpawnerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            AsteroidSpawner spawner = (AsteroidSpawner)target;
            if (GUILayout.Button("Spawn Asteroids"))
                spawner.Spawn();
            if (GUILayout.Button("Spawn 1"))
                spawner.SpawnSingle();
        }
    }
}