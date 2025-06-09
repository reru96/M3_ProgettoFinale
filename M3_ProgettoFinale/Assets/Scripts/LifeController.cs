using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    public enum DeathAction
    {
        None,    
        Disable, 
        Destroy  
    }

 
    [SerializeField] private int maxHp = 100;
    [SerializeField] private int currentHp;

    [SerializeField] private DeathAction onDeath = DeathAction.Destroy;

    public int CurrentHp => currentHp;
    public int MaxHp => maxHp;
    public bool IsAlive => currentHp > 0;

    private void Awake()
    {
        currentHp = maxHp;
    }

    public int SetHp(int newHp)
    {
        currentHp = Mathf.Clamp(newHp, 0, maxHp);

        if (currentHp <= 0)
        {
            HandleDeath();
        }

        return currentHp;
    }

    public int AddHp(int amount)
    {
        currentHp = Mathf.Clamp(currentHp + amount, 0, maxHp);

        if (amount < 0 && currentHp <= 0)
        {
            HandleDeath();
        }

        return currentHp;
    }

    public void Kill()
    {
        SetHp(0);
    }

    public void Revive()
    {
        SetHp(maxHp);
    }

    private void HandleDeath()
    {
        switch (onDeath)
        {
            case DeathAction.None:
                
                break;

            case DeathAction.Disable:
                gameObject.SetActive(false);
                break;

            case DeathAction.Destroy:
                Destroy(gameObject);
                break;
        }
    }
}
