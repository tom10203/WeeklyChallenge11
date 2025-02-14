using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class ChangeEnvironment : MonoBehaviour
{
    public Material skybox;
    public Light directionalLight;
    public Material snow;

    [SerializeField] private float exposureSummer;
    [SerializeField] private float lightIntensitySummer;
    [SerializeField] float summersnowfade;

    [SerializeField] private float exposureRain;
    [SerializeField] private float lightIntensityRain;
    [SerializeField] float rainSnowFade;
    [SerializeField] float rainSmoothness;
    [SerializeField] float rainMetalic;

    [SerializeField] private float exposureAutum;
    [SerializeField] private float lightIntensityAutum;
    [SerializeField] float autumSnowFade;
    [SerializeField] Color autumTintSkybox;
    [SerializeField] Color autumTintSnow;

    [SerializeField] private float exposureSnow;
    [SerializeField] private float lightIntensitySnow;
    [SerializeField] float winterSnowFade;

    // Default values for metallic and smoothness
    [SerializeField] private float defaultMetallic = 0.5f;
    [SerializeField] private float defaultSmoothness = 0f;
    [SerializeField] Color defaultTint;
    [SerializeField] Color defaultTintSnow = Color.white;

    private Coroutine transitionCoroutine;

    private void Start()
    {

        defaultTint = skybox.GetColor("_SkyTint");
    }

    public void ChangeToSnow()
    {
        if (transitionCoroutine != null)
            StopCoroutine(transitionCoroutine);
        transitionCoroutine = StartCoroutine(ChangeEnvironmentRoutine(exposureSnow, lightIntensitySnow, winterSnowFade, false, false));
    }

    public void ChangeToRain()
    {
        if (transitionCoroutine != null)
            StopCoroutine(transitionCoroutine);
        transitionCoroutine = StartCoroutine(ChangeEnvironmentRoutine(exposureRain, lightIntensityRain, rainSnowFade, true, false));
    }

    public void ChangeToSummer()
    {
        if (transitionCoroutine != null)
            StopCoroutine(transitionCoroutine);
        transitionCoroutine = StartCoroutine(ChangeEnvironmentRoutine(exposureSummer, lightIntensitySummer, summersnowfade, false, false));
    }

    public void ChangeToAutum()
    {
        if (transitionCoroutine != null)
            StopCoroutine(transitionCoroutine);
        transitionCoroutine = StartCoroutine(ChangeEnvironmentRoutine(exposureAutum, lightIntensityAutum, autumSnowFade, false, true));
    }

    private IEnumerator ChangeEnvironmentRoutine(float targetExposure, float targetIntensity, float targetSnowFade, bool isRain, bool isAutum)
    {
        float duration = 2.0f; // Adjust transition speed
        float elapsedTime = 0f;

        float startExposure = skybox.GetFloat("_Exposure");
        float startIntensity = directionalLight.intensity;
        float startSnowFade = snow.GetFloat("_SnowFade");
        float startMetallic = snow.GetFloat("_Metallic");
        float startSmoothness = snow.GetFloat("_Smoothness");

        Color skyboxTint = skybox.GetColor("_SkyTint");
        Color snowTint = snow.GetColor("_SnowColor");

        // Set target values for metallic and smoothness based on weather
        float targetMetallic = isRain ? rainMetalic : defaultMetallic;
        float targetSmoothness = isRain ? rainSmoothness : defaultSmoothness;

        Color targetColorSkybox = isAutum ? autumTintSkybox : defaultTint;
        Color targetColorSnow = isAutum ? autumTintSnow : defaultTintSnow;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            float newExposure = Mathf.Lerp(startExposure, targetExposure, t);
            float newIntensity = Mathf.Lerp(startIntensity, targetIntensity, t);
            float newFade = Mathf.Lerp(startSnowFade, targetSnowFade, t);
            float newMetallic = Mathf.Lerp(startMetallic, targetMetallic, t);
            float newSmoothness = Mathf.Lerp(startSmoothness, targetSmoothness, t);
            Color newSkyboxColor = Color.Lerp(skyboxTint, targetColorSkybox, t);
            Color newSnowColor = Color.Lerp(snowTint, targetColorSnow, t);

            skybox.SetFloat("_Exposure", newExposure);
            skybox.SetColor("_SkyTint", newSkyboxColor);
            directionalLight.intensity = newIntensity;
            snow.SetFloat("SnowFade", newFade);
            snow.SetFloat("_Metallic", newMetallic);
            snow.SetFloat("_Smoothness", newSmoothness);
            snow.SetColor("_SnowColor", newSnowColor);  

            yield return null;
        }

        // Ensure values are exactly set at the end
        skybox.SetFloat("_Exposure", targetExposure);
        directionalLight.intensity = targetIntensity;
        snow.SetFloat("SnowFade", targetSnowFade);
        snow.SetFloat("_Metallic", targetMetallic);
        snow.SetFloat("_Smoothness", targetSmoothness);
    }
}