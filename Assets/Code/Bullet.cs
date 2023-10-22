using UnityEngine;

namespace Code
{
    public class Bullet : MonoBehaviour
    {
        /// <summary>
        /// The speed at which the bullet moves.
        /// </summary>
        public float speed = 5f;

        /// <summary>
        /// The damage the bullet deals.
        /// </summary>
        public int damage = 5;

        /// <summary>
        /// True if the bullet is being shot by an enemy, false if it is being shot by the player.
        /// </summary>
        public bool isEnemyBullet = false;

        /// <summary>
        /// The Rigidbody2D component of the bullet.
        /// </summary>
        private Rigidbody2D _rb;
        
        /// <summary>
        /// Initializes the Rigidbody2D component and sets the velocity so that the bullet moves immediately upon
        /// being instantiated.
        /// </summary>
        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            
            if (isEnemyBullet)
            {
                _rb.velocity = Vector2.down * speed;
            }
            else
            {
                _rb.velocity = Vector2.up * speed;
            }
        }

        /// <summary>
        /// Checks if a bullet of the opposite type has collided with this bullet. If so, destroys both bullets.
        /// </summary>
        /// <param name="other">The object colliding with the bullet.</param>
        private void OnTriggerEnter2D(Collider2D other)
        {
            var bullet = other.GetComponent<Bullet>();
            if (bullet != null)
            {
                if ((isEnemyBullet && !bullet.isEnemyBullet) || (!isEnemyBullet && bullet.isEnemyBullet))
                {
                    Destroy(bullet.gameObject);
                    Destroy(gameObject);
                }
            }
        }

        /// <summary>
        /// Destroys the bullet if it goes off screen.
        /// </summary>
        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
    }
}