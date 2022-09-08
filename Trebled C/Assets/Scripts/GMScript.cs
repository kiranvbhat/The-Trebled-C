using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class GMScript : MonoBehaviour
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

    //---------------------------- AUDIO CLIPS -------------------------------------------
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

    public AudioClip twinkle1;
    public AudioClip twinkle2;
    public AudioClip twinkle3;
    public AudioClip twinkle4;
    public AudioClip twinkle5;
    public AudioClip twinkle6;

    //---------------------------- GAMEOBJECTS -------------------------------------------
    public GameObject player;
    [SerializeField] private Image BuildBlocksImage;
    [SerializeField] private Image JumpBlocksImage;
    [SerializeField] private Image PianoImage;
    [SerializeField] private Image StartNote;
    [SerializeField] private Image PressForSong;

    public GameObject terrainGround;
    public GameObject menu;

    //---------------------------- OTHER CLASSES -------------------------------------------
    public BuildSystem buildSystem;
    public PlayerMovement playerMovement;
    public CameraMovement cameraMovement;
    public TriggerNextSection triggerNextSection;
    public SoundPlayer soundPlayer;


    //----------------------------- SONG VARIABLES ---------------------------------------------
    private int[] song = new int[] {60, 60, 67, 67, 69, 69, 67, 65, 65, 64, 64, 62, 62, 60, 67, 67, 65, 65, 64, 64, 62, 67, 67, 65, 65, 64, 64, 62, 60, 60, 67, 67, 69, 69, 67, 65, 65, 64, 64, 62, 67, 60};    // array of midi Numbers of pitches in song
    // private int[] song = new int[] {60, 67, 64, 67, 69, 67, 64, 62, 67, 60, 67, 62, 59, 60 };
    
    // private float[] xs = new float[] {0, 3, 6};
    // private float[] ys = new float[] {0, 0, 0};
    // private List<Note> notes = new List<Note>();
    public int numBlocksInSection = 7;
    private int numNotes = 42;
    // private int numNotes = 14;

    //-------------------------- STATE OF GAME VARIABLES --------------------------------
    public int startingIndex = 0;  // index for pirate to start at each stage
    public int currentIndex = 0;   // current index of pirate

    private Vector2 startingBlockPos = new Vector2(-7, 0);  // position for block to start at each stage
    private Vector2 currentBlockPos = new Vector2(-7, 0);   // current position of placed block

    public Vector2 startingPlayerPos = new Vector2(-7, -2);

    private bool building = false;   // true when building mode is on
    private bool running = false;
    private bool inMenu = true;
    

    public float moveSpeed = 1.0f;
    public Vector3 moveVector;

    private void Start() {
        soundPlayer.PlayMenuSong();
    }

    public void NoteDownUpdate(int midiNum) {
        if (inMenu) {
            PlayNote(midiNum);
            if (midiNum == 48) {
                StartGame();
            }
            return;
        }

        if (midiNum == 72 && currentIndex == startingIndex) {    // 72 is C5, we want to play the song section if note is a C5 and the player is at the starting block.
            PlaySongSection();
            return;
        }
        bool correct = CheckNote(midiNum);
        if (building) {
            PlayNote(midiNum);
            player.GetComponent<ConstantMove>().enabled = false; // stop player from running (lock movement)
            if (correct) {
                PlaceBlock();
            } else {
                ResetBlocks();
            }
        } 

        else if (running) {
            if (correct) {
                if (currentIndex == startingIndex) {    // play backing track at start
                    soundPlayer.PlayBacking();
                }
                PlayNote(midiNum);
                player.GetComponent<ConstantMove>().enabled = true; // update player to start running
                Jump();
            } else {
                PlayNote(midiNum);
                DisableBlocks();
                //PlaySongSection();
            }
        }
    }
    

    public bool CheckNote(int midiNum) {
        if (currentIndex < numNotes) {
            int correctMidiNum = song[currentIndex];
            return midiNum == correctMidiNum;
        }
        return false;
    }

    public void PlaceBlock() {
        int currentNote = song[currentIndex];
        float nextX = currentBlockPos.x + 3f;

        // calculate y of the block using interval distance
        float nextY = 0;
        if (currentIndex < numNotes) {  // if there is a next note
            nextY = (float)offsetNote(currentNote - 12, 60);    // 60 offset so that c3 is -12, c4 is 0, c5 is 12
            nextY *= 0.3f;   // normalizing the height (height range is -3 to 3)
        }

        currentBlockPos = new Vector2(nextX, nextY);
        buildSystem.PlaceBlock(currentBlockPos);       // placing block

        currentIndex++;

        if (currentIndex == startingIndex + numBlocksInSection) {     // if we build all platforms, then reset index and start running
            Vector3 nextSectionTriggerPos = new Vector2(currentBlockPos.x + 1, currentBlockPos.y + 1);
            triggerNextSection.updateNextSectionTriggerPos(nextSectionTriggerPos);  // move next section trigger
            SetMode("running");
        }
    }

    public void ResetBlocks() {
        buildSystem.DestroyWithTag("Plank");    // destroy all planks
        currentIndex = startingIndex;   // reset current index
        currentBlockPos = startingBlockPos;   // reset block position
    }

    public void DisableBlocks() {
        buildSystem.DisableWithTag("Plank");
    }

    public void EnableBlocks() {
        buildSystem.EnableWithTag("Plank");
    }

    private void Jump() {
        float jumpForce;
        if (currentIndex < numNotes) {  // if there is a next note
            int currentNote = (currentIndex == 0) ? 60 : song[currentIndex - 1];
            int nextNote = song[currentIndex];
            int interval = nextNote - currentNote;
            jumpForce = (float)((interval + 16) * 26);     // calculate jump force with interval
            // jumpForce = (float)interval * 200;
            playerMovement.Jump(jumpForce);
            currentIndex++;
        }
    }

    public void NextSection() {
        triggerNextSection.updateNextSectionTriggerPos(new Vector2(0, 100));  //move it out of the way for now, we'll fix it's position when placing blocks
        player.GetComponent<ConstantMove>().enabled = false; // lock player movement
        SetMode("building");
        Vector3 newCameraPos = new Vector3(player.transform.position.x + 10.5f, 0, -10);
        cameraMovement.UpdateCamera(newCameraPos);
        startingIndex = currentIndex;
        startingBlockPos = currentBlockPos;
        startingPlayerPos = player.transform.position;
        GameObject startingBlock = Instantiate(terrainGround, currentBlockPos, Quaternion.identity) as GameObject;
        startingBlock.tag = "StartingBlock";
        PianoImage.enabled = false;
        PressForSong.enabled = false;
        StartNote.enabled = false;
    }

    public void StartGame() {
        soundPlayer.StopSoundPlayer();
        SetMode("building");
    }

    public void EndGame() {
        SetMode("inMenu");
        player.GetComponent<ConstantMove>().enabled = false; // lock player movement
        buildSystem.DestroyWithTag("Plank");  // destroy blocks of tag planks
        buildSystem.DestroyWithTag("StartingBlock");  // destroy blocks of tag StartingBlock
        // place very first block back
        player.transform.position = startingPlayerPos;  // move player back to starting position
        Vector3 newCameraPos = new Vector3(player.transform.position.x + 10.5f, 0, -10);
        cameraMovement.UpdateCamera(newCameraPos);
        
    }

    private void PlayNote(int midiNum) {
        AudioClip[] noteSounds = new AudioClip[] { c3, cs3, d3, ds3, e3, f3, fs3, g3, gs3, a3, as3, b3, c4, cs4, d4, ds4, e4, f4, fs4, g4, gs4, a4, as4, b4, c5 };
        AudioClip noteSound = noteSounds[offsetNote(midiNum, 48)];  // c3 at midi number 48, and c3 is at index 0, so offset is 48

        // Play the note
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = noteSound;
        audio.Play();
    }

    private void PlaySongSection() {
        AudioClip[] songSections = new AudioClip[] { twinkle1, twinkle2, twinkle3, twinkle4, twinkle5, twinkle6 };
        AudioClip songSection = songSections[startingIndex / numBlocksInSection];
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = songSection;
        audio.Play();
    }

    // takes in midi number, and the offset. Shifts midi number down by the offset
    private int offsetNote(int midiNum, int offset) {
        return midiNum - offset;
    }

    public void SetMode(string mode) {
        if (mode == "building") {
            inMenu = false;
            running = false;
            building = true;

            BuildBlocksImage.enabled = true;
            JumpBlocksImage.enabled = false;
            menu.SetActive(false);  // disable menu screen
        }

        else if (mode == "running") {
            inMenu = false;
            building = false;   // prevent player from building while jumping
            running = true;

            currentIndex = startingIndex;

            BuildBlocksImage.enabled = false;   // turn off build blocks image
            JumpBlocksImage.enabled = true;
            menu.SetActive(false);
        } 
        
        else if (mode == "inMenu") {
            building = false;
            running = false;
            inMenu = true;

            startingIndex = 0;  // reset starting index and current index to 0
            currentIndex = 0;
            startingBlockPos = new Vector2(-7, 0);
            currentBlockPos = new Vector2(-7, 0);
            startingPlayerPos = new Vector2(-7, -2);

            menu.SetActive(true);
            soundPlayer.PlayMenuSong();
            
        }
        
    }

}
