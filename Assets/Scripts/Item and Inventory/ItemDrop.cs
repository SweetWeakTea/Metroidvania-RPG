using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private int possibleItemDrop; // �ɵ������Ʒ����
    [SerializeField] private ItemData[] possibleDrop; // �ɵ�����Ʒ�б�
    private List<ItemData> dropList = new List<ItemData>(); // ʵ�ʵ������Ʒ�б�

    [SerializeField] private GameObject dropPrefab;

    public virtual void GenerateDrop()
    {
        if (possibleDrop.Length <= 0) // �ɵ�����ƷΪ0
            return;

        for (int i = 0; i < possibleDrop.Length; i++)
        {
            if (Random.Range(0, 100) <= possibleDrop[i].dropChance)
                dropList.Add(possibleDrop[i]);
        }

        for (int i = 0; i < possibleItemDrop; i++)
        {
            if (dropList.Count <= 0)
                return;

            ItemData randomItem = dropList[Random.Range(0, dropList.Count)]; // ����ҿ�����Ҫ�� Count - 1

            dropList.Remove(randomItem);
            DropItem(randomItem);
        }
    }

    protected void DropItem(ItemData _itemData)
    {
        GameObject newDrop = Instantiate(dropPrefab, transform.position, Quaternion.identity);

        Vector2 randomVelocity = new Vector2(Random.Range(-5, 5), Random.Range(15, 20));

        newDrop.GetComponent<ItemObject>().SetupItem(_itemData, randomVelocity);
    }
}