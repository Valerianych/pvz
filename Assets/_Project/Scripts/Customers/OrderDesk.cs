using UnityEngine;

public class OrderDesk : MonoBehaviour, IInteractable
{
    [Header("Очередь клиентов")]
    public CustomerQueue customerQueue;

    [Header("Награда за заказ")]
    public int rewardPerOrder = 100;

    public void Interact(PlayerInteractor player)
    {
        if (customerQueue == null)
            return;

        Customer customer = customerQueue.GetFirstCustomer();

        if (customer == null)
        {
            UIManager.Instance.ShowMessage("Клиентов нет");
            return;
        }

        if (!player.HasBox())
        {
            UIManager.Instance.ShowMessage("Нужна посылка: " + customer.orderNumber);
            return;
        }

        OrderBox box = player.GetCurrentBox();

        if (box.orderNumber != customer.orderNumber)
        {
            DayResultManager.Instance.AddMistake();

            UIManager.Instance.ShowMessage("Не тот заказ. Нужен: " + customer.orderNumber);
            return;
        }

        player.RemoveBoxFromHands();
        Destroy(box.gameObject);

        MoneyManager.Instance.AddMoney(rewardPerOrder);
        DayResultManager.Instance.AddCompletedOrder(rewardPerOrder);

        customer.CompleteOrder();

        UIManager.Instance.ShowMessage("Заказ выдан: +" + rewardPerOrder + " ₽");
    }

    public string GetInteractText(PlayerInteractor player)
    {
        if (customerQueue == null)
            return "Очередь не настроена";

        Customer customer = customerQueue.GetFirstCustomer();

        if (customer == null)
            return "Клиентов нет";

        return "E - выдать заказ " + customer.orderNumber;
    }
}