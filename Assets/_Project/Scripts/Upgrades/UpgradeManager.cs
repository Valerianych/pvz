using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;

    [Header("Игрок")]
    public PlayerMovement playerMovement;

    [Header("Визуальная прокачка")]
    public VisualUpgradeManager visualUpgradeManager;

    [Header("Уровни улучшений")]
    public int speedLevel;
    public int hallLevel;
    public int rackLevel;
    public int deskLevel;

    [Header("Реклама")]
    public bool adCampaignActive;

    private void Awake()
    {
        Instance = this;
    }

    public void BuySpeedUpgrade()
    {
        int price = 500 + speedLevel * 300;

        if (!MoneyManager.Instance.SpendMoney(price))
            return;

        speedLevel++;

        if (playerMovement != null)
            playerMovement.AddSpeed(0.5f);

        UIManager.Instance.ShowMessage("Скорость улучшена");
    }

    public void BuyHallUpgrade()
    {
        int price = 1500 + hallLevel * 1000;

        if (!MoneyManager.Instance.SpendMoney(price))
            return;

        hallLevel++;

        if (visualUpgradeManager != null)
            visualUpgradeManager.SetHallLevel(hallLevel);

        UIManager.Instance.ShowMessage("Зал улучшен");
    }

    public void BuyRackUpgrade()
    {
        int price = 800 + rackLevel * 600;

        if (!MoneyManager.Instance.SpendMoney(price))
            return;

        rackLevel++;

        if (visualUpgradeManager != null)
            visualUpgradeManager.SetRackLevel(rackLevel);

        UIManager.Instance.ShowMessage("Склад улучшен");
    }

    public void BuyDeskUpgrade()
    {
        int price = 1200 + deskLevel * 800;

        if (!MoneyManager.Instance.SpendMoney(price))
            return;

        deskLevel++;

        if (visualUpgradeManager != null)
            visualUpgradeManager.SetDeskLevel(deskLevel);

        UIManager.Instance.ShowMessage("Стойка улучшена");
    }

    public void BuyAdCampaign()
    {
        int price = 700;

        if (!MoneyManager.Instance.SpendMoney(price))
            return;

        adCampaignActive = true;
        UIManager.Instance.ShowMessage("Реклама куплена. Завтра клиентов будет больше");
    }

    public bool ConsumeAdCampaign()
    {
        if (!adCampaignActive)
            return false;

        adCampaignActive = false;
        return true;
    }
}