using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperLaser : MonoBehaviour
{

    LineRenderer line;
    [SerializeField] GameObject SniperObj;

    float range;

    // Start is called before the first frame update
    void Start()
    {
        range = SniperObj.GetComponent<Enemy_AI_2>().Range;
        line = gameObject.GetComponent<LineRenderer>();

        line.useWorldSpace = true;

    }

    private void Update()
    {
        line.SetPosition(0, transform.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        RaycastHit hit;
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {

            if (hit.collider)
            {
                line.SetPosition(1, hit.point);

                //Debug.Log("Hit Name : " + hit.transform.name);
                //Debug.Log("Hit Pos : " + hit.point);

            }
            

        }
        else
        {
            line.SetPosition(1, transform.forward * range);
        }
    }
}
