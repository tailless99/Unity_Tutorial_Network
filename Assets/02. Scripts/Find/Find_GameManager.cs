using Photon.Pun;
using System.Collections;
using TMPro;
using UnityEngine;

public class Find_GameManager : Singleton<Find_GameManager> {
    [SerializeField] private GameObject observerCamera;
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private TextMeshProUGUI winnerUI;

    private int score = 0;
    [SerializeField] private int winnerScore = 2;

    IEnumerator Start() {
        float randomTime = Random.Range(0f, 1f);
        yield return new WaitForSeconds(1f);

        int ranIndex = Random.Range(1, 6);

        var randomPos = new Vector3(Random.Range(-5, 5f), 0, Random.Range(-5, 5f));
        PhotonNetwork.Instantiate("Prefab/FindPlayer/Player_" + ranIndex, randomPos, Quaternion.identity);

        scoreUI.text = $"현재 점수는 0점 입니다.";
    }

    public void SetObserver() {
        observerCamera.SetActive(true);
    }

    public bool SetScore() {
        score++;
        scoreUI.text = $"현재 점수는 {score}점 입니다.";

        if (score >= winnerScore) {
            return true;
        }

        Debug.Log("점수 획득");
        return false;
    }

    public void EndGame(string nickName) {
        Fade.onFadeAction(3f, Color.white, true, () => {
            winnerUI.text = $"Winer is {nickName}!";
            winnerUI.gameObject.SetActive(true);
        });
    }
}
