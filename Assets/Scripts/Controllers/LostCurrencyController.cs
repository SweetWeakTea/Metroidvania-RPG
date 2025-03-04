using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostCurrencyController : MonoBehaviour
{
    public int currency;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            UI_InGame.instance.soulsDifference = currency;
            PlayerManager.instance.currency += currency;
            Destroy(this.gameObject);
        }
    }
}
