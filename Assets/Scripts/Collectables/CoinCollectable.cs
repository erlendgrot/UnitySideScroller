using UnityEngine;

public class CoinCollectable : MonoBehaviour
{
    [SerializeField] private AudioClip coinCollectableSound;
    [SerializeField] private float moveSpeed = 1.0f; // Speed of the up and down movement
    [SerializeField] private float moveDistance = 0.2f; // Maximum distance to move up and down

    [SerializeField] private CoinText numberOfCoins;

    private Vector3 initialPosition;

    private void Awake()
    {
        initialPosition = transform.position; // Store the initial position of the coin
    }

    private void Update()
    {
        // Calculate the new Y position based on a sine wave
        float newY = initialPosition.y + Mathf.Sin(Time.time * moveSpeed) * moveDistance;

        // Update the coin's position
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Use CompareTag for better performance
        {
            SoundManager.instance.PlaySound(coinCollectableSound);
            numberOfCoins.UpdateNumberOfCoins();
            gameObject.SetActive(false); // Disable the coin-collectable
        }
    }
}
