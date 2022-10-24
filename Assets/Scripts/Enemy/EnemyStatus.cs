using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{

    [SerializeField] float maxHealth;
    float currentHealth;

    EnemyManager enemyManagerInstance;
    GameManager gm;
    private void Start()
    {
        currentHealth = maxHealth;

        enemyManagerInstance = EnemyManager.Instance;
        gm = FindObjectOfType<GameManager>();
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

        gm.Money += (int)(50*gm.MoneyMult);


        gameObject.SetActive(false);

    }

}
