using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtAt;
    public float startAt;

    public Transform atPos;
    public float atRange;
    public LayerMask enemiLayer;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBtAt <= 0)
        {
            if (Input.GetKey(KeyCode.F))
            {
                Collider2D[] enimes = Physics2D.
                OverlapCircleAll(atPos.position,
                     atRange,enemiLayer);
                for(int i = 0; i < enimes.Length; i++)
                {
                    enimes[i].GetComponent<Enime>()
                    .damage(damage);
                }
            }
            timeBtAt = startAt;
        }
        else
        {
            timeBtAt -= Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(atPos.position, atRange);
    }
}
