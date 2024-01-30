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

    public GameObject SpawnParticles(string name, bool hasLifetime, Transform objTransform, bool isParented)
    {
        Particle p = GetParticle(name);

        if (p == null)
        {
            Debug.LogWarning($"Particle named {name} does not exist.");
            return null;
        }

        GameObject particleSystem = Instantiate(p.ParticlePrefab, objTransform.position, Quaternion.identity);

        if (!isParented)
        {
            particleSystem.transform.SetParent(null);
        }
        else
        {
            particleSystem.transform.SetParent(objTransform, true);
            particleSystem.transform.localScale = new Vector3(1, 1, 1);
        }
        
        if (hasLifetime)
        {
            Destroy(particleSystem, _particleLifetime);
        }

        return particleSystem;
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