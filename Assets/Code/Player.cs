using UnityEngine;

namespace Code
{
    public class Player : MonoBehaviour
    {
        /// <summary>
        /// The distance the player travels when moving left or right.
        /// </summary>
        public float speed = 2f;

        /// <summary>
        /// The player's health.
        /// </summary>
        public int health = 25;

        /// <summary>
        /// The bullet prefab that will be instantiated when the player shoots.
        /// </summary>
        public GameObject bulletPrefab;

        /// <summary>
        /// Listens for input and moves the player left or right accordingly, in increments per key press. Also listens 
        /// for the space bar and shoots a bullet when pressed.
        /// </summary>
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.position += Vector3.left * speed;
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.position += Vector3.right * speed;
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
            }
        }

        /// <summary>
        /// Instantiates a bullet at the player's position.
        /// </summary>
        private void Shoot()
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        }
        
        /// <summary>
        /// Takes damage and destroys the player if health is 0 or less. Also calls the GameOver method in the UI if
        /// the player dies.
        /// </summary>
        /// <param name="damage">The amount of damage the player will take.</param>
        private void TakeDamage(int damage)
        {
            health -= damage;
            UI.Singleton.SetHealth(health);
            
            if (health <= 0)
            {
                UI.Singleton.SetHealth(0);
                UI.Singleton.GameOver(false);
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Checks if an enemy bullet collided with the player. If so, the player takes damage and the bullet is
        /// destroyed. Also checks if an enemy collided with the player. If so, the player takes damage and the enemy
        /// is destroyed.
        /// </summary>
        /// <param name="other">The object that collided with the player.</param>
        private void OnTriggerEnter2D(Collider2D other)
        {
            var bullet = other.GetComponent<Bullet>();
            if (bullet != null && bullet.isEnemyBullet)
            {
                TakeDamage(bullet.damage);
                Destroy(other.gameObject);
            }
            
            var enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                TakeDamage(enemy.damage);
                Destroy(other.gameObject);
            }
        }
    }
}