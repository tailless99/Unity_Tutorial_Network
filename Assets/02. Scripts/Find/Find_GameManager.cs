using Photon.Pun;
using System.Collections;
using UnityEngine;

public class Find_GameManager : Singleton<Find_GameManager>
{
    IEnumerator Start() {
        float randomTime = Random.Range(0f, 1f);
        yield return new WaitForSeconds(1f);

        int ranIndex = Random.Range(1, 6);

        var randomPos = new Vector3(Random.Range(-5, 5f), 0, Random.Range(-5, 5f));
        PhotonNetwork.Instantiate("Prefab/Player_" + ranIndex, randomPos, Quaternion.identity);
    }
}
