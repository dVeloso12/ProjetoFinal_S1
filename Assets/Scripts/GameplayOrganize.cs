using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayOrganize : MonoBehaviour
{

    [SerializeField] GameObject StageRender;
    public bool canGenerateStages, canDeleteStages;
    public bool canGenerateShop, canDeleteShop;
    public bool canGenerateLobby, canDeleteLobby;
    [SerializeField] GameObject Player;
    GameObject saveStages;
    [SerializeField] GameObject Looby;
    void Start()
    {
        saveStages = null;
        Instantiate(Player, Looby.GetComponent<StageInfos>().StageSize, Quaternion.identity); 
    }

    void Update()
    {
        Generate_Detele_Stages();
    }
    void Generate_Detele_Stages()
    {

        if(canGenerateStages)
        {
            saveStages = Instantiate(StageRender, Vector3.zero, Quaternion.identity);
            canGenerateStages = false;
        }
        if(canDeleteStages)
        {
            Destroy(saveStages);
            saveStages = null;
            canDeleteStages = false;
        }

    }
    void Generate_Delete_Looby()
    {

    }
    void Generate_Delete_Shop()
    {

    }
}
