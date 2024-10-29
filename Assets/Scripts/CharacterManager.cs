using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    static private CharacterManager _instance;

    private Player _player;

    static public CharacterManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("CharacterManager").AddComponent<CharacterManager>();
            }
            return _instance;
        }
    }

    public Player Player
    {
        get { return _player; }
        set { _player = value; }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if(_instance !=this)
            {
                Destroy(gameObject);
            }
        }
    }
}
