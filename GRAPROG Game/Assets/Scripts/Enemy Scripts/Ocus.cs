using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ocus : Enemy
{
    public bool IsPlayerHiding;

    private bool _isNotAttacking;

    void Update()
    {
        DistanceFromPlayer = gameObject.transform.position.x - Player.transform.position.x;

    }

    //add attack animation wait for seconds then check if it's still not null then apply damage. make attacking false so move on update will not activate
    //yield return new WaitForSeconds(1.5f);
    void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {

        }
    }
}
