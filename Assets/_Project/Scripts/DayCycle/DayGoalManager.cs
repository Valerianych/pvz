using UnityEngine;

public class DayGoalManager : MonoBehaviour
{
    public static DayGoalManager Instance;

    [Header("Текущие цели дня")]
    public int targetOrders;
    public int targetMoney;
    public int maxLostCustomers;
    public int maxMistakes;

    [Header("Награда за цели")]
    public int rewardPerCompletedGoal = 150;
    public int rewardForAllGoals = 500;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetupGoalsForDay();
    }

    public void SetupGoalsForDay()
    {
        int day = 1;

        if (GameManager.Instance != null)
            day = GameManager.Instance.currentDay;

        if (day == 1)
        {
            targetOrders = 8;
            targetMoney = 800;
            maxLostCustomers = 2;
            maxMistakes = 3;
        }
        else if (day == 2)
        {
            targetOrders = 12;
            targetMoney = 1200;
            maxLostCustomers = 2;
            maxMistakes = 2;
        }
        else if (day == 3)
        {
            targetOrders = 15;
            targetMoney = 1500;
            maxLostCustomers = 2;
            maxMistakes = 1;
        }
        else
        {
            targetOrders = 15 + day * 2;
            targetMoney = 1500 + day * 300;
            maxLostCustomers = 2;
            maxMistakes = 1;
        }

        if (UIManager.Instance != null)
            UIManager.Instance.ShowMessage("Новые цели дня получены");
    }

    public bool IsOrdersGoalCompleted()
    {
        if (DayResultManager.Instance == null)
            return false;

        return DayResultManager.Instance.completedOrders >= targetOrders;
    }

    public bool IsMoneyGoalCompleted()
    {
        if (DayResultManager.Instance == null)
            return false;

        return DayResultManager.Instance.earnedMoney >= targetMoney;
    }

    public bool IsLostCustomersGoalCompleted()
    {
        if (DayResultManager.Instance == null)
            return false;

        return DayResultManager.Instance.lostCustomers <= maxLostCustomers;
    }

    public bool IsMistakesGoalCompleted()
    {
        if (DayResultManager.Instance == null)
            return false;

        return DayResultManager.Instance.mistakes <= maxMistakes;
    }

    public int GetCompletedGoalsCount()
    {
        int count = 0;

        if (IsOrdersGoalCompleted())
            count++;

        if (IsMoneyGoalCompleted())
            count++;

        if (IsLostCustomersGoalCompleted())
            count++;

        if (IsMistakesGoalCompleted())
            count++;

        return count;
    }

    public bool AreAllGoalsCompleted()
    {
        return GetCompletedGoalsCount() == 4;
    }

    public int GetGoalBonus()
    {
        int completedGoals = GetCompletedGoalsCount();
        int bonus = completedGoals * rewardPerCompletedGoal;

        if (AreAllGoalsCompleted())
            bonus += rewardForAllGoals;

        return bonus;
    }

    public string GetGoalsText()
    {
        return
            "Цели дня:\n" +
            "Выдать заказов: " + GetProgressText(DayResultManager.Instance.completedOrders, targetOrders, IsOrdersGoalCompleted()) + "\n" +
            "Заработать: " + GetProgressText(DayResultManager.Instance.earnedMoney, targetMoney, IsMoneyGoalCompleted()) + " ₽\n" +
            "Клиентов ушло не больше: " + maxLostCustomers + " " + GetStatusText(IsLostCustomersGoalCompleted()) + "\n" +
            "Ошибок не больше: " + maxMistakes + " " + GetStatusText(IsMistakesGoalCompleted());
    }

    private string GetProgressText(int current, int target, bool completed)
    {
        string status = completed ? "✓" : "×";
        return current + " / " + target + " " + status;
    }

    private string GetStatusText(bool completed)
    {
        return completed ? "✓" : "×";
    }
}