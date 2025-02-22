using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveManager
{
    // 在接口中声明方法时，不需要也不能写上访问权限修饰符。接口成员默认是公共的。
    void LoadData(GameData _data); 
    void SaveData(ref GameData _data);
}
