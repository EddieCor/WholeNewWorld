using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Camara de habitación. Comenta lo de la camara que sigue al jugador si quieres esta opción
    public float speed;
    private float currentPositionX;
    private Vector3 velocity = Vector3.zero;

    //Camara sigue a jugador
    //public Transform player;
    //public float aheadDistance;
    //public float cameraSpeed;
    //private float lookAhead;

    // Update is called once per frame
    void Update()
    {
        //Camara de habitación
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPositionX, transform.position.y, transform.position.z), ref velocity, speed);

        //Camara sigue a jugador
        //transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
        //lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);
    }

    public void MoveToNewRoom(Transform newRoom)
    {
        currentPositionX = newRoom.position.x;
    }
}
