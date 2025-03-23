using Control;
using UnityEngine;
using Zenject;

namespace PlayerParts
{
    public class Player : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public float rotationSpeed = 100f;

        CharacterController m_characterController;
        Vector3 m_playerVelocity;
        const float GravityValue = -9.81f;

        [Inject] Joystick m_joystick;
        [Inject] Camera m_camera;

        private void Start()
        {
            m_characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
            
            var grounded = m_characterController.isGrounded;

            if (grounded && m_playerVelocity.y < 0)
            {
                m_playerVelocity.y = 0f;
            }

            var moveInput = m_joystick.GetMoveVector();
            var rotateInput = m_joystick.GetLookVector();

            MovePlayer(moveInput);
            RotatePlayer(rotateInput);

            m_playerVelocity.y += GravityValue * Time.deltaTime;
            m_characterController.Move(m_playerVelocity * Time.deltaTime);
        }

        private void MovePlayer(Vector2 moveInput)
        {
            var forward = transform.forward * moveInput.y + transform.right * moveInput.x;
            var movement = forward * (moveSpeed * Time.deltaTime);
            m_characterController.Move(movement);
        }

        private void RotatePlayer(Vector2 rotateInput)
        {
            var rotation = rotateInput.x * rotationSpeed * Time.deltaTime;
            var deltaRotation = Quaternion.Euler(0, rotation, 0);
            transform.rotation = deltaRotation * transform.rotation;

            var rotationX = rotateInput.y * rotationSpeed * Time.deltaTime;
            var newRotationX = m_camera.transform.localRotation.eulerAngles.x - rotationX;
            if (newRotationX > 180f)
            {
                newRotationX -= 360f;
            }

            newRotationX = Mathf.Clamp(newRotationX, -55f, 55f);
            m_camera.transform.localRotation = Quaternion.Euler(newRotationX, 0f, 0f);
        }
        
        //  плавно возвращает X в 0 градусов.
        // private void RotatePlayer(Vector2 rotateInput)
        // {
        //     var rotation = rotateInput.x * rotationSpeed * Time.deltaTime;
        //     var deltaRotation = Quaternion.Euler(0, rotation, 0);
        //     transform.rotation = deltaRotation * transform.rotation;
        //
        //     var rotationX = rotateInput.y * rotationSpeed * Time.deltaTime;
        //     var newRotationX = m_camera.transform.localRotation.eulerAngles.x - rotationX;
        //
        //     if (newRotationX > 180f)
        //     {
        //         newRotationX -= 360f;
        //     }
        //
        //     newRotationX = Mathf.Clamp(newRotationX, -55f, 55f);
        //
        //     if (rotateInput == Vector2.zero) 
        //     {
        //         newRotationX = Mathf.Lerp(newRotationX, 0.0f, rotationSpeed * Time.deltaTime * 0.05f);
        //     }
        //
        //     m_camera.transform.localRotation = Quaternion.Euler(newRotationX, 0f, 0f);
        // }
        
    }
}