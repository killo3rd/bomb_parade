using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public GameObject MainMenu;
    public GameObject ExitDialog;

    [Header("New Game Menu")]
    public GameObject NewGameMain;
    public GameObject[] NewGameMenus;
    public Button[] playerButtons;
    public Button[] mapButtons;
    public Button[] ruleButtons;


    public Image player1Image;
    public Image player2Image;
    public Text mapText;
    public Text ruleText;


    int player1;
    int player2;
    int map;
    int rule;

    int currentPlayerSelection;

    int screenPosition = 0;

    void Start()
    {
        ResetGamePreferences();

        for (int i = 0; i < playerButtons.Length; i++)
        {
            int tmp = i;
            playerButtons[tmp].onClick.AddListener(() => SelectPlayer(tmp+1, playerButtons[tmp].image.color));
        }
 
        for (int i = 0; i < mapButtons.Length; i++)
        {
            int tmp = i;
            mapButtons[tmp].onClick.AddListener(() => SelectMap(tmp + 1, mapButtons[tmp].transform.GetChild(0).GetComponent<Text>().text));
        }

        for (int i = 0; i < ruleButtons.Length; i++)
        {
            int tmp = i;
            ruleButtons[tmp].onClick.AddListener(() => SelectRule(tmp + 1, ruleButtons[tmp].transform.GetChild(0).GetComponent<Text>().text));
        }
    }
 
    //
    // Main Menu
    // 

    public void BackToMenu(GameObject previous)
    {
        screenPosition = 0;
        ResetGamePreferences();

        previous.SetActive(false);
        MainMenu.SetActive(true);
    }

    //
    // New Game
    // 

    public void ShowNewGameDialog()
    {
        NewGameMain.SetActive(true);
        MainMenu.SetActive(false);

        RefreshNewGameDialog();

        for (int i = 0; i < NewGameMenus.Length; i++)
        {
            if (i == screenPosition)
                NewGameMenus[i].SetActive(true);
            else
                NewGameMenus[i].SetActive(false);
        }
    }

    public void NextSelection()
    {
        if (Validating())
        {
            screenPosition++;
            ShowNewGameDialog();
        }
    }

    public void PreviousSelection()
    {
        screenPosition--;
        ShowNewGameDialog();
    }

    bool Validating()
    {
        switch (screenPosition)
        {
            case 0:
                if (player1 == 0 || player2 == 0)
                    return false;
                break;

            case 1:
                if (map == 0)
                    return false;
                break;
             
            case 2:
                if (rule == 0)
                    return false;
                break;
        }

        return true;
    }

    void ResetGamePreferences()
    {
        player1 = 0;
        player2 = 0;
        map = 0;
        rule = 0;

        currentPlayerSelection = 0;

    }

    void RefreshNewGameDialog()
    {
        if (player1 == 0)
            player1Image.color = new Color(255,255,255,0);
        if (player2 == 0)
            player2Image.color = new Color(255, 255, 255, 0);
        if (map == 0)
            mapText.text = "";
        if (rule == 0)
            ruleText.text = "";

    }

    public void SelectPlayer(int player, Color color)
    {
        if (currentPlayerSelection == 0)
        {
            player1 = player;
            player1Image.color = color;
            currentPlayerSelection++;
        }
        else {
            player2 = player;
            player2Image.color = color;
            currentPlayerSelection = 0;
        }
    }

    public void SelectMap(int _map, string text)
    {
        map = _map;
        mapText.text = text;
    }

    public void SelectRule(int _rules, string text)
    {
        rule = _rules;
        ruleText.text = text;
    }

    //
    // Exit Dialog
    //
    public void ShowExitDialog()
    {
        MainMenu.SetActive(false);
        ExitDialog.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
