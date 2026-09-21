
using System.Collections;
using UnityEngine;

public class LightningController : MonoBehaviour
{
    [Header("References")]
    public Animator animator;
    public ParticleSystem lightningParticle;
    public Light lightningLight;

    [Header("Lightning")]
    public float flashIntensity = 100f;
    public float flashDuration = 0.1f;

    private bool canStrike = true;

    void Start()
    {
        lightningLight.enabled = false;
        lightningLight.intensity = 0f;

        lightningParticle.Stop(true,ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canStrike)
        {
            canStrike = false;
            animator.SetTrigger("pStrike");
        }
    }

   
    public void PlayLightning()
    {
        lightningParticle.Stop(true,ParticleSystemStopBehavior.StopEmittingAndClear);

        lightningParticle.Play();

        StartCoroutine(FlashLight());
    }

    public void EnableNextStrike()
    {
        canStrike = true;
    }

    IEnumerator FlashLight()
    {
        lightningLight.enabled = true;
        lightningLight.intensity = flashIntensity;

        yield return new WaitForSeconds(flashDuration);

        lightningLight.intensity = 0f;
        lightningLight.enabled = false;
    }
}

