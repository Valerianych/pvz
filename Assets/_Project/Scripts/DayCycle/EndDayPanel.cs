using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EndDayPanel : MonoBehaviour
{
    [Header("Панель итогов")]
    public GameObject panel;

    [Header("Тексты")]
    public TMP_Text titleText;
    public TMP_Text completedOrdersText;
    public TMP_Text lostCustomersText;
    public TMP_Text mistakesText;
    public TMP_Text earnedMoneyText;
    public TMP_Text penaltiesText;
    public TMP_Text totalMoneyText;
    public TMP_Text ratingText;
    public TMP_Text goalsText;
    public TMP_Text goalBonusText;

    [Header("Кнопки")]
    public Button upgradesButton;
    public Button nextDayButton;

    private bool moneyAdjustmentApplied;

    private void Start()
    {
        if (panel != null)
            panel.SetActive(false);

        if (nextDayButton != null)
            nextDayButton.onClick.AddListener(StartNextDay);
    }

    public void ShowPanel()
    {
        if (panel != null)
            panel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        DayResultManager results = DayResultManager.Instance;

        if (results == null)
            return;

        int penalties = results.GetPenalty();
        int dayIncome = results.earnedMoney;
        float rating = results.GetRating();

        int goalBonus = 0;

        if (DayGoalManager.Instance != null)
            goalBonus = DayGoalManager.Instance.GetGoalBonus();

        int finalDayResult = dayIncome - penalties + goalBonus;
        int moneyAdjustment = goalBonus - penalties;

        if (!moneyAdjustmentApplied)
        {
            moneyAdjustmentApplied = true;

            if (MoneyManager.Instance != null)
                MoneyManager.Instance.AddMoney(moneyAdjustment);
        }

        if (titleText != null)
            titleText.text = "Рабочий день завершён";

        if (completedOrdersText != null)
            completedOrdersText.text = "Выдано заказов: " + results.completedOrders;

        if (lostCustomersText != null)
            lostCustomersText.text = "Клиентов ушло: " + results.lostCustomers;

        if (mistakesText != null)
            mistakesText.text = "Ошибок выдачи: " + results.mistakes;

        if (earnedMoneyText != null)
            earnedMoneyText.text = "Доход: " + dayIncome + " ₽";

        if (penaltiesText != null)
            penaltiesText.text = "Штрафы: -" + penalties + " ₽";

        if (totalMoneyText != null)
            totalMoneyText.text = "Итог за день: " + finalDayResult + " ₽";

        if (ratingText != null)
            ratingText.text = "Рейтинг ПВЗ: " + rating.ToString("0.0") + " ★";

        if (goalsText != null && DayGoalManager.Instance != null)
            goalsText.text = DayGoalManager.Instance.GetGoalsText();

        if (goalBonusText != null)
            goalBonusText.text = "Бонус за цели: +" + goalBonus + " ₽";
    }

    private void StartNextDay()
    {
        moneyAdjustmentApplied = false;

        if (panel != null)
            panel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (DayResultManager.Instance != null)
            DayResultManager.Instance.ResetResults();

        if (DayTimeManager.Instance != null)
            DayTimeManager.Instance.ResetTime();

        if (GameManager.Instance != null)
            GameManager.Instance.NextDay();

        if (DayGoalManager.Instance != null)
            DayGoalManager.Instance.SetupGoalsForDay();
    }
}