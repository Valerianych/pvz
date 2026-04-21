using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [Header("Камера игрока")]
    public Camera playerCamera;

    [Header("Взаимодействие")]
    public float interactDistance = 3f;
    public LayerMask interactLayer;

    [Header("Точка в руках")]
    public Transform handPoint;

    private OrderBox currentBox;
    private IInteractable currentInteractable;

    private void Update()
    {
        CheckInteractable();

        if (Input.GetKeyDown(KeyCode.E))
            TryInteract();
    }

    private void CheckInteractable()
    {
        currentInteractable = null;

        if (playerCamera == null)
            return;

        int mask = interactLayer.value == 0 ? Physics.DefaultRaycastLayers : interactLayer.value;

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, interactDistance, mask))
        {
            currentInteractable = FindInteractable(hit.collider);

            if (currentInteractable != null)
            {
                UIManager.Instance.ShowHint(currentInteractable.GetInteractText(this));
                return;
            }
        }

        UIManager.Instance.HideHint();
    }

    private IInteractable FindInteractable(Collider targetCollider)
    {
        MonoBehaviour[] behaviours = targetCollider.GetComponentsInParent<MonoBehaviour>();

        foreach (MonoBehaviour behaviour in behaviours)
        {
            if (behaviour is IInteractable interactable && behaviour.enabled)
                return interactable;
        }

        return null;
    }

    private void TryInteract()
    {
        if (currentInteractable != null)
            currentInteractable.Interact(this);
    }

    public bool HasBox()
    {
        return currentBox != null;
    }

    public OrderBox GetCurrentBox()
    {
        return currentBox;
    }

    public void TakeBox(OrderBox box)
    {
        if (box == null)
            return;

        currentBox = box;
        currentBox.SetInHands(handPoint);
    }

    public OrderBox RemoveBoxFromHands()
    {
        OrderBox box = currentBox;
        currentBox = null;
        return box;
    }
}