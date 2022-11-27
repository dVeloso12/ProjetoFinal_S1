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

    Upgrade[] upg_choice = new Upgrade[12];

    public float[] prob = {75,20,5};

    public GameObject priceText;

    public Canvas canvas;

    public Button button;
    float xMult, yMult;

    void Start()
    {

        gm = FindObjectOfType<GameManager>();
        xMult = canvas.GetComponent<RectTransform>().sizeDelta.x / 1920;
        yMult = canvas.GetComponent<RectTransform>().sizeDelta.y / 1080;
        button.onClick.AddListener(Back);



        RandUpgrade();

    }

    // Update is called once per frame
    void Update()
    {
    }
    

    void RandUpgrade()
    {

        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                int r = Random.Range(0, gm.Upgrades.Count);

                //while (r == r_numb[0] || r == r_numb[1])
                //    r = Random.Range(0, gm.Upgrades.Count);
                int r2 = Random.Range(-5, 5);

                upg_choice[i*6+j] = Instantiate(gm.Upgrades[r], canvas.transform);
                upg_choice[i*6+j].transform.localPosition = new Vector3((-776 + (j * 274))*xMult, (238+(i*-540))*yMult);
                upg_choice[i * 6 + j].transform.localScale *= new Vector2(1.7f*xMult, 1.7f*yMult);
                upg_choice[i * 6 + j].price = 50 + r2;

                GameObject g = Instantiate(priceText, upg_choice[i * 6 + j].transform);
                g.transform.localPosition +=  new Vector3(0, 55*yMult, 0);
                g.transform.localScale *=new Vector2( .7f*xMult,.7f*yMult);
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
