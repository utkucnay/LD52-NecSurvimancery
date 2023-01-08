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
    [SerializeField] UnityEvent dieEvent;

    public void Start(GameObject gameObject)
    {
        this.gameObject = gameObject;
        health = maxHealth;
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
        dieEvent.Invoke();
    }
}
