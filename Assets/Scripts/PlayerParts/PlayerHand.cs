using Control;
using Objects;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Zenject;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace PlayerParts
{
    public class PlayerHand : MonoBehaviour
    {
        bool m_hasObject;
        bool m_objectIsAttached;

        MainObject m_liftedObject;

        [Inject] Joystick m_joystick;
        [Inject] Camera m_camera;

        void Start()
        {
            EnhancedTouchSupport.Enable();
            Touch.onFingerDown += OnTouchDown;
            Touch.onFingerUp += OnTouchUp;
        }


        void Update()
        {
            if (m_objectIsAttached)
            {
                m_liftedObject.transform.position = Vector3.MoveTowards(m_liftedObject.transform.position, transform.position, Time.deltaTime * 25f);
                if (m_liftedObject.transform.position == transform.position)
                {
                    m_objectIsAttached = false;
                    m_joystick.DropButtonVisible(true);
                    m_liftedObject.ActivateCollider(false);
                }
            }
        }

        void OnTouchUp(Finger finger)
        {
            if (m_hasObject || m_liftedObject == null) return;
            var ray = m_camera.ScreenPointToRay(finger.screenPosition);
            if (Physics.Raycast(ray, out var hit) && hit.transform == m_liftedObject.transform)
            {
                m_hasObject = true;
                m_objectIsAttached = true;
                m_liftedObject.StickTo(transform);
            }
        }

        void OnTouchDown(Finger finger)
        {
            if (m_hasObject) return;
            var ray = m_camera.ScreenPointToRay(finger.screenPosition);

            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.transform.TryGetComponent<MainObject>(out var obj))
                {
                    m_liftedObject = obj;
                }
            }
        }

        public void DropObject()
        {
            if (m_liftedObject == null) return;
            m_liftedObject.ToThrow(transform.forward * 5.0f);
            m_liftedObject = null;
            m_hasObject = false;
            m_joystick.DropButtonVisible(false);
        }

        private void OnDisable()
        {
            Touch.onFingerDown -= OnTouchDown;
            Touch.onFingerUp -= OnTouchUp;
            EnhancedTouchSupport.Disable();
        }
    }
}