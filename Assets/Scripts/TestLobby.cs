using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class TestLobby : MonoBehaviour
{
    public static TestLobby Singleton;
    

    async void Start()
    {
        await UnityServices.InitializeAsync();

        //change auth to Steam or any other service
        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log($"Signed in {AuthenticationService.Instance.PlayerId}");
        };
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    public async void CreateLobby() 
    {
        try
        {
            string lobbyName = "TestLobby";
            int maxPlayers = 4;

            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers);

            Debug.Log($"Created Lobby {lobby.Name} {lobby.MaxPlayers}");
        }
        catch (LobbyServiceException e)
        { Debug.Log(e); }
    }

    public async void ListLobbies() {
        try
        {
            QueryResponse queryResponse = await LobbyService.Instance.QueryLobbiesAsync();

            Debug.Log($"Lobbies found: {queryResponse.Results.Count}");
            foreach (Lobby lobby in queryResponse.Results)
            {
                Debug.Log(lobby.Name + " " + lobby.MaxPlayers);
            }
        }
        catch (LobbyServiceException e) { Debug.Log(e); }
    }
}
