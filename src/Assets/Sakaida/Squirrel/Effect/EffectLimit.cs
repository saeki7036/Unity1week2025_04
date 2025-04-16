using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectLimit : MonoBehaviour
{
    [SerializeField] ParticleSystem particleSystem;

    public float SpawnTime = 1;
    float spawnCount = 0;

    public float DieTime = 3;

    bool Die = false;
    private void Start()
    {
        float spawnCount = 0;
    }

    private void Update()
    {
        spawnCount += Time.deltaTime;
        if (spawnCount > SpawnTime && !Die) 
        {
            particleSystem.emissionRate = 0;
        Destroy(gameObject,DieTime);
            Die = true;
        }
    }
   
}
