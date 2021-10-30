using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class UIController : MonoBehaviour
{
    [Header("Set Components")]
    public Slider FuelBar;
    public Slider FrostBar;
    public Material FrostEffect;
    public Material LanternPower;
    public Image FrostWarning;
    public PlayerController PlayerControllerScript;
    public PlayerState PlayerStateScript;
    
    [HideInInspector]
    public float _maxFrostDamage;
    [HideInInspector]
    public float _maxHealth;
    [HideInInspector]
    public bool _isTakingFrostDamage = false;

    private float _transparency;

    void Start()
    {
        _maxHealth = FuelBar.maxValue;
        _maxFrostDamage = FrostBar.value;
        FrostEffect.SetFloat("_Fade", 0);
    }
    
    void Update()
    {
        StartCoroutine(DoFrostWarning());
        StartCoroutine(TurnOffLantern());
        StartCoroutine(TurnOnLantern());
        if (!_isTakingFrostDamage)
        FrostWarning.color = new Color(1, 1, 1, 0);

        FuelBar.value = _maxHealth - PlayerStateScript.Health;
        FrostBar.value = PlayerStateScript._currentFrostDamage;

        FrostEffect.SetFloat("_Fade", PlayerStateScript._iceState);
        LanternPower.SetFloat("_Fill",PlayerControllerScript.LanternPowerFade);
    }
    
    IEnumerator DoFrostWarning()
    {
        while (_isTakingFrostDamage)
        {
            FrostWarning.color = new Color(1, 1, 1, _transparency);
            float pingpong = Mathf.PingPong(Time.time, 0.5f);
            _transparency = Mathf.Lerp(0f, 0.65f, pingpong);
            yield return 0;
        }
    }

    IEnumerator TurnOffLantern()
    {
        while (PlayerStateScript.Health == 0)
        {
            PlayerControllerScript.LightRadius = Mathf.Clamp(PlayerControllerScript.LightRadius - 0.00001f, 0f, 2.25f);
            PlayerControllerScript.Lantern.GetComponent<Light2D>().pointLightOuterRadius = PlayerControllerScript.LightRadius;
            yield return 0;
        }
    }

    IEnumerator TurnOnLantern()
    {
        while (PlayerStateScript.Health > 0 && PlayerControllerScript.LightRadius < 0.5f)
        {
            PlayerControllerScript.LightRadius = Mathf.Clamp(PlayerControllerScript.LightRadius + 0.00001f, 0f, 0.5f);
            PlayerControllerScript.Lantern.GetComponent<Light2D>().pointLightOuterRadius = PlayerControllerScript.LightRadius;
            yield return 0;
        }
        
    }
}
