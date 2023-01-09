using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct HealthSubsystem
{
    [HideInInspector] public float health;
    public float maxHealth;
    GameObject gameObject;
    System.Action Die;

    public void Start(GameObject gameObject, System.Action action)
    {
        this.gameObject = gameObject;
        health = maxHealth;
        Die = action;
    }

    public void Damage(float damage)
    {
        health -= damage;
        if(health < 0) 
            OnDie();
    }

    public void OnDie()
    {
        gameObject.SetActive(false);
        Die();
    }
}
