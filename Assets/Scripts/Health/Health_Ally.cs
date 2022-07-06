using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Ally : Health
{
    public override void OnDeath()
    {
        base.OnDeath();
        Destroy(gameObject ,1f);
    }
}
