using UnityEngine;
using Cinemachine;

public class CameraSizeController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Transform characterTransform;
    [SerializeField] private float initialCameraDistance = 10f;
    [SerializeField] private float distanceMultiplier = 1f;
    [SerializeField] private float smoothSpeed = 5f;

    private float characterSize;
    private CinemachineTransposer cameraOffset;

    private void Start()
    {
        cameraOffset = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        characterSize = characterTransform.localScale.magnitude;
    }

    private void Update()
    {
        if (characterTransform == null)
            return;

        UpdateCharacterSize();
        UpdateCameraDistance();
    }

    private void UpdateCharacterSize()
    {
        characterSize = characterTransform.localScale.magnitude;
    }

    private void UpdateCameraDistance()
    {
        float targetDistance = initialCameraDistance + (characterSize * distanceMultiplier);
        Vector3 offset = new Vector3(0f, targetDistance, -targetDistance);
        cameraOffset.m_FollowOffset = Vector3.Lerp(cameraOffset.m_FollowOffset, offset, smoothSpeed * Time.deltaTime);
    }
}
