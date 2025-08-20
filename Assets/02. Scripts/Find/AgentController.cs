using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviourPun
{
    private NavMeshAgent agent;
    private Animator anim;

    [SerializeField] private float wanderRadius = 30f;

    private float minWaitTime = 1f;
    private float maxWaitTime = 5f;

    [SerializeField] private float turnSpeed = 10f;

    private bool isDead;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        agent.updateRotation = false;
    }

    void Start() {
        if (PhotonNetwork.IsMasterClient) {
            StartCoroutine(WanderRoutine());
        }

    }

    private void Update() {
        if (isDead) return;

        Vector3 dir = agent.desiredVelocity;

        if (dir != Vector3.zero) {
            Quaternion targetRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, turnSpeed * Time.deltaTime);
        }
    }

    IEnumerator WanderRoutine() {
        while (!isDead) {
            // ���� ������ ����
            var randomDir = Random.insideUnitSphere * wanderRadius;
            randomDir += transform.position;
            photonView.RPC("SetDestination", RpcTarget.AllBuffered, randomDir);

            var moveType = Random.Range(0, 2) == 0 ? .5f : 1;
            anim.SetFloat("Speed", moveType);
            agent.speed = moveType * 4f; // 2 or 4

            yield return new WaitUntil(() => !isDead && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance);

            anim.SetFloat("Speed", 0);
            float idleTime = Random.Range(minWaitTime, maxWaitTime);
            yield return new WaitForSeconds(idleTime);
        }
    }

    // ���� ������ ����
    [PunRPC]
    private void SetDestination(Vector3 randomDir) {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDir, out hit, wanderRadius, NavMesh.AllAreas)) {
            agent.SetDestination(hit.position);
        }
    }

    public void GetHit() {
        photonView.RPC("Dead", RpcTarget.AllBuffered);
    }

    [PunRPC]
    private void Dead() {
        if (!isDead) {
            isDead = true;
            StopAllCoroutines();
            agent.updatePosition = false;
            agent.updateRotation = false;
            agent.isStopped = true;
            GetComponent<Collider>().enabled = false;
            anim.SetTrigger("Death");
        }
    }
}
