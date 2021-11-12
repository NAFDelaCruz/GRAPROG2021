using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHidingScript : MonoBehaviour
{
    public Ocus OcusScript;
    public Raider RaiderScript;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OcusScript._isPlayerHiding = true;
            RaiderScript._isPlayerHiding = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OcusScript._isPlayerHiding = false;
            RaiderScript._isPlayerHiding = false;
        }
    }
}
