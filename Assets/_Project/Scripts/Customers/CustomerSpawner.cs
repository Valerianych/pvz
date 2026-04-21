using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [Header("Префаб клиента")]
    public GameObject customerPrefab;

    [Header("Очередь")]
    public CustomerQueue customerQueue;

    [Header("Точка появления")]
    public Transform spawnPoint;

    [Header("Интервалы появления")]
    public float normalSpawnInterval = 10f;
    public float rushSpawnInterval = 5f;

    private float timer;

    private void Start()
    {
        timer = normalSpawnInterval;
    }

    private void Update()
    {
        if (GameManager.Instance != null && !GameManager.Instance.isDayActive)
            return;

        if (customerQueue == null || !customerQueue.HasFreePlace())
            return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnCustomer();
            timer = GetSpawnInterval();
        }
    }

    private void SpawnCustomer()
    {
        if (customerPrefab == null || spawnPoint == null || customerQueue == null)
            return;

        GameObject customerObject = Instantiate(customerPrefab, spawnPoint.position, spawnPoint.rotation);
        Customer customer = customerObject.GetComponent<Customer>();

        if (customer == null)
        {
            Destroy(customerObject);
            return;
        }

        string orderNumber = GetOrderNumberForCustomer();

        customer.Setup(orderNumber, customerQueue);
        customerQueue.AddCustomer(customer);

        UIManager.Instance.ShowMessage("Пришёл клиент: " + orderNumber);
    }

    private string GetOrderNumberForCustomer()
    {
        if (StorageSystem.Instance != null && StorageSystem.Instance.HasAnyBoxes())
            return StorageSystem.Instance.GetRandomStoredOrderNumber();

        return OrderNumberGenerator.Generate();
    }

    private float GetSpawnInterval()
    {
        if (DayTimeManager.Instance != null && DayTimeManager.Instance.IsRushHour())
            return rushSpawnInterval;

        return normalSpawnInterval;
    }
}