using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Text TimerText;
    public Text RedNumberText;

    private int _Timer;
    private int _RedNumber;
    private int _CurrentRedNumber;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        _Timer = 240;
        TimerText.text = _Timer.ToString();
        _RedNumber = 6;
        _CurrentRedNumber = 0;
        RedNumberText.text = _CurrentRedNumber.ToString() + "/" + _RedNumber.ToString();
        InvokeRepeating("DecreaseTimer", 1f, 1f);
    }

    void Update()
    {
        if(_CurrentRedNumber == _RedNumber)
        {
            _CurrentRedNumber--;
            UiManager.Instance.ShowPanelWin();
        }
    }

    private void DecreaseTimer()
    {
        _Timer--;
        TimerText.text = _Timer.ToString();

        if (_Timer == 0)
        {
            CancelInvoke("DecreaseTimer");

            GameObject white = GameObject.Find("White");
            white.GetComponent<WhiteControl>().CauseDamage(100);
        }
    }

    public void AddRedSaved()
    {
        _CurrentRedNumber++;
        RedNumberText.text = _CurrentRedNumber.ToString() + "/" + _RedNumber.ToString();
    }
}