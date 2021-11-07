using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    [Header("Set Components")]
    public Slider FuelBar;
    public Slider FrostBar;
    public Material FrostEffect;
    public Material LanternPower;
    public Image FrostWarning;
    public Image DeathBG;
    public Image DeathText;
    public GameObject DeathScreenButtons;
    public PlayerController PlayerControllerScript;
    public PlayerState PlayerStateScript;
    public TextMeshProUGUI PlayerLayerGUI;

    [HideInInspector]
    public float _maxFrostDamage;
    [HideInInspector]
    public float _maxHealth;
    [HideInInspector]
    public bool _isTakingFrostDamage = false;

    private float _frostTransparency;
    private float _deathBGTransparency = 0;
    private float _deathTextTransparency = 0;
    private float _fadeTime = 0;

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

        if (Time.time > _fadeTime && PlayerStateScript._currentFrostDamage == 0 && _deathTextTransparency < 1f)
        {
           DeathBG.color = new Color(0, 0, 0, _deathBGTransparency = Mathf.Clamp(_deathBGTransparency += 0.01f, 0, 0.6f));
           DeathText.color = new Color(1, 1, 1, _deathTextTransparency = Mathf.Clamp(_deathTextTransparency += 0.01f, 0, 1f));
           _fadeTime = Time.time + 0.1f;
        }

        if (_deathTextTransparency >= 1)
        {
            DeathScreenButtons.SetActive(true);
        }
        
        PlayerLayerGUI.text = PlayerControllerScript._layerOrder.ToString();
    }
    
    IEnumerator DoFrostWarning()
    {
        while (_isTakingFrostDamage)
        {
            FrostWarning.color = new Color(1, 1, 1, _frostTransparency);
            float pingpong = Mathf.PingPong(Time.time, 0.5f);
            _frostTransparency = Mathf.Lerp(0f, 0.65f, pingpong);
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
            PlayerControllerScript.LightRadius = Mathf.Clamp(PlayerControllerScript.LightRadius + 0.00001f, 0f, 0.5f);
            yield return 0;
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("Demo");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
