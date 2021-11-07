using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Set Components")]
    public PlayerController PlayerControllerScript;
    public PlayerState PlayerStateScript;
    public Transform Player, Waypoint1, Waypoint2, ThisEnemy;
    public Animator EnemyAnimator;

    [Header("Enemy Stats")]
    public float MovementSpeed;
    public float DetectRadius;
    public float FrostDamage;
    public float HPDamage;
    public int MaxAttacks;
    
    [HideInInspector]
    public int AttackCounter;
    [HideInInspector]
    public float DistanceFromPlayer;
    [HideInInspector]
    public int Direction;
    [HideInInspector]
    public float EnemyPositionX;

    public void GetMoveDirection()
    {

        if (ThisEnemy.position.x > Player.transform.position.x)
            Direction = -1;

        if (ThisEnemy.position.x < Player.transform.position.x)
            Direction = 1;
    }

    public void ApproachPlayer()
    {
        ThisEnemy.Translate(Vector2.right * Time.deltaTime * MovementSpeed * Direction);
    }

    public void Patrol()
    {
        EnemyPositionX = ThisEnemy.position.x;
        EnemyPositionX = Mathf.Lerp(Waypoint1.position.x, Waypoint2.position.x, Random.Range(5, 10));
    }

    public void GetDetectRadius()
    {
        DetectRadius = PlayerControllerScript.LightRadius * 2;
    }

    public void DoDamage()
    {

    }
 }
