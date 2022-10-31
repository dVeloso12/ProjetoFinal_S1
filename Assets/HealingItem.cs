using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class HealingItem : MonoBehaviour
{
    // Start is called before the first frame update

    public int MaxCount;
    int Count;
    public float HealAmount;

    protected PlayerInput playerInput;

    TextMeshProUGUI CountText;

    void Start()
    {
        Count = MaxCount;
        playerInput = new PlayerInput();

        playerInput.Player.Enable();

        playerInput.Player.Heal.performed += Heal;

        CountText = GameObject.Find("HealsC").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
   

    public virtual void Heal(InputAction.CallbackContext obj)
    {

        if (Count > 0)
        {
            gameObject.GetComponent<Player>().PlayerHp += HealAmount;
            Count--;
        }

        CountText.text = Count.ToString();

    }




}
