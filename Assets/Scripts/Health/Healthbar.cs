using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Health PlayerHealth;
    public Image TotalHealthBar;
    public Image CurrentHealthBar;

    // Start is called before the first frame update
    void Start()
    {
        TotalHealthBar.fillAmount = PlayerHealth.CurrentHealth / 10;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentHealthBar.fillAmount = PlayerHealth.CurrentHealth / 10;
    }
}
