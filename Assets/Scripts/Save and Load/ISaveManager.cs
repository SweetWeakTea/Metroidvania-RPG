using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveManager
{
    // �ڽӿ�����������ʱ������ҪҲ����д�Ϸ���Ȩ�����η����ӿڳ�ԱĬ���ǹ����ġ�
    void LoadData(GameData _data); 
    void SaveData(ref GameData _data);
}
