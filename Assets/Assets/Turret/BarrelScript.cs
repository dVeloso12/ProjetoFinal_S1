using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelScript : MonoBehaviour
{
    [SerializeField] GameObject TurretGameobject;
    TurretScript Turret;
    LineRenderer line;
    const float MaxRange = 1000f;


    private void Start()
    {
        Turret = TurretGameobject.GetComponent<TurretScript>();
        line = GetComponent<LineRenderer>();
        line.useWorldSpace = true;
    }

    private void Update()
    {  
        if (Turret.isAlive) line.SetPosition(0, transform.position);

        if (Turret.isAlive) line.enabled = true;
        else line.enabled = false;


    }

    void FixedUpdate()
    {
        RaycastHit hit;
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, MaxRange,layerMask))
        {
            if (hit.collider && Turret.isAlive)
            {
                line.SetPosition(1,hit.point);
            }
            
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);        
        }
        else
        {      
            if(Turret.isAlive) line.SetPosition(1, transform.forward * MaxRange);
        }    
    }
}
