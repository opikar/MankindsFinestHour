using UnityEngine;
using System.Collections;

public class TransportPlatform : MovingPlatform {

    GameManager manager;
    GameObject c;
    public FireworksController fireworkdController;

    protected override void Start()
    {
        base.Start();        
        c = GameObject.Find("Main Camera");
    }

    protected override void NextIndex()
    {
        if (++i_index >= waypoint.Count) {
			//Dont destroy on load doesen't work if object isn't on top level
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			player.transform.parent = null; 
			player.GetComponent<PlayerManager>().SaveState();

			GameObject.Find("LevelManager").GetComponent<LevelManager>().CompleteLevel();
		}
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (other.tag != "Player") return;
        manager = GameObject.Find("Manager").GetComponent<GameManager>();
        manager.SetState(State.Win);
        other.rigidbody2D.velocity = Vector2.zero;
        if (fireworkdController != null)
            StartCoroutine(fireworkdController.Activate());
        BoxCollider2D[] box = c.GetComponentsInChildren<BoxCollider2D>();
        foreach (BoxCollider2D b in box)
            b.enabled = false;
    }
}
