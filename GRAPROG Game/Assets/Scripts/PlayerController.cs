using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    public PlayerState PlayerStateScript;
    public SpriteRenderer PlayerSprite;
    public Animator PlayerAnimator;
    public GameObject Lantern;
    public float PlayerSpeed;
    

    [HideInInspector]
    public float LightRadius;
    [HideInInspector]
    public float MovePlayer;
    [HideInInspector]
    public float LanternPowerFade = 1f;

    private bool _isStillAlive;

    void Start()
    {
        LightRadius = Lantern.GetComponent<Light2D>().pointLightOuterRadius;
        _isStillAlive = true;
    }
    
    void Update()
    {
        if (_isStillAlive)
        {
            float LanternPowerPosition = Input.GetAxis("Mouse ScrollWheel");
            float LanternPower = LanternPowerPosition * 2.5f;

            if (LanternPower != 0 && PlayerStateScript.Health > 0)
            {
                LightRadius = Mathf.Clamp(LightRadius += LanternPower, 0.5f, 2.25f);
                LanternPowerFade = Mathf.Clamp(LanternPowerFade += LanternPowerPosition, 0.3f, 1f);
                Lantern.GetComponent<Light2D>().pointLightOuterRadius = LightRadius;
            }

            MovePlayer = Input.GetAxis("Horizontal");

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

        if (PlayerStateScript._currentFrostDamage == 0)
        {
            PlayerAnimator.SetFloat("Damage", PlayerStateScript._currentFrostDamage);
            _isStillAlive = false;
        }
   
    }
}
