using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    public float StartingHealth;
    public float CurrentHealth { get; private set; }

    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    public float iFramesDuration;
    public int FlashNumber;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    public Behaviour[] components;

    [Header("Sounds")]
    public AudioClip deathSound;
    public AudioClip hurtSound;



    private void Awake()
    {
        CurrentHealth = StartingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, StartingHealth);

        if(CurrentHealth > 0)
        {
            //jugador herido
            anim.SetTrigger("Hurt");
            //iframes
            StartCoroutine(Invulnerability());
            SoundManager.Instance.PlaySound(hurtSound);
        }
        else
        {
            //jugador derrotado
            if (!dead)
            {
                anim.SetTrigger("Die");

                //Jugador
                if (GetComponent<PlayerMovement>() != null)
                {
                    //GetComponent<PlayerMovement>().enabled = false;
                }

                //Enemigo
                if (GetComponentInParent<EnemyPatrol>() != null)
                {
                    GetComponentInParent<EnemyPatrol>().enabled = false;
                    GetComponent<Enemy>().enabled = false;
                }

                dead = true;
                SoundManager.Instance.PlaySound(deathSound);
            }
        }
    }

    public void AddHealth(float value)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + value, 0, StartingHealth);
    }

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(8, 9, true);
        for (int i = 0; i < FlashNumber; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration/ (FlashNumber * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (FlashNumber * 2));
        }
        //Duraci�n invulnerabilidad
        Physics2D.IgnoreLayerCollision(8, 9, false);

    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void Respawn()
    {
        dead = false;
        AddHealth(StartingHealth);
        anim.ResetTrigger("Die");
        anim.Play("Idle");
        StartCoroutine(Invulnerability());

        foreach(Behaviour component in components)
        {
            component.enabled = true;
        }
    }
}
