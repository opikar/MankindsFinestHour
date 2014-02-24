using UnityEngine;
using System.Collections.Generic;

public class FindPath : MonoBehaviour
{
		List<GameObject> platforms = new List<GameObject> ();
		public Transform target;
		Movement movement;
		GameObject currentPlatform;

		// Use this for initialization
		void Start ()
		{
				movement = GetComponent<Movement> ();

				int groundLayer = LayerMask.NameToLayer ("Ground");
				GameObject[] objs = FindObjectsOfType<GameObject> ();

				foreach (GameObject obj in objs) {
						if (obj.layer == groundLayer) {
								platforms.Add (obj);
						}
				}
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (Vector2.Distance (target.position, transform.position) < 1f)
						return;

				float direction = Mathf.Sign (target.position.x - transform.position.x);
				
				GameObject closest = FindClosestReachable (direction);
		
				if (closest == null) {
						movement.Move (direction);
				} else {
						currentPlatform = closest;
						movement.Move (direction);
						if (rigidbody2D.velocity.y <= 0)
								movement.Jump ();
				}
					
		}

		GameObject FindClosestReachable (float direction)
		{
				Vector2 pos = transform.position;
				float closestDistance = float.MaxValue;
				GameObject closest = null;
				foreach (GameObject obj in platforms) {
						if (!JumpPossible (obj.transform.position) || (obj.transform.position.x - pos.x) * direction < 0 || obj == currentPlatform)
								continue;
						float distance = Vector2.Distance (pos, obj.transform.position);
						if (closestDistance > distance) {
								closest = obj;
								closestDistance = distance;
						}
				}
				return closest;
		}

		float MaxJumpHeight ()
		{
				float jumpTime = movement.f_jumpForce / Physics2D.gravity.y;

				return GetJumpHeight (jumpTime);
		}

		bool JumpPossible (Vector3 pos)
		{
				if (!movement.b_grounded)
						return false;
				Vector2 distance = transform.position - pos;
				float move = movement.f_speed;
				float moveTime = distance.x / move;

				float jumpHeight = GetJumpHeight (moveTime);

				return jumpHeight > distance.y;
		}

		float GetJumpHeight (float jumpTime)
		{
				float jump = movement.f_jumpForce;
				float gravity = Physics2D.gravity.y;

				float jumpHeight = jump * jumpTime - 0.5f * gravity * jumpTime * jumpTime;
				jumpHeight *= 1.9f; //double jump with margin of error for ai
		
				return jumpHeight;
		}
}
