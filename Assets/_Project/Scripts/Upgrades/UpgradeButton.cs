using UnityEngine;
using UnityEngine.UI;

public enum UpgradeType
{
    Speed,
    Hall,
    Rack,
    Desk,
    AdCampaign
}

public class UpgradeButton : MonoBehaviour
{
    public UpgradeType upgradeType;
    public Button button;

    private void Awake()
    {
        if (button == null)
            button = GetComponent<Button>();
    }

    private void Start()
    {
        if (button != null)
            button.onClick.AddListener(BuyUpgrade);
    }

    private void BuyUpgrade()
    {
        if (UpgradeManager.Instance == null)
            return;

        switch (upgradeType)
        {
            case UpgradeType.Speed:
                UpgradeManager.Instance.BuySpeedUpgrade();
                break;

            case UpgradeType.Hall:
                UpgradeManager.Instance.BuyHallUpgrade();
                break;

            case UpgradeType.Rack:
                UpgradeManager.Instance.BuyRackUpgrade();
                break;

            case UpgradeType.Desk:
                UpgradeManager.Instance.BuyDeskUpgrade();
                break;

            case UpgradeType.AdCampaign:
                UpgradeManager.Instance.BuyAdCampaign();
                break;
        }
    }
}