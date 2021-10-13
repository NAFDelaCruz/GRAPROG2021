using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    [Header("Set Components")]
    public Slider FuelBar;
    public Slider FrostBar;
    public Material FrostEffect;
    public Image FrostWarning;
    public PlayerController PlayerControllerScript;

    [Header("Game Values")]
    public float Health;
    [Tooltip("The amount of Frost taken overtime")]
    public float OvertimeFrostDamage;
    [Tooltip("The number of seconds before Frost increases")]
    public float FrostEffectsDelay;
    [Tooltip("This value is divided by the Lantern's Point Light Outer Radius which results into the delay of HP deplete.")]
    public float HPDepleteRate;
    
    private float _maxHealth;
    private float _maxFrostDamage;
    private float _currentFrostDamage;
    private float _frostDamageExecuteTime = 0;
    private float _hpDepleteTime = 0;
    private float _defrostExecuteTime = 0;
    private float _transparency;
    private float _iceState;
    private bool _isTakingFrost = false;

    void Start()
    {
        _maxFrostDamage = FrostBar.value;
        _currentFrostDamage = _maxFrostDamage;
        _maxHealth = Health;
        FrostEffect.SetFloat("_Fade", 0);
    }    

    void Update()
    {
        if (_currentFrostDamage < 0 && _isTakingFrost)
        {
            FrostWarning.color = new Color(1, 1, 1, _transparency);
            float pingpong = Mathf.PingPong(Time.time, 0.5f);
            _transparency = Mathf.Lerp(0f, 0.65f, pingpong);
        }
        
        if (Input.GetKeyDown(KeyCode.H) && Health < _maxHealth)
        {
            Health = Mathf.Clamp(Health += 10, 0, 100);
            FuelBar.value = _maxHealth - Health;
        }

        if (Time.time >= _hpDepleteTime && Health > 0)
        {
            Health = Mathf.Clamp(Health -= 1, 0, 100);
            FuelBar.value = _maxHealth - Health;
            _hpDepleteTime = Time.time + (HPDepleteRate / PlayerControllerScript.LightRadius);
        }

        //Frost Effect
        if ((PlayerControllerScript.LightRadius <= 3.0f && PlayerControllerScript.LightRadius > 2.0f) && (_currentFrostDamage <= 100 && _currentFrostDamage > 75) && Time.time > _frostDamageExecuteTime)
        {
            AddFrostEffecT();
            _isTakingFrost = true;
        }
        else if ((PlayerControllerScript.LightRadius <= 2.0f && PlayerControllerScript.LightRadius >= 1f) && (_currentFrostDamage <= 100 && _currentFrostDamage > 50) && Time.time > _frostDamageExecuteTime)
        {
            AddFrostEffecT();
            _isTakingFrost = true;
        }

        //Defrost Effect 
        if (PlayerControllerScript.LightRadius > 3.0f && (_currentFrostDamage < 100 && _currentFrostDamage >= 50) && Time.time > _defrostExecuteTime)
        {
            DefrostEffecT();
        }
        else if ((PlayerControllerScript.LightRadius <= 3.0f && PlayerControllerScript.LightRadius > 2f) && (_currentFrostDamage < 75 && _currentFrostDamage >= 50) && Time.time > _defrostExecuteTime)
        {
            DefrostEffecT();
        } 

        if (Health == 0 && Time.time > _frostDamageExecuteTime && _currentFrostDamage > 0)
        {
            AddFrostEffecT();
        }

        if (_currentFrostDamage >= 0 && _currentFrostDamage <= 49)
        {
            FrostWarning.color = new Color(1, 1, 1, 0);
            FrostEffect.SetFloat("_Fade", _iceState = Mathf.Clamp(_iceState += 0.1f, 0, 1) * Time.deltaTime);
        }
        else if (Health > 0 && _iceState > 0)
        {
            FrostEffect.SetFloat("_Fade", _iceState = Mathf.Clamp(_iceState -= 0.1f, 0, 1) * Time.deltaTime);
        }
    }

    void AddFrostEffecT()
    {
        _currentFrostDamage -= OvertimeFrostDamage;
        FrostBar.value = _currentFrostDamage;
        _frostDamageExecuteTime = Time.time + FrostEffectsDelay;
    }

    void DefrostEffecT()
    {
        _currentFrostDamage += OvertimeFrostDamage;
        FrostBar.value = _currentFrostDamage;
        _defrostExecuteTime = Time.time + FrostEffectsDelay;
    }
}
