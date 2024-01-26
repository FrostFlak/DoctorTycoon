using UnityEngine;
using UnityEngine.AI;

public enum HumanType
{
    WomanDrees = 0,
    ManCasual = 1,
}

[RequireComponent(typeof(CapsuleCollider) , typeof(Rigidbody) , typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator) , typeof(HumanAnimationController) , typeof(HumanPathFinder))]
public abstract class Human : MonoBehaviour
{
    [SerializeField] private bool _isLaying;
    [SerializeField] private HumanType _humanType;
    public HumanType HumanType { get { return _humanType; } private set { _humanType = value; } }

    private void Start()
    {
        
    }
}
