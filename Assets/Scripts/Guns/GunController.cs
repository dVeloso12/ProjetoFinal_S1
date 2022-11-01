using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Cinemachine;

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

    public CinemachineVirtualCamera Camera;

    protected RaycastHit collisionDetected;

    public GameObject MarkSprite;

    public float Distance;

    public Transform GunPos,DwonSights;

    Transform origin;

    bool AimingDown=false;

    public GameObject dmgText;
    
    // Start is called before the first frame update
    protected void Start()
    {
        playerInput = new PlayerInput();

        playerInput.Player.Enable();

        playerInput.Player.Shoot.performed += ActivateShoot;
        playerInput.Player.Reload.performed += ActivateReload;

        playerInput.Player.Aim.performed += AimDown;
        playerInput.Player.Aim.canceled += AimDown;

        FireRateCounting = FireRate;

        gm = FindObjectOfType<GameManager>();

        Ammo = AmmoClipSize;

        AmmoCount = GameObject.Find("AmmoC").GetComponent<TextMeshProUGUI>();

        AmmoCount.text = Ammo.ToString() + "/" + AmmoClipSize.ToString();

        origin = GunPos;

    }

    // Update is called once per frame
    protected  virtual void Update()
    {
        if (shoot)
            Shoot();

        FireRateCounting -= Time.deltaTime;

        if (AimingDown)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, DwonSights.localPosition, 3*Time.deltaTime);
            SetFOV(Mathf.Lerp(Camera.m_Lens.FieldOfView, .6f*60,3*Time.deltaTime));

        }
        else
            transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, 3 * Time.deltaTime);
            SetFOV(Mathf.Lerp(Camera.m_Lens.FieldOfView, 60, 3 * Time.deltaTime));
    }


    void SetFOV(float fov)
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


        FireRateCounting = FireRate*gm.FireRateMod;

        Ammo--;

        AmmoCount.text = Ammo.ToString() + "/" + AmmoClipSize.ToString();
    }



    public virtual void ActivateReload(InputAction.CallbackContext obj)
    {
        StartCoroutine(Reload());
    }

    IEnumerator Reload()
    {
        Ammo = 0;
        yield return new WaitForSeconds(ReloadSpeed);
        Ammo = AmmoClipSize;
        AmmoCount.text = Ammo.ToString() + "/" + AmmoClipSize.ToString();
    }

    public virtual void AimDown(InputAction.CallbackContext obj)
    {
        AimingDown = !AimingDown;
    }
}
