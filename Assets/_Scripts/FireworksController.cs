using UnityEngine;
using System.Collections;

public class FireworksController : MonoBehaviour {

    public GameObject fireworks;
    public int numberOfRockets = 20;
    private bool active;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	}

    public IEnumerator Activate()
    {
        if (active) yield break;// return;
        active = true;
        for (int i = 0; i < numberOfRockets; i++)
        {
            yield return new WaitForSeconds(Random.Range(0, 0.8f));
            Vector3 position = (Vector3)Random.insideUnitCircle * 28;
            
            GameObject go = (GameObject)Instantiate(fireworks, transform.position + position, transform.rotation);
            //Destroy(go, 5f);
            
        }
        


    }
}
