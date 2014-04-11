using UnityEngine;
using System.Collections;

public class TransportPlatform : MovingPlatform {

    protected override void NextIndex()
    {
        if (++i_index >= waypoint.Count) GameObject.Find("LevelManager").GetComponent<LevelManager>().CompleteLevel();   
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }
}
