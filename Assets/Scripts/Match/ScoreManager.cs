using TMPro;
using UnityEngine;

namespace Match
{
    public class ScoreManager : MonoBehaviour
    {
        private int _score = 0;
        private string _scoreTextFormat = "Score: {0}";

        public TMP_Text scoreText; // Reference to the TMP_Text component

        public delegate void ScoreUpdated(int newScore);
        public event ScoreUpdated OnScoreUpdated;

        // AddScore method to increase the score
        public void AddScore(int scoreToAdd)
        {
            if (scoreToAdd <= 0)
            {
                Debug.LogWarning("Attempted to add a non-positive score.");
            }

            _score += scoreToAdd;
            Debug.Log($"Score added: {scoreToAdd}, Total Score: {_score}");

            OnScoreUpdated?.Invoke(_score);  // Notify listeners that the score has been updated
            UpdateScoreUI(); // Update the score UI
        }

        // ResetScore method to reset the score to 0
        public void ResetScore()
        {
            _score = 0;
            Debug.Log("Score reset to 0.");

            OnScoreUpdated?.Invoke(_score);  // Notify listeners of score reset
            UpdateScoreUI(); // Update the score UI
        }

        // GetScore method to return the current score
        public int GetScore()
        {
            return _score;
        }

        // Method to update the TMP_Text with the latest score
        private void UpdateScoreUI()
        {
            if (scoreText != null)
            {
                scoreText.text = string.Format(_scoreTextFormat, _score);
                Debug.Log($"Score UI updated: {scoreText.text}");
            }
            else
            {
                Debug.LogWarning("scoreText is not assigned!");
            }
        }

        // Optional: Start method for debugging initialization
        private void Start()
        {
            if (scoreText == null)
            {
                Debug.LogWarning("scoreText is not assigned in the inspector.");
            }
        }
    }
}
