using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemDrop : ItemDrop
{
    [Header("Player's drop")]
    [SerializeField] private float chanceToLoseItem;
    [SerializeField] private float chanceToLoseMaterials;

    public override void GenerateDrop()
    {
        Inventory inventory = Inventory.instance;

        List<InventoryItem> itemsToUnequip = new List<InventoryItem>(); 
        List<InventoryItem> materialsToLose = new List<InventoryItem>(); // �½� List ������Ҫ�Ƴ��� װ��

        //foreach (InventoryItem item in inventory.GetEquipmentList())
        //{
        //    if (Random.Range(0, 100) <= chanceToLoseItem)
        //    {
        //        DropItem(item.data);
        //        itemsToUnequip.Add(item);
        //    }
        //}

        for (int i = 0; i < inventory.GetEquipmentList().Count; i++) // foreach �޷�ʵ�֣�ԭ������unity�°��bug����for Ҳ����ʱ���
        {
            if (Random.Range(0, 100) <= chanceToLoseItem)
            {
                DropItem(inventory.GetEquipmentList()[i].data);
                itemsToUnequip.Add(inventory.GetEquipmentList()[i]);
            }
        }

        for (int i = 0; i < itemsToUnequip.Count; i++)
        {
            inventory.UnequipItem(itemsToUnequip[i].data as ItemData_Equipment);
        }

        //foreach (InventoryItem item in inventory.GetStashList())
        //{
        //    if (Random.Range(0, 100) <= chanceToLoseMaterials)
        //    {
        //        DropItem(item.data);
        //        materialsToLose.Add(item);
        //    }
        //}

        for (int i = 0; i < inventory.GetStashList().Count; i++) // ������һ����Ʒ
        {
            if (Random.Range(0, 100) <= chanceToLoseItem)
            {
                DropItem(inventory.GetStashList()[i].data);
                materialsToLose.Add(inventory.GetStashList()[i]);
            }
        }

        for (int i = 0; i < materialsToLose.Count; i++)
        {
            inventory.RemoveItem(materialsToLose[i].data);
        }
    }
}
