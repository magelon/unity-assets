using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNTScript : MonoBehaviour {

	
	public void Explode () {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        GetComponent<Rigidbody>().AddForce(Random.Range(-1, 2), Random.Range(0, 5), Random.Range(-1, 2), ForceMode.VelocityChange);
        StartCoroutine(SpawnExplosion());
    }
    IEnumerator SpawnExplosion()
    {
        yield return new WaitForSecondsRealtime(3);
        GetComponent<SphereCollider>().enabled = true;
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Block")
        {
            other.transform.GetComponent<BlockScript>().isExploded = true;
            Debug.Log("Boom");
            Destroy(other.gameObject);
            Destroy(gameObject);
        }    
    }
}
