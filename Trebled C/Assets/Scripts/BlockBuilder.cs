using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBuilder : MonoBehaviour
{
    [SerializeField]
    public Sprite[] solidBlocks;
    [SerializeField]
    public string[] solidNames;

    public Block[] allBlocks;

    private void Awake()
    {
        allBlocks = new Block[solidBlocks.Length];
        allBlocks[0] = new Block(0, solidNames[0], solidBlocks[0], true);
    }
}
public class Block
{
    public int blockID;
    public string blockName;
    public Sprite blockSprite;
    public bool isSolid;


    public Block(int id, string myName, Sprite mySprite, bool solid)
    {
        blockID = id;
        blockName = myName;
        blockSprite = mySprite;
        isSolid = solid;
    }
}