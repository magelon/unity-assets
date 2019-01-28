using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlocks : MonoBehaviour
{
    //transform of last block
    public Transform t;

    public MovingBackGround mb;
    //block prefab for spawn
    public GameObject block;

    //spring join 2d form current block
    SpringJoint2D spring;
    Rigidbody2D rb;

    public AudioSource source;
    
   

    float dirx;

    private static LinkedList<GameObject> list = new LinkedList<GameObject>();

    public float speed = .2f;
    //time between drop box time
    float DBetweenTime = 1f;
    //Drop box time
    float DropBoxTime;

    //control bottom box moving between time
    float BBetweenTime = 1f;
    //bottom box moving time
    float BottomBoxTime;

    public static int life=3;
    public static int score = 0;
    public static bool land = true;

    // Start is called before the first frame update


    void Start()
    {
        BottomBoxTime=0;
        DropBoxTime = 0;
        dirx = 0.1f;
        InstantiateNewBlock(dirx);
        source = gameObject.GetComponent<AudioSource>();


    }
    // Update is called once per frame
    void Update()
    {
        GameObject block = list.Last.Value;
        if (Input.GetMouseButtonDown(0) && Time.time > DropBoxTime + DBetweenTime && land==true)
        {
            
            if (block.transform.position.x < 0)
            {
                dirx = -0.1f;
            }
            else
            {
                dirx = 0.1f;
            }
            StartCoroutine("InstantiateBox");
            block.GetComponent<SpringJoint2D>().enabled = false;
            block.GetComponent<Collider2D>().enabled = true;
            land = false;
           
            if (list.Count > 3)
            {
                Destroy(list.First.Value);
                list.RemoveFirst();
                moveBottomBlock();
                mb.moveBackGround();
            }
        }
        if (Time.time < BottomBoxTime + BBetweenTime && BottomBoxTime != 0)
        {
            GameObject bottomBlock = list.First.Value;
            Rigidbody2D rbBB = bottomBlock.GetComponent<Rigidbody2D>();
            //rbBB.isKinematic = true;
            //Vector2 velocity = new Vector2(0, -1f);
            //rbBB.MovePosition(rbBB.position + velocity*speed * Time.deltaTime);
        }
    }

    private void dropBox() {
        DropBoxTime = Time.time;
    }


        private void moveBottomBlock()
        {
            BottomBoxTime = Time.time;
        }
        //prevent collision with droping box
        private IEnumerator InstantiateBox()
    {
    //not using wait time will fix the bug in game
    //yield return null;
        yield return new WaitForSeconds(1);
        InstantiateNewBlock(dirx);   
    }
    public void PlaySound() {
        source.Play();
        land = true;
        score++;
    }

    public void BlockDied() {
        life--;
        source.Play();
        land = true;
    }

    void InstantiateNewBlock(float dirForX) {
        GameObject blockNew = Instantiate(block, new Vector2(t.transform.position.x - dirx, t.transform.position.y-0.5f), Quaternion.identity);
        rb = blockNew.GetComponent<Rigidbody2D>();
        rb.isKinematic = true;

        spring = blockNew.AddComponent<SpringJoint2D>();
        spring.connectedAnchor = new Vector2(t.transform.position.x, t.transform.position.y );

        //t.transform.Translate(Vector2.up*0.5f);
        spring.autoConfigureDistance = false;
        spring.distance = 1;
        spring.frequency = 10;
        spring.autoConfigureConnectedAnchor = false;
        rb.isKinematic = false;
        blockNew.GetComponent<Collider2D>().enabled = false;
        
        list.AddLast(blockNew);
        dropBox();
    }

   
}
