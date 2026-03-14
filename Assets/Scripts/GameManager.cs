using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Singleton;

    public TestRelay RelayManager;
    public TestLobby LobbyManager;

    void Awake()
    {
        if (Singleton != null) Destroy(this);
        Singleton = this;
    }
}
