using Photon.Pun;
using UnityEngine;

public class HitBoxEvent : MonoBehaviour
{
    private PhotonView myPv;

    private void Awake() {
        myPv = transform.root.GetComponent<PhotonView>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Npc")) {
            other.GetComponent<AgentController>().GetHit();
        }
        else if (other.CompareTag("Player")) {
            other.GetComponent<Find_PlayerController>().GetHit();

            if (myPv.IsMine) {
                var isWinner = Find_GameManager.Instance.SetScore();
                string nickName = myPv.Owner.NickName;

                if (isWinner) {
                    myPv.RPC("Winner", RpcTarget.AllBuffered, nickName);
                }
            }
        }
    }
}
