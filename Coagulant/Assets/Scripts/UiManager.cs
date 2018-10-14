using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;
    public GameObject PanelWin;
    public GameObject PanelLose;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ShowPanelWin()
    {
        AudioManager.Instance.PlaySfxWin();
        PanelWin.SetActive(true);
        Time.timeScale = 0;
    }

    public void ShowPanelLose()
    {
        PanelLose.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartStage()
    {
        AudioManager.Instance.PlaySfxClickButton();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMenu()
    {
        AudioManager.Instance.PlaySfxClickButton();
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void GoToTutorial()
    {
        AudioManager.Instance.PlaySfxClickButton();
        SceneManager.LoadScene(1);
    }

    public void GoToStage()
    {
        AudioManager.Instance.PlaySfxClickButton();
        SceneManager.LoadScene(2);
    }
}