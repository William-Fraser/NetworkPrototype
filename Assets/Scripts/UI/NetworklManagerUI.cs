using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NetworklManagerUI : MonoBehaviour
{
    //networking
    [SerializeField] private Button serverBtn;
    [SerializeField] private Button hostBtn;
    [SerializeField] private Button clientBtn;

    //relay
    [SerializeField] private Button genJoinCodeBtn;
    [SerializeField] private TextMeshProUGUI genJoinCodeTxt;
    [SerializeField] private Button useJoinCodeBtn;
    [SerializeField] private Text useJoinCodeTxt;

    //lobby
    [SerializeField] private Button createLobbyBtn;
    [SerializeField] private Button ListLobbiesBtn;

    public delegate void SetCodeREsync(string code);
    public SetCodeREsync JoinCodeREsync;

    private void Awake()
    {
        JoinCodeREsync = (string joinCode) => { genJoinCodeTxt.SetText(joinCode); };

        //basic networking
        serverBtn.onClick.AddListener(() => 
        {
            NetworkManager.Singleton.StartServer();
        });
        hostBtn.onClick.AddListener(() => 
        {
            NetworkManager.Singleton.StartHost();
        });
        clientBtn.onClick.AddListener(() => 
        {
            NetworkManager.Singleton.StartClient();
        });

        //relay
        genJoinCodeBtn.onClick.AddListener(() =>
        {
            GameManager.Singleton.RelayManager.CreateRelay(JoinCodeREsync);

            //next line will depricate in a few updates
            genJoinCodeBtn.gameObject.SetActive(false);
        });
        useJoinCodeBtn.onClick.AddListener(() =>
        {
            GameManager.Singleton.RelayManager.JoinRelay(useJoinCodeTxt.text);

            //next line will depricate in a few updates
            //useJoinCodeBtn.gameObject.SetActive(false);
        });

        //lobby
        createLobbyBtn.onClick.AddListener(() =>
        {
            GameManager.Singleton.LobbyManager.CreateLobby();
        });
        ListLobbiesBtn.onClick.AddListener(() =>
        {
            GameManager.Singleton.LobbyManager.ListLobbies();
        });
    }
}
