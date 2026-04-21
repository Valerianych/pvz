using UnityEngine;

public class DayTimeManager : MonoBehaviour
{
    public static DayTimeManager Instance;

    [Header("Время рабочего дня")]
    public int hour = 9;
    public int minute = 0;

    [Header("Скорость времени")]
    public float realSecondsPerGameMinute = 0.5f;

    private float timer;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateTimeUI();
    }

    private void Update()
    {
        if (GameManager.Instance != null && !GameManager.Instance.isDayActive)
            return;

        timer += Time.deltaTime;

        if (timer >= realSecondsPerGameMinute)
        {
            timer = 0f;
            AddMinute();
        }
    }

    private void AddMinute()
    {
        minute++;

        if (minute >= 60)
        {
            minute = 0;
            hour++;
        }

        UpdateTimeUI();

        if (hour >= 21)
        {
            if (GameManager.Instance != null)
                GameManager.Instance.EndDay();
        }
    }

    public bool IsRushHour()
    {
        bool lunchRush = hour >= 12 && hour < 14;
        bool eveningRush = hour >= 18 && hour < 20;

        return lunchRush || eveningRush;
    }

    public string GetTimeText()
    {
        return hour.ToString("00") + ":" + minute.ToString("00");
    }

    public void ResetTime()
    {
        hour = 9;
        minute = 0;
        timer = 0f;

        UpdateTimeUI();
    }

    private void UpdateTimeUI()
    {
        if (UIManager.Instance != null)
            UIManager.Instance.SetTime(GetTimeText());
    }
}