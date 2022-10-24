using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveInfosShop : MonoBehaviour
{
    public GameObject Portal;
    public GameObject Button;

    public bool getCollidePortal()
    {
        return Portal.GetComponent<ShopPortal>().playerColide;
    }
    public void setCollidePortal(bool change)
    {
        Portal.GetComponent<ShopPortal>().playerColide = change;
    }
    public bool getCollideButton()
    {
        return Button.GetComponent<ShopButtomPortal>().playerColide;
    }
    public void setCollideButton(bool change)
    {
        Button.GetComponent<ShopPortal>().playerColide = change;
    }


}
