using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePooler : MonoBehaviour
{
    private float particlesDuration;
    // Start is called before the first frame update
    void Awake()
    {
        var particles = GetComponentInChildren<ParticleSystem>();
        particlesDuration = particles.duration;
    }

    // Update is called once per frame
    void OnEnable()
    {
        StartCoroutine(AutoDisable());
    }

    private IEnumerator AutoDisable()
    {
        yield return new WaitForSeconds(particlesDuration);
        gameObject.SetActive(false);
    }
}
