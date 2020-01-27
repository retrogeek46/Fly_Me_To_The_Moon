using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct Interaction {
    public string type;
    public string message;
}

[System.Serializable]
[CreateAssetMenu(fileName = "New Event", menuName = "Event")]
public class Events : ScriptableObject {
    
    /// Event
    /// Give brief information about this event
    public string eventName;
    public string eventDescription;
    
    /// Event Text
    /// List all the text/audio that this even can deliver to the user
    [TextArea(4,5)]
    public List<string> eventText = new List<string>();
    public List<AudioClip> eventAudio = new List<AudioClip>();

    /// Activation Triggers
    /// These are the triggers that are necessary for activating this event
    public List<string> activationTriggers = new List<string>();

    /// Actions and Choices
    /// Define the actions, interactions, choices etc the player can take in this event
    public List<string> actions = new List<string>();

    /// Triggers
    /// Define the triggers that can be activated by this event
    public List<string> consequences = new List<string>();

    /// Next event
    /// Point to the event that comes after this event
    public Events nextEvent;
}
