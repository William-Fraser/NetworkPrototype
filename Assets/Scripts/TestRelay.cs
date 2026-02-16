using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;

public class TestRelay : MonoBehaviour
{
    public static TestRelay Singleton;

    private async void Start()
    {
        if (Singleton != null) Destroy(this);
        Singleton = this;

        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed in" + AuthenticationService.Instance.PlayerId);
        };
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        
    }

    public async void CreateRelay(NetworklManagerUI.SetCodeAsync SetJoinCode)
    {
        try
        {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(3);

            string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

            SetJoinCode(joinCode);
        }
        catch (RelayServiceException e) {
            Debug.Log(e);
        };
    }
}
