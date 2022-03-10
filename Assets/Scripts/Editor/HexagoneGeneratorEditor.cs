using Terrain;
using UnityEditor;
using UnityEngine;

namespace Terrain
{
    [CustomEditor(typeof(HexagoneGenerator))]
    public class HexagoneGeneratorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            HexagoneGenerator generator = (HexagoneGenerator) target;
            if (GUILayout.Button("Build"))
            {
                generator.SetupMesh();
            }
        }
    }
}