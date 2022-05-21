using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public AudioClip CPsound;
    private Transform currentCP;
    private Health playerHealth;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
    }

    public void Respawn()
    {
        transform.position = currentCP.position; //movemos al jugador a la posción del CheckPoint
        //Reiniciamos vida y animación del jugador
        playerHealth.Respawn();

        //Mover camara a CheckPoint
        Camera.main.GetComponent<CameraController>().MoveToNewRoom(currentCP.parent);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Checkpoint")
        {
            currentCP = collision.transform;
            SoundManager.Instance.PlaySound(CPsound);
            collision.GetComponent<Collider2D>().enabled = false; //Desactivar collider de CheckPoint
            collision.GetComponent<Animator>().SetTrigger("Appear");
        }
    }  
}
