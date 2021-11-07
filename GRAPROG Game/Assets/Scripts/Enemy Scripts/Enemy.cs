using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Set Components")]
    public PlayerController PlayerControllerScript;
    public PlayerState PlayerStateScript;
    public GameObject Player, Waypoint1, Waypoint2, ThisEnemy;
    public Animator EnemyAnimator;
    public Collider2D EnemyCollider;
    public ParticleSystem DamageParticles;

    [Header("Enemy Stats")]
    public float MovementSpeed;
    public float DetectRadius;
    public float FrostDamage;
    public float HPDamage;
    public int MaxAttacks;
    
    [HideInInspector]
    public float DistanceFromPlayer;
    [HideInInspector]
    public int Direction;
    [HideInInspector]
    public bool _isNotAttacking = true;
    [HideInInspector]
    public bool _isPlayerHiding = false;
    [HideInInspector]
    public bool _hasTransformed = false;
    [HideInInspector]
    public bool _flipToggle = false;
    [HideInInspector]
    public bool _isNotIdle = true;

    private float _enemyPositionX;

    void Start()
    {
        _enemyPositionX = ThisEnemy.transform.position.x;
    }

    public void GetMoveDirection()
    {
        if (ThisEnemy.transform.position.x > Player.transform.position.x)
        {
            ThisEnemy.GetComponent<SpriteRenderer>().flipX = false;
            Direction = -1;
        }

        if (ThisEnemy.transform.position.x < Player.transform.position.x)
        {
            ThisEnemy.GetComponent<SpriteRenderer>().flipX = true;
            Direction = 1;
        }
    }

    public void ApproachPlayer()
    {
        GetMoveDirection();
        ThisEnemy.transform.Translate(Vector2.right * Time.deltaTime * MovementSpeed * Direction);
        ThisEnemy.GetComponent<SpriteRenderer>().sortingOrder = Player.GetComponent<SpriteRenderer>().sortingOrder;
        EnemyAnimator.SetInteger("State", 1);
    }

    public void Patrol()
    {
        if (!_flipToggle && _isNotIdle && ThisEnemy.transform.position.x > Waypoint1.transform.position.x)
        {
            ThisEnemy.transform.Translate(Vector2.right * Time.deltaTime * MovementSpeed * -1);
            EnemyAnimator.SetInteger("State", 1);
        }
        if (_flipToggle && _isNotIdle && ThisEnemy.transform.position.x < Waypoint2.transform.position.x)
        {
            ThisEnemy.transform.Translate(Vector2.right * Time.deltaTime * MovementSpeed * 1);
            EnemyAnimator.SetInteger("State", 1);
        }

        if (ThisEnemy.transform.position.x < Waypoint1.transform.position.x)
        {
            StartCoroutine(Idle());
        }
        else if (ThisEnemy.transform.position.x > Waypoint2.transform.position.x)
        {
            StartCoroutine(Idle());
        }

        ThisEnemy.GetComponent<SpriteRenderer>().flipX = _flipToggle;
    }

    public IEnumerator Idle()
    {
        _isNotIdle = false;
        EnemyAnimator.SetInteger("State", 0);
        yield return new WaitForSeconds(5);
        _isNotIdle = true;
        if (ThisEnemy.transform.position.x < Waypoint1.transform.position.x)
        {
            _flipToggle = true;
        }
        else if (ThisEnemy.transform.position.x > Waypoint2.transform.position.x)
        {
            _flipToggle = false;
        }
    }

    public void DoDamage()
    {
        if (EnemyCollider != null)
        {
            PlayerStateScript.Health -= HPDamage;
            PlayerStateScript._currentFrostDamage -= FrostDamage;
            DamageParticles.Play();
        }
    }

    public void IsStillAttacking()
    {
        EnemyAnimator.SetInteger("State", 2);
        _isNotAttacking = false;
    }

    public void IsDoneAttacking()
    {
        _isNotAttacking = true;
    }

    public void HasTransformed()
    {
        _hasTransformed = true;
        EnemyAnimator.SetBool("HasTransformed", _hasTransformed);
    }
}
