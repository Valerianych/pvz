using UnityEngine;
using TMPro;

public class Customer : MonoBehaviour
{
    [Header("Заказ клиента")]
    public string orderNumber;

    [Header("Ожидание")]
    public float maxWaitTime = 60f;
    public float currentWaitTime;

    [Header("UI над клиентом")]
    public TMP_Text orderText;

    private CustomerQueue queue;
    private bool isWaiting = true;

    private void Start()
    {
        currentWaitTime = maxWaitTime;
        UpdateOrderText();
    }

    private void Update()
    {
        if (!isWaiting)
            return;

        currentWaitTime -= Time.deltaTime;

        if (currentWaitTime <= 0f)
        {
            LeaveAngry();
        }
    }

    public void Setup(string newOrderNumber, CustomerQueue customerQueue)
    {
        orderNumber = newOrderNumber;
        queue = customerQueue;
        currentWaitTime = maxWaitTime;

        UpdateOrderText();
    }

    private void UpdateOrderText()
    {
        if (orderText != null)
            orderText.text = orderNumber;
    }

    public void CompleteOrder()
    {
        isWaiting = false;

        if (queue != null)
            queue.RemoveCustomer(this);

        Destroy(gameObject);
    }

    private void LeaveAngry()
    {
        isWaiting = false;

        if (queue != null)
            queue.RemoveCustomer(this);

        DayResultManager.Instance.AddLostCustomer();

        UIManager.Instance.ShowMessage("Клиент ушёл: " + orderNumber);

        Destroy(gameObject);
    }
}