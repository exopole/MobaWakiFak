using System;
using System.Collections;
using System.Linq;
using Controller.Player;
using Terrain;
using UnityEngine;

namespace Controller
{
    public class GameController : MonoBehaviour
    {
        public PlayerController Player;

        private TerrainGenerator _terrainGenerator;
        private IEnumerator Start()
        {
            _terrainGenerator = FindObjectOfType<TerrainGenerator>();
            _terrainGenerator.SetupTerrain();
            yield return null;

            if (_terrainGenerator.Hexagones.Count > 0)
            {
                HexagoneGenerator hex = _terrainGenerator.Hexagones.OrderBy(h => Vector3.Distance(Player.transform.position, h.transform.position)).ToArray()[0];
                if (hex != null)
                {
                    Player.Initialization(hex.transform.position);
                }
            }
        }
    }
}