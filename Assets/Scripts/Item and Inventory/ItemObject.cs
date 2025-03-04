using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private ItemData itemData;

    private void SetupVisuals()
    {
        if (itemData == null)
            return;

        GetComponent<SpriteRenderer>().sprite = itemData.itemIcon;
        gameObject.name = "Item object - " + itemData.itemName;
    }

    public void SetupItem(ItemData _itemData, Vector2 _Velocity)
    {
        itemData = _itemData;
        rb.velocity = _Velocity;

        SetupVisuals();
    }

    public void PickupItem()
    {
        if (!Inventory.instance.CanAddItem() && itemData.itemType == ItemType.Equipment)
        {
            //rb.velocity = new Vector2(0, 7); //无法拾取物品时 物品跳动
            PlayerManager.instance.player.fx.CreatePopUpText("Inventory is full");
            return;
        }

        Inventory.instance.AddItem(itemData);
        Destroy(gameObject);
    }
}