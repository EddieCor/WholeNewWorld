using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private GameObject DialogueMark;
    [SerializeField] private GameObject DialoguePanel;
    [SerializeField] private TMP_Text DialogueText;
    [SerializeField, TextArea(4, 8)] public string[] dialogueLines;

    private float typeTime = 0.05f;

    private bool PlayerInRange = false;
    private bool DialogueStart;
    private int lineIndex;

    // Update is called once per frame
    void Update()
    {
        if(PlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!DialogueStart)
            {
                StartDialogue();
            }
            else if(DialogueText.text == dialogueLines[lineIndex])
            {
                NextDialogueLine();
            }
            else
            {
                StopAllCoroutines();
                DialogueText.text = dialogueLines[lineIndex];
            }
        }
    }

    private void StartDialogue()
    {
        DialogueStart = true;
        DialoguePanel.SetActive(true);
        DialogueMark?.SetActive(false);
        lineIndex = 0;
        Time.timeScale = 0f;

        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if(lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            DialogueStart=false;
            DialoguePanel.SetActive(false);
            DialogueMark.SetActive(true);
            Time.timeScale = 1f;
        }
    }

    private IEnumerator ShowLine()
    {
        DialogueText.text = string.Empty;

        foreach (char ch in dialogueLines[lineIndex])
        {
            DialogueText.text += ch;
            yield return new WaitForSecondsRealtime(typeTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerInRange = true;
            DialogueMark.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerInRange = false;
            DialogueMark.SetActive(false);
        }
    }
}
