using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Simple_NetworkManager : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1";

    private void Awake() {
        Screen.SetResolution(1920,1080, false);     // �ػ� ����
        PhotonNetwork.SendRate = 60;                // �� ��ǻ�� ���� ������ ���� ���۷�
        PhotonNetwork.SerializationRate = 30;       // Photon View ���� ���� ��� ���� ���۷�
        PhotonNetwork.GameVersion = gameVersion;    // ���� ����
    }

    private void Start() {
        Connect();
    }

    private void Connect() {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("���� ����");
    }

    public override void OnConnectedToMaster() {
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 20 }, null);
        Debug.Log("���� ���� �Ϸ�");
    }

    public override void OnJoinedRoom() {
        base.OnJoinedRoom();
        Debug.Log("�� ����");
        PhotonNetwork.Instantiate("Player", Vector3.zero + Vector3.up, Quaternion.identity); // Resource ������ �ִ� Player ��� �̸��� ������Ʈ ����
    }
}
