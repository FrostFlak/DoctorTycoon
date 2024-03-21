using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    public class CharacterJoystickMovement : CharacterMovment
    {   
        [SerializeField] private FloatingJoystick _joystick;
        private Vector3 _scaledMovement;

        private void Update()
        {
            if (GameStateController.Instance.Started && !GameStateController.Instance.FirstPlaySetting && !GameStateController.Instance.Tutorial)
            {
                TargetFace();
                Move(Agent);
            }

        }
        public override void Move(NavMeshAgent agent)
        {
            _scaledMovement = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical) * Time.deltaTime * Agent.speed;
            if ((_scaledMovement.x != 0 || _scaledMovement.y != 0 || _scaledMovement.z != 0) && CameraType.CurrentCameraIndex == (int)CameraTypes.ThirdPerson)
            {
                IsWalking = true;
                agent.Move(_scaledMovement);
            }
            else
                IsWalking = false;
        }
        public override void TargetFace()
        {
            Agent.transform.LookAt((Agent.transform.position + _scaledMovement) * LookRotationSpeed, Vector3.up);
        }
    }

}