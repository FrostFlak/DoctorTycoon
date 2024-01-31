using UnityEngine;
using UnityEngine.AI;
namespace People
{
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
        [SerializeField] private bool _leftQueue;
        [SerializeField] private HumanType _humanType;
        [SerializeField] private Animator _animator;
        private const string _layAnimation = "Lay";

        public bool LeftQueue { get { return _leftQueue; } set { _leftQueue = value; } }
        public HumanType HumanType { get { return _humanType; } private set { _humanType = value; } }

        public void StartLayAnimation()
        {
            _isLaying = true;
            //_animator.Play(_layAnimation);
        }

    }

}

