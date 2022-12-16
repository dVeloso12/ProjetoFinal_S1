using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Add_Upgrade : MonoBehaviour
{
    // Start is called before the first frame update
    GameManager gm;

    Upgrade[] upg_choice = new Upgrade[3];

    int[] r_numb = new int[3];
    int[] rar_numb = new int[3];

    public float[] prob = { 75, 20, 5 };
    float xMult, yMult;

    public Canvas canvas;
    void Start()
    {

        gm = FindObjectOfType<GameManager>();
        xMult = 1;
        yMult = 1;
        RandUpgrade();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RandUpgrade()
    {

        for(int i = 0; i < 3; i++)
        {
            float rare_choice = Random.value * 100;
            int rarity = 0;

            while (rare_choice > prob[rarity])
            {
                rare_choice -= prob[rarity];
                rarity++;
            }


            int r = Random.Range(0, gm.Upgrade_Rarity[rarity].Count);

            int f = 60;

            while ((rarity == rar_numb[0] || rarity == rar_numb[1]) && (r == r_numb[0] || r == r_numb[1])&&f>0)
            {
                r = Random.Range(0, gm.Upgrade_Rarity[rarity].Count);
                f--;
            }

            Debug.Log(rarity + "lol " + r);

            upg_choice[i] = Instantiate(gm.Upgrades[gm.Upgrade_Rarity[rarity][r]],canvas.transform);
            upg_choice[i].transform.localPosition = new Vector3((-330 + (i * 330)) * xMult, 30 * yMult);
            upg_choice[i].transform.localScale *= new Vector2(3f, 3f);

            r_numb[i] = r;
            rar_numb[i] = rarity;

            //Pos X -389 389 Pos Y -97 97
            // Y=50 X=-250 +250...



        }

    }
}
