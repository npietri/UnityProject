using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEffects : MonoBehaviour {

    // Singleton
    public static SpecialEffects Instance;

    public ParticleSystem smokeEffect;
    public ParticleSystem fireEffect;

    void Awake()
    {
        // Register the singleton
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of SpecialEffects!");
        }

        Instance = this;
    }

    // Create an explosion at the given location
    public void Explosion(Vector3 position)
    {
        // Smoke 
        Instantiate(smokeEffect, position);

        // Fire 
        Instantiate(fireEffect, position);
    }

    // Instantiate a Particle system from prefab
    private ParticleSystem Instantiate(ParticleSystem prefab, Vector3 position)
    {
        ParticleSystem newParticleSystem = Instantiate(
          prefab,
          position,
          Quaternion.identity
        ) as ParticleSystem;

        // Make sure it will be destroyed
        Destroy(
          newParticleSystem.gameObject,
          newParticleSystem.startLifetime
        );

        return newParticleSystem;
    }
}
