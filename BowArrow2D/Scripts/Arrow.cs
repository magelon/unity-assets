using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour
{
	public float flyTime;
	//public Collider childCollider;
	
	private bool flying = true;
	private float stopTime;
	private Transform anchor;
	
	void Start()
	{
		this.stopTime = Time.time + this.flyTime;
	}
	
	void Update ()
	{

        //this part of update is only executed, if a rigidbody is present
        // the rigidbody is added when the arrow is shot (released from the bowstring)
        if (transform.GetComponent<Rigidbody2D>() != null)
        {
            // do we fly actually?
            if (GetComponent<Rigidbody2D>().velocity != Vector2.zero)
            {
                // get the actual velocity
                Vector3 vel = GetComponent<Rigidbody2D>().velocity;
                // calc the rotation from x and y velocity via a simple atan2 Radians-to-degrees conversion constant 
                float angleZ = Mathf.Atan2(vel.y, vel.x) * Mathf.Rad2Deg;
                float angleY = Mathf.Atan2(vel.z, vel.x) * Mathf.Rad2Deg;
                // rotate the arrow according to the trajectory
                transform.eulerAngles = new Vector3(0, -angleY, angleZ);
            }
        }

        if (this.stopTime <= Time.time && this.flying)
		{
			GameObject.Destroy(gameObject);	
		}
		
		if(this.flying)
		{
			//this.rotate();	
		} 
		else if(this.anchor != null)
		{
			this.transform.position = anchor.transform.position;
			this.transform.rotation = anchor.transform.rotation;
		}
		
	}
	
	void OnCollisionEnter2D(Collision2D collision)
	{	
		if(this.flying)
		{
			this.flying = false;
			this.transform.position = collision.contacts[0].point;
			//this.childCollider.isTrigger = true;
				
			GameObject anchor = new GameObject("ARROW_ANCHOR");
			anchor.transform.position = this.transform.position;
			anchor.transform.rotation = this.transform.rotation;
			anchor.transform.parent = collision.transform;
			this.anchor = anchor.transform;
			
			Destroy(GetComponent<Rigidbody2D>());
			//collision.gameObject.SendMessage("arrowHit", SendMessageOptions.DontRequireReceiver);
		}
	}
}
