using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDetector : MonoBehaviour
{
    public GameObject player;
    public GMScript gmScript;
    public SoundPlayer soundPlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.GetComponent<ConstantMove>().enabled = false;
        player.transform.position = gmScript.startingPlayerPos;
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        gmScript.currentIndex = gmScript.startingIndex;
        gmScript.EnableBlocks();
        soundPlayer.StopSoundPlayer();   // cut off the backing audio
    }
}
