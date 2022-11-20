using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;

    public TextMeshProUGUI HealthBar;

    GameManager gm;

    //Spawn spawnScript;

    private void Start()
    {
        currentHealth = maxHealth;

        gm = FindObjectOfType<GameManager>();
        Debug.Log(gm.tag);

        HealthBar.text = maxHealth.ToString() + "/" + maxHealth.ToString();

        /*awnScript = GenerateRun.instance.EnemiesManagerInstantiated.GetComponent<Spawn>();*/
    }

    public void Damage(float dmg)
    {
        if (currentHealth > 0)
        {
            currentHealth -= dmg;
            HealthBar.text = currentHealth.ToString() + "/" + maxHealth.ToString();

            if (currentHealth <= 0)
            {
                Death();
            }
        }
    }

    private void Death()
    {
        gm.DeadEnemy(this.gameObject);

        //gameObject.SetActive(false);
        Destroy(gameObject);

    }

}
