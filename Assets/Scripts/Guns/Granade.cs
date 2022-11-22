using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Granade : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Granada;

    PlayerInput playerInput;

    public Transform place;

    TextMeshProUGUI Timer;

    public float NadeCooldown;
    private float NadeTimer;

    public Image GranadeIcon;
    public float fillAmount;
    GameManager gm;

    void Start()
    {
        playerInput = new PlayerInput();
        gm = FindObjectOfType<GameManager>();

        playerInput.Player.Enable();

        playerInput.Player.Granade.performed += CreateNade;

        Timer = GameObject.Find("GranadeC").GetComponent<TextMeshProUGUI>();
        GranadeIcon = GameObject.Find("granade").GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        if (NadeTimer > 0)
        {
            NadeTimer -= Time.deltaTime;
            fillAmount += Time.deltaTime * (1 / (NadeCooldown/gm.CDMod));
            Timer.text = Mathf.Round(NadeTimer).ToString();
            GranadeIcon.fillAmount = fillAmount;
        }
    }

    void CreateNade(InputAction.CallbackContext context)
    {
        if (NadeTimer <= 0)
        {
            Instantiate(Granada, place.position, place.rotation);
            NadeTimer = NadeCooldown/gm.CDMod;
            fillAmount = 0;
        }

    }

}
