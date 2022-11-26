using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    CapsuleCollider capsuleCollider;
   MeshRenderer renderer;
    bool toHide;
    [SerializeField] List<GameObject> toHideList;
    int toHideIndex;
    [SerializeField] GameObject shop;



    private void Start()
    {
        saveCurrentList = Npc.Position_1;
        stageIndex = 0;
        msgIndex = 0;
        transform.position = Npc.NpcSpawnPosition[stageIndex];
        deffChest = GameObject.Find("ChestStageDeff").GetComponent<ChestScript>();
        canvas = GameObject.Find("TutoCanvas");
        sphereCollider = GetComponent<SphereCollider>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        renderer = GetComponent<MeshRenderer>();
        shop.SetActive(false);
       
    }
    private void Update()
    {
        if(inRange) rotateNpc();
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
                else
                {
                    DisplayMsg("TUTO DONE");
                }
            }        
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
            renderer.enabled = true;
            sphereCollider.enabled = true;
            capsuleCollider.enabled = true;
        }
        else
        {
            toHide = true;
            canvas.SetActive(false);
            renderer.enabled = false;
            sphereCollider.enabled = false;
            capsuleCollider.enabled = false;


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
