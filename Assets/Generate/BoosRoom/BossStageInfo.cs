using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStageInfo : MonoBehaviour
{
    [SerializeField] GameObject BossPortal;

    public bool getCollidePortal()
    {
        return BossPortal.GetComponent<EndBossPortal>().canTp;
    }
}
