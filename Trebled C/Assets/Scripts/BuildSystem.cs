using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSystem : MonoBehaviour
{
    public GameObject player;
    
    private BlockBuilder blockSys;
    int currentBlockID = 0;
    private Block currentBlock;

    // Block template
    // private GameObject blockTemplate;
    private SpriteRenderer currentRend;

    private bool buildModeOn = true;

    // float to adjust size of blocks:
    [SerializeField]
    private float blockSizeMod;

    //maybe there should be sizes for the intervals... like the size of an eighth etc. 
    // public float templatePosX = 2f;
    // public float templatePosY = 1.5f;

    private void Awake()
    {
        blockSys = GetComponent<BlockBuilder>();
    }

    private void Update()
    {
        // if (Input.GetKeyDown("e"))
        // {
        //     buildModeOn = !buildModeOn;
        // }

        // if (blockTemplate != null)
        // {
        //     Destroy(blockTemplate);
        // }

        if (currentBlock == null)
        {
            if (blockSys.allBlocks[currentBlockID] != null)
            {
                currentBlock = blockSys.allBlocks[currentBlockID];
            }

        }


        if (buildModeOn)
        {
            //create new Object
            // blockTemplate = new GameObject("CurrentBlockTemplate");
            // Add and store reference to sprite
            // currentRend = blockTemplate.AddComponent<SpriteRenderer>();
            // currentRend.sprite = currentBlock.blockSprite;
        }

        // if(GameObject.FindGameObjectsWithTag("Plank").Length >= 3) {

        //    player.GetComponent<ConstantMove>().enabled = true;
        // }


        // if (buildModeOn && blockTemplate != null)
        // {
        //     float newPosX = templatePosX;
        //     float newPosY = templatePosY;
        //     blockTemplate.transform.position = new Vector2(templatePosX, templatePosY);


        //     // This is building:
        //     // if (Input.GetMouseButtonDown(0))
        //     // {
        //     //     GameObject newBlock = new GameObject(currentBlock.blockName);
        //     //     newBlock.transform.position = blockTemplate.transform.position;
        //     //     SpriteRenderer newRend = newBlock.AddComponent<SpriteRenderer>();
        //     //     newRend.sprite = currentBlock.blockSprite;

        //     //     newBlock.AddComponent<BoxCollider2D>();

        //     //     templatePosX += 3f;
        //     //     templatePosY += Random.Range(-1, 1);
        //     // }

        //     if (midiScript.noteDown == 48) {
        //         GameObject newBlock = new GameObject(currentBlock.blockName);
        //         newBlock.transform.position = blockTemplate.transform.position;
        //         SpriteRenderer newRend = newBlock.AddComponent<SpriteRenderer>();
        //         newRend.sprite = currentBlock.blockSprite;

        //         newBlock.AddComponent<BoxCollider2D>();

        //         templatePosX += 3f;
        //         templatePosY += Random.Range(-1, 1);
        //     }

        // }
    }

    public void DestroyWithTag (string destroyTag) {
        GameObject[] destroyObject;
        destroyObject = GameObject.FindGameObjectsWithTag(destroyTag);
        foreach (GameObject oneObject in destroyObject) 
            Destroy(oneObject);
    }

    public void DisableWithTag(string disableTag) {
        GameObject[] disableObjects;
        disableObjects = GameObject.FindGameObjectsWithTag(disableTag);
        foreach (GameObject oneObject in disableObjects) 
            oneObject.GetComponent<BoxCollider2D>().enabled = false;
            // MAKE PLANKS SEE-THROUGH HERE PEDRO
    }

    public void EnableWithTag(string enableTag) {
        GameObject[] enableObjects;
        enableObjects = GameObject.FindGameObjectsWithTag(enableTag);
        foreach (GameObject oneObject in enableObjects) 
            oneObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void PlaceBlock(Vector2 newBlockPos) {
        GameObject newBlock = new GameObject(currentBlock.blockName);
        newBlock.tag = "Plank";
        newBlock.transform.position = newBlockPos;
        SpriteRenderer newRend = newBlock.AddComponent<SpriteRenderer>();
        newRend.sprite = currentBlock.blockSprite;
        newBlock.AddComponent<BoxCollider2D>();
    }

}
