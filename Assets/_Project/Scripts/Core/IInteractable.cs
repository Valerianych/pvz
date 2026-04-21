public interface IInteractable
{
    void Interact(PlayerInteractor player);
    string GetInteractText(PlayerInteractor player);
}