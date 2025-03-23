using PlayerParts;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Control
{
    public class Joystick : MonoBehaviour
    {
        [SerializeField] GameObject dropButton;
        Vector2 m_moveVector;
        Vector2 m_lookVector;


        [Inject] PlayerHand m_playerHand;

        void OnMove(InputValue value)
        {
            m_moveVector = value.Get<Vector2>();
        }

        void OnLook(InputValue value)
        {
            m_lookVector = value.Get<Vector2>();
        }

        void OnDrop(InputValue value)
        {
            DropButtonVisible(false);
            m_playerHand.DropObject();
        }

        public Vector2 GetMoveVector()
        {
            return m_moveVector;
        }

        public Vector2 GetLookVector()
        {
            return m_lookVector;
        }

        public void DropButtonVisible(bool visible)
        {
            dropButton.SetActive(visible);
        }
    }
}