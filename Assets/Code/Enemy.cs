using UnityEngine;

namespace Code
{
    public class Enemy : MonoBehaviour
    {
        /// <summary>
        /// The speed at which the enemy moves downward.
        /// </summary>
        public float speed = 0.5f;

        /// <summary>
        /// The enemy's health.
        /// </summary>
        public float health = 10f;

        /// <summary>
        /// The amount of damage the enemy deals.
        /// </summary>
        public int damage = 10;

        /// <summary>
        /// The amount of time between enemy shots in seconds.
        /// </summary>
        public float shootingInterval = 5f;

        /// <summary>
        /// The bullet prefab that will be instantiated when the enemy shoots.
        /// </summary>
        public GameObject bulletPrefab;

        /// <summary>
        /// The Rigidbody2D component of the enemy.
        /// </summary>
        private Rigidbody2D _rb;
        
        /// <summary>
        /// Gets the Rigidbody2D component, starts moving the enemy downward slowly, and starts invoking Shoot
        /// repeatedly.
        /// </summary>
        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _rb.velocity = Vector2.down * speed;
            
            // we want to start shooting bullets after a random initial delay
            InvokeRepeating(nameof(Shoot), Random.Range(0, shootingInterval), shootingInterval);
        }

        /// <summary>
        /// Instantiates a bullet at the enemy's position.
        /// </summary>
        private void Shoot()
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        }

        /// <summary>
        /// Takes damage and destroys the enemy if health is 0 or less. Also calls the IncrementScore method of UI if 
        /// the enemy is destroyed.
        /// </summary>
        /// <param name="damage">The amount of damage the enemy will take.</param>
        private void TakeDamage(int damage)
        {
            health -= damage;
            
            if (health <= 0)
            {
                UI.IncrementScore();
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Checks if a player bullet collided with the enemy. If so, the enemy takes damage and the bullet is
        /// destroyed.
        /// </summary>
        /// <param name="other">The object that collided with the enemy.</param>
        private void OnTriggerEnter2D(Collider2D other)
        {
            var bullet = other.GetComponent<Bullet>();
            if (bullet != null && !bullet.isEnemyBullet)
            {
                TakeDamage(bullet.damage);
                Destroy(other.gameObject);
            }
        }
    }
}