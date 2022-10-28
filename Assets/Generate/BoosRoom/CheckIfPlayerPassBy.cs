using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfPlayerPassBy : MonoBehaviour
{

    [SerializeField] GameObject Boss;
    [SerializeField] List<GameObject> TurretList;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.name == "Player")
        {
            Boss.GetComponent<BossScript>().activateUIHP = true;
            foreach(GameObject go in TurretList)
            {
                go.GetComponent<TurretScript>().showUI = true;
            }
            GetComponent<Collider>().enabled = false;
        }
    }
}
