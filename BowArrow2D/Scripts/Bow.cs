using UnityEngine;
using System.Collections;

public class Bow : MonoBehaviour
{
	public float maxPower;
	public float powerIncreaseRate;
	public GameObject arrow;
	//public Collider origin;
    public Collider2D origin;
	
	private bool pulling = false;
	private float power = 0;
	
	void OnGUI()
	{
		GUI.Box(new Rect(0, 0, 100, 50), this.power.ToString());	
	}
	
	void Update ()
	{

        Vector3 forward = transform.TransformDirection(Vector3.right)*3;
        Debug.DrawRay(transform.position, forward, Color.green);

        if (Input.GetMouseButtonDown(0))
		{
			this.startPull();
		}
		
		if(Input.GetMouseButtonUp(0))
		{
			this.stopPull();
		}
		
		if(pulling){
			power += powerIncreaseRate;
			
			if(power > maxPower) power = maxPower;
		}
	}
	
	private void startPull()
	{
		this.pulling = true;
		this.power = 0;
	}
	
	private void stopPull()
	{
		this.pulling = false;
		this.fire();
	}
	
	private void fire()
	{
		GameObject arrow = this.spawnArrow();


        arrow.GetComponent<Rigidbody2D>()
            .AddForce(Quaternion
            .Euler(new Vector3(transform.rotation.eulerAngles.x, 
            transform.rotation.eulerAngles.y,
            transform.rotation.eulerAngles.z))
            * new Vector3(25f * power, 0, 0), ForceMode2D.Force);


        //arrow.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.right * this.power);
		
	}
	
	private GameObject spawnArrow()
	{
		Vector3 start = transform.position;
		Quaternion rotation = transform.rotation;
		
		GameObject obj = (GameObject)GameObject.Instantiate(this.arrow, start, rotation);
		Arrow arrow = obj.GetComponent<Arrow>();
        if (arrow != null) {
            Physics2D.IgnoreCollision(this.origin, arrow.GetComponent<Collider2D>());
        }
       
		//Physics.IgnoreCollision(this.origin, arrow.childCollider);
		
		return obj;
	}
}
