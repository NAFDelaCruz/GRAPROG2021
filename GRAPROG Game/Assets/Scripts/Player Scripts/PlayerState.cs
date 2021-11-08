using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerState : MonoBehaviour
{
    [Header("Set Components")]
    public UIController UIControllerScript;
    public PlayerController PlayerControllerScript;

    [Header("Game Values")]
    public float Health;
    [Tooltip("The amount of Frost taken overtime")]
    public float OvertimeFrostDamage;
    [Tooltip("The number of seconds before Frost increases")]
    public float FrostEffectsDelay;
    [Tooltip("This value is divided by the Lantern's Point Light Outer Radius which results into the delay of HP deplete.")]
    public float HPDepleteRate;

    //[HideInInspector]
    public float _currentFrostDamage;
    [HideInInspector]
    public float _iceState;
    private float _frostDamageExecuteTime = 0;
    private float _hpDepleteTime = 0;
    private float _defrostExecuteTime = 0;
    private float _lastCurrentFrostDamage = 49;

    void Start()
    {
        _currentFrostDamage = 100;
        UIControllerScript._maxHealth = Health;
    }    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Health = Mathf.Clamp(Health += 10, 0, 100);
        }
        //Depletes Fuel/HP overtime with rate being controlled by Lantern Power
        if (Time.time >= _hpDepleteTime && Health > 0 && PlayerControllerScript.LightRadius >= 0.5f)
        {
            Health = Mathf.Clamp(Health -= 1, 0, 100);
            _hpDepleteTime = Time.time + (HPDepleteRate / PlayerControllerScript.LightRadius);
        }

        //Add Frost damage if Lantern Power is low
        if (Health > 0 && (PlayerControllerScript.LightRadius <= 1.75f && PlayerControllerScript.LightRadius > 1.25f) && (_currentFrostDamage <= 100 && _currentFrostDamage > 75) && Time.time > _frostDamageExecuteTime)
        {
            AddFrostEffecT();
            UIControllerScript._isTakingFrostDamage = true;
        }
        else if (_currentFrostDamage == 75)
        {
            UIControllerScript._isTakingFrostDamage = false;
        }

        if (Health > 0 && (PlayerControllerScript.LightRadius <= 1.25f && PlayerControllerScript.LightRadius >= 0.5f) && (_currentFrostDamage <= 100 && _currentFrostDamage > 50) && Time.time > _frostDamageExecuteTime)
        {
            AddFrostEffecT();
            UIControllerScript._isTakingFrostDamage = true;
        }
        else if (_currentFrostDamage == 50)
        {
            UIControllerScript._isTakingFrostDamage = false;
        }

        //Subract Frost damage if Lantern Power is sufficient 
        if (Health > 0 && PlayerControllerScript.LightRadius > 1.75f && (_currentFrostDamage < 100 && _currentFrostDamage > 0) && Time.time > _defrostExecuteTime)
        {
            DefrostEffecT();
            UIControllerScript._isTakingFrostDamage = false;
        }
        else if (Health > 0 && (PlayerControllerScript.LightRadius <= 1.75f && PlayerControllerScript.LightRadius > 1.25f) && (_currentFrostDamage < 75 && _currentFrostDamage >= 50) && Time.time > _defrostExecuteTime)
        {
            DefrostEffecT();
            UIControllerScript._isTakingFrostDamage = false;
        }
        else if (Health > 0 && (PlayerControllerScript.LightRadius <= 1.75f && PlayerControllerScript.LightRadius >= 0.5f) && (_currentFrostDamage < 50 && _currentFrostDamage > 0) && Time.time > _defrostExecuteTime)
        {
            DefrostEffecT();
            UIControllerScript._isTakingFrostDamage = false;
        } 

        //Add Frost damage if Fuel/HP = 0
        if (Health == 0 && Time.time > _frostDamageExecuteTime && _currentFrostDamage > 0)
        {
            AddFrostEffecT();
            if (_currentFrostDamage >= 40)
            {
                UIControllerScript._isTakingFrostDamage = true;
            } 
            else
            {
                UIControllerScript._isTakingFrostDamage = false;
            }
        }

        //Initiate Icicle effect
        if (Health == 0 && _currentFrostDamage <= 49)
        {
            if (_lastCurrentFrostDamage > _currentFrostDamage)
            {
                _lastCurrentFrostDamage = _currentFrostDamage;
                _iceState = Mathf.Clamp(_iceState = (1 - (_currentFrostDamage / 49)), 0, 1);
            }
                
        }
        else if (Health > 0 && _currentFrostDamage > 0)
        {
            _lastCurrentFrostDamage = 49;
            _iceState = Mathf.Clamp(_iceState = (1 - (_currentFrostDamage / 49)), 0, 1);
        }
    }
    //Function to add frost damage overtime
    void AddFrostEffecT()
    {
        _currentFrostDamage += -OvertimeFrostDamage;
        _frostDamageExecuteTime = Time.time + FrostEffectsDelay;
    }

    //Function to subtract frost damage overtime
    void DefrostEffecT()
    {
        _currentFrostDamage -= -OvertimeFrostDamage;
        _defrostExecuteTime = Time.time + FrostEffectsDelay;
    }
}
