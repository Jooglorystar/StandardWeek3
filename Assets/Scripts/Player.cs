using UnityEngine;

public class Player : MonoBehaviour
{
    private void Start()
    {
        CharacterManager.Instance.Player = this;
    }
}
