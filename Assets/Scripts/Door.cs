using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform previousRoom;
    public Transform nextRoom;
    public CameraController camcont;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(collision.transform.position.x < transform.position.x)
            {
                camcont.MoveToNewRoom(nextRoom);
            }
            else
            {
                camcont.MoveToNewRoom(previousRoom);
            }
        }
    }
}
