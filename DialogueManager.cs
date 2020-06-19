using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    private Queue<string> sentences;
    public Text dialogueText;
    public GameObject continueButton;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        continueButton = GameObject.Find("ContinueButton");
        continueButton.GetComponent<Image>().enabled = false;
    }

    private void Update()
    {

        if((Input.GetKeyDown(KeyCode.K) == true)&&(continueButton.GetComponent<Image>().enabled == true))
        {
            Debug.Log("Continue key pressed");
            DisplayNextSentence();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        GameObject.Find("Player").GetComponent<DialogueTrigger>().dialogueActive = true;
        GameObject.Find("Player").GetComponent<PlayerController>().enabled = false;

        Debug.Log("Starting conversation with " + dialogue.name);
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        continueButton.GetComponent<Image>().enabled = false;
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
        continueButton.GetComponent<Image>().enabled = true;
    }

    public void EndDialogue()
    {
        dialogueText.text = "";
        continueButton.GetComponent<Image>().enabled = false;
        GameObject.Find("Player").GetComponent<DialogueTrigger>().dialogueActive = false;
        GameObject.Find("Player").GetComponent<PlayerController>().enabled = true;
        Debug.Log("End Of Conversation.");
    }
}
