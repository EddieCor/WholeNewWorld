using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject Player01_Padle;
    public GameObject Player02_Padle;
    [Space]
    public Text Player1ScoreText;
    public Text Player2ScoreText;
    [Space]
    public GameObject Player01_WinsSide;
    public GameObject Player02_WinsSide;
    [Space]
    public Text P01_points;
    public Text P02_points;

    private GameObject lastselected;

    // Start is called before the first frame update
    void Start()
    {
        Player01_WinsSide.SetActive(false);
        Player02_WinsSide.SetActive(false);
        float P01_posY = PlayerPrefs.GetFloat("P01_pos", -1000);
        float P02_posY = PlayerPrefs.GetFloat("P02_pos", -1000);
        if ( P01_posY == -1000)
        {
            P01_posY = 0;
        }
        if (P02_posY == -1000)
        {
            P02_posY = 0;
        }

        Player01_Padle.transform.position = new Vector3(Player01_Padle.transform.position.x, P01_posY, Player01_Padle.transform.position.z);
        Player02_Padle.transform.position = new Vector3(Player02_Padle.transform.position.x, P02_posY, Player02_Padle.transform.position.z);

        //Player scores
        int WinnerScore = PlayerPrefs.GetInt("WinnerPoints", 0);
        int LooserScore = PlayerPrefs.GetInt("LooserPoints", 0);
        int difScore = PlayerPrefs.GetInt("ScoreDif", 0);
        string WinnerPlayerID = PlayerPrefs.GetString("WinnerPlayer", "Player Unknown");

        if( WinnerPlayerID == "Player 01")
        {
            Player1ScoreText.text = WinnerScore.ToString();
            Player2ScoreText.text = LooserScore.ToString();
            Player01_WinsSide.SetActive(true);
            P01_points.text = difScore.ToString();
        }

        if (WinnerPlayerID == "Player 02")
        {
            Player1ScoreText.text = LooserScore.ToString();
            Player2ScoreText.text = WinnerScore.ToString();
            Player02_WinsSide.SetActive(true);
            P02_points.text = difScore.ToString();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastselected);
        }
        else
        {
            lastselected = EventSystem.current.currentSelectedGameObject;
        }
    }

    public void PlayAgainButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        //Forma Alternativa SceneManager.LoadScene(1);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
        //Forma Alternativa SceneManager.LoadScene(0);
    }
}
