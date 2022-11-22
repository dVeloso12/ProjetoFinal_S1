using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Cinemachine;

public class GunController : MonoBehaviour
{
    
    [SerializeField] protected ParticleSystem muzzleFlash;
    [SerializeField] protected ParticleSystem hiteffect;


    //public GameObject bullet;

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

    public CinemachineVirtualCamera Camera;

    protected RaycastHit hit;

    public GameObject MarkSprite;

    public float Distance;

    public Transform DwonSights;

    Transform origin;

    bool AimingDown=false;

    public GameObject dmgText;

    protected float finaldmg;

    protected bool Modifier = false;

    // Start is called before the first frame update
    protected void Start()
    {
        playerInput = new PlayerInput();

        playerInput.Player.Enable();

        playerInput.Player.Shoot.performed += ActivateShoot;
        playerInput.Player.Reload.performed += ActivateReload;

        
        playerInput.Player.Modifier.performed += _Modifier;
        playerInput.Player.Modifier.canceled += _Modifier;

        FireRateCounting = 0;

        gm = FindObjectOfType<GameManager>();

        Ammo = AmmoClipSize;

        AmmoCount = GameObject.Find("AmmoC").GetComponent<TextMeshProUGUI>();

        AmmoCount.text = Ammo.ToString() + "/" + AmmoClipSize.ToString();

        //origin = GunPos;

    }

    // Update is called once per frame
    protected  virtual void Update()
    {
        if (shoot)
            Shoot();

        FireRateCounting -= Time.deltaTime;

        AimDown();
    }


    protected void SetFOV(float fov)
    {
       Camera.m_Lens.FieldOfView = fov;
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

        AmmoCount.text = Ammo.ToString() + "/" + AmmoClipSize.ToString();
    }



    public virtual void ActivateReload(InputAction.CallbackContext obj)
    {
        StartCoroutine(Reload(ReloadSpeed));
    }

    public IEnumerator Reload(float time)
    {
        Ammo = 0;
        yield return new WaitForSeconds(time);
        Ammo = AmmoClipSize;
        AmmoCount.text = Ammo.ToString() + "/" + AmmoClipSize.ToString();
    }

    public virtual void AimDown()
    {
        if (Modifier)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, DwonSights.localPosition, 20 * Time.deltaTime);
            SetFOV(Mathf.Lerp(Camera.m_Lens.FieldOfView, .7f * 60, 3 * Time.deltaTime));
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, 20 * Time.deltaTime);
            SetFOV(Mathf.Lerp(Camera.m_Lens.FieldOfView, 60, 3 * Time.deltaTime));
        }
    }

    public virtual void _Modifier(InputAction.CallbackContext obj)
    {
        Modifier = !Modifier;
    }
}
