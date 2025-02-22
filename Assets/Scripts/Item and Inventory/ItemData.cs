using System.Text;
using UnityEngine;

#if UNITY_EDITOR // ��ע�⣬OnValidate ����ֻ�ڱ༭���е��ã��������ʹ�� #if UNITY_EDITOR Ԥ����ָ����ȷ����Щ����ֻ�ڱ༭���б�������С�
using UnityEditor;
#endif

public enum ItemType
{
    Material,
    Equipment
}

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    public ItemType itemType;
    public string itemName;
    public Sprite itemIcon;
    public string itemId;

    [Range(0, 100)]
    public float dropChance;

    protected StringBuilder sb = new StringBuilder();

    private void OnValidate()
    {
#if UNITY_EDITOR // ��ע�⣬OnValidate ����ֻ�ڱ༭���е��ã��������ʹ�� #if UNITY_EDITOR Ԥ����ָ����ȷ����Щ����ֻ�ڱ༭���б�������С�
        string path = AssetDatabase.GetAssetPath(this);
        itemId = AssetDatabase.AssetPathToGUID(path);
#endif
    }

    public virtual string GetDescription()
    {
        return "";
    }
}
