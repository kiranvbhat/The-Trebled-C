using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TriggerGameEnd : MonoBehaviour
{
    public GMScript gmScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gmScript.EndGame();
    }
}
