using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSound : MonoBehaviour
{
    
     SpawnBlocks sb;

    // Start is called before the first frame update
    void Start()
    {
        sb = GameObject.FindGameObjectWithTag("Player").GetComponent<SpawnBlocks>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision) {
            sb.PlaySound();
            Destroy(gameObject);
        }
        
    }

    private void Update()
    {
        if (transform.position.y < -3)
        {
            sb.BlockDied();
            Destroy(gameObject);
        }
    }
   
}
