using People;
using UnityEngine;

public class QuitHospitalPlace : MonoBehaviour
{
    [SerializeField] private HumansManager _humansManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Human human))
        {
            human.LeftHospital = true;
            _humansManager.Pool.Release(human);
            _humansManager.StartCoroutine(_humansManager.SpawnCoroutine());
        }
    }
}
