using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DialogueSystem : MonoBehaviour
{
    [System.Serializable]
    public class Dialogue
    {
        public string characterName;
        [TextArea] public List<string> sentences;
    }

    public List<Dialogue> dialogues;
    public Text dialogueText;
    public string playerName = "Игрок";
    public string npcName = "NPC";

    private int dialogueIndex = 0;
    private int sentenceIndex = 0;

    private bool isPlayerInRange = false;

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (dialogueIndex < dialogues.Count)
            {
                if (sentenceIndex < dialogues[dialogueIndex].sentences.Count)
                {
                    ShowNextSentence();
                }
                else
                {
                    // Переход к следующему персонажу
                    dialogueIndex++;
                    sentenceIndex = 0;

                    if (dialogueIndex < dialogues.Count)
                    {
                        ShowNextSentence();
                    }
                    else
                    {
                        EndDialogue();
                    }
                }
            }
        }
    }

    private void ShowNextSentence()
    {
        string currentCharacterName = dialogues[dialogueIndex].characterName;
        dialogueText.text = $"{currentCharacterName}: {dialogues[dialogueIndex].sentences[sentenceIndex]}";
        sentenceIndex++;
    }

    private void EndDialogue()
    {
        dialogueText.text = ""; // Скрыть текст диалога
        dialogueIndex = 0; // Сбросить индекс диалога, если нужно повторить
        sentenceIndex = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Убедитесь, что у вас установлен тег "Player" на игрока
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            EndDialogue();
        }
    }
}
