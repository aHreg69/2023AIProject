using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{

    public GameObject gameOverUI;
    public GameObject youWinUI;
    public GameObject healthBarUI;
    public PlayerController player;

    public TextMeshProUGUI text;

    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if ((gameOverUI.activeInHierarchy) || (youWinUI.activeInHierarchy))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        } else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        text.text = player.coinCount.ToString();
    }
     public void gameOver()
    {
        gameOverUI.SetActive(true);
        healthBarUI.SetActive(false);
    }
    public void winGame()
    {
        youWinUI.SetActive(true);
        healthBarUI.SetActive(false);
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void quitToMainMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    

}
