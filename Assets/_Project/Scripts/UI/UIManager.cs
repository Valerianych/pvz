using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Подсказка взаимодействия")]
    [SerializeField] private TMP_Text hintText;

    [Header("Сообщение")]
    [SerializeField] private TMP_Text messageText;

    [Header("Деньги")]
    [SerializeField] private TMP_Text moneyText;

    [Header("Время")]
    [SerializeField] private TMP_Text timeText;

    private float messageTimer;

    private void Awake()
    {
        Instance = this;

        HideHint();

        if (messageText != null)
            messageText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (messageTimer > 0f)
        {
            messageTimer -= Time.deltaTime;

            if (messageTimer <= 0f && messageText != null)
                messageText.gameObject.SetActive(false);
        }
    }

    public void ShowHint(string text)
    {
        if (hintText == null)
            return;

        hintText.gameObject.SetActive(true);
        hintText.text = text;
    }

    public void HideHint()
    {
        if (hintText == null)
            return;

        hintText.gameObject.SetActive(false);
    }

    public void ShowMessage(string text, float duration = 2f)
    {
        if (messageText == null)
            return;

        messageText.gameObject.SetActive(true);
        messageText.text = text;
        messageTimer = duration;
    }

    public void SetMoney(int money)
    {
        if (moneyText != null)
            moneyText.text = money + " ₽";
    }

    public void SetTime(string time)
    {
        if (timeText != null)
            timeText.text = time;
    }
}