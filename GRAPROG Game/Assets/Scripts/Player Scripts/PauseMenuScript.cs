﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    public List<Sprite> ImageSequences;
    public List<GameObject> ImageSequencesTexts;
    public Image Page;
    public GameObject HelpPage;
    public GameObject PauseMenu;
    public PlayerController PlayerControllerScript;

    private int _index = 0;
    private int _textIndex = 0;
    private bool _isPaused = false;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_isPaused)
        {
            Pause();
        } 
        else if (Input.GetKeyDown(KeyCode.Escape) && _isPaused)
        {
            Unpause();
            CloseHelpPage();
        }

        for (int _textIndex = 0; _textIndex < ImageSequencesTexts.Capacity; _textIndex++)
        {
            if (_textIndex == _index)
            {
                ImageSequencesTexts[_textIndex].SetActive(true);
            }
            else if (_textIndex != _index)
            {
                ImageSequencesTexts[_textIndex].SetActive(false);
            }
        }
    }

    public void NextPage()
    {
        _index = Mathf.Clamp(_index + 1, 0, ImageSequences.Capacity - 1);
        Page.sprite = ImageSequences[_index];
        _textIndex++;
    }

    public void LastPage()
    {
        _index = Mathf.Clamp(_index - 1, 0, ImageSequences.Capacity - 1);
        Page.sprite = ImageSequences[_index];
        _textIndex--;
    }

    public void OpenHelpPage()
    {
        HelpPage.SetActive(true);
    }

    public void CloseHelpPage()
    {
        _index = 0;
        HelpPage.SetActive(false);
    }

    public void Pause()
    {
        _isPaused = true;
        Time.timeScale = 0;
        PauseMenu.SetActive(true);
        PlayerControllerScript.enabled = false;
    }

    public void Unpause()
    {
        _isPaused = false;
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
        PlayerControllerScript.enabled = true;
    }
}

