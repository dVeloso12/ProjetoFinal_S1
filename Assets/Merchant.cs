using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Player" && Input.GetKey(KeyCode.H))
        {
            GameManager gm = FindObjectOfType<GameManager>();
            gm.gameState = 1;
            gm.AddUpgrade();
        }
    }
    
}
