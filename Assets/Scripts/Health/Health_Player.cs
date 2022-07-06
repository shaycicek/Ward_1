using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Player : Health
{
    public GameObject panel;
    public override void OnDeath()
    {
        base.OnDeath();
        gameObject.SetActive(false);
        Time.timeScale = 0;
        panel.SetActive(true);

    }

}
