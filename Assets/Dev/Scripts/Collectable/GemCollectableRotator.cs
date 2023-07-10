using UnityEngine;

namespace Collectable
{
    public class GemCollectableRotator : MonoBehaviour
    {
        [SerializeField] private float spinDuration;

        private void Update()
        {
            var angleUnit = 360f * (Time.deltaTime / spinDuration);
            var currentEulerAngles = transform.eulerAngles;
            transform.eulerAngles = new Vector3(currentEulerAngles.x, currentEulerAngles.y + angleUnit,
                currentEulerAngles.z);
        }
    }
}