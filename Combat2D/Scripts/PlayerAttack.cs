using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBetweenAttack=.5f;
    private float nextAttackTime;

    PlayerStates ps;

    public Transform atPos;
    public float atRange;
    public LayerMask enemiLayer;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        nextAttackTime = Time.time + timeBetweenAttack;
        ps = GetComponent<PlayerStates>();
    }


    void Update()
    {
        if (Time.time > nextAttackTime && Input.GetMouseButtonDown(0))
        {
                ps.attack = true;
                Collider2D[] enimes = Physics2D.
                OverlapCircleAll(atPos.position,
                     atRange, enemiLayer);
                for (int i = 0; i < enimes.Length; i++)
                {
                    enimes[i].GetComponent<Enime>()
                    .damage(damage);
                }
                nextAttackTime = Time.time + timeBetweenAttack;
        }
        else
        {
            ps.attack = false;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(atPos.position, atRange);
    }
}
