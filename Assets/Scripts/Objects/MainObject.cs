using UnityEngine;

namespace Objects
{
    public class MainObject : MonoBehaviour
    {
        Rigidbody m_rigidbody;
        Collider m_collider;

        void Start()
        {
            m_rigidbody = GetComponent<Rigidbody>();
            m_collider = GetComponent<Collider>();
        }

        public void ToThrow(Vector3 forward)
        {
            ActivateCollider(true);
            StickTo(null);
            m_rigidbody.AddForce(forward, ForceMode.Impulse);
        }

        public void StickTo(Transform t)
        {
            transform.SetParent(t);
        }

        public void ActivateCollider(bool activate)
        {
            m_collider.enabled = activate;
            m_rigidbody.isKinematic = !activate;
        }
    }
}