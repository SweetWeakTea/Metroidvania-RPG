using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSound : MonoBehaviour
{
    [SerializeField] private int areaSoundIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            //if (AudioManager.instance.currentCoroutine != null) // �����е����ɫ����
            //    StopCoroutine(AudioManager.instance.currentCoroutine);
            AudioManager.instance.PlaySFX(areaSoundIndex, null);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
            AudioManager.instance.StopSFXWithTime(areaSoundIndex);
    }
}
