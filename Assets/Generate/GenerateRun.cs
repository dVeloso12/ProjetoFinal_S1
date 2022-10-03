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

    void Start()
    {
        saveStagesList();
        Generate();
    }
    void saveStagesList()
    {
        //UseStage = new List<GameObject>();
        //for(int i= 0; i < ListStages.Count;i++)
        //{
        //    UseStage.Add(ListStages[i]);
        //}
        UseStage = new List<GameObject>(ListStages);

    }
    void Generate()
    {
        //int stagesAdd = 0;
        //int[] saveIds = new int[maxNStages];
        //while (stagesAdd != maxNStages)
        //{
        //    int Number = Choose_Stage(UseStage);
        //    saveIds[stagesAdd] = Number;
        //    for(int i = 0; i < UseStage.Count;i++)
        //    {
        //        if(UseStage[i].GetComponent<StageInfos>().Stage_Id == Number)
        //        {
        //            UseStage.RemoveAt(i);
        //        }
        //    }
        //    stagesAdd++;
        //}
        // Debug.Log(("1º Stage :"+saveIds[0]+"\n 2º Stage:"+saveIds[1]));





        Instantiate(StageStart, Vector3.zero, Quaternion.identity);
        //int stagesCreated = 0;
        Vector3 nextPosition = Vector3.zero;
        //while (stagesCreated != maxNStages)
        //{
        //        for (int i = 0; i < ListStages.Count; i++)
        //        {
        //            if (ListStages[i].GetComponent<StageInfos>().Stage_Id == saveIds[stagesCreated])
        //            {
        //                if (stagesCreated == 0)
        //                {
        //                   Instantiate(ListStages[i], new Vector3(0f, 0f, 0f), Quaternion.identity);
        //                   nextPosition = ListStages[i].GetComponent<StageInfos>().StageSize;
        //                }
        //                else
        //                {  
        //                   Instantiate(ListStages[i], nextPosition,Quaternion.identity);
        //                   nextPosition += ListStages[i].GetComponent<StageInfos>().StageSize;
        //                }  
                       
        //            }
        //        }
        //        stagesCreated++;

        //}

        for (int i = 0; i < maxNStages; i++)
        {
            int ran = Random.Range(0, UseStage.Count);
            Instantiate(UseStage[ran], nextPosition, Quaternion.identity);
            nextPosition += UseStage[ran].GetComponent<StageInfos>().StageSize;
            UseStage.RemoveAt(ran);
        }





        Instantiate(BossRoom, nextPosition,Quaternion.identity);
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
    
