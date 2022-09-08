using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerNextSection : MonoBehaviour
{
    public GMScript gmScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gmScript.NextSection();
    }

    public void updateNextSectionTriggerPos(Vector2 newPos) {
        transform.position = newPos;
    }
}
