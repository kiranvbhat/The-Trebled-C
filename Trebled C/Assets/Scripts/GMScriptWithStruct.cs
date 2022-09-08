using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GMScriptWithStruct : MonoBehaviour
{
    // enum Midi
    // {
    //     C3=48,
    //     CS3,
    //     D3,
    //     DS3,
    //     E3,
    //     F3,
    //     FS3,
    //     G3,
    //     GS3,
    //     A3,
    //     AS3,
    //     B3,
    //     C4,
    //     CS4,
    //     D4,
    //     DS4,
    //     E4,
    //     F4,
    //     FS4,
    //     G4,
    //     GS4,
    //     A4,
    //     AS4,
    //     B4,
    //     C5
    // }

    public AudioClip c3;
    public AudioClip cs3;
    public AudioClip d3;
    public AudioClip ds3;
    public AudioClip e3;
    public AudioClip f3;
    public AudioClip fs3;
    public AudioClip g3;
    public AudioClip gs3;
    public AudioClip a3;
    public AudioClip as3;
    public AudioClip b3;
    public AudioClip c4;
    public AudioClip cs4;
    public AudioClip d4;
    public AudioClip ds4;
    public AudioClip e4;
    public AudioClip f4;
    public AudioClip fs4;
    public AudioClip g4;
    public AudioClip gs4;
    public AudioClip a4;
    public AudioClip as4;
    public AudioClip b4;
    public AudioClip c5;

    public BuildSystem buildSystem;
    public PlayerMovement playerMovement;
    public CameraMovement cameraMovement;

    public struct Note
    {
        public int midiNum;    // midi number
        public float x;    // x position of plank
        public float y;    // y position of plank
    }

    //----------------------------- SONG VARIABLES ---------------------------------------------
    private int[] song = new int[] {48, 50, 52};    // array of midi Numbers of pitches in song
    private float[] xs = new float[] {0, 3, 6};
    private float[] ys = new float[] {0, 0, 0};
    private List<Note> notes = new List<Note>();
    private int numNotes = 3;

    //-------------------------- STATE OF GAME VARIABLES --------------------------------
    private int startingIndex = 0;  // index for pirate to start at each stage
    private int currentIndex = 0;   // current index of pirate

    // private Vector2 cameraPos = new Vector2(0, 0, -10);

    private bool building = true;   // true when building mode is on
    private bool running = false;
    private bool stageComplete = false;
    // private int numPlatformsInSection = 7;

    public float moveSpeed = 1.0f;
    public Vector3 moveVector;

    void Start() {
        // Fill in our Note list with Notes
        for (int i = 0; i < numNotes; i++) {
            Note newNote;
            newNote.midiNum = song[i];
            newNote.x = xs[i];
            newNote.y = ys[i];
            notes.Add(newNote);
        }
    }

    public void NoteDownUpdate(int midiNum) {
        bool correct = CheckNote(midiNum);

        if (building) {
            PlayNote(midiNum);
            if (correct) {
                PlaceBlock();
            } else {
                ResetBlocks();
                // PlaySongPortion();  // play back the portion to memorize
            }
        } 

        else if (running) {
            if (correct) {
                PlayNote(midiNum);
                playerMovement.Jump(600f);
            } else {
                PlayNote(midiNum);
                // WE WANT TO MOVE PLAYER 
                
                // ResetBlocks();
                // ResetPlayer();   // this will move player back to original position
            }
        }
        CheckState();
    }
    
    // checks the overall state of the game, makes necessary changes;
    void CheckState() {
        if (building) {
            // stop player from running (lock movement)
        } else if (running) {
            building = false;   // prevent player from building while jumping
            // update player to start running
        }
        

        if (stageComplete) {
            print("YOU WINNNNNN");
            ResetBlocks();
        }
    }

    public bool CheckNote(int midiNum) {
        int correctMidiNum = notes[currentIndex].midiNum;
        return midiNum == correctMidiNum;
    }

    public void PlaceBlock() {
        Note currentNote = notes[currentIndex];
        // buildSystem.PlaceBlock(currentNote.x, currentNote.y);
        // int numPlatforms = currentIndex - startingIndex;
        // if (numPlatforms == numPlatformsInSection) {
        //     updateCamera();
        //     startingIndex = currentIndex;
        // }
        currentIndex++;

        if (currentIndex == numNotes) {     // if we build all platforms, then reset index and start running
            currentIndex = startingIndex;
            building = false;
            running = true;
        }
    }

    public void ResetBlocks() {
        buildSystem.DestroyWithTag("Plank");    // destroy all planks
        currentIndex = startingIndex;   // reset current index
    }


    void PlayNote(int midiNum) {
        int offset = 48;    // c3 at midi number 48, and c3 is at index 0, so offset is 48
        AudioClip[] noteSounds = new AudioClip[] { c3, cs3, d3, ds3, e3, f3, fs3, g3, gs3, a3, as3, b3, c4, cs4, d4, ds4, e4, f4, fs4, g4, gs4, a4, as4, b4, c5 };
        AudioClip noteSound = noteSounds[midiNum - offset];

        // Play the note
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = noteSound;
        audio.Play();
    }

}
