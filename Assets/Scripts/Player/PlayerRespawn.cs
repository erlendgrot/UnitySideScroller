
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    private Transform currentCheckpoint; //To store our last checkpoint
    private Health playerHealth;
    private UIManager uiManager;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        uiManager = FindObjectOfType<UIManager>(); // Returs the first active loaded object of type UIManager
    }

    public void CheckRespawn()
    {
        // Check if checkpoint is available. If not -> game over
        if (currentCheckpoint == null)
        {
            // Show the game over screen
            uiManager.GameOver();

            return; //Dont execute the rest of the function
        }

        transform.position = currentCheckpoint.position; //Move player to checkpoint position
        playerHealth.Respawn(); // To restore player health and animation

        // Move the camera to the checkpoint room
        Camera.main.GetComponent<CameraController>().MoveToNewRoom(currentCheckpoint.parent);
    }

    // Activate checkpoints
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform; //Store the checkpoint we activated as the current checkpoint
            SoundManager.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false; // Deactivate checkpoint collider
            collision.GetComponent<Animator>().SetTrigger("appear"); //Trigger the checkpoint animation
        }
    }
}
