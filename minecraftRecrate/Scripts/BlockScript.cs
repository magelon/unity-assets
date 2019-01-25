using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour {
    public Transform topBlockFace;
    public Transform bottomBlockFace;
    public Transform leftBlockFace;
    public Transform rightBlockFace;
    public Transform frontBlockFace;
    public Transform backBlockFace;

    public LayerMask lMask;

    public Transform player;

    bool topBlockFaceIsCovered;
    bool bottomBlockFaceIsCovered;
    bool frontBlockFaceIsCovered;
    bool backBlockFaceIsCovered;
    bool rightBlockFaceIsCovered;
    bool leftBlockFaceIsCovered;

    public static bool debugMode = false;

    public bool isExploded = false;
    Transform xChunkToUpdate;
    Transform zChunkToUpdate;

    /*void Update()
    {
        //kolla om spelaren flyttar på sig

        //front
        
        if (!frontBlockFaceIsCovered)
        {
            if (player.position.z < (transform.position.z + 0.5f) && frontBlockFace.GetComponent<Renderer>().enabled == true)
            {
                frontBlockFace.GetComponent<Renderer>().enabled = false;
                PerlinNoiseGenerator.BlockFaces -= 1;
            }
            else if (player.position.z > (transform.position.z + 0.5f) && frontBlockFace.GetComponent<Renderer>().enabled == false)
            {
                frontBlockFace.GetComponent<Renderer>().enabled = true;
                PerlinNoiseGenerator.BlockFaces += 1;
            }
        }

        //back
        if (!backBlockFaceIsCovered)
        {
            if (player.position.z > (transform.position.z - 0.5f) && backBlockFace.GetComponent<Renderer>().enabled == true)
            {
                backBlockFace.GetComponent<Renderer>().enabled = false;
                PerlinNoiseGenerator.BlockFaces -= 1;
            }
            else if (player.position.z < (transform.position.z - 0.5f) && backBlockFace.GetComponent<Renderer>().enabled == false)
            {
                backBlockFace.GetComponent<Renderer>().enabled = true;
                PerlinNoiseGenerator.BlockFaces += 1;
            }
        }

        //right
        if (!rightBlockFaceIsCovered)
        {
            if (player.position.x > (transform.position.x - 0.5f) && rightBlockFace.GetComponent<Renderer>().enabled == true)
            {
                rightBlockFace.GetComponent<Renderer>().enabled = false;
                PerlinNoiseGenerator.BlockFaces -= 1;
            }
            else if (player.position.x < (transform.position.x - 0.5f) && rightBlockFace.GetComponent<Renderer>().enabled == false)
            {
                rightBlockFace.GetComponent<Renderer>().enabled = true;
                PerlinNoiseGenerator.BlockFaces += 1;
            }

        }

        //left
        if (!leftBlockFaceIsCovered)
        {
            if (player.position.x < (transform.position.x + 0.5f) && leftBlockFace.GetComponent<Renderer>().enabled == true)
            {
                leftBlockFace.GetComponent<Renderer>().enabled = false;
                PerlinNoiseGenerator.BlockFaces -= 1;
            }
            else if (player.position.x > (transform.position.x + 0.5f) && leftBlockFace.GetComponent<Renderer>().enabled == false)
            {
                leftBlockFace.GetComponent<Renderer>().enabled = true;
                PerlinNoiseGenerator.BlockFaces += 1;
            }
        }

        //top
        if (!topBlockFaceIsCovered)
        {
            if (player.position.y < (transform.position.y + 0.5f) && topBlockFace.GetComponent<Renderer>().enabled == true)
            {
                topBlockFace.GetComponent<Renderer>().enabled = false;
                PerlinNoiseGenerator.BlockFaces -= 1;
            }
            else if (player.position.y > (transform.position.y + 0.5f) && topBlockFace.GetComponent<Renderer>().enabled == false)
            {
                topBlockFace.GetComponent<Renderer>().enabled = true;
                PerlinNoiseGenerator.BlockFaces += 1;
            }
        }


        //bottom
        if (!bottomBlockFaceIsCovered) {
            if (player.position.y > (transform.position.y - 0.5f) && bottomBlockFace.GetComponent<Renderer>().enabled == true)
            {
                bottomBlockFace.GetComponent<Renderer>().enabled = false;
                PerlinNoiseGenerator.BlockFaces -= 1;
            }
            else if (player.position.y < (transform.position.y - 0.5f) && bottomBlockFace.GetComponent<Renderer>().enabled == false)
            {
                bottomBlockFace.GetComponent<Renderer>().enabled = true;
                PerlinNoiseGenerator.BlockFaces += 1;
            }
        }
        
    }*/

    void OnDestroy()
    {
        if (isExploded)
        {
            transform.parent.GetComponent<ChunkScript>().UpdateBlockFacesInChunk();
            if (transform.position.x - transform.parent.position.x == 9)
            {
                Debug.Log("1_right");
                RaycastHit chunkChecker;
                Physics.Raycast(new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z), Vector3.right, out chunkChecker, 0.3f);
                if (chunkChecker.collider != null)
                {
                    Debug.DrawRay(new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z), Vector3.right, Color.red, 1);
                    Debug.Log("2_right");
                    chunkChecker.transform.parent.GetComponent<ChunkScript>().UpdateBlockFacesInChunk();
                }
            }
            if (transform.position.x - transform.parent.position.x == 0)
            {
                Debug.Log("1_left");
                RaycastHit chunkChecker;
                Physics.Raycast(new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z), Vector3.left, out chunkChecker, 0.3f);
                if (chunkChecker.collider != null)
                {
                    Debug.DrawRay(new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z), Vector3.left, Color.blue, 1);
                    Debug.Log("2_left");
                    chunkChecker.transform.parent.GetComponent<ChunkScript>().UpdateBlockFacesInChunk();
                }
            }
            if (transform.position.z - transform.parent.position.z == 9)
            {
                Debug.Log("1_forward");
                RaycastHit chunkChecker;
                Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.5f), Vector3.forward, out chunkChecker, 0.3f);
                if (chunkChecker.collider != null)
                {
                    Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.5f), Vector3.forward, Color.green, 1);
                    Debug.Log("2_forward");
                    chunkChecker.transform.parent.GetComponent<ChunkScript>().UpdateBlockFacesInChunk();
                }
            }
            if (transform.position.z - transform.parent.position.z == 0)
            {
                Debug.Log("1_back");
                RaycastHit chunkChecker;
                Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.5f), Vector3.back, out chunkChecker, 0.3f);
                if (chunkChecker.collider != null && chunkChecker.transform != player)
                {
                    Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.5f), Vector3.back, Color.yellow, 1);
                    Debug.Log("2_back");
                    chunkChecker.transform.parent.GetComponent<ChunkScript>().UpdateBlockFacesInChunk();
                }
            }
        }
        if(transform == CameraScript.BlockToDestroy)
        {
            if (CameraScript.xChunkToUpdate != null)
            {
                CameraScript.xChunkToUpdate.GetComponent<ChunkScript>().UpdateBlockFacesInChunk();
                CameraScript.xChunkToUpdate = null;
            }
            if (CameraScript.zChunkToUpdate != null)
            {
                CameraScript.zChunkToUpdate.GetComponent<ChunkScript>().UpdateBlockFacesInChunk();
                CameraScript.zChunkToUpdate = null;
            }

            /*if (transform.name == "Wood(Clone)" || transform.name == "Leaves(Clone)")
            {
                transform.parent.parent.GetComponent<ChunkScript>().UpdateBlockFacesInChunk();
            }
            else
            {*/
                transform.parent.GetComponent<ChunkScript>().UpdateBlockFacesInChunk();
            //}
            
        }
    }
    
    

    void Start () {
        if (name == "Wood(Clone)" && transform.parent.name == "Tree"|| name == "Leaves(Clone)" && transform.parent.name == "Tree")
        {
            Debug.Log("Tree disassembled");
            transform.parent = transform.parent.parent;
        }
        PerlinNoiseGenerator.BlockFaces += 6;

        //player = GameObject.Find("Main Camera").transform;

        UpdateBlockFaces();

        topBlockFace = transform.GetChild(4);
        bottomBlockFace = transform.GetChild(5);
        leftBlockFace = transform.GetChild(3);
        rightBlockFace = transform.GetChild(2);
        frontBlockFace = transform.GetChild(1);
        backBlockFace = transform.GetChild(0);

        if (transform.position.y == 0)
        {
            bottomBlockFace.GetComponent<Renderer>().enabled = false;
        }
    }
	

	// Update is called once per frame
	public void UpdateBlockFaces() {
        //up
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Vector3.up, 0.3f, lMask))
        {
            topBlockFace.GetComponent<Renderer>().enabled = false;
            topBlockFace.GetComponent<Collider>().enabled = false;
            PerlinNoiseGenerator.BlockFaces -= 1;
            topBlockFaceIsCovered = true;
        }
        else
        {
            topBlockFace.GetComponent<Renderer>().enabled = true;
            topBlockFace.GetComponent<Collider>().enabled = true;
            PerlinNoiseGenerator.BlockFaces += 1;
        }
        //down
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z), Vector3.down, 0.3f, lMask))
        {
            bottomBlockFace.GetComponent<Renderer>().enabled = false;
            bottomBlockFace.GetComponent<Collider>().enabled = false;
            PerlinNoiseGenerator.BlockFaces -= 1;
            bottomBlockFaceIsCovered = true;
        }
        else if(transform.position.y != 0)
        {
            bottomBlockFace.GetComponent<Renderer>().enabled = true;
            bottomBlockFace.GetComponent<Collider>().enabled = true;
            PerlinNoiseGenerator.BlockFaces += 1;
        }
        //right
        if (Physics.Raycast(new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z), Vector3.left, 0.3f, lMask))
        {
            rightBlockFace.GetComponent<Renderer>().enabled = false;
            rightBlockFace.GetComponent<Collider>().enabled = false;
            PerlinNoiseGenerator.BlockFaces -= 1;
            rightBlockFaceIsCovered = true;
        }
        else
        {
            rightBlockFace.GetComponent<Renderer>().enabled = true;
            rightBlockFace.GetComponent<Collider>().enabled = true;
            PerlinNoiseGenerator.BlockFaces += 1;
        }
        //left
        if (Physics.Raycast(new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z), Vector3.right, 0.3f, lMask))
        {
            leftBlockFace.GetComponent<Renderer>().enabled = false;
            leftBlockFace.GetComponent<Collider>().enabled = false;
            PerlinNoiseGenerator.BlockFaces -= 1;
            leftBlockFaceIsCovered = true;
        }
        else
        {
            leftBlockFace.GetComponent<Renderer>().enabled = true;
            leftBlockFace.GetComponent<Collider>().enabled = true;
            PerlinNoiseGenerator.BlockFaces += 1;
        }
        //front
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.5f), Vector3.forward, 0.3f, lMask))
        {
            frontBlockFace.GetComponent<Renderer>().enabled = false;
            frontBlockFace.GetComponent<Collider>().enabled = false;
            PerlinNoiseGenerator.BlockFaces -= 1;
            frontBlockFaceIsCovered = true;
        }
        else
        {
            frontBlockFace.GetComponent<Renderer>().enabled = true;
            frontBlockFace.GetComponent<Collider>().enabled = true;
            PerlinNoiseGenerator.BlockFaces += 1;
        }
        //back
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.5f), Vector3.back, 0.3f, lMask))
        {
            backBlockFace.GetComponent<Renderer>().enabled = false;
            backBlockFace.GetComponent<Collider>().enabled = false;
            PerlinNoiseGenerator.BlockFaces -= 1;
            backBlockFaceIsCovered = true;
        }
        else
        {
            backBlockFace.GetComponent<Renderer>().enabled = true;
            backBlockFace.GetComponent<Collider>().enabled = true;
            PerlinNoiseGenerator.BlockFaces += 1;
        }
    }
}
