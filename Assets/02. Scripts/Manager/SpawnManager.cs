using Photon.Pun;
using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] npcPrefabs;
    [SerializeField] private int npcAmount = 10;

    IEnumerator Start() {
        yield return null;

        if(PhotonNetwork.IsMasterClient){
            for (int i = 0; i < npcAmount; i++) {
                int ranIndex = Random.Range(0, npcPrefabs.Length);
                Vector3 ranPos = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));

                GameObject npc = PhotonNetwork.Instantiate("Prefab/NPC/NPC_" + (ranIndex + 1), ranPos, Quaternion.identity);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
