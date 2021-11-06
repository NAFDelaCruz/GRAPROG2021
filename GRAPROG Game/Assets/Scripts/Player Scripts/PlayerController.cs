using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Set Components")]
    public PlayerState PlayerStateScript;
    public SpriteRenderer PlayerSprite;
    public Animator PlayerAnimator;
    public GameObject Lantern;
    public float PlayerSpeed;
    public TextMeshProUGUI PlayerLayerGUI;
    

    [HideInInspector]
    public float LightRadius;
    [HideInInspector]
    public float MovePlayer;
    [HideInInspector]
    public float ChangeOrder;
    [HideInInspector]
    public float LanternPowerFade = 1f;
    [HideInInspector]
    public int _layerOrder;

    private bool _isStillAlive;

    void Start()
    {
        LightRadius = Lantern.GetComponent<Light2D>().pointLightOuterRadius;
        _isStillAlive = true;
        _layerOrder = PlayerSprite.sortingOrder;
    }
    
    void Update()
    {
        if (_isStillAlive)
        {
            float LanternPowerPosition = Input.GetAxis("Mouse ScrollWheel");
            float LanternPower = LanternPowerPosition * 2.5f;
            MovePlayer = Input.GetAxis("Horizontal");

            if (LanternPower != 0 && PlayerStateScript.Health > 0)
            {
                LightRadius = Mathf.Clamp(LightRadius += LanternPower, 0.5f, 2.25f);
                LanternPowerFade = Mathf.Clamp(LanternPowerFade += LanternPowerPosition, 0.3f, 1f);
                Lantern.GetComponent<Light2D>().pointLightOuterRadius = LightRadius;
            }

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

            if (Input.GetKeyDown(KeyCode.W))
            {
                _layerOrder = Mathf.Clamp(_layerOrder += 1, 1, 3);
                PlayerSprite.sortingOrder = _layerOrder;
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                _layerOrder = Mathf.Clamp(_layerOrder -= 1, 1, 3);
                PlayerSprite.sortingOrder = _layerOrder;
            }
            
            PlayerLayerGUI.text = _layerOrder.ToString();
        }

        if (PlayerStateScript._currentFrostDamage == 0)
        {
            PlayerAnimator.SetFloat("Damage", PlayerStateScript._currentFrostDamage);
            _isStillAlive = false;
        }
    }
}
