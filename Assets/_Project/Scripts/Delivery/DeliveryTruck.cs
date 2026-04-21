using System.Collections.Generic;
using UnityEngine;

public class DeliveryTruck : MonoBehaviour, IInteractable
{
    [Header("Префаб посылки")]
    public GameObject boxPrefab;

    [Header("Точка появления посылки")]
    public Transform boxSpawnPoint;

    [Header("Состояние машины")]
    public bool isArrived;

    private readonly Queue<string> orderNumbers = new Queue<string>();

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Arrive(List<string> newOrders)
    {
        orderNumbers.Clear();

        foreach (string orderNumber in newOrders)
            orderNumbers.Enqueue(orderNumber);

        isArrived = true;
        gameObject.SetActive(true);

        UIManager.Instance.ShowMessage("Приехала доставка: " + orderNumbers.Count + " посылок");
    }

    public void Interact(PlayerInteractor player)
    {
        if (!isArrived)
            return;

        if (player.HasBox())
        {
            UIManager.Instance.ShowMessage("Сначала положи посылку на склад");
            return;
        }

        if (orderNumbers.Count <= 0)
        {
            UIManager.Instance.ShowMessage("Машина пустая");
            Leave();
            return;
        }

        string orderNumber = orderNumbers.Dequeue();

        GameObject boxObject = Instantiate(boxPrefab, boxSpawnPoint.position, Quaternion.identity);
        OrderBox box = boxObject.GetComponent<OrderBox>();

        if (box != null)
            box.SetOrderNumber(orderNumber);

        player.TakeBox(box);

        UIManager.Instance.ShowMessage("Посылка взята из машины: " + orderNumber);

        if (orderNumbers.Count <= 0)
            UIManager.Instance.ShowMessage("Все посылки разгружены");
    }

    public string GetInteractText(PlayerInteractor player)
    {
        if (!isArrived)
            return "";

        if (player.HasBox())
            return "Сначала положи посылку на склад";

        if (orderNumbers.Count <= 0)
            return "E - закрыть пустую машину";

        return "E - взять посылку из машины (" + orderNumbers.Count + ")";
    }

    private void Leave()
    {
        isArrived = false;
        gameObject.SetActive(false);
    }
}