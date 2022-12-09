using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperLaser : MonoBehaviour
{
    // Start is called before the first frame update
    LineRenderer lineRenderer;
    [SerializeField] int MaxRange;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        //lineRenderer.SetPosition(0, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        //lineRenderer.SetPosition(0, transform.position);
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        //if (Physics.Raycast(transform.position, Vector3.forward, out hit, MaxRange))
        //{
        //    //if (hit.collider)
        //    //{
        //        float dis = (transform.position - hit.point).magnitude;
        //        lineRenderer.SetPosition(1, new Vector3(0,0,dis));
        //    //}
        //    Debug.Log("mnb,vxm" + hit.point + hit.transform.name);
        //}
        //else
        //lineRenderer.SetPosition(1,new Vector3(0,0, 100000));
        //else
        //{
        //    Debug.Log("asdasdf");
        //   lineRenderer.SetPosition(1, transform.forward * MaxRange);
        //}
    }
}
