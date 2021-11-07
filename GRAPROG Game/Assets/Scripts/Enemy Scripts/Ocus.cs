using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ocus : Enemy
{
    private bool _playerIsTargeted = false;
     
    void Update()
    {
        DistanceFromPlayer = Mathf.Abs(gameObject.transform.position.x - Player.transform.position.x);
        EnemyAnimator.SetFloat("Distance", DistanceFromPlayer);

        DetectRadius = PlayerControllerScript.LightRadius + 1.5f;
        if (!_isPlayerHiding && DistanceFromPlayer < DetectRadius && _isNotAttacking && _hasTransformed)
        {
            ApproachPlayer();
            _playerIsTargeted = true;
        }
        else if (_isPlayerHiding && DistanceFromPlayer < DetectRadius && PlayerControllerScript.LightRadius != 0.5f)
        {
            ApproachPlayer();
            _playerIsTargeted = true;
        }
        else if (_hasTransformed && _isNotAttacking)
        {
            Patrol();
            _playerIsTargeted = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && _playerIsTargeted)
        {
            EnemyCollider = collision;
            IsStillAttacking();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && _playerIsTargeted)
            EnemyCollider = null;
    }
}   
