
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies; //array of enemies inside of a room
    private Vector3[] initPosition;

    private void Awake()
    {
        initPosition = new Vector3[enemies.Length];

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
                initPosition[i] = enemies[i].transform.position;
        }
    }

    public void ActivateRoom(bool _status)
    {
        //Activate/deactivate enemies
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                enemies[i].SetActive(_status);
                enemies[i].transform.position = initPosition[i];
            }
                
        }
    }
}
