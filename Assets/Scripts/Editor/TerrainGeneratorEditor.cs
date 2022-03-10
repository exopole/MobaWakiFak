using UnityEditor;
using UnityEngine;

namespace Terrain
{
    [CustomEditor(typeof(TerrainGenerator))]
    public class TerrainGeneratorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            TerrainGenerator generator = (TerrainGenerator) target;
            if (GUILayout.Button("Build"))
            {
                generator.SetupTerrain();
            }
            if (GUILayout.Button("Clean"))
            {
                generator.Clean();
            }
        }
    }
}