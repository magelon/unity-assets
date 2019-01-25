using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    Vector2 mouseLook;
    Vector2 smoothV;
    Vector3 interactionPoint;
    public float sensitivity = 5f;
    public float smoothing = 2f;

    public LayerMask LmaskBlockPlacing;
    public LayerMask LmaskBlockDestruction;

    GameObject player;
    public static Transform BlockToDestroy;
    public static Transform xChunkToUpdate;
    public static Transform zChunkToUpdate;

    //blockplacing
    public Transform PlacingGraphics;
    Transform BlockToPlace;
    //blocks
    public Transform Stone_block;
    public Transform Plank_block;
    public Transform Glass_block;
    public Transform Wood_block;
    public Transform Cobble_block;
    public Transform Brick_block;
    public Transform TNT_block;

    public static int block_ = 1;

    public AudioClip grass_audio;
    public AudioClip stone_audio;
    public AudioClip dirt_audio;
    public AudioClip wood_audio;

    AudioSource AS;

    bool stepAudioIsPlaying = false;

    void Start () {
        AS = transform.GetComponent<AudioSource>();
        player = this.transform.parent.gameObject;
        Cursor.lockState = CursorLockMode.Locked;
    }
	
	void Update () {
        if (PlayerScript.grounded && Input.GetAxis("Vertical") != 0 || PlayerScript.grounded && Input.GetAxis("Horizontal") != 0)
        {
            if (!stepAudioIsPlaying)
            {
                if (PlayerScript.blockBeneath.transform.name == "Dirt_block")
                {
                    StartCoroutine(PlayStep(dirt_audio));
                }
                else if (PlayerScript.blockBeneath.transform.name == "Stone_block" ||
                    PlayerScript.blockBeneath.transform.name == "Glass(Clone)" || PlayerScript.blockBeneath.transform.name == "Cobble(Clone)" ||
                    PlayerScript.blockBeneath.transform.name == "Stone(Clone)" || PlayerScript.blockBeneath.transform.name == "Brick(Clone)")
                {
                    StartCoroutine(PlayStep(stone_audio));
                }
                else if (PlayerScript.blockBeneath.transform.name == "Wood(Clone)" || PlayerScript.blockBeneath.transform.name == "Planks(Clone)")
                {
                    StartCoroutine(PlayStep(wood_audio));
                }
                else if (PlayerScript.blockBeneath.transform.name == "Grass_block" || PlayerScript.blockBeneath.transform.name == "Leaves(Clone)")
                {
                    StartCoroutine(PlayStep(grass_audio));
                }
            }
        }
        if (Input.GetButtonUp("Q") && Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (Input.GetButtonDown("Q") && Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (block_ > 7)
        {
            block_ = 1;
        }
        if (block_ < 1)
        {
            block_ = 7;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            block_++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            block_--;
        }


        #region // blocklist
        if (block_ == 1)
        {
            BlockToPlace = Stone_block;
        }
        else if (block_ == 2)
        {
            BlockToPlace = Plank_block;
        }
        else if (block_ == 3)
        {
            BlockToPlace = Glass_block;
        }
        else if (block_ == 4)
        {
            BlockToPlace = Wood_block;
        }
        else if (block_ == 5)
        {
            BlockToPlace = Cobble_block;
        }
        else if (block_ == 6)
        {
            BlockToPlace = TNT_block;
        }
        else if (block_ == 7)
        {
            BlockToPlace = Brick_block;
        }
        #endregion


        PlaceBlockGraphics();
        if (Input.GetMouseButtonDown(1))
        {
            PlaceBlock(BlockToPlace);
        }

        if (Input.GetMouseButtonDown(0))
        {
            DestroyBlock();
        }

        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        mouseLook += smoothV;
        mouseLook.y = Mathf.Clamp(mouseLook.y, -90f, 90f);

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        player.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, player.transform.up);

        Debug.DrawRay(transform.position, transform.forward, Color.red);
    }

    IEnumerator PlayStep(AudioClip audioClip)
    {
        stepAudioIsPlaying = true;
        AS.PlayOneShot(audioClip, 0.1f);
        yield return new WaitForSecondsRealtime(0.5f);
        stepAudioIsPlaying = false;
    }

    void PlaceBlock(Transform Block)
    {
        RaycastHit hitInfo;
        Physics.Raycast(transform.position, transform.forward, out hitInfo, 5, LmaskBlockPlacing);
        if (hitInfo.transform != null)
        {
            
            if (BlockToPlace.name == "Stone" || BlockToPlace.name == "Glass" || BlockToPlace.name == "Cobble" || BlockToPlace.name == "Brick")
            {
                AS.PlayOneShot(stone_audio);
            }
            else if (BlockToPlace.name == "Wood" || BlockToPlace.name == "Planks")
            {
                AS.PlayOneShot(wood_audio);
            }


            Vector3 parentBlockPos = hitInfo.transform.parent.transform.position;
            if (hitInfo.transform.name == "top_face")
            {
                Transform block = Instantiate(Block, new Vector3(parentBlockPos.x, parentBlockPos.y + 1, parentBlockPos.z), Quaternion.identity);
                block.parent = hitInfo.transform.parent.parent;
                hitInfo.transform.parent.parent.GetComponent<ChunkScript>().UpdateBlockFacesInChunk();
            }
            else if (hitInfo.transform.name == "bottom_face")
            {
                Transform block = Instantiate(Block, new Vector3(parentBlockPos.x, parentBlockPos.y - 1, parentBlockPos.z), Quaternion.identity);
                block.parent = hitInfo.transform.parent.parent;
                hitInfo.transform.parent.parent.GetComponent<ChunkScript>().UpdateBlockFacesInChunk();
            }
            else if (hitInfo.transform.name == "front_face")
            {
                Transform block = Instantiate(Block, new Vector3(parentBlockPos.x, parentBlockPos.y, parentBlockPos.z + 1), Quaternion.identity);
                block.parent = hitInfo.transform.parent.parent;
                hitInfo.transform.parent.parent.GetComponent<ChunkScript>().UpdateBlockFacesInChunk();
            }
            else if (hitInfo.transform.name == "back_face")
            {
                Transform block = Instantiate(Block, new Vector3(parentBlockPos.x, parentBlockPos.y, parentBlockPos.z - 1), Quaternion.identity);
                block.parent = hitInfo.transform.parent.parent;
                hitInfo.transform.parent.parent.GetComponent<ChunkScript>().UpdateBlockFacesInChunk();
            }
            else if (hitInfo.transform.name == "left_face")
            {
                Transform block = Instantiate(Block, new Vector3(parentBlockPos.x + 1, parentBlockPos.y, parentBlockPos.z), Quaternion.identity);
                block.parent = hitInfo.transform.parent.parent;
                hitInfo.transform.parent.parent.GetComponent<ChunkScript>().UpdateBlockFacesInChunk();
            }
            else if (hitInfo.transform.name == "right_face")
            {
                Transform block = Instantiate(Block, new Vector3(parentBlockPos.x - 1, parentBlockPos.y, parentBlockPos.z), Quaternion.identity);
                block.parent = hitInfo.transform.parent.parent;
                hitInfo.transform.parent.parent.GetComponent<ChunkScript>().UpdateBlockFacesInChunk();
            }
        }
    }

    void PlaceBlockGraphics()
    {
        RaycastHit hitInfo;
        Physics.Raycast(transform.position, transform.forward, out hitInfo, 5, LmaskBlockPlacing);
        if(hitInfo.transform == null)
        {
            PlacingGraphics.position = new Vector3(0,0,0);
        }
        if (hitInfo.transform != null)
        {
            if (hitInfo.transform.name == "top_face")
            {
                PlacingGraphics.position = new Vector3(hitInfo.transform.position.x, hitInfo.transform.position.y + 0.02f, hitInfo.transform.position.z);
                PlacingGraphics.eulerAngles = new Vector3(-90, 0, 0);
            }
            else if (hitInfo.transform.name == "bottom_face")
            {
                PlacingGraphics.position = new Vector3(hitInfo.transform.position.x, hitInfo.transform.position.y - 0.02f, hitInfo.transform.position.z);
                PlacingGraphics.eulerAngles = new Vector3(90, 0, 0);
            }
            else if (hitInfo.transform.name == "front_face")
            {
                PlacingGraphics.position = new Vector3(hitInfo.transform.position.x, hitInfo.transform.position.y, hitInfo.transform.position.z + 0.02f);
                PlacingGraphics.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (hitInfo.transform.name == "back_face")
            {
                PlacingGraphics.position = new Vector3(hitInfo.transform.position.x, hitInfo.transform.position.y, hitInfo.transform.position.z - 0.02f);
                PlacingGraphics.eulerAngles = new Vector3(180, 0, 0);
            }
            else if (hitInfo.transform.name == "left_face")
            {
                PlacingGraphics.position = new Vector3(hitInfo.transform.position.x + 0.02f, hitInfo.transform.position.y, hitInfo.transform.position.z);
                PlacingGraphics.eulerAngles = new Vector3(0, 90, 0);
            }
            else if (hitInfo.transform.name == "right_face")
            {
                PlacingGraphics.position = new Vector3(hitInfo.transform.position.x - 0.02f, hitInfo.transform.position.y, hitInfo.transform.position.z);
                PlacingGraphics.eulerAngles = new Vector3(0, -90, 0);
            }
        }
    }

    void DestroyBlock()
    {
        RaycastHit hitInfo;
        Physics.Raycast(transform.position, transform.forward, out hitInfo, 5, LmaskBlockDestruction);
        if (hitInfo.transform != null)
        {
            if (hitInfo.transform.name == "TNT(Clone)")
            {
                hitInfo.transform.GetComponent<TNTScript>().Explode();
            }
            if (hitInfo.transform.tag == "Block" && hitInfo.transform.name != "TNT(Clone)")
            {
                if (hitInfo.transform.name == "Dirt_block")
                {
                    AS.PlayOneShot(dirt_audio);
                }
                else if (hitInfo.transform.name == "Stone_block" || hitInfo.transform.name == "Stone(Clone)" || hitInfo.transform.name == "Glass(Clone)" || hitInfo.transform.name == "Cobble(Clone)" || hitInfo.transform.name == "Stone(Clone)" || hitInfo.transform.name == "Brick(Clone)")
                {
                    AS.PlayOneShot(stone_audio);
                }
                else if (hitInfo.transform.name == "Wood(Clone)" || hitInfo.transform.name == "Planks(Clone)")
                {
                    AS.PlayOneShot(wood_audio);
                }
                else if (hitInfo.transform.name == "Grass_block" || hitInfo.transform.name == "Leaves(Clone)")
                {
                    AS.PlayOneShot(grass_audio);
                }

                    Debug.Log(hitInfo.transform.name + " hit!");
                if (hitInfo.transform.position.x - hitInfo.transform.parent.position.x == 9)
                {
                    Debug.Log("1_right");
                    RaycastHit chunkChecker;
                    Physics.Raycast(new Vector3(hitInfo.transform.position.x + 0.5f, hitInfo.transform.position.y, hitInfo.transform.position.z), Vector3.right, out chunkChecker, 0.3f);
                    if (chunkChecker.collider != null)
                    {
                        Debug.DrawRay(new Vector3(hitInfo.transform.position.x + 0.5f, hitInfo.transform.position.y, hitInfo.transform.position.z), Vector3.right, Color.red, 1);
                        Debug.Log("2_right");
                        //chunkChecker.transform.parent.GetComponent<ChunkScript>().UpdateBlockFacesInChunk();
                        xChunkToUpdate = chunkChecker.transform.parent;
                    }
                }
                if(hitInfo.transform.position.x - hitInfo.transform.parent.position.x == 0)
                {
                    Debug.Log("1_left");
                    RaycastHit chunkChecker;
                    Physics.Raycast(new Vector3(hitInfo.transform.position.x - 0.5f, hitInfo.transform.position.y, hitInfo.transform.position.z), Vector3.left, out chunkChecker, 0.3f);
                    if (chunkChecker.collider != null)
                    {
                        Debug.DrawRay(new Vector3(hitInfo.transform.position.x - 0.5f, hitInfo.transform.position.y, hitInfo.transform.position.z), Vector3.left, Color.blue, 1);
                        Debug.Log("2_left");
                        //chunkChecker.transform.parent.GetComponent<ChunkScript>().UpdateBlockFacesInChunk();
                        xChunkToUpdate = chunkChecker.transform.parent;
                    }
                }
                if (hitInfo.transform.position.z - hitInfo.transform.parent.position.z == 9)
                {
                    Debug.Log("1_forward");
                    RaycastHit chunkChecker;
                    Physics.Raycast(new Vector3(hitInfo.transform.position.x, hitInfo.transform.position.y, hitInfo.transform.position.z + 0.5f), Vector3.forward, out chunkChecker, 0.3f);
                    if (chunkChecker.collider != null)
                    {
                        Debug.DrawRay(new Vector3(hitInfo.transform.position.x, hitInfo.transform.position.y, hitInfo.transform.position.z + 0.5f), Vector3.forward, Color.green, 1);
                        Debug.Log("2_forward");
                        //chunkChecker.transform.parent.GetComponent<ChunkScript>().UpdateBlockFacesInChunk();
                        zChunkToUpdate = chunkChecker.transform.parent;
                    }
                }
                if (hitInfo.transform.position.z - hitInfo.transform.parent.position.z == 0)
                {
                    Debug.Log("1_back");
                    RaycastHit chunkChecker;
                    Physics.Raycast(new Vector3(hitInfo.transform.position.x, hitInfo.transform.position.y, hitInfo.transform.position.z - 0.5f), Vector3.back, out chunkChecker, 0.3f);
                    if (chunkChecker.collider != null && chunkChecker.transform != player)
                    {
                        Debug.DrawRay(new Vector3(hitInfo.transform.position.x, hitInfo.transform.position.y, hitInfo.transform.position.z - 0.5f), Vector3.back, Color.yellow, 1);
                        Debug.Log("2_back");
                        //chunkChecker.transform.parent.GetComponent<ChunkScript>().UpdateBlockFacesInChunk();
                        zChunkToUpdate = chunkChecker.transform.parent;
                    }
                }

                BlockToDestroy = hitInfo.transform;
                Destroy(hitInfo.transform.gameObject);
                
            }
        }
    }
}
