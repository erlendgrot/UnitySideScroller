
using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Use CompareTag for better performance
        {
            uiManager.levelComplete();
        }
    }
}
