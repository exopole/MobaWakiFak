using System;
using System.Collections;
using System.Linq;
using Controller.Player;
using ScriptableObjects;
using Terrain;
using UnityEngine;

namespace Controller
{
    public class GameController : MonoBehaviour
    {
        public PlayerController Player, Bot;
        [SerializeField] private GameSettings _Settings;
        [SerializeField] private MunitionSo _MunitionSo;
        [SerializeField] private Transform _MunitionsParent;
        private TerrainGenerator _terrainGenerator;
        
        private IEnumerator Start()
        {
            _terrainGenerator = FindObjectOfType<TerrainGenerator>();
            _terrainGenerator.SetupTerrain();
            _terrainGenerator.RemoveRandomeHexagones(_Settings.NumberOfHole);
            yield return null;
            if (_terrainGenerator.Hexagones.Count < 0)
            {
                Debug.LogError("GameController => Terrain generation failed");
                yield break;
            }
            
            InitializePlayer(player:Player);
            InitializePlayer(player:Bot);

            yield return null;

            AddMunition(_Settings.NumberOfMunition);
        }

        private void InitializePlayer(PlayerController player)
        {
            HexagoneGenerator hexSpawnPlayer = _terrainGenerator.Hexagones.Where(h => h.gameObject.activeInHierarchy).OrderBy(h => Vector3.Distance(player.PhysicsTransform.position, h.transform.position)).ToArray()[0];
            if (hexSpawnPlayer != null)
            {
                player.Initialization(hexSpawnPlayer.transform.position, _Settings.GaugeMax);
            }
        }
        
        private void AddMunition(int range)
        {
            var hexagonRandom = _terrainGenerator.GetRandomHex(range);
            foreach (var hex in hexagonRandom)
            {
                AddMunition(hex);
            }
        }

        private void AddMunition(HexagoneGenerator hex)
        {
            var position = hex.transform.position;
            hex.HasElement = true;
            var munition = _MunitionSo.GetRandomMunitions(new Vector2(position.x, position.z), UseMunition, _MunitionsParent);
            munition.HexCurrent = hex;
        }

        private void UseMunition()
        {
            StartCoroutine(UseMunitionCoroutine());
        }

        private IEnumerator UseMunitionCoroutine()
        {
            yield return null;
            AddMunition(1);
        }
    }
}