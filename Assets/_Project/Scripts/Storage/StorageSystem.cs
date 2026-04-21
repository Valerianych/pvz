using System.Collections.Generic;
using UnityEngine;

public class StorageSystem : MonoBehaviour
{
    public static StorageSystem Instance;

    private Dictionary<string, OrderBox> boxes = new Dictionary<string, OrderBox>();

    private void Awake()
    {
        Instance = this;
    }

    public void RegisterBox(OrderBox box)
    {
        if (box == null)
            return;

        if (!boxes.ContainsKey(box.orderNumber))
            boxes.Add(box.orderNumber, box);
    }

    public void UnregisterBox(OrderBox box)
    {
        if (box == null)
            return;

        if (boxes.ContainsKey(box.orderNumber))
            boxes.Remove(box.orderNumber);
    }

    public bool HasOrder(string orderNumber)
    {
        return boxes.ContainsKey(orderNumber);
    }

    public OrderBox GetBox(string orderNumber)
    {
        if (boxes.TryGetValue(orderNumber, out OrderBox box))
            return box;

        return null;
    }
    public bool HasAnyBoxes()
    {
        return boxes.Count > 0;
    }

    public string GetRandomStoredOrderNumber()
    {
        if (boxes.Count == 0)
            return "";

        int index = Random.Range(0, boxes.Count);
        int currentIndex = 0;

        foreach (string orderNumber in boxes.Keys)
        {
            if (currentIndex == index)
                return orderNumber;

            currentIndex++;
        }

        return "";
    }
}