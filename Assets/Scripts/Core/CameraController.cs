
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Room camera
    //[SerializeField] private float speed;
    //private float currentPosX;
    //private Vector3 velocity = Vector3.zero;

    //Follow player (Player camera)
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    private float lookAhead;

    // Update is called once per frame
    private void Update()
    {
        //Room camera
        //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z), ref velocity, speed);

        //Follow player
        transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z); //Moves the camera
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);
    }

    // Not necesary when using the traditional camera
/*     public void MoveToNewRoom(Transform _newRoom)
    {
        currentPosX = _newRoom.position.x;
    } */
}
