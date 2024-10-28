using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerController controller;

    private void Start()
    {
        CharacterManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
    }
}
