using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class GunController : MonoBehaviour
{
    
    [SerializeField] protected ParticleSystem muzzleFlash;

    public GameObject bullet;

    public Transform ShotingPlace;

    protected PlayerInput playerInput;

    protected bool shoot=false;

    public float FireRate;

    public float dmg;

    protected float FireRateCounting;

    protected GameManager gm;


    public float ReloadSpeed;
    public int AmmoClipSize;
    protected int Ammo;

    TextMeshProUGUI AmmoCount;

    public Transform _camera;

    protected RaycastHit collisionDetected;

    public GameObject MarkSprite;

    public float Distance;


    // Start is called before the first frame update
    protected void Start()
    {
        playerInput = new PlayerInput();

        playerInput.Player.Enable();

        playerInput.Player.Shoot.performed += ActivateShoot;
        playerInput.Player.Reload.performed += ActivateReload;

        FireRateCounting = FireRate;

        gm = FindObjectOfType<GameManager>();

        Ammo = AmmoClipSize;

        AmmoCount = GameObject.Find("AmmoC").GetComponent<TextMeshProUGUI>();

        AmmoCount.text = Ammo.ToString() + "/" + ((int)(AmmoClipSize*gm.ClipModifier)).ToString();
    }

    // Update is called once per frame
    protected  virtual void Update()
    {
        if (shoot)
            Shoot();

        FireRateCounting -= Time.deltaTime;
    }

    public virtual void ActivateShoot(InputAction.CallbackContext obj)
    {

        if(FireRateCounting<=0 && Ammo>0)
            shoot = !shoot;


    }

    protected virtual void Shoot()
    {

        muzzleFlash.Play();


        FireRateCounting = FireRate/gm.FireRateMod;

        Ammo--;

        AmmoCount.text = Ammo.ToString() + "/" + ((int)(AmmoClipSize * gm.ClipModifier)).ToString();
    }



    public virtual void ActivateReload(InputAction.CallbackContext obj)
    {
        StartCoroutine(Reload());
    }

    IEnumerator Reload()
    {
        Ammo = 0;
        yield return new WaitForSeconds(ReloadSpeed);
        Ammo = (int)(AmmoClipSize*gm.ClipModifier);
        AmmoCount.text = Ammo.ToString() + "/" + ((int)(AmmoClipSize * gm.ClipModifier)).ToString();
    }
}
