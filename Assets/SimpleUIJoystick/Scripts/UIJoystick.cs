using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets
{
    public class UIJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [SerializeField]
        private RectTransform stickPosition;
        [SerializeField]
        private RectTransform circleRectTransform;

        private float circleRange;
        [SerializeField]
        private float minStickMoveRange;

        private Vector3 moveVector;
        private float playerRotationAngle;

        public float GetInputX()
        {
            return moveVector.normalized.x;
        }

        public float GetInputY()
        {
            return moveVector.normalized.y;
        }

        public void Awake()
        {
            circleRange = circleRectTransform.sizeDelta.x / 2f;
        }

        #region touch events

        public void OnPointerDown(PointerEventData data)
        {
            PlayerInteractWithJoystick(data.position);
        }

        public void OnPointerUp(PointerEventData data)
        {
            PlayerEndJoystickInteraction();
        }

        public void OnDrag(PointerEventData data)
        {
            PlayerInteractWithJoystick(data.position);
        }

        #endregion

        private void PlayerEndJoystickInteraction ()
        {
            stickPosition.position = circleRectTransform.position;
            moveVector.x = 0f;
            moveVector.y = 0f;
        }

        private void PlayerInteractWithJoystick(Vector3 pos)
        {
            moveVector.x = pos.x - circleRectTransform.position.x;
            moveVector.y = pos.y - circleRectTransform.position.y;

            UpdateJoystick(moveVector);

            if (moveVector.sqrMagnitude < minStickMoveRange * minStickMoveRange)
            {
                moveVector.x = 0f;
                moveVector.y = 0f;
            }
        }

        private void UpdateJoystick(Vector3 dir)
        {
            if (dir.sqrMagnitude > circleRange * circleRange)
            {
                dir = dir.normalized * circleRange;
            }

            stickPosition.localPosition = dir;
        }
    }
}