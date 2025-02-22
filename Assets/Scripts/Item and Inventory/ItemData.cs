using System.Text;
using UnityEngine;

#if UNITY_EDITOR // 请注意，OnValidate 方法只在编辑器中调用，因此我们使用 #if UNITY_EDITOR 预处理指令来确保这些代码只在编辑器中编译和运行。
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
#if UNITY_EDITOR // 请注意，OnValidate 方法只在编辑器中调用，因此我们使用 #if UNITY_EDITOR 预处理指令来确保这些代码只在编辑器中编译和运行。
        string path = AssetDatabase.GetAssetPath(this);
        itemId = AssetDatabase.AssetPathToGUID(path);
#endif
    }

    public virtual string GetDescription()
    {
        return "";
    }
}
