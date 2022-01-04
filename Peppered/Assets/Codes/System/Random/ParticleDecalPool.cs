using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDecalPool : MonoBehaviour
{
    public int maxDecals = 100;
    private int particleDecalIndex;

    private ParticleSystem decalParticleSystem;
    private ParticleDecalData[] particleData;
    private ParticleSystem.Particle[] particles;

    // Start is called before the first frame update
    void Start()
    {
        decalParticleSystem = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[maxDecals];
        particleData = new ParticleDecalData[maxDecals];

        for (int i = 0; i < maxDecals; i++)
        {
            particleData[i] = new ParticleDecalData();
        }
    }

    public void ParticleHit(ParticleCollisionEvent particleCollisionEvent)
    {
        SetParticleData(particleCollisionEvent);
        DisplayParticles();
    }

    void SetParticleData(ParticleCollisionEvent particleCollisionEvent)
    {

        if (particleDecalIndex >= maxDecals)
        {
            particleDecalIndex = 0;
        }


        particleData[particleDecalIndex].Position = particleCollisionEvent.intersection;
        //Vector3 particleRotationEuler = Quaternion.LookRotation(particleCollisionEvent.normal).eulerAngles;
        //particleRotationEuler.z = Random.Range(0, 360);
        //particleData[particleDecalIndex].Rotation = particleRotationEuler;

        particleDecalIndex++;
    }

    void DisplayParticles()
    {
        for (int i = 0; i < particleData.Length; i++)
        {
            particles[i].position = particleData[i].Position;
            particles[i].rotation3D = particleData[i].Rotation;
        }

        decalParticleSystem.SetParticles(particles, particles.Length);
    }
}
