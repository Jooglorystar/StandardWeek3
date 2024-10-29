using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIOption : MonoBehaviour
{
    public void OnOption(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && !gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
            CharacterManager.Instance.Player.controller.canLook = false;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void CloseOption()
    {
        gameObject.SetActive(false);
        CharacterManager.Instance.Player.controller.canLook = true;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
