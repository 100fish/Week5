using UnityEngine;

public abstract class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] private DialougeText[] dialougeText;
    private int dialougeIndex_count = 0;

    private const float INTERACT_DISTANCE = 2f;

    PlayerInput input;

    bool added_to_interactable = false;

    public void Start()
    {
        input = Gamemanager.Get_Player().Get_PlayerInput();
    }

    public void Update()
    {
        if (input.Standard.Interact.WasPressedThisFrame() && IsWithinInteractDistance())
        {
            
            Interact();
        }

        if (IsWithinInteractDistance() && !added_to_interactable)
        {
            DialougeController.Instance.StartInteractText();
            print("Interacting! 1");
            added_to_interactable = true;
        }
        else if (!IsWithinInteractDistance() && added_to_interactable)
        {
            if (DialougeController.Instance.IsInTextRightNow())
            {
                dialougeIndex_count--;
                DialougeController.Instance.EndConversation();
            }

                DialougeController.Instance.FinishInteractText();
            print("Interacting! 2");
            added_to_interactable = false;
        }
    }

    public abstract void Interact();

    private bool IsWithinInteractDistance()
    {
        float _interact_distance = INTERACT_DISTANCE;
        if (added_to_interactable) _interact_distance *= 1.1f;

        if (Vector2.Distance(Gamemanager.Get_Player().transform.position, transform.position) < _interact_distance) return true;
        return false;
    }

    public DialougeText Get_Current_DialougeText()
    {
        if (dialougeIndex_count >= dialougeText.Length) dialougeIndex_count = dialougeText.Length - 1;

        DialougeText _dialougeText = dialougeText[dialougeIndex_count];
        if(!DialougeController.Instance.IsInTextRightNow()) dialougeIndex_count++;
        return _dialougeText;
    }

    public void Talk(string name, DialougeText dialougeText)
    {
        //start convo
        DialougeController.Instance.DisplayNextParagraph(name, dialougeText);
    }
}
