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

    public delegate void SetCodeREsync(string code);
    public SetCodeREsync JoinCodeREsync;

    private void Awake()
    {
        JoinCodeREsync = (string joinCode) => { genJoinCodeTxt.SetText(joinCode); };

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
            TestRelay.Singleton.CreateRelay(JoinCodeREsync);

            //next line will depricate in a few updates
            genJoinCodeBtn.gameObject.SetActive(false);
        });
        useJoinCodeBtn.onClick.AddListener(() =>
        {
            TestRelay.Singleton.JoinRelay(useJoinCodeTxt.text);

            //next line will depricate in a few updates
            //useJoinCodeBtn.gameObject.SetActive(false);
        });
    }
}
