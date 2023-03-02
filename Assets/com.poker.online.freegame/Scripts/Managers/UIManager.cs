using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject background;

    [Space(10)]
    [SerializeField] GameObject menu;
    [SerializeField] GameObject game;
    [SerializeField] GameObject rules;

    [Space(10)]
    [SerializeField] Button dealBtn;

    private void Start()
    {
        rules.SetActive(false);
        game.SetActive(false);
        menu.SetActive(true);

        background.SetActive(true);
    }

    private void Update()
    {
        dealBtn.interactable = Player.IsMyStep;
    }

    public void JoinRoom()
    {
        background.SetActive(false);
        GameManager.Instance.StartGame();
    
        menu.SetActive(false);
        game.SetActive(true);

        SFXManager.Instance.PlayEffect(2);
    }

    public void ExitRoom()
    {
        background.SetActive(true);
        GameManager.Instance.ExitRoom();

        menu.SetActive(true);
        game.SetActive(false);

        SFXManager.Instance.PlayEffect(2);
    }

    public void OpenRules(bool IsOpen)
    {
        rules.SetActive(IsOpen);
        SFXManager.Instance.PlayEffect(2);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
