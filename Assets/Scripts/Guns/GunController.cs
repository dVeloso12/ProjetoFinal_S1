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

    [SerializeField] protected float backKick, recoilY, recoilZ;
    [SerializeField] protected float snapiness, returnSpeed;

    [SerializeField] GameObject GunDirectionOBJ;


    protected PlayerBullets playerBulletsScript;
    [SerializeField] protected GameObject Player;

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
    protected int Ammo,orAmmo;
    [SerializeField] TextMeshProUGUI bullettxt;


    public AudioSource sound,reload;

    
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

    protected bool Modifier = false, isRunning = false;

    Animator ARAnimator;

    protected Recoil RecoilScript;

    public TrailRenderer NormalTrail;
    public Transform barrelMuzzle;
    public int layerMask;

    public bool IsRunning
    {
        get { return isRunning; }
        set { isRunning = value; }
    }


    private void Awake()
    {
        orAmmo = AmmoClipSize;
    }
    // Start is called before the first frame update
    protected void Start()
    {
        playerInput = new PlayerInput();

        layerMask = 1 << 9;
        layerMask = ~layerMask;

        playerInput.Player.Enable();

        playerInput.Player.Shoot.performed += ActivateShoot;
        playerInput.Player.Reload.performed += ActivateReload;

        
        playerInput.Player.Modifier.performed += _Modifier;
        playerInput.Player.Modifier.canceled += _Modifier;

        FireRateCounting = 0;

        gm = FindObjectOfType<GameManager>();

        Ammo = AmmoClipSize;



        UpdateBulletTxt();


        ARAnimator = GetComponent<Animator>();

        if(ARAnimator == null)
        {
            ARAnimator = GetComponentInChildren<Animator>();
        }
        //origin = GunPos;

        RecoilScript = GunDirectionOBJ.GetComponent<Recoil>();
        playerBulletsScript = Player.gameObject.GetComponent<PlayerBullets>();

    }
    private void OnEnable()
    {
        if(RecoilScript == null) RecoilScript = GunDirectionOBJ.GetComponent<Recoil>();

        RecoilScript.ChangeRecoilValues(snapiness, returnSpeed, gameObject);
    }

    // Update is called once per frame
    protected  virtual void Update()
    {
        if (shoot&&!isRunning)
            Shoot();

        FireRateCounting -= Time.deltaTime;
        AimDown();
    }

    void UpdateBulletTxt()
    {
        bullettxt.text = Ammo.ToString();
    }
    protected void SetFOV(float fov)
    {
       Camera.m_Lens.FieldOfView = fov;
    }

    public virtual void ActivateShoot(InputAction.CallbackContext obj)
    {

        if(FireRateCounting<=0 && Ammo>0 )//&& !isRunning)
            shoot = !shoot;


    }

    protected virtual void Shoot()
    {

        RecoilScript.RecoilFire(backKick, recoilY, recoilZ);

        muzzleFlash.Play();


        

        FireRateCounting = FireRate/gm.FireRateMod;

        Ammo--;

        //AmmoCount.text = Ammo.ToString() + "/" + AmmoClipSize.ToString();
        UpdateBulletTxt();
    }



    public virtual void ActivateReload(InputAction.CallbackContext obj)
    {
        Debug.Log("Ammo clip size : " + AmmoClipSize);
        if (Ammo < AmmoClipSize)
        {
            ARAnimator.SetTrigger("Reload");
            reload.Play();
            Ammo = 0;
        }
        //StartCoroutine(Reload(ReloadSpeed));
    }

    public void Reloaded()
    {
        Ammo = AmmoClipSize;
        ARAnimator.ResetTrigger("Reload");

        //Debug.Log("Ammo : " + Ammo);

        //AmmoCount.text = Ammo.ToString() + "/" + AmmoClipSize.ToString();
        UpdateBulletTxt();
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


    public void OnDeath()
    {
        AmmoClipSize = orAmmo;
        Ammo = AmmoClipSize;
        UpdateBulletTxt();
    }


    public void  PauseManager()
    {
        FireRateCounting = .1f;
    }
}
