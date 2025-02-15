using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;
using UnityEngine.VFX;

public class WeatherSystem : MonoBehaviour
{
    [Header("Global")]
    public Material globalMaterial;
    public Light sunLight;
    public Material skyboxMaterial;
    public TMP_Text weatherText;

    [Header("Winter Assets")]
    public ParticleSystem winterParticleSystem;
    public Volume winterVolume;

    [Header("Rain Assets")]
    public ParticleSystem rainParticleSystem;
    public Volume rainVolume;

    [Header("Autumn Assets")]
    public ParticleSystem autumnParticleSystem;
    public Volume autumnVolume;

    [Header("Summer Assets")]
    public ParticleSystem summerParticleSystem;
    public Volume summerVolume;

    ChangeEnvironment changeEnvironment;

    [SerializeField] GameObject snowFx;
    [SerializeField] GameObject rainFx;
    private void Start()
    {
        changeEnvironment = FindFirstObjectByType<ChangeEnvironment>();
        Summer();
    }

    public void Winter()
    {
       changeEnvironment.ChangeToSnow();
        rainFx.SetActive(false);
        snowFx.SetActive(true);
    }

    public void Rain()
    {
        changeEnvironment.ChangeToRain();
        rainFx.SetActive(true);
        snowFx.SetActive(false);
    }

    public void Autumn()
    {
        changeEnvironment.ChangeToAutum();
        rainFx.SetActive(false);
        snowFx.SetActive(false);
    }

    public void Summer()
    {
        changeEnvironment.ChangeToSummer();
        rainFx.SetActive(false);
        snowFx.SetActive(false);
    }

}
