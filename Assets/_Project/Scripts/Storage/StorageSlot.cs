using UnityEngine;

public class StorageSlot : MonoBehaviour, IInteractable
{
    [Header("Точка, куда кладётся посылка")]
    public Transform boxPoint;

    [Header("Посылка на полке")]
    public OrderBox storedBox;

    public bool IsEmpty()
    {
        return storedBox == null;
    }

    public void Interact(PlayerInteractor player)
    {
        if (IsEmpty())
            PutBox(player);
        else
            TakeBox(player);
    }

    private void PutBox(PlayerInteractor player)
    {
        if (!player.HasBox())
        {
            UIManager.Instance.ShowMessage("В руках нет посылки");
            return;
        }

        OrderBox box = player.RemoveBoxFromHands();

        storedBox = box;
        storedBox.SetOnStorage(boxPoint);

        StorageSystem.Instance.RegisterBox(storedBox);

        UIManager.Instance.ShowMessage("Посылка положена на полку");
    }

    private void TakeBox(PlayerInteractor player)
    {
        if (player.HasBox())
        {
            UIManager.Instance.ShowMessage("Руки заняты");
            return;
        }

        StorageSystem.Instance.UnregisterBox(storedBox);

        OrderBox box = storedBox;
        storedBox = null;

        player.TakeBox(box);

        UIManager.Instance.ShowMessage("Посылка взята");
    }

    public string GetInteractText(PlayerInteractor player)
    {
        if (IsEmpty())
        {
            if (player.HasBox())
                return "E - положить посылку";

            return "Пустая полка";
        }

        if (player.HasBox())
            return "Руки заняты";

        return "E - взять посылку " + storedBox.orderNumber;
    }
}