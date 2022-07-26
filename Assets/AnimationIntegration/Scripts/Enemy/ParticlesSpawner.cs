using UnityEngine;

public class ParticlesSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform _parent;

    [SerializeField]
    private ParticleSystem _enterParticles;
    [SerializeField]
    private ParticleSystem _exitParticles;

    public void SpawnEnterPartickles(HitInfo hit)
    {
        SpawnParts(hit, _exitParticles);
    }

    public void SpawnExitPartickles(HitInfo hit)
    {
        SpawnParts(hit, _enterParticles);
    }

    private void SpawnParts(HitInfo hit, ParticleSystem particles)
    {
        var parts = Instantiate(particles, _parent, true);

        parts.transform.SetPositionAndRotation(
            hit.position,
            Quaternion.LookRotation(hit.attackDirection)
        );
        parts.Play();
    }
}
