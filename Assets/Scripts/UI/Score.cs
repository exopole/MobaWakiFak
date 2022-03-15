using System;
using Controller;
using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class Score : MonoBehaviour
    {
        private TextMeshProUGUI _text;
        private string _format = "Player {0} : {1} Bot";
        private GameController _gameController;
        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            _gameController = GameController.Instance;
        }

        private void Update()
        {
            _text.text = string.Format(_format, _gameController.Player.Score, _gameController.Bot.Score);
        }
    }
}