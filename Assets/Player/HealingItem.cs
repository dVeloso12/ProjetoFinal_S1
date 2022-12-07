using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class HealingItem : MonoBehaviour
{
    // Start is called before the first frame update

    public int MaxCount;
    int count;
    public float HealAmount;

    protected PlayerInput playerInput;

    TextMeshProUGUI CountText;


    void Start()
    {
        count = MaxCount;
        playerInput = new PlayerInput();

        playerInput.Player.Enable();

        playerInput.Player.Heal.performed += Heal;

        CountText = GameObject.Find("HealsC").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
   
    public void ResetCount()
    {
        count = MaxCount;
        CountText = GameObject.Find("HealsC").GetComponent<TextMeshProUGUI>();
        CountText.text = count.ToString();
    }

    public virtual void Heal(InputAction.CallbackContext obj)
    {

        if (count > 0)
        {
            gameObject.GetComponent<Player>().PlayerHp += HealAmount;
            count--;
        }

        CountText.text = count.ToString();

    }




}
