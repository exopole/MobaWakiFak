using System;
using System.Collections;
using System.Collections.Generic;
using Terrain;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public HexagoneGenerator HexagonePrefab;
    public float radius = 10;
    private List<HexagoneGenerator> _hexagones = new List<HexagoneGenerator>();

    public List<HexagoneGenerator> Hexagones => _hexagones;


    // Update is called once per frame
    public void SetupTerrain()
    {
        Clean();
        float zndex = radius;
        float xndex = -radius;
        float lineCount = 0;
        while (zndex >= -radius)
        {
            xndex = -radius;
            if (lineCount %2 != 0)
            {
                xndex += 1;
            }
            while (xndex <= radius)
            {
                CreateHexagone(xndex, zndex);
                xndex += 2;
            }

            lineCount++;
            zndex-=1.5f;
        }
    }

    public void Clean()
    {
        foreach (Transform child in transform)
        {
            if (Application.isPlaying)
            {
                Destroy(child.gameObject);
            }
            else
            {
                DestroyImmediate(child.gameObject);
            }
        }
    }

    private void CreateHexagone(float x, float z)
    {
        Vector3 hexagonePos = new Vector3(x, 0, z);
        if (Vector3.Distance(hexagonePos, Vector3.zero) > radius)
        {
            return;
        }
        HexagoneGenerator hexagone = Instantiate(HexagonePrefab, this.transform, true);
        hexagone.transform.position = hexagonePos;
        _hexagones.Add(hexagone);
#if UNITY_EDITOR
        hexagone.SetupMesh();
#endif
    }
}
