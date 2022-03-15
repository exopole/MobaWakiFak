using System;
using System.Collections;
using System.Linq;
using Controller.Player;
using ScriptableObjects;
using Terrain;
using UI;
using UnityEngine;

namespace Controller
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance;
        public PlayerController Player, Bot;
        [SerializeField] private GameSettings _Settings;
        [SerializeField] private MunitionSo _MunitionSo;
        [SerializeField] private Transform _MunitionsParent;
        
        private TerrainGenerator _terrainGenerator;
        private IAController _ia;
        private ProjectilesController _projectilesController;
        private Timer _timer;
        private Gauge _gauge;
        private EndGame _endGame;
        
        public TerrainGenerator TerrainGenerator => _terrainGenerator;

        #region Unity Methods
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        private void Start()
        {
            Player.OnAddMunition += () => _gauge.AddMunition(percentageFill: (float)Player.CurrentMunition / _Settings.GaugeMax);
            if (!_projectilesController)
            {
                _projectilesController = FindObjectOfType<ProjectilesController>();
            }

            if (!_timer)
            {
                _timer = FindObjectOfType<Timer>();
                _timer.OnEndTimer += EndGame;
            }

            if (!_terrainGenerator)
            {
                _terrainGenerator = FindObjectOfType<TerrainGenerator>();
            }
            
            
            if (!_gauge)
            {
                _gauge = FindObjectOfType<Gauge>();
            }

            if (!_endGame)
            {
                _endGame = FindObjectOfType<EndGame>(true);
            }

            if (!_ia)
            {
                _ia = FindObjectOfType<IAController>(true);
            }
            Initialize();
        }


        #endregion
        private void EndGame()
        {
            _endGame.gameObject.SetActive(true);
            Player.StopAllCoroutines();
            Player.Behaviour.StopAllCoroutines();
            Player.Spawn();
            Bot.StopAllCoroutines();
            Bot.Behaviour.StopAllCoroutines();
            _ia.Stop();
            _ia.StopAllCoroutines();
            _ia.Clean();
            Bot.Spawn();
        }

        public void Fire(PlayerController player)
        {
            _projectilesController.Fire(player);
            if (player == Player)
            {
                ResetMunitionPlayer();
            }
        }

        public void ResetMunitionPlayer()
        {
            _gauge.ResetMunition();
        }

        public void Initialize()
        {
            _terrainGenerator.SetupTerrain();
            _terrainGenerator.RemoveRandomeHexagones(_Settings.NumberOfHole);

            _endGame.gameObject.SetActive(false);

            StartCoroutine(InitializeCoroutine());
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

        private IEnumerator InitializeCoroutine()
        {
            yield return null;
            if (_terrainGenerator.Hexagones.Count < 0)
            {
                Debug.LogError("GameController => Terrain generation failed");
                yield break;
            }
            
            InitializePlayer(player:Player);
            InitializePlayer(player:Bot);

            yield return null;

            _MunitionSo.Clear();
            AddMunition(_Settings.NumberOfMunition);

            yield return null;
            _timer.StartTimer();

            yield return null;
            _ia.Initialize(_terrainGenerator.Hexagones);
        }

        private IEnumerator UseMunitionCoroutine()
        {
            yield return null;
            AddMunition(1);
        }
    }
}