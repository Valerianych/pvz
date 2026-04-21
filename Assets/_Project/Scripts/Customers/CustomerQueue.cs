using System.Collections.Generic;
using UnityEngine;

public class CustomerQueue : MonoBehaviour
{
    [Header("Точки очереди")]
    public Transform[] queuePoints;

    private readonly List<Customer> customers = new List<Customer>();

    public bool HasFreePlace()
    {
        return customers.Count < queuePoints.Length;
    }

    public void AddCustomer(Customer customer)
    {
        if (customer == null)
            return;

        if (!HasFreePlace())
        {
            Destroy(customer.gameObject);
            return;
        }

        customers.Add(customer);
        RefreshPositions();
    }

    public void RemoveCustomer(Customer customer)
    {
        customers.Remove(customer);
        RefreshPositions();
    }

    public Customer GetFirstCustomer()
    {
        if (customers.Count == 0)
            return null;

        return customers[0];
    }

    private void RefreshPositions()
    {
        for (int i = 0; i < customers.Count; i++)
        {
            if (customers[i] == null)
                continue;

            customers[i].transform.position = queuePoints[i].position;
            customers[i].transform.rotation = queuePoints[i].rotation;
        }
    }
}