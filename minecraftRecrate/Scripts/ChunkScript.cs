using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkScript : MonoBehaviour {
    //blocks
    public Transform Stone_block;
    public Transform Dirt_block;
    public Transform Grass_block;
    public Transform Rose;
    public Transform Tall_Grass;
    public Transform Tree;
    public static Texture2D Heightmap;

    Transform[] BlocksInChunk;

    int structureChance;

	void Start () {
        GenerateChunk();
	}
    public void UpdateBlockFacesInChunk()
    {
        //Destroy(CameraScript.BlockToDestroy.gameObject);
        BlocksInChunk = GetComponentsInChildren<Transform>();
        foreach (Transform block in BlocksInChunk)
        {
            if (block != null && block.GetComponent<BlockScript>())
            {
                block.GetComponent<BlockScript>().UpdateBlockFaces();
            }
        }
    }
	
	void GenerateChunk () {
        // loopa igenom alla block på X axeln
        for (int xOffset = (int)transform.position.x; xOffset <(transform.position.x + 10); xOffset++) 
        {
            // loopa igenom alla block på Z axeln för varje X axel
            for (int zOffset = (int)transform.position.z; zOffset < (transform.position.z + 10); zOffset++)
            {
                // loopa igenom alla block på Y axeln för varje Z axel
                for (int yOffset = 0; yOffset < (Heightmap.GetPixel(xOffset, zOffset).r * 100); yOffset++)
                {
                    // dirt
                    if ((int)(Heightmap.GetPixel(xOffset, zOffset).r * 100) - yOffset < 4 && (Heightmap.GetPixel(xOffset, zOffset).r * 100) - yOffset > 1)
                    {
                        Transform block = Instantiate(Dirt_block, new Vector3(xOffset, yOffset, zOffset), Quaternion.identity);
                        block.parent = transform;
                        block.name = ("Dirt_block");
                    }
                    // grass
                    if ((int)(Heightmap.GetPixel(xOffset, zOffset).r * 100) - yOffset == 0)
                    {
                        Transform block = Instantiate(Grass_block, new Vector3(xOffset, yOffset, zOffset), Quaternion.identity);
                        block.parent = transform;
                        block.name = ("Grass_block");
                        structureChance = Random.Range(1, 201);
                        if (structureChance <= 20)
                        {
                            Transform grass = Instantiate(Tall_Grass, new Vector3(xOffset, yOffset + 1, zOffset), Quaternion.identity);
                            grass.parent = block;
                            grass.name = ("Tall_Grass");
                        }
                        if (structureChance == 200)
                        {
                            Transform grass = Instantiate(Rose, new Vector3(xOffset, yOffset + 1, zOffset), Quaternion.identity);
                            grass.parent = block;
                            grass.name = ("Rose");
                        }
                        if (structureChance == 199)
                        {
                            Transform grass = Instantiate(Tree, new Vector3(xOffset, yOffset + 1, zOffset), Quaternion.identity);
                            grass.parent = transform;
                            grass.name = ("Tree");
                        }
                    }
                    // stone
                    if ((int)(Heightmap.GetPixel(xOffset, zOffset).r * 100) - yOffset > 3)
                    {
                        Transform block = Instantiate(Stone_block, new Vector3(xOffset, yOffset, zOffset), Quaternion.identity);
                        block.parent = transform;
                        block.name = ("Stone_block");
                    }


                }
            }
        }
	}
}
