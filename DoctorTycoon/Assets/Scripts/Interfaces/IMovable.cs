
using UnityEngine.AI;

public interface IMovable {
    void AssignInputs();
    void TargetFace();
    void Move(NavMeshAgent agent);
}
