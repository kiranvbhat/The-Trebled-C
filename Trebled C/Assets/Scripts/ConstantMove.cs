using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantMove : MonoBehaviour
{
    private GMScript gmscript;
 

    // Start is called before the first frame update
    void Start()
    {
        GameObject controller = GameObject.FindGameObjectWithTag("GameController");
        gmscript = controller.GetComponent<GMScript>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(gmscript.moveVector * gmscript.moveSpeed * Time.deltaTime);
    }
}
