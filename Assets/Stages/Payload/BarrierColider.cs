using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierColider : MonoBehaviour
{
    [SerializeField] GameObject Payload;
    [SerializeField] GameObject PayloadBoxColider;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);

        if (other.gameObject.name == "Payload")
        {
            Debug.Log(other.gameObject.name);
            Payload.GetComponent<PayloadScript>().canMove = false;
            Payload.GetComponent<PayloadScript>().PayloadCompleted = true;
            PayloadBoxColider.SetActive(false);
        }
    }
}
