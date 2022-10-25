using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    [SerializeField] GameObject TurretGameobject;
    public Transform savePlayer;
    TurretScript Turret;
    private void Start()
    {
        Turret = TurretGameobject.GetComponent<TurretScript>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.transform.name);
        if(other.transform.name == "Player")
        {
            Turret.canAim = true;
            savePlayer = other.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.name == "Player")
        {
            Turret.canAim = false;
            //Turret.firing = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.name == "Player")
        {
            Turret.Fire();
        }
    }
}
