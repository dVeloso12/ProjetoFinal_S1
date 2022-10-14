using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRun : MonoBehaviour
{
    [SerializeField] List<GameObject> ListStages;
    List<GameObject> UseStage;
    [SerializeField]int maxNStages;
    [SerializeField] GameObject BossRoom;
    [SerializeField] GameObject StageStart;
    //[SerializeField] GameObject Player;


    void Start()
    {
     
        saveStagesList();
        Generate();
       //Instantiate(Player, StageStart.GetComponent<StageInfos>().StageSize, Quaternion.identity);
    }
    void saveStagesList()
    {
        UseStage = new List<GameObject>(ListStages);
    }
    void Generate()
    {
        var st0 = Instantiate(StageStart, Vector3.zero, Quaternion.identity);
        st0.transform.parent = this.transform;
        Vector3 nextPosition = Vector3.zero;  

        for (int i = 0; i < maxNStages; i++)
        {
            int ran = Random.Range(0, UseStage.Count);
            var st1 = Instantiate(UseStage[ran], nextPosition, Quaternion.identity);
            st1.transform.parent = this.transform;
            nextPosition += UseStage[ran].GetComponent<StageInfos>().StageSize;
            UseStage.RemoveAt(ran);
        }

        var bossStg = Instantiate(BossRoom, nextPosition,Quaternion.identity);
        bossStg.transform.parent = this.transform;

    }

    private int Choose_Stage(List<GameObject> ToUseStages)
    {
        int[] Numbers = SaveStagesInNumbers(ToUseStages);
        var index = Random.Range(0, Numbers.Length);
        return Numbers[index];
    }

    private int[] SaveStagesInNumbers(List<GameObject> ToUseStages)
    {
        int[] stageNumbers = new int[ToUseStages.Count];
        for (int i = 0; i < ToUseStages.Count; i++)
        {
            stageNumbers[i] = ToUseStages[i].GetComponent<StageInfos>().Stage_Id;
        }

        return stageNumbers;
    }
}
    
