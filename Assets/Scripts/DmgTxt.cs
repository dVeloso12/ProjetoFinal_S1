using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DmgTxt : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]TextMeshPro text;
    [SerializeField] float descSpeed;
    [SerializeField] float timer;
    [SerializeField] float alphaSpeed;
    Transform etransform;
    Vector3 despos;
    void Start()
    {
        //despos = transform.position;
        //Debug.Log(despos);
        StartCoroutine(DestroySelf());
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position -= new Vector3(0, descSpeed, 0);
        despos-= new Vector3(0, descSpeed, 0);
        if (etransform != null)
            transform.position = etransform.position + despos;
        else
            transform.position += despos;
        text.alpha -= alphaSpeed;
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }

    public void ChangeText(int dmg,Color color,Transform enemy)
    {
        SetEParent(enemy);
        text.text = dmg.ToString();
        text.color = color;
    }

    public void SetEParent(Transform enemy)
    {
        despos =transform.position- enemy.position;
        //Debug.Log(despos + "sa");
        etransform = enemy;
    }
}
