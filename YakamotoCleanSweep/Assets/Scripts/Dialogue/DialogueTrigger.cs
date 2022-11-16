using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    private Message[] messages;
    [SerializeField]
    private Actor[] actors;

    public void StartDialogue()
    {
        //Creates a singleton in the dialogue manager with the messages and actors in the fields.
        DialogueManager.GetInstance().OpenDialogue(messages, actors);
    }

    //REMOVE THIS START FUNCTION IF WE PLAN ON IMPLEMENTING DIALOGUE WITHIN LEVELS AND MAKE INHERITTED CLASS FOR SCENE ONLY DIALOGUE
    void Start()
    {
        StartDialogue();
    }
}

[System.Serializable]
public class Message
{
    public int actorid;
    public string message;
}
[System.Serializable]
public class Actor
{
    public string name;
    public Sprite sprite;
}
