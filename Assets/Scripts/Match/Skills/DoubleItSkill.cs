using Match.View;
using UnityEngine;

namespace Match
{
    public class DoubleItSkill : MonoBehaviour
    {
        private ScoreManager _scoreManager;
        private UIController _uiController;

        public void Initialize(UIController uiController, ScoreManager scoreManager)
        {
            _uiController = uiController;
            _scoreManager = scoreManager;
        }

        public void UseDoubleItSkill()
        {
            if (_uiController == null || _scoreManager == null)
            {
                Debug.LogWarning("UIController or ScoreManager is not assigned.");
                return;
            }

            // Mevcut skoru al
            int currentScore = _scoreManager.GetScore();

            // Skoru ikiyle çarp
            int doubledScore = currentScore * 2;

            // Skoru güncelle
            _scoreManager.AddScore(doubledScore); // Add the doubled score through ScoreManager

            // Disable the button after using the skill
            _uiController.doubleItSkillButton.interactable = false;
        }
    }
}
