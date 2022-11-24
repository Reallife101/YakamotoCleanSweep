using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instance;

    //All objects which are to be displayed 
    public GameObject dialogueBox;
    public Image actorImage;
    public TextMeshProUGUI actorName;
    public TextMeshProUGUI messageText;

    private Message[] currentMessages;
    private Actor[] currentActors;
    private int activeMessage = 0;
    public static bool isActive = false;

    private void Awake()
    {
        instance = this;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    public void OpenDialogue(Message[] messages, Actor[] actors)
    {
        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;
        isActive = true;
        dialogueBox.SetActive(true);
        Debug.Log("Loaded convo");
        DisplayMessage();
    }

    private void DisplayMessage()
    {
        Message messageToDisplay = currentMessages[activeMessage];
        messageText.text = messageToDisplay.message;

        Actor actorToDisplay = currentActors[messageToDisplay.actorid];
        actorName.text = actorToDisplay.name;
        actorImage.sprite = actorToDisplay.sprite;
    }

    public void NextMessage()
    {
        activeMessage++;
        if (activeMessage < currentMessages.Length)
        {
            DisplayMessage();
        }
        else
        {
            Debug.Log("Convo ended");
            isActive = false;
            dialogueBox.SetActive(false);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        dialogueBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isActive == true)
        {
            NextMessage();
        }
        

    }
}
