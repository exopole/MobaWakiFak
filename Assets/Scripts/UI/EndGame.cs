using System;
using Controller;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class EndGame : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _TextResult;
        [SerializeField] private Button _ButtonRetry;

        private string _format = "{0} gagne avec {1} points";
        
        private void Start()
        {
            _ButtonRetry.onClick.AddListener(GameController.Instance.Initialize);
        }

        private void OnEnable()
        {
            int botScore = GameController.Instance.Bot.Score;
            int playerScore = GameController.Instance.Player.Score;

            if (botScore > playerScore)
            {
                _TextResult.text = string.Format(_format, "bot", botScore);
            }
            else if (playerScore > botScore)
            {
                _TextResult.text = string.Format(_format, "player", playerScore);
            }
            else
            {
                _TextResult.text = "Egalité";
            }
        }
    }
}