using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raider : Enemy
{
    private bool _playerIsTargeted = false;

    void Update()
    {
        DistanceFromPlayer = Mathf.Abs(gameObject.transform.position.x - Player.transform.position.x);

        DetectRadius = PlayerControllerScript.LightRadius + 1.5f;
        if (!_isPlayerHiding && DistanceFromPlayer <= DetectRadius && _isNotAttacking)
        {
            ApproachPlayer();
            _playerIsTargeted = true;
        }
        else if (_isPlayerHiding && DistanceFromPlayer <= DetectRadius && PlayerControllerScript.LightRadius != 0.5f && _isNotAttacking)
        {
            ApproachPlayer();
            _playerIsTargeted = true;
        }
        else if (_isPlayerHiding && DistanceFromPlayer <= DetectRadius && PlayerControllerScript.LightRadius == 0.5f && _isNotAttacking && ThisEnemy.GetComponent<SpriteRenderer>().sortingOrder == Player.GetComponent<SpriteRenderer>().sortingOrder)
        {
            ApproachPlayer();
            _playerIsTargeted = true;
        }
        else if (_isNotAttacking)
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
