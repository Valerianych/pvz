using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;

    [Header("Деньги игрока")]
    public int money;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UIManager.Instance.SetMoney(money);
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UIManager.Instance.SetMoney(money);
    }

    public bool SpendMoney(int amount)
    {
        if (money < amount)
        {
            UIManager.Instance.ShowMessage("Недостаточно денег");
            return false;
        }

        money -= amount;
        UIManager.Instance.SetMoney(money);
        return true;
    }
}