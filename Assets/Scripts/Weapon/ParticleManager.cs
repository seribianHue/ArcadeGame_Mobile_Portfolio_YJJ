using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    ParticleSystem _particle;

    // Start is called before the first frame update
    void Start()
    {
        _particle = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_particle.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
