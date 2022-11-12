using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;

    public TextMeshProUGUI HealthBar;

    EnemyManager enemyManagerInstance;

    Spawn spawnScript;

    private void Start()
    {
        currentHealth = maxHealth;

        enemyManagerInstance = EnemyManager.Instance;

        HealthBar.text = maxHealth.ToString() + "/" + maxHealth.ToString();

        spawnScript = GenerateRun.instance.EnemiesManagerInstantiated.GetComponent<Spawn>();
    }

    public void Damage(float dmg)
    {

        currentHealth -= dmg;
        HealthBar.text = currentHealth.ToString() + "/" + maxHealth.ToString();

        if (currentHealth <= 0)
        {
            Death();
        }

    }

    private void Death()
    {

        //enemyManagerInstance.DeActivateEnemy(gameObject);

        //spawnScript.RemoveEnemiesFromList(gameObject);

        //enemyManagerInstance.survivalScript.ZombieKilled();

        gameObject.SetActive(false);
        //Destroy(gameObject);

    }

}
