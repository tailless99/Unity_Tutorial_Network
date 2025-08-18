using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1";

    [SerializeField] private TMP_InputField nickNameField;
    [SerializeField] private Button connectButton;

    private void Awake() {
        Screen.SetResolution(1920, 1080, false);     // �ػ� ����
        PhotonNetwork.SendRate = 60;                // �� ��ǻ�� ���� ������ ���� ���۷�
        PhotonNetwork.SerializationRate = 30;       // Photon View ���� ���� ��� ���� ���۷�
        PhotonNetwork.GameVersion = gameVersion;    // ���� ����
    }

    private void Start() {
        connectButton.onClick.AddListener(Connect);
    }

    private void Connect() {
        PhotonNetwork.NickName = nickNameField.text;
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("���� ����");
    }

    public override void OnConnectedToMaster() {
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 20 }, null);
        Debug.Log("���� ���� �Ϸ�");
    }

    public override void OnJoinedRoom() {
        PhotonNetwork.LoadLevel(1);
        Debug.Log("�� ����");
    }
}
