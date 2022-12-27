using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeffStageController : MonoBehaviour
{
    [SerializeField] PlateScript plate;
    TextMeshProUGUI txtPlate;
    void Start()
    {
        txtPlate = GameObject.Find("DeffText").GetComponent<TextMeshProUGUI>();
        txtPlate.enabled = false;
    }

    void Update()
    {
        UpdateUI();
    }
    void UpdateUI()
    {
        float value = Mathf.Round(plate.PlatePorc*100f);
        txtPlate.text = "Capture the Plate\n Plate: "+value.ToString()+"%";
    }

    public void disableObjTxt()
    {
        txtPlate.enabled = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            txtPlate.enabled = true;

        }
    }
}
