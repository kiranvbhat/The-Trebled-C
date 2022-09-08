using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public GameObject midiManager;
    public void LoadScene (int midiNum)
    {
        if (midiNum == 48) {
            Destroy(midiManager);
            SceneManager.LoadScene("Level 1");
        }

        else if (midiNum == 50) {
            Destroy(midiManager);
            SceneManager.LoadScene("Level 2");
        }
    }
}
