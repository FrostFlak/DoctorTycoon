using UnityEngine;
using UnityEngine.AI;

public abstract class CharacterMovment : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] protected NavMeshAgent Agent;
    [SerializeField] protected float LookRotationSpeed;
    public bool IsWalking { get; set; }

    public virtual void Initialize()
    {
        AssignInputs();
    }

    public virtual void AssignInputs() { }

    public virtual void TargetFace() { }
    public virtual void TargetFace(NavMeshAgent agent) { }

    public virtual void Move(NavMeshAgent agent) { }
}
