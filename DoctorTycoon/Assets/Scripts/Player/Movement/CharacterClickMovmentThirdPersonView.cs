using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    public class CharacterClickMovmentThirdPersonView : CharacterMovment
    {
        [Header("Properties")]
        [SerializeField] private LayerMask _clickableLayer;
        [SerializeField] private ParticleSystem _clickParticle;
        [SerializeField] private Camera _camera;
        private Mouse _input;


        private void OnEnable()
        {
            AssignInputs();
        }
        private void OnDisable()
        {
            _input.Disable();
        }
        private void Update()
        {
            TargetFace(Agent);
        }
        public override void AssignInputs()
        {
            _input = new();
            _input.Enable();
            _input.Main.Move.performed += ctx => Move(Agent);
        }

        public override void Move(NavMeshAgent agent)
        {
            /*RaycastHit hit;
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit, 100, _clickableLayer))
            {
                agent.destination = hit.point;
                if (_clickParticle != null)
                    Instantiate(_clickParticle, hit.point += new Vector3(0f, 0.1f, 0f), _clickParticle.transform.rotation);
            }*/
        }

        public override void TargetFace(NavMeshAgent agent)
        {
            if (agent.velocity != Vector3.zero)
            {
                Vector3 direction = (agent.destination - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x + 0.001f, 0, direction.z + 0.001f));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * LookRotationSpeed);
            }
        }

    }

}
