using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    public abstract class CharacterMovment : MonoBehaviour
    {
        [Header("Properties")]
        [SerializeField] protected NavMeshAgent Agent;
        [SerializeField] protected float LookRotationSpeed;
        [SerializeField] protected CameraViewChanger CameraType;
        private float _defalultSpeed = 3.5f;
        private float _reducedSpeed = 1.5f;
        public bool IsWalking { get; set; }
        public float LookRotationSpeedProperty { get { return LookRotationSpeed; } set { LookRotationSpeed = value; } }

        private void Start()
        {
            EventsManager.Instance.OnStayInRegistrationTriggerZone += ReduceSpeed;
            EventsManager.Instance.OnStayInBedTriggerZone += ReduceSpeed;
            EventsManager.Instance.OnExitRegistartionTriggerZone += UnReduceSpeed;
            EventsManager.Instance.OnExitBedTriggerZone += UnReduceSpeed;
        }
        private void OnDisable()
        {
            EventsManager.Instance.OnStayInRegistrationTriggerZone -= ReduceSpeed;
            EventsManager.Instance.OnStayInBedTriggerZone -= ReduceSpeed;
            EventsManager.Instance.OnExitRegistartionTriggerZone -= UnReduceSpeed;
            EventsManager.Instance.OnExitBedTriggerZone -= UnReduceSpeed;
        }
        public virtual void Initialize()
        {
            AssignInputs();
        }

        public virtual void AssignInputs() { }
        public virtual void TargetFace() { }
        public virtual void TargetFace(NavMeshAgent agent) { }
        public virtual void Move(NavMeshAgent agent) { }
  
        private void ReduceSpeed() => Agent.speed = _reducedSpeed;
        private void UnReduceSpeed() => Agent.speed = _defalultSpeed;

    }

}
