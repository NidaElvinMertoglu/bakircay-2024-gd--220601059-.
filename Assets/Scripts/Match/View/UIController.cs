using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Match.View
{
    public class UIController : MonoBehaviour
    {
        public TMP_Text scoreText;
        public Button mixSkillButton;
        public Button easyMatchSkillButton;
        public Button doubleItSkillButton;
        public Button resetSkillButton;

        private ScoreManager _scoreManager;
        private ItemSpawner _itemSpawner;

        public void Initialize(ItemSpawner itemSpawner, ScoreManager scoreManager)
        {
            _itemSpawner = itemSpawner;
            _scoreManager = scoreManager;

            _scoreManager.OnScoreUpdated += UpdateScoreUI;  // Subscribe to score updates
            SetInitialValues();
            GameEvents.OnItemsSpawned += SetInitialValues;
        }

        private void OnDestroy()
        {
            _scoreManager.OnScoreUpdated -= UpdateScoreUI;  // Unsubscribe from score updates
            GameEvents.OnItemsSpawned -= SetInitialValues;
        }

        private void SetInitialValues()
        {
            EnableAllSkillButtons();
        }

        public void OnItemMatched(ItemData data)
        {
            Debug.LogWarning(data.itemName);
            _scoreManager.AddScore(data.itemScore);  // Add score through ScoreManager
        }

        public void UpdateScoreUI(int newScore)
        {
            scoreText.text = string.Format("Score: {0}", newScore);  // Update UI when score changes
        }

        private void EnableAllSkillButtons()
        {
            mixSkillButton.interactable = true;
            easyMatchSkillButton.interactable = true;
            doubleItSkillButton.interactable = true;
            resetSkillButton.interactable = true;
        }

        public void OnMixSkillButtonClick()
        {
            mixSkillButton.interactable = false;
            GameEvents.OnMixSkillUsed?.Invoke();
            Debug.Log("Mix Skill Button Clicked");
        }

        public void OnEasyMatchSkillButtonClick()
        {
            easyMatchSkillButton.interactable = false;
            GameEvents.OnEasyMatchSkillUsed?.Invoke();
        }

        public void OnDoubleItSkillButtonClick()
        {
            doubleItSkillButton.interactable = false;
            GameEvents.OnDoubleItSkillUsed?.Invoke();
        }

        public void OnResetSkillButtonClick()
        {
            resetSkillButton.interactable = false;
            GameEvents.OnResetSkillUsed?.Invoke();
        }

        public void ResetScore()
        {
            _scoreManager.ResetScore();  // Reset score through ScoreManager
        }
    }
}
