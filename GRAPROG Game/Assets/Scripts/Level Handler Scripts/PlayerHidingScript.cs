using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHidingScript : MonoBehaviour
{
    public Ocus OcusScript;
    //public Bandit BanditScript;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            OcusScript._isPlayerHiding = true;
            
         
            //BanditScript._isPlayerHiding = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            OcusScript._isPlayerHiding = false;
            //BanditScript._isPlayerHiding = false;
    }
}
