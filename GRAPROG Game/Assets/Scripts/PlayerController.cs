using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    public SpriteRenderer PlayerSprite;
    public Animator PlayerAnimator;
    public GameObject Lantern;
    public float PlayerSpeed;

    [HideInInspector]
    public float LightRadius;

    void Start()
    {
        LightRadius = Lantern.GetComponent<Light2D>().pointLightOuterRadius;
    }
    
    void Update()
    {
        float LanternPower = Input.GetAxis("Mouse ScrollWheel") * 5f;

        if (LanternPower != 0)
        {
            LightRadius = Mathf.Clamp(LightRadius += LanternPower, 1.0f, 4.5f);
            Lantern.GetComponent<Light2D>().pointLightOuterRadius = LightRadius;
        }

        float MovePlayer = Input.GetAxis("Horizontal");

        if (MovePlayer > 0)
        {
            transform.Translate(Vector2.right * Time.deltaTime * PlayerSpeed * MovePlayer);
            PlayerSprite.flipX = false;
            PlayerAnimator.SetFloat("Moving", Mathf.Abs(MovePlayer));
        }

        if (MovePlayer < 0)
        {
            transform.Translate(Vector2.left * Time.deltaTime * PlayerSpeed * -MovePlayer);
            PlayerSprite.flipX = true;
            PlayerAnimator.SetFloat("Moving", Mathf.Abs(MovePlayer));
        }
    }
}
