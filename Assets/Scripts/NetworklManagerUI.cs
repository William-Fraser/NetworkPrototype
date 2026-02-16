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
    [SerializeField] private TextMeshProUGUI joinCodeTxt;

    public delegate void SetCodeAsync(string code);
    public SetCodeAsync JoinCodeAsync;

    private void Awake()
    {
        JoinCodeAsync = SetJoinCode;

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
        genJoinCodeBtn.onClick.AddListener(() =>
        {
            TestRelay.Singleton.CreateRelay(JoinCodeAsync);
        });
    }

    private void SetJoinCode(string joinCode)
    {
        joinCodeTxt.SetText(joinCode);
    }
}
