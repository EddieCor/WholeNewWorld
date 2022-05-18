using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float StartingHealth;
    public float CurrentHealth { get; private set; }

    private Animator anim;
    private bool dead;


    private void Awake()
    {
        CurrentHealth = StartingHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, StartingHealth);

        if(CurrentHealth > 0)
        {
            //jugador herido
            anim.SetTrigger("Hurt");
            //iframes
        }
        else
        {
            //jugador derrotado
            if (!dead)
            {
                anim.SetTrigger("Die");
                GetComponent<PlayerMovement>().enabled = false;
                dead = true;
            }
        }
    }

    public void AddHealth(float value)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + value, 0, StartingHealth);
    }
}
