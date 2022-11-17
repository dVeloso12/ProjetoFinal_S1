using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ShopUpgrades : MonoBehaviour
{
    // Start is called before the first frame update
    GameManager gm;

    Upgrade[] upg_choice = new Upgrade[11];

    public float[] prob = {75,20,5};

    public GameObject priceText;

    public Canvas canvas;

    public Button button;
    
    void Start()
    {

        gm = FindObjectOfType<GameManager>();
        RandUpgrade();
        button.onClick.AddListener(Back);

    }

    // Update is called once per frame
    void Update()
    {
    }
    

    void RandUpgrade()
    {

        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 6-i; j++)
            {
                int r = Random.Range(0, gm.Upgrades.Count);

                //while (r == r_numb[0] || r == r_numb[1])
                //    r = Random.Range(0, gm.Upgrades.Count);
                int r2 = Random.Range(-5, 5);

                upg_choice[i*6+j] = Instantiate(gm.Upgrades[r], canvas.transform);
                upg_choice[i*6+j].transform.localPosition = new Vector3(-359 + (j * 144), 103+(i*-190));
                upg_choice[i * 6 + j].transform.localScale = new Vector3(1.04f, 1.52f, 1);
                upg_choice[i * 6 + j].price = 50 + r2;

                GameObject g = Instantiate(priceText, upg_choice[i * 6 + j].transform);
                g.transform.localPosition +=  new Vector3(0, 35, 0);
                g.transform.localScale *= .7f;
                g.GetComponent<TextMeshProUGUI>().text = upg_choice[i * 6 + j].price + " $";



            }

            //Pos X -389 389 Pos Y -97 97
            // Y=50 X=-250 +250...



        }

    }
    void Back()
    {
        
    
        gm.CloseAddUpgrade();
    }

}
