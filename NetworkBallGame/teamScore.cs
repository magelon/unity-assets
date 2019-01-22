using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;

public class teamScore : NetworkBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public Text t;

    [SyncVar (hook="OnChange")]
    public int score = 0;


    public void goal()
    {
        if (!isServer) return;
        score++;
        CmdNewBall();
    }

    void OnChange(int s)
    {
        t.text = s.ToString();
    }

    [Command]
    void CmdNewBall()
    {
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        NetworkServer.Spawn(bullet);
    }

}