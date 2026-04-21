using UnityEngine;
using TMPro;

public class OrderBox : MonoBehaviour
{
    [Header("Номер заказа")]
    public string orderNumber = "A-100";

    private Rigidbody rb;
    private Collider[] colliders;
    private PickupBox pickupBox;
    private TMP_Text[] labels;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
        pickupBox = GetComponent<PickupBox>();
        labels = GetComponentsInChildren<TMP_Text>();

        UpdateLabels();
    }

    public void SetOrderNumber(string newNumber)
    {
        orderNumber = newNumber;
        UpdateLabels();
    }

    private void UpdateLabels()
    {
        foreach (TMP_Text label in labels)
        {
            label.text = orderNumber;
        }
    }

    public void SetInHands(Transform handPoint)
    {
        transform.SetParent(handPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        if (rb != null)
            rb.isKinematic = true;

        SetColliders(false);
        SetPickupEnabled(false);
    }

    public void SetOnStorage(Transform storagePoint)
    {
        transform.SetParent(storagePoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        if (rb != null)
            rb.isKinematic = true;

        SetColliders(true);
        SetPickupEnabled(false);
    }

    public void SetLoose()
    {
        transform.SetParent(null);

        if (rb != null)
            rb.isKinematic = false;

        SetColliders(true);
        SetPickupEnabled(true);
    }

    private void SetColliders(bool value)
    {
        foreach (Collider boxCollider in colliders)
        {
            boxCollider.enabled = value;
        }
    }

    private void SetPickupEnabled(bool value)
    {
        if (pickupBox != null)
            pickupBox.enabled = value;
    }
}