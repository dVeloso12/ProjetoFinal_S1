using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DmgTxt : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]TextMeshPro text;
    void Start()
    {
        StartCoroutine(DestroySelf());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(0, 1, 0);
        text.alpha -= .01f;
    }

    IEnumerator DestroySelf()
    {

        yield return new WaitForSeconds(.3f);
        Destroy(gameObject);
    }
}
