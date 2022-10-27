using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{

    [SerializeField] float maxHealth;
    float currentHealth;

    EnemyManager enemyManagerInstance;

    private void Start()
    {
        currentHealth = maxHealth;

        enemyManagerInstance = EnemyManager.Instance;
    }

    public void Damage(float dmg)
    {

        currentHealth -= dmg;

        if(currentHealth <= 0)
        {
            Death();
        }

    }

    private void Death()
    {

        enemyManagerInstance.DeActivateEnemy(gameObject);

        gameObject.SetActive(false);

    }

}
