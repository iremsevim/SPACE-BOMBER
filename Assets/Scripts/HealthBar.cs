using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    
    public int health;
    public int maxhealth = 100;
    public System.Action OnDamaged;
    public System.Action OnDead;
    public bool IsDead = false;

    public void HealthDamage(int damageamount)
    {
        if (IsDead) return;
        health -= damageamount;
        OnDamaged?.Invoke();
      
        if(health<=0)
        {
            OnDead?.Invoke();
            IsDead = true;

        }
    }
    public float HealthAmount
    {
      get
        {
            return (float)health / (float)maxhealth;
        }
    }
}
