using System;
using UnityEngine;

public class HealthSystem
{
    // @author rasmushy
    // event handler for healthbar updates
    public event EventHandler OnHealthChanged;
    private float health;
    private float healthMax;

    // Everytime we want to get health for something we create healthsystem for it
    // We use this at Start() = healthsystem = new HealthSystem(Data_maxHealth) 
    public HealthSystem(float healthMax)
    {
        this.healthMax = healthMax;
        health = healthMax;
    }

    public float GetHealth()
    {
        return health;
    }

    public float GetHealthPercent()
    {
        return health / healthMax;
    }

    public void Damage(float damageAmount)
    {
        health -= damageAmount;
        if (health < 0) health = 0;
        if(OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }

    public void Heal(float healAmount)
    {
        health += healAmount;
        if (health > healthMax) health = healthMax;
        if(OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }

}

