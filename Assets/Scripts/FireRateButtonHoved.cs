using UnityEngine;
using UnityEngine.EventSystems;

public class FireRateButtonHoved : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public UpgradeShop upgradeShop;
    public string price;  // Store the price for this specific button

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse entered the button");
        upgradeShop.ShowPriceTooltip(price, transform.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse exited the button");
        upgradeShop.HidePriceTooltip();
    }
}
