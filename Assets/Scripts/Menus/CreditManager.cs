using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CreditManager : MonoBehaviour
{
    private GameObject lastselected;

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastselected);
        }
        else
        {
            lastselected = EventSystem.current.currentSelectedGameObject;
        }
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
