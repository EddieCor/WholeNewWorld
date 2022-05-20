using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Point")]
    public Transform leftEdge;
    public Transform rightEdge;

    [Header("Enemy")]
    public Transform enemy;

    [Header("Movement Parameters")]
    public float speed;
    private Vector3 InitialScale;
    private bool movingLeft;

    [Header("Idle Behavior")]
    public float IdleDuration;
    private float idleTimer;

    [Header("Animator")]
    public Animator anim;

    private void Awake()
    {
        InitialScale = enemy.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(movingLeft)
        {
            if(enemy.position.x >= leftEdge.position.x)
            {
                MoveInDirection(-1);
            }
            else
            {
                //Cambiar dirección
                DirectionChange();
            }
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
            {
                MoveInDirection(1);
            }
            else
            {
                //Cambiar dirección
                DirectionChange();
            }
        }
    }

    private void DirectionChange()
    {
        anim.SetBool("Moving", false);

        idleTimer += Time.deltaTime;

        if(idleTimer > IdleDuration)
        {
            movingLeft = !movingLeft;
        }
    }

    private void MoveInDirection(int direction)
    {
        idleTimer = 0;
        anim.SetBool("Moving", true);
        
        //Enemigo vera en una dirección
        enemy.localScale = new Vector3(Mathf.Abs(InitialScale.x) * direction, InitialScale.y, InitialScale.z);

        //Enemigo se movera en esa dirección
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direction * speed, enemy.position.y, enemy.position.z);
    }

    private void OnDisable()
    {
        anim.SetBool("Moving",false);
    }

    // Start is called before the first frame update
    void Start()
    {

    }
}
