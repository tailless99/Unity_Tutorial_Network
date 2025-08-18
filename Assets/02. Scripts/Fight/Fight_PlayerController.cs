using Photon.Pun;
using System.Collections;
using TMPro;
using UnityEngine;

public class Fight_PlayerController : MonoBehaviourPun {
    [SerializeField] private TextMeshPro nickName;

    [SerializeField] private GameObject punchBox;
    [SerializeField] private GameObject kickBox;


    private Animator anim;


    private void Start() {
        anim = GetComponent<Animator>();

        //if (photonView.IsMine) {
        //    nickName.text = PhotonNetwork.NickName;
        //    nickName.color = Color.green;
        //}
        //else {
        //    nickName.text = photonView.Owner.NickName;
        //    nickName.color = Color.red;
        //}
    }

    void OnPunch() {
        StartCoroutine(PunchRoutine());
    }

    IEnumerator PunchRoutine() {
        anim.SetTrigger("Punch");

        yield return new WaitForSeconds(.5f);
        punchBox.SetActive(true);

        yield return new WaitForSeconds(.5f);
        punchBox.SetActive(false);
    }

    void OnKick() {
        StartCoroutine(KickRoutine());
    }

    IEnumerator KickRoutine() {
        anim.SetTrigger("Kick");

        yield return new WaitForSeconds(.6f);
        kickBox.SetActive(true);

        yield return new WaitForSeconds(.2f);
        kickBox.SetActive(false);
    }
}
