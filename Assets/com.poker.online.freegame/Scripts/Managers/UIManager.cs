using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject background;

    [Space(10)]
    [SerializeField] GameObject menu;
    [SerializeField] GameObject game;

    private void Start()
    {
        game.SetActive(false);
        menu.SetActive(true);

        background.SetActive(true);
    }

    public void JoinRoom()
    {
        background.SetActive(false);
        GameManager.Instance.StartGame();
    
        menu.SetActive(false);
        game.SetActive(true);
    }

    public void ExitRoom()
    {
        background.SetActive(true);
        GameManager.Instance.ExitRoom();

        menu.SetActive(true);
        game.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
