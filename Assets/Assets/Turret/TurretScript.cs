using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    //public fireProjectile weapon;
    public Transform target;
    public Transform barrel;
    public bool canAim;

    [Header("Weapon")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField]float RateOfFire;
    [SerializeField] GameObject pointOfBarrel;
    public bool firing;
    float fireTimer;


    private void Update()
    {

        if (canAim)
        {
            RotateTurret();
            Shoot();
        }

    }

    private void RotateTurret()
    {
        // TURN
        float angleY = vector3AngleOnPlane(target.position, transform.position, -transform.up, transform.forward);
        Vector3 rotationY = new Vector3(0, angleY, 0);
        transform.Rotate(rotationY, Space.Self);

        // UP / DOWN
        if (barrel != null)
        {
            //float angleX = vector3AngleOnPlane(target.position, transform.position, -transform.right, transform.forward);
            float angleX = vector3AngleOnPlane(target.position, barrel.position, -transform.right, transform.forward);

            Vector3 rotationX = new Vector3(angleX, 0, 0);
            barrel.localRotation = Quaternion.Euler(rotationX);
        }
    }

        float vector3AngleOnPlane(Vector3 from, Vector3 to, Vector3 planeNormal, Vector3 toZeroAngle)
        {
            Vector3 projectedVector = Vector3.ProjectOnPlane(from - to, planeNormal);
            float projectedVectorAngle = Vector3.SignedAngle(projectedVector, toZeroAngle, planeNormal);

            return projectedVectorAngle;
        }
        
    void Shoot()
    {

        if(firing)
        {
            while (fireTimer >= 1f / RateOfFire)
            {
                Instantiate(bulletPrefab, pointOfBarrel.transform.position, pointOfBarrel.transform.rotation);
                fireTimer -= 1f / RateOfFire;
            }

            fireTimer += Time.deltaTime;
            firing = false;
        }
        else
        {
            if (fireTimer < 1f / RateOfFire)
            {
                fireTimer += Time.deltaTime;
            }
            else
            {
                fireTimer = 1f / RateOfFire;
            }
        }
      
       
    }

    public void Fire()
    {
        firing = true;
    }

}
