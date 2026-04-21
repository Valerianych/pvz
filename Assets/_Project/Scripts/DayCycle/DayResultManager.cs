using UnityEngine;

public class DayResultManager : MonoBehaviour
{
    public static DayResultManager Instance;

    [Header("Статистика дня")]
    public int completedOrders;
    public int lostCustomers;
    public int mistakes;
    public int earnedMoney;

    private void Awake()
    {
        Instance = this;
    }

    public void AddCompletedOrder(int reward)
    {
        completedOrders++;
        earnedMoney += reward;
    }

    public void AddLostCustomer()
    {
        lostCustomers++;
    }

    public void AddMistake()
    {
        mistakes++;
    }

    public int GetPenalty()
    {
        int lostCustomerPenalty = lostCustomers * 50;
        int mistakePenalty = mistakes * 100;

        return lostCustomerPenalty + mistakePenalty;
    }

    public int GetTotalMoneyForDay()
    {
        return earnedMoney - GetPenalty();
    }

    public float GetRating()
    {
        float rating = 5f;

        rating -= lostCustomers * 0.3f;
        rating -= mistakes * 0.5f;

        if (completedOrders >= 10)
            rating += 0.2f;

        return Mathf.Clamp(rating, 1f, 5f);
    }

    public void ResetResults()
    {
        completedOrders = 0;
        lostCustomers = 0;
        mistakes = 0;
        earnedMoney = 0;
    }
}