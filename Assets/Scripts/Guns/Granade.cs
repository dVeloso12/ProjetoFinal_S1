using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Granade : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Granada;

    PlayerInput playerInput;

    public Transform place;

    TextMeshProUGUI Timer;

    public float NadeCooldown;
    private float NadeTimer;

    GameManager gm;

    void Start()
    {
        playerInput = new PlayerInput();

        playerInput.Player.Enable();

        playerInput.Player.Granade.performed += CreateNade;

        Timer = GameObject.Find("GranadeC").GetComponent<TextMeshProUGUI>();

        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (NadeTimer > 0)
        {
            NadeTimer -= Time.deltaTime;
            Timer.text = Mathf.Round(NadeTimer).ToString();
        }
    }

    void CreateNade(InputAction.CallbackContext context)
    {
        if (NadeTimer <= 0)
        {
            Instantiate(Granada, place.position, place.rotation);
            NadeTimer = NadeCooldown/gm.CooldownReduction;
        }

    }

}
