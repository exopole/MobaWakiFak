using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Extensions;
using Terrain;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public HexagoneGenerator HexagonePrefab;
    public float radius = 10;
    private List<HexagoneGenerator> _hexagones = new List<HexagoneGenerator>();
    private Dictionary<float, Dictionary<float, HexagoneGenerator>> _map = new Dictionary<float, Dictionary<float, HexagoneGenerator>>();

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
                CreateHexagon(xndex, zndex);
                xndex += 2;
            }

            lineCount++;
            zndex-=1.5f;
        }
        
        SetupNeighbors();
    }

    public void RemoveRandomeHexagones(int range = 1)
    {
        var hexs = _hexagones.Shuffle();
        for (int i = 0; i < range; i++)
        {
            hexs[i].gameObject.SetActive(false);
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
        
        _hexagones.Clear();
        
    }

    public List<HexagoneGenerator> GetRandomHex(int range)
    {
        var result = _hexagones.Shuffle().Where(h => h.gameObject.activeInHierarchy && !h.HasElement).ToList();
        int numberOfHex =  result.Count;
        if (numberOfHex > range)
        {
            return result.GetRange(0, range);
        }

        if (numberOfHex == range)
        {
            return result;
        }

        // Not enough hex in the terrain
        Debug.LogError(string.Format("TerrainGenerator => terrain ({0}) is less than the range ({1})", numberOfHex, range));
        return result;
    }

    private void CreateHexagon(float x, float z)
    {
        Vector3 hexagonePos = new Vector3(x, 0, z);
        if (Vector3.Distance(hexagonePos, Vector3.zero) > radius)
        {
            return;
        }
        var hexagon = Instantiate(HexagonePrefab, transform, true);
        hexagon.transform.position = hexagonePos;
        _hexagones.Add(hexagon);
        AddHexagonInMap(x, z, hexagon);
#if UNITY_EDITOR
        hexagon.SetupMesh();
#endif
    }

    private void AddHexagonInMap(float x, float z, HexagoneGenerator hex)
    {
        if (!_map.ContainsKey(x) || _map[x] == null)
        {
            _map[x] = new Dictionary<float, HexagoneGenerator>
            {
                [z] = hex
            };
            return;
        }

        _map[x][z] = hex;
    }

    private void SetupNeighbors()
    {
        foreach (var line in _map)
        {
            foreach (var depth in line.Value)
            {
                depth.Value.NE = FindNE(line.Key, depth.Key);
                depth.Value.E = FindE(line.Key, depth.Key);
                depth.Value.SE = FindSE(line.Key, depth.Key);
                depth.Value.SO = FindSO(line.Key, depth.Key);
                depth.Value.O = FindO(line.Key, depth.Key);
                depth.Value.NO = FindNO(line.Key, depth.Key);
            }
        }
    }

    private HexagoneGenerator FindNE(float x, float z)
    {
        float newX = x + 1;
        float newZ = z + 1.5f;
        return GetHexagonFromMap(newX, newZ);
    }
    private HexagoneGenerator FindE(float x, float z)
    {
        float newX = x + 2;
        float newZ = z;
        return GetHexagonFromMap(newX, newZ);
    }
    private HexagoneGenerator FindSE(float x, float z)
    {
        float newX = x + 1;
        float newZ = z - 1.5f;
        return GetHexagonFromMap(newX, newZ);
    }
    private HexagoneGenerator FindSO(float x, float z)
    {
        float newX = x - 1;
        float newZ = z - 1.5f;
        return GetHexagonFromMap(newX, newZ);
    }
    private HexagoneGenerator FindO(float x, float z)
    {
        float newX = x - 2;
        float newZ = z ;
        return GetHexagonFromMap(newX, newZ);
    }
    private HexagoneGenerator FindNO(float x, float z)
    {
        float newX = x - 1;
        float newZ = z + 1.5f;
        return GetHexagonFromMap(newX, newZ);
    }
    

    private HexagoneGenerator GetHexagonFromMap(float x, float z)
    {
        if (!_map.ContainsKey(x) || !_map[x].ContainsKey(z))
        {
            return null;
        }

        return _map[x][z];
    }
}
