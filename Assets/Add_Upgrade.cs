using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Add_Upgrade : MonoBehaviour
{
    // Start is called before the first frame update
    GameManager gm;

    Upgrade[] upg_choice = new Upgrade[3];

    int[] r_numb = new int[3];

    public Canvas canvas;
    void Start()
    {

        gm = FindObjectOfType<GameManager>();
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

            int r = Random.Range(0, gm.Upgrades.Count);

            while (r==r_numb[0]||r==r_numb[1])
            r= Random.Range(0, gm.Upgrades.Count);

            upg_choice[i] = Instantiate(gm.Upgrades[r],canvas.transform);
            upg_choice[i].transform.localPosition = new Vector3(-250+(i*250),50);

            r_numb[i] = r;

            //Pos X -389 389 Pos Y -97 97
            // Y=50 X=-250 +250...



        }

    }
}
