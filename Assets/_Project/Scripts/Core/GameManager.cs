using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("День")]
    public int currentDay = 1;

    [Header("Состояние игры")]
    public bool isDayActive = true;

    private void Awake()
    {
        Instance = this;
    }

    public void StartDay()
    {
        isDayActive = true;
        UIManager.Instance.ShowMessage("День " + currentDay + " начался");
    }

    public void EndDay()
    {
        isDayActive = false;
        UIManager.Instance.ShowMessage("День " + currentDay + " завершён");
    }

    public void NextDay()
    {
        currentDay++;
        StartDay();
    }
}