using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGroundCheck : MonoBehaviour
{
    [SerializeField] public int mapWidth, mapHeight;
    [SerializeField] public Vector3 mapCenter;

    [SerializeField] bool drawGizmos;

    [SerializeField] float RaycastHeight;

    [SerializeField] public Vector3 gridOriginPositionOffset;

    private void Start()
    {
    }

    

    public bool CheckIfIsWalkable(LayerMask groundLayer)
    {
        //Debug.Log("Checking Raycast");
        bool hasCollided = false;

        RaycastHit hit;

        hasCollided = Physics.Raycast(new Vector3(transform.position.x, RaycastHeight, transform.position.z), -transform.up, out hit, 100f, groundLayer);

        Vector3 originVector = new Vector3(transform.position.x, RaycastHeight, transform.position.z);

        if (hasCollided)
        {
            Vector3 destinationVector = new Vector3(transform.position.x, hit.point.y, transform.position.z);
            Debug.DrawLine(originVector, destinationVector, Color.green, 100f);

        }
        else
        {
            Vector3 destinationVector = new Vector3(transform.position.x, RaycastHeight - 100f, transform.position.z);
            Debug.DrawLine(originVector, destinationVector, Color.red, 100f);

        }

        //if (Physics.Raycast(new Vector3(transform.position.x, RaycastHeight, transform.position.z), -transform.up, out hit, 100f))
        //{
        //    Vector3 destinationVector = new Vector3(transform.position.x, hit.point.y, transform.position.z);
        //    Debug.DrawLine(originVector, destinationVector, Color.magenta, 100f);
        //    Debug.Log("hit : " + hit.collider.name);
        //    Debug.Log("Position : " + hit.point);
        //}

        //Debug.Log("Num of hits : " + hit.)

        //Debug.Log("Hit Name : " + hit.collider.name);
        //Debug.Log("Hit Tag : " + hit.collider.tag);

        //Debug.DrawLine(new Vector3(transform.position.x, RaycastHeight, transform.position.z),
        //    new Vector3(transform.position.x, RaycastHeight, transform.position.z) - transform.up * 100f,
        //    Color.red, 100f);


        //Debug.Log("Has Collided ? : " + hasCollided);

        //if (hasCollided)
        //{
        //    Debug.Log("World Position : " + transform.position);
        //}

        //Physics.OverlapSphere(transform.position, cellSize, groundLayer);
        //Physics.OverlapBox(transform.position, new Vector3(0.1f, 20f, 0.1f), Quaternion.identity, groundLayer);

        return hasCollided;
    }

    

    private void OnDrawGizmos()
    {
        Collider collider = GetComponent<Collider>();
        Vector3 finalSize = new Vector3(transform.localScale.x * collider.bounds.size.x,
                                    transform.localScale.y * collider.bounds.size.y,
                                    transform.localScale.z * collider.bounds.size.z);

        if (drawGizmos)
        {
            Gizmos.DrawCube(mapCenter, new Vector3(mapWidth, 10f, mapHeight));
            Gizmos.DrawCube(transform.position, new Vector3(0.1f, 0.1f, 0.1f));

        }
    }
}
