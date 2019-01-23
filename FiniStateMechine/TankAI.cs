using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//drag player to gameobject
public class TankAI : MonoBehaviour
{
    Animator anim;
    public GameObject bullet;
    public Transform turret;
    public GameObject player;
    public GameObject GetPlayer() {
        return player;
    }
   void Fire() {
        GameObject b = Instantiate(bullet, turret.transform.position, turret.transform.rotation);
        b.GetComponent<Rigidbody>().AddForce(turret.transform.forward * 500);
   }

    public void StopFiring() {
        CancelInvoke("Fire");
    }

    public void StartFiring() {
        InvokeRepeating("Fire", 0.5f, 0.5f);
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        anim.SetFloat("distance", Vector3.Distance(transform.position, player.transform.position));
    }
}
