using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("День")]
    public int currentDay = 1;

    [Header("Состояние игры")]
    public bool isDayActive = true;

    [Header("Экран итогов дня")]
    public EndDayPanel endDayPanel;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartDay();
    }

    public void StartDay()
    {
        isDayActive = true;

        if (UIManager.Instance != null)
            UIManager.Instance.ShowMessage("День " + currentDay + " начался");
    }

    public void EndDay()
    {
        if (!isDayActive)
            return;

        isDayActive = false;

        if (UIManager.Instance != null)
            UIManager.Instance.ShowMessage("День " + currentDay + " завершён");

        if (endDayPanel != null)
            endDayPanel.ShowPanel();
    }

    public void NextDay()
    {
        currentDay++;
        StartDay();
    }
}