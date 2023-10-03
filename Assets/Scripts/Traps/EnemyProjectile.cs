
using UnityEngine;

public class EnemyProjectile : EnemyDamage  // Will damage the player every time the player touches the object
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifetime;

    public void ActivateProjectile()
    {
        lifetime = 0;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed,0, 0);

        lifetime += Time.deltaTime;

        if (lifetime > resetTime)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision); // Execute logic from parent script
        gameObject.SetActive(false);  // Deativates the arrow when it hits another object
    }
}
