using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Parameters")]
    public float speed;
    public float jumpPower;

    [Header("Layers")]
    public LayerMask groundLayer;
    public LayerMask wallLayer;

    [Header("Dashing")]
    private bool canDash = true;
    private bool isDashing;
    public float DashingPower = 24f;
    public float DashingTime = 0.2f;
    public float DashingCooldown = 1;

    public TrailRenderer tr;

    [Header("Sounds")]
    public AudioClip jumpSound;

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

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            return;
        }

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
        if (wallJumpCooldown > 0.2f)
        {
            rb.velocity = new Vector2(HorizontalMovement * speed, rb.velocity.y);

            if (onWall() && !isGrounded())
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

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SoundManager.Instance.PlaySound(jumpSound);
                }
            }
        }
        else
        {
            wallJumpCooldown += Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.Z) && canDash)
        {
            anim.SetTrigger("Dash");
            StartCoroutine(Dash());
        }
    }

    private void Jump()
    {
        if (isGrounded())
        {            
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            SoundManager.Instance.PlaySound(jumpSound);
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

    public bool canAttack()
    {
        return HorizontalMovement == 0 && isGrounded() && !onWall();
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * DashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(DashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(DashingCooldown);
        canDash = true;
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
    }
}
