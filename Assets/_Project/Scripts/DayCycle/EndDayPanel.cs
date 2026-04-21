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

    [Header("Кнопки")]
    public Button upgradesButton;
    public Button nextDayButton;

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
        int total = results.GetTotalMoneyForDay();
        float rating = results.GetRating();

        if (titleText != null)
            titleText.text = "Рабочий день завершён";

        if (completedOrdersText != null)
            completedOrdersText.text = "Выдано заказов: " + results.completedOrders;

        if (lostCustomersText != null)
            lostCustomersText.text = "Клиентов ушло: " + results.lostCustomers;

        if (mistakesText != null)
            mistakesText.text = "Ошибок выдачи: " + results.mistakes;

        if (earnedMoneyText != null)
            earnedMoneyText.text = "Доход: " + results.earnedMoney + " ₽";

        if (penaltiesText != null)
            penaltiesText.text = "Штрафы: -" + penalties + " ₽";

        if (totalMoneyText != null)
            totalMoneyText.text = "Итог за день: " + total + " ₽";

        if (ratingText != null)
            ratingText.text = "Рейтинг ПВЗ: " + rating.ToString("0.0") + " ★";
    }

    private void StartNextDay()
    {
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
    }
}