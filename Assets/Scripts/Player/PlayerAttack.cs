using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Parameters")]
    public float AttackCooldown;
    public float range;
    public int damage;

    [Header("Collider Parameters")]
    public float colliderDistance;
    public BoxCollider2D bc;

    [Header("Enemy Layer")]
    public LayerMask enemyLayer;
    private float CoolDownTimer = Mathf.Infinity;

    [Header("Attack Sound")]
    public AudioClip attackSound;



    //References
    private Animator animator;
    private Health enemyHealth;

    private PlayerMovement playerMovement;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X) && CoolDownTimer > AttackCooldown && playerMovement.canAttack())
        {
            Attack();
        }
        
        CoolDownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        CoolDownTimer = 0;
        animator.SetTrigger("Attack");
        SoundManager.Instance.PlaySound(attackSound);
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(bc.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(bc.bounds.size.x * range, bc.bounds.size.y, bc.bounds.size.z),
            0, Vector2.left, 0, enemyLayer);

        if (hit.collider != null)
        {
            enemyHealth = hit.transform.GetComponent<Health>();
        }

        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(bc.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(bc.bounds.size.x * range, bc.bounds.size.y, bc.bounds.size.z));
    }

    private void DamageEnemy()
    {
        if (PlayerInSight())
        {
            enemyHealth.TakeDamage(damage);
        }
    }
}
