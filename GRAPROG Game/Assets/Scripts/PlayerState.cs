using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    public Slider FuelBar;
    public Slider FrostBar;
    public Material FrostEffect;
    public Image FrostWarning;
    public float Health;
    public float OvertimeFrostDamage;
    public float FrostDamageDelay;

    private float _damage = 10;
    private float _maxHealth;
    private float _maxFrostDamage;
    private float _currentFrostDamage;
    private float _frostDamageExecuteTime = 0;
    private float _defrostExecuteTime = 0;
    private float _transparency;
    private float _iceState;

    void Start()
    {
        _maxFrostDamage = FrostBar.value;
        _currentFrostDamage = _maxFrostDamage;
        _maxHealth = Health;
        FrostEffect.SetFloat("_Fade", 0);
    }    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && Health > 0)
        {
            Health -= _damage;
            FuelBar.value = _maxHealth - Health;
        }

        if (Input.GetKeyDown(KeyCode.H) && Health < _maxHealth)
        {
            Health += 10;
            FuelBar.value = _maxHealth - Health;
        }

        if (Health == 0 && Time.time > _frostDamageExecuteTime && _currentFrostDamage > 0)
        {
            _currentFrostDamage -= OvertimeFrostDamage;
            FrostBar.value = _currentFrostDamage;
            _frostDamageExecuteTime = Time.time + FrostDamageDelay;
        }
        else if (Health > 0 && Time.time > _defrostExecuteTime && _currentFrostDamage < _maxFrostDamage)
        {
            if (Health == 100 && Health >= 75)
            {
                _currentFrostDamage += 5;
                _defrostExecuteTime = Time.time + 0.5f;
            }
            else if (Health < 75 && Health >= 50 )
            {
                _currentFrostDamage += 3;
                _defrostExecuteTime = Time.time + 1f;
            }
            else if (Health < 50 && Health >= 1)
            {
                _currentFrostDamage += 1;
                _defrostExecuteTime = Time.time + 1.5f;
            }

            FrostBar.value = _currentFrostDamage;
        }

        if (_currentFrostDamage > 49 && _currentFrostDamage <= 99 && Health == 0)
        {
            FrostWarning.color = new Color(1, 1, 1, _transparency);
            float pingpong = Mathf.PingPong(Time.time, 0.5f);
            _transparency = Mathf.Lerp(0f, 0.65f, pingpong);
        }
        else if (_currentFrostDamage >= 0 && _currentFrostDamage <= 49 && Health == 0 && _iceState < 1)
        {
            FrostWarning.color = new Color(1, 1, 1, 0);
            FrostEffect.SetFloat("_Fade", _iceState += 0.1f * Time.deltaTime);
        }
        else if (Health > 0 && _iceState > 0)
        {
            FrostEffect.SetFloat("_Fade", _iceState -= 0.1f * Time.deltaTime);
        }

        if (Health > 100) Health = 100;
        if (_currentFrostDamage > 100) _currentFrostDamage = 100;
        if (_iceState > 1) _iceState = 1;
        if (_iceState < 0) _iceState = 0;
    }
}
