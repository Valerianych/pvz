using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    [Header("Машина доставки")]
    public DeliveryTruck truck;

    [Header("Настройки доставки")]
    public float firstDeliveryDelay = 10f;
    public float deliveryInterval = 120f;
    public int minBoxes = 5;
    public int maxBoxes = 8;

    private float timer;

    private void Start()
    {
        timer = firstDeliveryDelay;
    }

    private void Update()
    {
        if (GameManager.Instance != null && !GameManager.Instance.isDayActive)
            return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            StartDelivery();
            timer = deliveryInterval;
        }
    }

    private void StartDelivery()
    {
        if (truck == null)
            return;

        int count = Random.Range(minBoxes, maxBoxes + 1);
        List<string> orders = new List<string>();

        for (int i = 0; i < count; i++)
            orders.Add(OrderNumberGenerator.Generate());

        truck.Arrive(orders);
    }
}