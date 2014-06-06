using UnityEngine;
using System.Collections;

public class Fireworks : MonoBehaviour {

    float time;

	// Use this for initialization
	void Start () {
        Invoke("Fire", 0);
	}

    private void Fire()
    {
        particleSystem.Play();
        Invoke("Disable", 0.6f);
    }

    private void Disable()
    {
        particleSystem.Stop();
        time = Random.Range(0.7f, 1.4f);
        Invoke("Fire", time);
    }
}
