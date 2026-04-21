using UnityEngine;

[RequireComponent(typeof(OrderBox))]
public class PickupBox : MonoBehaviour, IInteractable
{
    private OrderBox orderBox;

    private void Awake()
    {
        orderBox = GetComponent<OrderBox>();
    }

    public void Interact(PlayerInteractor player)
    {
        if (player.HasBox())
        {
            UIManager.Instance.ShowMessage("Руки заняты");
            return;
        }

        player.TakeBox(orderBox);
        UIManager.Instance.ShowMessage("Посылка взята: " + orderBox.orderNumber);
    }

    public string GetInteractText(PlayerInteractor player)
    {
        if (player.HasBox())
            return "Руки заняты";

        return "E - взять посылку " + orderBox.orderNumber;
    }
}