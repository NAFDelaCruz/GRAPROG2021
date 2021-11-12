using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenScript : MonoBehaviour
{
    public GameObject ExitConfirm;

    public void ConfirmExit()
    {
        ExitConfirm.SetActive(true);
    }

    public void AbortExit()
    {
        ExitConfirm.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Works");
    }
}
