using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawner : MonoBehaviour
{
    [SerializeField]
    private float _respawnTime;

    [SerializeField]
    private Vector2 _spawnSquareSize;
    [SerializeField]
    private float _distanceToGround;

    [SerializeField]
    private LayerMask _groundLayer;

    [SerializeField]
    private RagdollInitiator enemy;

    [SerializeField]
    private Color _gizmosColor = Color.red;

    private YieldInstruction _waitForRespawn;
    private void Awake()
    {
        _waitForRespawn = new WaitForSeconds(_respawnTime);
    }

    public void StartRespawnProcess()
    {
        Debug.Log("Respawn Started");
        StartCoroutine(RespawnProcess());
    }

    private IEnumerator RespawnProcess()
    {
        yield return _waitForRespawn;
        Respawn();
    }

    private void Respawn()
    {
        Debug.Log("Respawn");

        var pos = GetRandomPoint();
        var forward = Random.insideUnitCircle;
        var rotation = Quaternion.LookRotation(
            new Vector3(forward.x, 0, forward.y),
            Vector3.up
        );

        enemy.FinishRagdoll();
        enemy.transform.SetPositionAndRotation(pos, rotation);
    }

    private Vector3 GetRandomPoint()
    {
        var x = Random.Range(-_spawnSquareSize.x / 2, _spawnSquareSize.x / 2);
        var y = Random.Range(-_spawnSquareSize.y / 2, _spawnSquareSize.y / 2);
        var position = transform.position + transform.forward * transform.localScale.z * y + transform.right * transform.localScale.x * x;

        if (Physics.Raycast(position, -transform.up, out var hit, _distanceToGround, _groundLayer))
        {
            return hit.point;
        }

        return GetRandomPoint();
    }

    private void OnDrawGizmos()
    {
        var height = _distanceToGround / transform.localScale.y;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = _gizmosColor;
        Gizmos.DrawWireCube(
            Vector3.down * height / 2,
            new Vector3(_spawnSquareSize.x, height, _spawnSquareSize.y)
        );
    }
}
