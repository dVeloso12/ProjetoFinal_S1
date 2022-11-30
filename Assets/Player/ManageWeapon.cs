using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    GameManager gm;

    [SerializeField] GameObject shotgunPrefab;
    [SerializeField] GameObject assaultRiflePrefab;
    [SerializeField] GameObject pistolPrefab;

    void Awake()
    {
        //gm = FindObjectOfType<GameManager>();

        //gameObject.GetComponent<Pistol>().enabled = false;
        //gameObject.GetComponent<Shotgun>().enabled = false;
        //gameObject.GetComponent<AssaultRifle>().enabled = false;

        //switch (gm.weaponType)
        //{
        //    case WeaponType.Shotgun:
        //        gameObject.GetComponent<Shotgun>().enabled = true;
        //        break;

        //    case WeaponType.AR:
        //        gameObject.GetComponent<AssaultRifle>().enabled = true;
        //        break;

        //    case WeaponType.Pistol:
        //        gameObject.GetComponent<Pistol>().enabled = true;
        //        break;

        //    default:
        //        break;

        //}
    }

    // Update is called once per frame
    public void ChangeWeapon()
    {
        gm = FindObjectOfType<GameManager>();



        pistolPrefab.GetComponentInChildren<Pistol>().enabled = false;
        if(shotgunPrefab != null) 
            shotgunPrefab.GetComponentInChildren<Shotgun>().enabled = false;
        assaultRiflePrefab.GetComponentInChildren<AssaultRifle>().enabled = false;

        switch (gm.weaponType)
        {
            case WeaponType.Shotgun:
                if(shotgunPrefab != null)
                {
                    shotgunPrefab.GetComponentInChildren<Shotgun>().enabled = true;
                    shotgunPrefab.SetActive(true);
                }
                break;

            case WeaponType.AR:
                if (assaultRiflePrefab != null)
                {
                    assaultRiflePrefab.GetComponentInChildren<AssaultRifle>().enabled = true;
                    assaultRiflePrefab.SetActive(true);
                }
                break;

            case WeaponType.Pistol:
                if (pistolPrefab != null)
                {
                    pistolPrefab.GetComponentInChildren<Pistol>().enabled = true;
                    pistolPrefab.SetActive(true);
                }
                break;

            default:
                break;

        }
    }
}
