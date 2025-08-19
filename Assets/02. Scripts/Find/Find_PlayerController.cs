using Photon.Pun;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class Find_PlayerController : MonoBehaviourPun
{
    [SerializeField] private Transform playerRoot;

    private Animator anim;
    [SerializeField] private GameObject punchBox;
    [SerializeField] private GameObject kickBox;

    void Awake() {
        anim = GetComponent<Animator>();
    }

    private void Start() {
        if (photonView.IsMine) {
            var followCamera = FindFirstObjectByType<CinemachineCamera>();
            followCamera.Target.TrackingTarget = playerRoot;
        }
        else {
            GetComponent<PlayerInput>().enabled = false;
        }
    }

    void OnPunch() {
        photonView.RPC(nameof(RPC_Punch), RpcTarget.All);
    }

    [PunRPC]
    private void RPC_Punch() {
        StartCoroutine(PunchRoutine());
    }

    IEnumerator PunchRoutine() {
        anim.SetTrigger("Punch");
        yield return new WaitForSeconds(0.5f);
        punchBox.SetActive(true);

        yield return new WaitForSeconds(0.3f);
        punchBox.SetActive(false);
    }

    void OnKick() {
        photonView.RPC(nameof(RPC_Kick), RpcTarget.All);
    }

    [PunRPC]
    private void RPC_Kick() {
        StartCoroutine(KickRoutine());
    }

    IEnumerator KickRoutine() {
        anim.SetTrigger("Kick");
        yield return new WaitForSeconds(0.6f);
        kickBox.SetActive(true);

        yield return new WaitForSeconds(0.2f);
        kickBox.SetActive(false);
    }
}
