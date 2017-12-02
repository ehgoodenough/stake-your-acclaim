﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Dialogue : MonoBehaviour {

    const float TRIGGER_DISTANCE = 5f;

    private Text _textComponent;

    public string[]  DialogueStrings;

    public float TimeBetweenCharacters = 0.05f;
    public float characterRate  = 0.5f;

    private bool _isStringBeingRevealed = false;
    private bool _isDialoguePlaying = false;
    private bool _isEndofDialogue = false;

    public GameObject continueText;
    public GameObject stopText;

    private GameObject dialoguePanel;



	// Use this for initialization
	void Start () {
		_textComponent = GetComponent<Text>();
        _textComponent.text = " ";
	}
	
	// Update is called once per frame
	void Update () {

		GameObject player = GameObject.Find("Protag");
        GameObject antag  = GameObject.Find("Antag");

         if (Mathf.Abs(player.transform.position.x - antag.transform.position.x) < TRIGGER_DISTANCE
        && Mathf.Abs(player.transform.position.y - antag.transform.position.y) < TRIGGER_DISTANCE
        && Input.GetKey("space"))
        {
            if(!_isDialoguePlaying)
                        {
                            _isDialoguePlaying = true;
                            StartCoroutine(StartDialogue());

                        }


                }
	        }


    private IEnumerator StartDialogue()
    {
        int dialogueLength = DialogueStrings.Length;
        int currentDialogueIndex = 0;
            
            while(currentDialogueIndex < dialogueLength || !_isStringBeingRevealed )
                {
                    if(!_isStringBeingRevealed)
                        {
                            
                         _isStringBeingRevealed = true;
                StartCoroutine(DisplayString(DialogueStrings[currentDialogueIndex++]));

                if (currentDialogueIndex >= dialogueLength)
                {
                    _isEndofDialogue = true;
                }
            }

            yield return 0;

        }

        while (true)
        {
            if (Input.GetKey("space"))
            {
                break;
            }

            yield return 0;

        }

        HideStuff();
        _isEndofDialogue = false;
        _isDialoguePlaying = false;

    }

    private IEnumerator DisplayString(string stringToDisplay)
    {
        int stringLength = stringToDisplay.Length;
        int currentCharacterIndex = 0;

        HideStuff();

        _textComponent.text = "";

        while (currentCharacterIndex < stringLength)
        {
            _textComponent.text += stringToDisplay[currentCharacterIndex];
            currentCharacterIndex++;

            if (currentCharacterIndex < stringLength)
            {
                if (Input.GetKey("space"))
                {
                    yield return new WaitForSeconds(TimeBetweenCharacters * characterRate);
                }
                else
                {
                    yield return new WaitForSeconds(TimeBetweenCharacters);
                }
            }

            else
            {
                break;
            }
        }

        ShowIcon();

        while (true)
        {
            if (Input.GetKey("space"))
            {
                break;
            }

            yield return 0;
        }

        HideStuff();

        _isStringBeingRevealed = false;
        _textComponent.text = "";
    }



    private void HideStuff()
    {
        continueText.SetActive(false);
        stopText.SetActive(false);
    }

    private void ShowIcon()
    {
        if (_isEndofDialogue)
        {
            stopText.SetActive(true);
            return;
        }

        continueText.SetActive(true);
    }

}