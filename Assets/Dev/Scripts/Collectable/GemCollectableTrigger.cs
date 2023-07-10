
using Managers;
using UnityEngine;
namespace Collectable
{
    public class GemCollectableTrigger : MonoBehaviour
    {
        [Tooltip("Tag of what is going to trigger it")]
        [SerializeField] private string triggerTargetTag;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(triggerTargetTag)) return;
            
            GameEvents.collectableEvent?.Invoke(this.gameObject);
            gameObject.SetActive(false);
        }
    }
}