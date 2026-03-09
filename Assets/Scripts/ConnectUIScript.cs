using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using System;

public class ConnectUIScript : MonoBehaviour {
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start() {
        hostButton.onClick.AddListener(HostButtonOnClick);
        clientButton.onClick.AddListener(ClientButtonOnClick);
    }

    private void HostButtonOnClick() { // If host button is clicked, try to host game
        NetworkManager.Singleton.StartHost();
    }

    private void ClientButtonOnClick() { // If client button is clicked, try to join game
        NetworkManager.Singleton.StartClient();
    }
}
