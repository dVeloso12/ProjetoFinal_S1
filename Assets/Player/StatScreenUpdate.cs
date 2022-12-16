using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class StatScreenUpdate : MonoBehaviour
{
    // Start is called before the first frame update
    public List<TextMeshProUGUI> statText = new List<TextMeshProUGUI>();
    GameManager gm;
    Player player;
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        player = FindObjectOfType < Player >();
        
    }

    public void UpdateStats()
    {
        statText[0].text = "Health: " + player.PlayerHp + "/" + player.saveMaxHP;

        statText[1].text = "Move Speed: " + Math.Round(gm.MoveSpeedMod, 1)+"X";

        statText[2].text = "Damage: " + gm.DamageMod * 100 + "%";

        statText[3].text = "Fire Rate: " + gm.FireRateMod * 100 + "%";

        statText[4].text = "HS Mult: " + Math.Round(gm.HSMod, 1) + "X";

        statText[5].text = "CD Reduction: " + (gm.CDMod-1) * 100 + "%";

    }
    
}
