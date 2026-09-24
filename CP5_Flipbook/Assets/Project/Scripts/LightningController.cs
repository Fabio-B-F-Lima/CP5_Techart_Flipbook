using System.Collections;
using UnityEngine;

public class LightningController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Animator animator;
    [SerializeField] ParticleSystem lightningParticle;
    [SerializeField] ParticleSystem shockwaveParticle;
    [SerializeField] Light lightningLight;
    [SerializeField] Renderer swordRenderer;

    [Header("Sword Materials")]
    [SerializeField] Material normalMaterial0;
    [SerializeField] Material normalMaterial1;
    [SerializeField] Material normalMaterial3;
    [SerializeField] Material normalMaterial5;

    [SerializeField] Material electricMaterial;

    [Header("Lightning")]
    [SerializeField] float flashIntensity = 300;
    [SerializeField] float flashDuration = 0.5f;

    private bool canStrike = true;

    void Start()
    {
        lightningLight.enabled = false;
        lightningLight.intensity = 0f;

        lightningParticle.Stop(true,ParticleSystemStopBehavior.StopEmittingAndClear);

        shockwaveParticle.Stop(true,ParticleSystemStopBehavior.StopEmittingAndClear);

        SetNormalMaterials();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canStrike)
        {
            canStrike = false;
            animator.SetTrigger("pStrike");
        }
    }

    // Raio atinge a espada
    public void PlayLightning()
    {
        lightningParticle.Stop(true,ParticleSystemStopBehavior.StopEmittingAndClear
        );

        lightningParticle.Play();

        SetElectricMaterials();

        StartCoroutine(FlashLight());
        print("Tocou animação");
    }

    // Espada bate no chão
    public void PlayShockwave()
    {
        shockwaveParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        shockwaveParticle.Play();
    }

    //  Coloca o material elétrico
    public void SetElectricMaterials()
    {
        Material[] materials = swordRenderer.materials;

        materials[0] = electricMaterial;
        materials[1] = electricMaterial;
        materials[3] = electricMaterial;
        materials[5] = electricMaterial;

        swordRenderer.materials = materials;
    }

    // Volta para os materiais normais
    public void SetNormalMaterials()
    {
        Material[] materials = swordRenderer.materials;

        materials[0] = normalMaterial0;
        materials[1] = normalMaterial1;
        materials[3] = normalMaterial3;
        materials[5] = normalMaterial5;

        swordRenderer.materials = materials;
    }

    // Final da animação
    public void EnableNextStrike()
    {
        SetNormalMaterials();

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