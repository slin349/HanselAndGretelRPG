using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    public SimpleHealthBar expBar;
    public int expToNextLevel = 100;
    private int _currExp = 0;
    private int _level = 1;
    private const int EXPVALUE = 75;

    private void Start()
    {
        expBar.UpdateBar(_currExp, expToNextLevel);
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Experience")
        {
            _currExp += EXPVALUE;
            if (_currExp >= expToNextLevel)
            {
                // level up
                _level += 1;
                increaseMaxHealth();
                increaseDamage();
                _currExp -= expToNextLevel;
            }
            expBar.UpdateBar(_currExp, expToNextLevel);
        } 
    }

    private void increaseMaxHealth()
    {
        Health health = GetComponentInParent<Health>();
        health.maxHealth += 5;
        health.currhealth = health.maxHealth;
        health.healthBar.UpdateBar(health.currhealth, health.maxHealth);
    }

    private void increaseDamage()
    {
        PlayerAttack playerAttack = GetComponentInChildren<PlayerAttack>();
        playerAttack.attackDamage += 1;
    }

}
