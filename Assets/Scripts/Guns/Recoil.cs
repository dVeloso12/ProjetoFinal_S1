using UnityEngine;
using UnityEngine.Animations;

public class Recoil : MonoBehaviour
{

    private Vector3 currentRotation, targetRotation;

    private Vector3 currentPosition, targetPosition;

    float snapiness, returnSpeed;
    [SerializeField] Vector3 defaultRotation, defaultPosition;

    Vector3 currentPlayerPos;

    GameObject activatedWeapon;

    public Transform player;

    GameObject GunBarrel;

    void Start()
    {
        snapiness = 6;
        returnSpeed = 2;

       
    }

    public void ChangeRecoilValues(float snapiness, float returnSpeed, GameObject activatedWeapon)
    {
        this.snapiness = snapiness;
        this.returnSpeed = returnSpeed;
        this.activatedWeapon = activatedWeapon;

        //ParentConstraint temp = gameObject.GetComponent<ParentConstraint>();

        foreach(Transform t in activatedWeapon.GetComponentsInChildren<Transform>())
        {
            if(t.name == "BarrelExit")
            {
                GunBarrel = t.gameObject;
                break;
            }
        }

        ////transform.SetParent(null, false);
        //temp.enabled = true;
    }

    void Update()
    {

        currentPlayerPos = activatedWeapon.transform.localPosition;

        //Debug.Log("Current weapon pos : " + currentPlayerPos);

        Debug.Log("Default Rotation : " + defaultRotation);

        targetRotation = Vector3.Lerp(targetRotation, defaultRotation, returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snapiness * Time.deltaTime);

        targetPosition = Vector3.Lerp(targetPosition, currentPlayerPos, returnSpeed * Time.deltaTime);
        currentPosition = Vector3.Lerp(currentPosition, targetPosition, snapiness * Time.deltaTime);

        transform.localRotation = Quaternion.Euler(currentRotation);

        //Debug.Log("Current Rotation : " + currentRotation);

        //Debug.Log("Target Pos : " + targetPosition);
        //Debug.Log("Current Pos : " + currentPosition);

        /*activatedWeapon.*/
        transform.localPosition = currentPosition;
    }

    public void RecoilFire(float backKick, float recoilY, float recoilZ)
    {

        //Z affects Vertical Axis and Y the Horizontal one
        targetRotation += new Vector3(0, recoilY,-recoilZ);

      
        Vector3 directionToMove = GunBarrel.transform.up;

        Debug.Log("Activated weapon : " + activatedWeapon.name);

        //if (player != null)
        //{
        //    Debug.Log("Not null");
        //    directionToMove = -player.transform.forward.normalized;
        //}

        //directionToMove *= -1;

        Vector3 tempPos = activatedWeapon.transform.position;

        if(directionToMove.z > 0f)
            directionToMove.z *= -1;

        Debug.Log("Dir to Move : " + directionToMove);

        Debug.DrawLine(tempPos, directionToMove * 100f, Color.red, 5f);
        targetPosition += directionToMove * backKick;

    }
}
