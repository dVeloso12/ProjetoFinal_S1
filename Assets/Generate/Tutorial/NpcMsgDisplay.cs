using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NpcMsgDisplay : MonoBehaviour
{
    [SerializeField] NpcMsgs Npc;
    [SerializeField] TextMeshProUGUI txtmsg;
    GameObject player;
    bool inRange;
   public  bool TutorialDone;
    float timer;
    int stageIndex,msgIndex=0;
    List<string> saveCurrentList;
    ChestScript deffChest;
    [Header("All NPC Parts")]
    GameObject canvas;
    SphereCollider sphereCollider;
    public SkinnedMeshRenderer[] meshList;
    bool toHide;
    [SerializeField] List<GameObject> toHideList;
    int toHideIndex;
    [SerializeField] GameObject shop;
    bool update;
    GameplayOrganize orggame;
    GameManager gameManager;

    public bool canTP;
    public bool canInteract,canInteractOnPc;

    private void Start()
    {   
        saveCurrentList = Npc.Position_1;
        stageIndex = 0;
        msgIndex = 0;
        transform.position = Npc.NpcSpawnPosition[stageIndex];
        canvas = GameObject.Find("TutoCanvas");
        sphereCollider = GetComponent<SphereCollider>();
        orggame = GameObject.Find("GameManager").GetComponent<GameplayOrganize>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        update = true;
    }
    private void Update()
    {
        if (update)
        {
            deffChest = GameObject.Find("ChestStageDeff").GetComponent<ChestScript>();
            toHideList.Add(GameObject.Find("ChestStageDeff"));
            toHideList.Add(GameObject.Find("Boss"));
            shop = GameObject.Find("ShopTuto");
            shop.SetActive(false);
            update = false;
        }
        if (inRange) rotateNpc();
        toHideManager();
        UiManager();
    }
    void toHideManager()
    {
        if(toHide)
        {
            if (toHideList[toHideIndex].name == "ChestStageDeff")
            {
                if(toHideList[toHideIndex].GetComponent<ChestScript>().canAppear)
                {
                    configChestTuto(true);
                    toHide = false;
                    toHideIndex++;
                }
               
            }
            else if(toHideList[toHideIndex].name == "Boss")
            {
                if (toHideList[toHideIndex].GetComponent<BossScript>().isDead)
                {
                    configChestTuto(true);
                    shop.SetActive(true);
                    toHide = false;
                    toHideIndex++;
                }
            }
        }
    }
    void UiManager()
    {

        if (Input.GetKeyDown(KeyCode.H) && inRange && !TutorialDone)
        {
            if (msgIndex != saveCurrentList.Count)
            {
                DisplayMsg(saveCurrentList[msgIndex]);
                msgIndex++;
            }
            else
            {
                stageIndex++;
                getMsgList();
                if(!TutorialDone)
                {
                    changeNpcPosition(stageIndex);
                    msgIndex = 0;
                    DisplayMsg(saveCurrentList[msgIndex]);
                }
            }        
        }
        if(TutorialDone)
        {
            orggame.tutorialFinished = true;
            TutorialDone = false;
            
        }
    }
    void rotateNpc()
    {
        float angleY = vector3AngleOnPlane(player.transform.position, transform.position, -transform.up, transform.forward);
        Vector3 rotationY = new Vector3(0, angleY, 0);
        transform.Rotate(rotationY, Space.Self);
    }
    float vector3AngleOnPlane(Vector3 from, Vector3 to, Vector3 planeNormal, Vector3 toZeroAngle)
    {
        Vector3 projectedVector = Vector3.ProjectOnPlane(from - to, planeNormal);
        float projectedVectorAngle = Vector3.SignedAngle(projectedVector, toZeroAngle, planeNormal);

        return projectedVectorAngle;
    }
    void getMsgList()
    {
        switch (stageIndex)
        {
            case 0:
                {
                    saveCurrentList = Npc.Position_1;
                    break;
                }
            case 1:
                {
                    saveCurrentList = Npc.Position_2;
                    break;
                    
                }
            case 2:
                {
                    saveCurrentList = Npc.Position_3;
                    break;
                }
            case 3:
                {
                    saveCurrentList= Npc.Position_4;
                    break;
                }
            case 4:
                {
                    saveCurrentList = Npc.Position_5;
                    configChestTuto(false);
                    break;
                }
            case 5:
                {
                    saveCurrentList = Npc.Position_6;
                    break;
                }
            case 6:
                {
                    saveCurrentList = Npc.Position_7;
                    configChestTuto(false);
                    break;
                }
            case 7:
                {
                    saveCurrentList = Npc.Position_8;
                    canTP = true;
                    break;
                }
            case 8:
                {
                    saveCurrentList = Npc.Position_9;   
                    gameManager.Money = 200;
                    break;
                }
            case 9:
                {
                    saveCurrentList = Npc.Position_10;
                    canInteract = true;
                    break;
                }
            case 10:
                {
                    saveCurrentList = Npc.Position_11;
                    canInteractOnPc = true;
                    break;
                }
            case 11:
                {
                    saveCurrentList = Npc.Position_12;
                    break;
                }
            default:
                TutorialDone = true;
                break;
        }
    }

    void configChestTuto(bool canAppear)
    {
        if(canAppear)
        {
            canvas.SetActive(true);
            //renderer.enabled = true;
            foreach(SkinnedMeshRenderer mesh in meshList)
            {
                mesh.enabled = true;
            }
            sphereCollider.enabled = true;
        }
        else
        {
            Debug.Log("Hello");
            toHide = true;
            canvas.SetActive(false);
            foreach (SkinnedMeshRenderer mesh in meshList)
            {
                mesh.enabled = false;
            }
            sphereCollider.enabled = false;


        }
    }
    void DisplayMsg(string msg)
    {
        txtmsg.text = msg;
    }
    
    void changeNpcPosition(int index)
    {
        transform.position = Npc.NpcSpawnPosition[index];
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            inRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            inRange = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            player = other.gameObject;
        }
    }

}
