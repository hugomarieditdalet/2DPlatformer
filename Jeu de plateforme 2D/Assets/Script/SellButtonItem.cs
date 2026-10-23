using UnityEngine;
using UnityEngine.UI;
public class SellButtonItem : MonoBehaviour
{
    public Text itemName;
    public Image itemImage;
    public Text itemPrice;

    public Item item;
    public void BuyIttem()
    {
        Inventory inventory = Inventory.instance;

        if (Inventory.instance.coinsCount >= item.price)
        {
            Inventory.instance.content.Add(item);
            Inventory.instance.UpdateInventoryUI();
            inventory.coinsCount -= item.price;
            inventory.UpdateTextUI();
        }
    }
}
