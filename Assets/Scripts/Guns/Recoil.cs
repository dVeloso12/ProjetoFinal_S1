using UnityEngine;

public class Recoil : MonoBehaviour
{

    private Vector3 currentRotation, targetRotation;

    [SerializeField] float snapiness, returnSpeed;
    [SerializeField] Vector3 defaultRotation;

    void Start()
    {
        
    }


    void Update()
    {

        targetRotation = Vector3.Lerp(targetRotation, defaultRotation, returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snapiness * Time.deltaTime);

        transform.localRotation = Quaternion.Euler(currentRotation);

    }

    public void RecoilFire(float recoilX, float recoilY, float recoilZ)
    {

        //Z affects Vertical Axis and Y the Horizontal one
        targetRotation += new Vector3(recoilX, Random.Range(-recoilY,recoilY), Random.Range(-recoilZ, recoilZ));
    }
}
