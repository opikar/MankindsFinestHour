using UnityEngine;
using System.Collections;

public class FireworksController : MonoBehaviour {

    public GameObject fireworks;
    public int numberOfRockets = 20;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public IEnumerator Activate()
    {
        for (int i = 0; i < numberOfRockets; i++)
        {
            Vector3 position = Random.onUnitSphere * 50;
            position.z = 0;
            GameObject go = (GameObject)Instantiate(fireworks, transform.position + position, transform.rotation);
            Destroy(go, 5f);
            yield return new WaitForSeconds(Random.Range(0,2f));
        }
        


    }
}
