using UnityEngine;

public class EndBossPortal : MonoBehaviour
{
    [SerializeField] Vector3 ShopPos;
      public bool canTp;

    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.name == "Player")
        {
            canTp = true;
            //other.transform.position = ShopPos;
            //Physics.SyncTransforms();
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.transform.name == "Player")
        {
            canTp = false;
        }
    }

}
