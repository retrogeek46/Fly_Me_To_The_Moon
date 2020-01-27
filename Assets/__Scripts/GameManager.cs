using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum Commands { 
    Next,
    Yes,
    No,
    Back,
    None
}

public class GameManager : MonoBehaviour {

    [Header("Events")]
    public Events startEvent;
    private Events currentEvent;
    private bool eventActive;
    private Commands currentCommand;
    private Commands commandCache;

    [Header("GameObjects and Properties")]
    public Text textBox;
    public float delay;

    public AudioSource narrator;
    // Start is called before the first frame update
    void Start () {
        currentEvent = startEvent;
        eventActive = false;
        currentCommand = Commands.None;
    }

    // Update is called once per frame
    void Update () {
        // take user input
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
            currentCommand = Commands.Next;
        } 
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            currentCommand = Commands.Yes;
        } 
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
            currentCommand = Commands.No;
        } 
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
            currentCommand = Commands.Back;
        } 
        else {
            currentCommand = Commands.None;
        }

        if (!eventActive && currentEvent == startEvent) {
            eventActive = true;
            StartCoroutine(RunEvent(startEvent));
        }
    }

    private IEnumerator RunEvent (Events coroutineEvent) {
        for (int i = 0; i < coroutineEvent.eventText.Count; i++) {
            // display message
            PlayEventActions(coroutineEvent, i);
            // wait for player input
            if (currentCommand == Commands.None) {
                Commands[] validCommands = { Commands.Next, Commands.Back};
                yield return WaitForKeyPress(validCommands);
            }
            //TODO: add functionality for choices
            if (!TextAnimation.textAnimatorRunning) {
                if (commandCache == Commands.Next) {
                    commandCache = Commands.None;
                    Debug.Log("pressed Next");
                } else if (commandCache == Commands.Back) {
                    commandCache = Commands.None;
                    Debug.Log("pressed Back");
                    i -= 1;
                } else  if (commandCache == Commands.Yes) {
                    commandCache = Commands.Yes;
                    Debug.Log("pressed Yes");
                } else if (commandCache == Commands.No) {
                    commandCache = Commands.No;
                    Debug.Log("pressed No");
                }
            }            
        }
        currentEvent = coroutineEvent.nextEvent;
    }

    private void PlayEventActions (Events coroutineEvent, int index) {
        StartCoroutine(TextAnimation.TextAnimator(delay, textBox, coroutineEvent.eventText[index]));
        narrator.clip = coroutineEvent.eventAudio[index];
        narrator.Play();
    }

    // code sample by LiterallyJeff : https://forum.unity.com/threads/waiting-for-input-in-a-custom-function.474387/
    private IEnumerator WaitForKeyPress (Commands[] validCommands) {
        bool done = false;
        // essentially a "while true", but with a bool to break out naturally
        while (!done) {
            if (currentCommand != Commands.None
                && !TextAnimation.textAnimatorRunning
                && !narrator.isPlaying
                && validCommands.Contains(currentCommand)) {
                commandCache = currentCommand;
                // breaks the loop
                done = true;
            }
            // wait until next frame, then continue execution from here (loop continues)
            yield return null;
        }

    }
}
