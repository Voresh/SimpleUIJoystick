using System;
using UnityEngine;

namespace Assets
{
    public class MoveController : MonoBehaviour
    {
        [SerializeField]
        private UIJoystick inputJoystick;

        [Range(0.01f, 2f)]
        [SerializeField]
        private float speed = 0.01f;
        [SerializeField]
        private CharacterController controller;
        [SerializeField]
        private Animator cachedUnitAnimator;

        [SerializeField]
        private string animatorSpeedXName;
        [SerializeField]
        private string animatorSpeedZName;

        private Vector3 moveVector;

        public void Awake()
        {
            moveVector.y = 0f;
        }

        public void Update ()
        {
            float x = inputJoystick.GetInputX();
            float z = inputJoystick.GetInputY();
            cachedUnitAnimator.SetFloat(animatorSpeedXName, Mathf.Abs(x));
            cachedUnitAnimator.SetFloat(animatorSpeedZName, Mathf.Abs(z));
            if (Math.Abs(x) > 0 || Math.Abs(z) > 0)
            {
                moveVector.x = x;
                moveVector.z = z;
                controller.Move(moveVector * speed * Time.deltaTime);
                transform.forward = moveVector;
            }
        }
    }
}
