using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour {

    public Texture2D image;
    public int blocksPerLine = 4;
    public int shuffleLength = 20;
    public float defaultMoveDuration = .2f;
    public float shuffleMoveDuration = .1f;

    enum PuzzleState { Solved, Shuffling, InPlay };
    PuzzleState state;

    Block emptyBlock;
    Block[,] blocks;
    Queue<Block> inputs;
    bool blockIsMoving;
    int shuffleMovesRemaining;
    Vector2Int prevShuffleOffset;



    private void Start()


    {
        getPhoto();

        //if not android
        //CreatePuzzle();

        //getPhoto();
       
    }

    public void getPhoto()
    {
        if (NativeGallery.IsMediaPickerBusy())
            return;
        PickImage(1024);


    }

    void Update()
    {
       

       // if (state == PuzzleState.Solved && Input.GetKeyDown(KeyCode.Space))
        //{
        //    StartShuffle();
        //}
    }

    void CreatePuzzle()
    {
        blocks = new Block[blocksPerLine, blocksPerLine];
        //                                                  imgae
        // Create a copy of the texture by reading and applying the raw texture data.
        Texture2D texCopy = new Texture2D(image.width, image.height, image.format, image.mipmapCount > 1);
       
        texCopy.LoadRawTextureData(image.GetRawTextureData());
        //Debug.Log(texCopy.isReadable);
        texCopy.Apply();
        Texture2D[,] imageSlices = ImageSlicer.GetSlices(texCopy, blocksPerLine);


        for (int y = 0; y < blocksPerLine; y++)
        {
            for (int x = 0; x < blocksPerLine; x++)
            {
                GameObject blockObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
                blockObject.transform.position = -Vector2.one * (blocksPerLine - 1) * .5f + new Vector2(x, y);
                blockObject.transform.parent = transform;

                Block block = blockObject.AddComponent<Block>();
                block.OnBlockPressed += PlayerMoveBlockInput;
                block.OnFinishedMoving += OnBlockFinishedMoving;
                block.Init(new Vector2Int(x, y), imageSlices[x, y]);
                blocks[x, y] = block;

                if (y == 0 && x == blocksPerLine - 1)
                {
                    emptyBlock = block;
                }
            }
        }

        Camera.main.orthographicSize = blocksPerLine; //* .55f;
        inputs = new Queue<Block>();
    }

    void PlayerMoveBlockInput(Block blockToMove)
    {
        if (state == PuzzleState.InPlay)
        {
            inputs.Enqueue(blockToMove);
            MakeNextPlayerMove();
        }
    }

    void MakeNextPlayerMove()
    {
		while (inputs.Count > 0 && !blockIsMoving)
		{
            MoveBlock(inputs.Dequeue(), defaultMoveDuration);
		}
    }

    void MoveBlock(Block blockToMove, float duration)
    {
		if ((blockToMove.coord - emptyBlock.coord).sqrMagnitude == 1)
		{
            blocks[blockToMove.coord.x, blockToMove.coord.y] = emptyBlock;
            blocks[emptyBlock.coord.x, emptyBlock.coord.y] = blockToMove;

			Vector2Int targetCoord = emptyBlock.coord;
			emptyBlock.coord = blockToMove.coord;
			blockToMove.coord = targetCoord;

			Vector2 targetPosition = emptyBlock.transform.position;
			emptyBlock.transform.position = blockToMove.transform.position;
            blockToMove.MoveToPosition(targetPosition, duration);
            blockIsMoving = true;
		}
    }

    void OnBlockFinishedMoving()
    {
        blockIsMoving = false;
        CheckIfSolved();

        if (state == PuzzleState.InPlay)
        {
            MakeNextPlayerMove();
        }
        else if (state == PuzzleState.Shuffling)
        {
            if (shuffleMovesRemaining > 0)
            {
                MakeNextShuffleMove();
            }
            else
            {
                state = PuzzleState.InPlay;
            }
        }
    }

    void StartShuffle()
    {
        state = PuzzleState.Shuffling;
        shuffleMovesRemaining = shuffleLength;
        emptyBlock.gameObject.SetActive(false);
        MakeNextShuffleMove();
    }

    void MakeNextShuffleMove()
    {
        Vector2Int[] offsets = { new Vector2Int(1, 0), new Vector2Int(-1, 0), new Vector2Int(0, 1), new Vector2Int(0, -1) };
        int randomIndex = Random.Range(0, offsets.Length);

        for (int i = 0; i < offsets.Length; i++)
        {
            Vector2Int offset = offsets[(randomIndex + i) % offsets.Length];
            if (offset != prevShuffleOffset * -1)
            {
                Vector2Int moveBlockCoord = emptyBlock.coord + offset;

                if (moveBlockCoord.x >= 0 && moveBlockCoord.x < blocksPerLine && moveBlockCoord.y >= 0 && moveBlockCoord.y < blocksPerLine)
                {
                    MoveBlock(blocks[moveBlockCoord.x, moveBlockCoord.y], shuffleMoveDuration);
                    shuffleMovesRemaining--;
                    prevShuffleOffset = offset;
                    break;
                }
            }
        }
      
    }

    void CheckIfSolved()
    {
        foreach (Block block in blocks)
        {
            if (!block.IsAtStartingCoord())
            {
                return;
            }
        }

        state = PuzzleState.Solved;
        emptyBlock.gameObject.SetActive(true);
    }

    private void PickImage(int maxSize)
    {
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            Debug.Log("Image path: " + path);
            if (path != null)
            {
                // Create Texture from selected image

                Texture2D texture = NativeGallery.LoadImageAtPath(path, maxSize);
                /*
                Texture2D texCopy = new Texture2D(texture.width, texture.height, texture.format, texture.mipmapCount > 1);
                texCopy.LoadRawTextureData(texture.GetRawTextureData());
                texCopy.Apply();

                byte[] pix = texture.GetRawTextureData();
                Texture2D readableText = new Texture2D(texture.width, texture.height, texture.format, false);
                readableText.LoadRawTextureData(pix);
                readableText.Apply();
            */

                RenderTexture renderTex = RenderTexture.GetTemporary(
                texture.width,
                texture.height,
                0,
                RenderTextureFormat.Default,
                RenderTextureReadWrite.Linear);

                Graphics.Blit(texture, renderTex);
                RenderTexture previous = RenderTexture.active;
                RenderTexture.active = renderTex;
                Texture2D readableText2 = new Texture2D(texture.width, texture.height);
                readableText2.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
                readableText2.Apply();
                RenderTexture.active = previous;
                RenderTexture.ReleaseTemporary(renderTex);


                //if (texture == null)
                //{
                //    Debug.Log("Couldn't load texture from " + path);
                //    return;
                //}

                 blocks = new Block[blocksPerLine, blocksPerLine];
                 Texture2D[,] imageSlices = ImageSlicer.GetSlices(readableText2, blocksPerLine);
                 for (int y = 0; y < blocksPerLine; y++)
                 {
                     for (int x = 0; x < blocksPerLine; x++)
                     {
                         GameObject blockObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
                         blockObject.transform.position = -Vector2.one * (blocksPerLine - 1) * .5f + new Vector2(x, y);
                         blockObject.transform.parent = transform;

                         Block block = blockObject.AddComponent<Block>();
                         block.OnBlockPressed += PlayerMoveBlockInput;
                         block.OnFinishedMoving += OnBlockFinishedMoving;
                         block.Init(new Vector2Int(x, y), imageSlices[x, y]);
                         blocks[x, y] = block;

                         if (y == 0 && x == blocksPerLine - 1)
                         {
                             emptyBlock = block;
                         }
                     }
                 }

                 Camera.main.orthographicSize = blocksPerLine; //* .55f;
                 inputs = new Queue<Block>();

                StartShuffle();
                /*
                           // Assign texture to a temporary quad and destroy it after 5 seconds
                           GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
                            quad.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2.5f;
                           quad.transform.forward = Camera.main.transform.forward;
                           //1 2
                            quad.transform.localScale = new Vector3(1f, readableText2.height / (float)readableText2.width, 1f);

                            Material material = quad.GetComponent<Renderer>().material;
                            if (!material.shader.isSupported) // happens when Standard shader is not included in the build
                                material.shader = Shader.Find("Legacy Shaders/Diffuse");
                            //3
                            material.mainTexture = readableText2;


                           Destroy(quad, 5f);

                           // If a procedural texture is not destroyed manually, 
                           // it will only be freed after a scene change
                           //4
                            Destroy(readableText2, 5f);
                            **/
            }
        }, "Select a PNG image", "image/png", maxSize);



        Debug.Log("Permission result: " + permission);
    }
}
