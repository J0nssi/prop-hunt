using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
using Steamworks;

public class CustomNetworkManager : NetworkManager
{
    [SerializeField] private PlayerObjectController GamePlayerPrefab;
    public List<PlayerObjectController> GamePlayers { get; } = new List<PlayerObjectController>();

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        if(SceneManager.GetActiveScene().name == "Lobby")
        {
            PlayerObjectController GamePlayerInstance = Instantiate(GamePlayerPrefab);
            GamePlayerInstance.ConnectionID = conn.connectionId;
            GamePlayerInstance.PlayerIdNumber = GamePlayers.Count + 1;
            GamePlayerInstance.PlayerSteamID = (ulong)SteamMatchmaking.GetLobbyMemberByIndex((CSteamID)SteamLobby.Instance.CurrentLobbyID, GamePlayers.Count);

            NetworkServer.AddPlayerForConnection(conn, GamePlayerInstance.gameObject);
        }
    }

    public void StartGame(string SceneName)
    {
        AssignRoles();
        ServerChangeScene(SceneName);
    }
    private void AssignRoles()
    {

    if (GamePlayers.Count == 0) return;


        // Randomly pick one player to be the Hunter
        int hunterIndex = Random.Range(0, GamePlayers.Count);
        Debug.Log($"Number of players: {GamePlayers.Count}");
        Debug.Log($"Selected Hunter Index: {hunterIndex}");

        for (int i = 0; i < GamePlayers.Count; i++)
       {
         if (i == hunterIndex)
         {
            GamePlayers[i].AssignRole(PlayerObjectController.PlayerRole.Prop);
         }
         else
         {
            GamePlayers[i].AssignRole(PlayerObjectController.PlayerRole.Prop);
         }
       }
    }
}
