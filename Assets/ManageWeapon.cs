using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    GameManager gm;
    void Start()
    {
        gm = FindObjectOfType<GameManager>();

        gameObject.GetComponent<Pistol>().enabled = false;
        gameObject.GetComponent<Shotgun>().enabled = false;
        gameObject.GetComponent<AssaultRifle>().enabled = false;

        switch (gm.weaponType)
        {
            case WeaponType.Shotgun:
                gameObject.GetComponent<Shotgun>().enabled = true;
                break;

            case WeaponType.AR:
                gameObject.GetComponent<AssaultRifle>().enabled = true;
                break;

            case WeaponType.Pistol:
                gameObject.GetComponent<Pistol>().enabled = true;
                break;

            default:
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
