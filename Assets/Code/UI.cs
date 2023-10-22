using TMPro;
using UnityEngine;

namespace Code
{
    public class UI : MonoBehaviour
    {
        /// <summary>
        /// There will only ever be one UI object, so we just store it in a static field so we don't have to call
        /// FindObjectOfType(), which is expensive.
        /// </summary>
        public static UI Singleton;
        
        /// <summary>
        /// Displays the player's current score (how many enemies they've killed).
        /// </summary>
        public TextMeshProUGUI scoreText;
        
        /// <summary>
        /// Display's the player's current health.
        /// </summary>
        public TextMeshProUGUI healthText;
        
        /// <summary>
        /// Displays either win text or lose text.
        /// </summary>
        public TextMeshProUGUI gameOverText;

        /// <summary>
        /// The number of enemies in the level. Used to determine if the player has won.
        /// </summary>
        private int _numEnemies = -1;

        /// <summary>
        /// The player's current score.
        /// </summary>
        private int _score = 0;

        /// <summary>
        /// Assigns this UI object to the Singleton field. Also finds the number of enemies in the level. Also sets the 
        /// score text to 0. Also sets the game over text to an empty string.
        /// </summary>
        private void Start()
        {
            Singleton = this;
            _numEnemies = FindObjectsOfType<Enemy>().Length;
            scoreText.text = "Score: 0";
            gameOverText.text = "";
        }
        
        /// <summary>
        /// Updates the score text when an enemy is killed. Also calls the GameOver method if the player has killed all 
        /// enemies.
        /// </summary>
        public void IncrementScore()
        {
            _score++;
            scoreText.text = $"Score: {_score}";
            
            if (_score == _numEnemies)
            {
                GameOver(true);
            }
        }

        /// <summary>
        /// Updates the health text to the specified value.
        /// </summary>
        /// <param name="health">The player's current health.</param>
        public void SetHealth(int health)
        {
            healthText.text = $"Health: {health}";
        }
        
        /// <summary>
        /// Updates the game over text to either "You Win!" or "You Lose!".
        /// </summary>
        /// <param name="win">True if the player won (all enemies destroyed), false if the player lost (the player
        /// died).</param>
        public void GameOver(bool win)
        {
            if (win)
            {
                gameOverText.text = "You Win!";
            }
            else
            {
                gameOverText.text = "You Lose!";
            }
        }
    }
}