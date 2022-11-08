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
    void Start()
    {
        StartCoroutine(DestroySelf());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(0, descSpeed, 0);
        text.alpha -= alphaSpeed;
    }

    IEnumerator DestroySelf()
    {

        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }

    public void ChangeText(int dmg,Color color)
    {
        text.text = dmg.ToString();
        text.color = color;
    }
}
