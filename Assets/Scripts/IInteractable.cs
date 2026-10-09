public interface IInteractable
{
    bool ShowPrompt { get; }
    void Interact();
    void OnPlayerExit();
}