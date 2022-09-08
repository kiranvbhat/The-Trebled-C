using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerJump : MonoBehaviour
{

    public PlayerMovement Movement;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Movement.jump = true;
        Debug.Log("Worked");
    }
}
