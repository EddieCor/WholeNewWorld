using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10.0f;
    public float jumpPower = 10.0f;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
  
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D collider;
    private float wallJumpCooldown;
    private float HorizontalMovement;

    private void Awake()
    {
        //Referencias para Rigidbody, Animador y Collider
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalMovement = Input.GetAxis("Horizontal");

       //Gira el modelo del personaje a izquierda o derecha
        if(HorizontalMovement > 0.01f)
        {
            transform.localScale = new Vector3(2.5f,2.5f,2.5f);
        }
        else if (HorizontalMovement < -0.01f)
        {
            transform.localScale = new Vector3(-2.5f, 2.5f, 2.5f);
        }

        //Parametros para animador
        anim.SetBool("Run", HorizontalMovement != 0);
        anim.SetBool("Grounded", isGrounded());

        //Logica de WallJump
        if(wallJumpCooldown > 0.2f)
        {
            rb.velocity = new Vector2(HorizontalMovement * speed, rb.velocity.y);

            if(onWall() && !isGrounded())
            {
                rb.gravityScale = 0;
                rb.velocity = Vector2.zero;
            }
            else
            {
                rb.gravityScale = 2.5f;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }
        }
        else
        {
            wallJumpCooldown += Time.deltaTime;
        }
    }

    private void Jump()
    {
        if (isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            anim.SetTrigger("Jump");
        }
        else if(onWall() && !isGrounded())
        {
            if(HorizontalMovement == 0)
            {
                rb.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                rb.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            }
            wallJumpCooldown = 0;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0, Vector2.down, 0.01f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.01f, wallLayer);
        return raycastHit.collider != null;
    }
}
