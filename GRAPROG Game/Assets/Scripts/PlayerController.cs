using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public SpriteRenderer PlayerSprite;
    public Animator PlayerAnimator;
    public float PlayerSpeed;

    // Update is called once per frame
    void Update()
    {
        float MovePlayer = Input.GetAxis("Horizontal");

        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.Translate(Vector2.right * Time.deltaTime * PlayerSpeed * MovePlayer);
            PlayerSprite.flipX = false;
            PlayerAnimator.SetFloat("Moving", Mathf.Abs(MovePlayer));
        }

        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.Translate(Vector2.left * Time.deltaTime * PlayerSpeed * -MovePlayer);
            PlayerSprite.flipX = true;
            PlayerAnimator.SetFloat("Moving", Mathf.Abs(MovePlayer));
        }
    }
}
