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
        /// Calls IncrementScoreInternal.
        /// </summary>
        public static void IncrementScore()
        {
            Singleton.IncrementScoreInternal();
        }

        /// <summary>
        /// Updates the score text when an enemy is killed. Also calls the GameOver method if the player has killed all 
        /// enemies.
        /// </summary>
        private void IncrementScoreInternal()
        {
            _score++;
            scoreText.text = $"Score: {_score}";
            
            if (_score == _numEnemies)
            {
                GameOver(true);
            }
        }

        /// <summary>
        /// Calls SetHealthInternal and passes the specified health.
        /// </summary>
        /// <param name="health">The player's current health.</param>
        public static void SetHealth(int health)
        {
            Singleton.SetHealthInternal(health);
        }

        /// <summary>
        /// Updates the health text to the specified value.
        /// </summary>
        /// <param name="health">The player's current health.</param>
        private void SetHealthInternal(int health)
        {
            healthText.text = $"Health: {health}";
        }
        
        /// <summary>
        /// Calls GameOverInternal and passes the specified win value.
        /// </summary>
        /// <param name="win">True if the player won (all enemies destroyed), false if the player lost (the player
        /// died).</param>
        public static void GameOver(bool win)
        {
            Singleton.GameOverInternal(win);
        }

        /// <summary>
        /// Updates the game over text to either "You Win!" or "You Lose!".
        /// </summary>
        /// <param name="win">True if the player won (all enemies destroyed), false if the player lost (the player
        /// died).</param>
        private void GameOverInternal(bool win)
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