using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stopMoving : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private Image CustomImage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.GetComponent<ConstantMove>().enabled = false;
        // Camera should Move
        CustomImage.enabled = true;
    }
}
