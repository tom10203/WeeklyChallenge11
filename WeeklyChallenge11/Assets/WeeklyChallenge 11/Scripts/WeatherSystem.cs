using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;

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

    private void Start()
    {
        changeEnvironment = FindFirstObjectByType<ChangeEnvironment>();
        Summer();
    }

    public void Winter()
    {
       changeEnvironment.ChangeToSnow();
    }

    public void Rain()
    {
        changeEnvironment.ChangeToRain();
    }

    public void Autumn()
    {
        changeEnvironment.ChangeToAutum();
    }

    public void Summer()
    {
        changeEnvironment.ChangeToSummer();
    }

}
