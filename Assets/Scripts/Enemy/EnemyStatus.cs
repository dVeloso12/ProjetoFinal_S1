using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;

    public TextMeshProUGUI HealthBar;
    public int money;
    GameManager gm;

    //Spawn spawnScript;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();

        maxHealth = Mathf.FloorToInt(maxHealth * gm.DifficultyMod);

        currentHealth = maxHealth;


        HealthBar.text = maxHealth.ToString() + "/" + maxHealth.ToString();

        /*awnScript = GenerateRun.instance.EnemiesManagerInstantiated.GetComponent<Spawn>();*/
    }

    public void Damage(float dmg)
    {
        if (currentHealth > 0)
        {
            currentHealth -= dmg;
            HealthBar.text = currentHealth.ToString() + "/" + maxHealth.ToString();

            Debug.LogWarning("Receiving DMG");

            if (currentHealth <= 0)
            {
                Death();
            }
        }
    }

    private void Death()
    {
        gm.Money += Mathf.RoundToInt( money*gm.MoneyMod);
        gm.DeadEnemy(this.gameObject);

        //gameObject.SetActive(false);
        Destroy(gameObject);

    }

}
