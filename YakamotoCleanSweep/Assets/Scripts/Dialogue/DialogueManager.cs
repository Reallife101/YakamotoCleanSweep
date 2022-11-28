using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instance;

    //All objects which are to be displayed 
    [SerializeField] GameObject dialogueObject;
    [SerializeField] GameObject dialogueBox;
    [SerializeField] Image actorImage;
    [SerializeField] TextMeshProUGUI actorName;
    [SerializeField] TextMeshProUGUI messageText;

    [SerializeField] List<AudioClip> dialogueClips;

    private Message[] currentMessages;
    private Actor[] currentActors;
    private int nextLevel;
    private int activeMessage = 0;
    public static bool isActive = false;
    private AudioSource au;

    private void Awake()
    {
        instance = this;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    public void OpenDialogue(Message[] messages, Actor[] actors, int next)
    {
        currentMessages = messages;
        currentActors = actors;
        nextLevel = next;
        activeMessage = 0;
        au = GetComponent<AudioSource>();
        isActive = true;
        dialogueObject.SetActive(true);
        Debug.Log("Loaded convo");
        DisplayMessage();
    }

    private void DisplayMessage()
    {
        Message messageToDisplay = currentMessages[activeMessage];
        messageText.text = messageToDisplay.message;

        if (activeMessage<dialogueClips.Count)
        {
            au.Stop();
            au.clip = dialogueClips[activeMessage];
            au.Play();
        }

        Actor actorToDisplay = currentActors[messageToDisplay.actorid];
        actorName.text = actorToDisplay.name;
        actorImage.sprite = actorToDisplay.sprite;
        actorImage.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(actorToDisplay.sprite.rect.width, actorToDisplay.sprite.rect.height);
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
            /*Debug.Log("Convo ended");
            isActive = false;
            dialogueObject.SetActive(false);*/
            SceneManager.LoadScene(nextLevel);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //isActive = false;
        //dialogueObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && isActive == true)
        {
            NextMessage();
        }
        

    }
}
