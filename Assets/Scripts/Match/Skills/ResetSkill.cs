using UnityEngine;
using UnityEngine.SceneManagement;

namespace Match
{
    public class ResetSkill : MonoBehaviour
    {
        private ItemSpawner _itemSpawner;

        // Initialize method to pass references
        public void Initialize(ItemSpawner itemSpawner)
        {
            _itemSpawner = itemSpawner;
        }

        // Method to reset the game
        public void ResetGame()
        {
            // Reset the score (directly inside ScoreManager, or however you handle score)
            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
            if (scoreManager != null)
            {
                scoreManager.ResetScore(); // Reset the score
            }

            // Reload the current scene to reset everything
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name); // This will reload the current scene and reset everything
        }
    }
}
