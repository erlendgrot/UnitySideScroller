using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectable : MonoBehaviour
{
    [SerializeField] private float healthValue;
    [SerializeField] private AudioClip healthCollectableSound;

    // Gets called when we have a collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") // Check for collision with the player
        {
            SoundManager.instance.PlaySound(healthCollectableSound);
            collision.GetComponent<Health>().IncreaseHealth(healthValue);
            gameObject.SetActive(false); // Disable the health-collectable
        }
    }
}
