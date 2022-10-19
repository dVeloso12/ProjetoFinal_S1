using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Pistol : GunController
{


    public GameObject markSprite;

    private RaycastHit collisionDetected;

    public Transform _camera;
    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    protected override void Shoot()
    {
        if (Physics.Raycast(_camera.position, _camera.forward, out collisionDetected))
        {
            Instantiate(markSprite, collisionDetected.point+(collisionDetected.normal*.1f), Quaternion.LookRotation(collisionDetected.normal)).transform.Rotate(Vector3.right*90);

            Debug.Log("Shoot");



        }
        
        
        //Instantiate(bullet, ShotingPlace.position, ShotingPlace.rotation);


        shoot = !shoot;

        base.Shoot();
    }
}
