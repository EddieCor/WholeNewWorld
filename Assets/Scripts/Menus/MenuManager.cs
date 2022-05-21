using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private GameObject lastselected;

    public GameObject MainMenu;
    public GameObject OptionsMenu;
    public GameObject HighscoreMenu;
    public GameObject CreditsMenu;

    // Start is called before the first frame update
    void Start()
    {
        MainMenu.SetActive(true);
        OptionsMenu.SetActive(false);
        HighscoreMenu.SetActive(false);
        CreditsMenu.SetActive(false);
    }

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

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //El ID de Menu es 0 y el de juego es 1. Por eso se suma 1
        //Forma Alternativa SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
