using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Particle
{
    public string Name;
    [Space]
    public GameObject ParticlePrefab;
}

public class ParticleManager : MonoBehaviour
{
    [SerializeField] private float _particleLifetime = 10f;
    [SerializeField] private List<Particle> _particles = new List<Particle>();

    public void SpawnParticles(string name, bool hasLifetime, Transform parent, bool isParented)
    {
        Particle p = GetParticle(name);

        if (p == null)
        {
            Debug.LogWarning($"Particle named {name} does not exist.");
            return;
        }

        GameObject particleSystem = Instantiate(p.ParticlePrefab, parent);

        if (!isParented)
        {
            particleSystem.transform.SetParent(null);
        }
        
        if (hasLifetime)
        {
            Destroy(particleSystem, _particleLifetime);
        }
    }

    private Particle GetParticle(string name)
    {
        foreach (Particle p in _particles)
        {
            if (p.Name == name)
            {
                return p;
            }
        }

        return null;
    }
}