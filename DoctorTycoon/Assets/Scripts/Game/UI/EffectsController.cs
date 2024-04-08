using UnityEngine;

namespace UI
{
    public class EffectsController : MonoBehaviour
    {
       [SerializeField] private ParticleSystem _heartParticle;

        private void Start()
        {
            EventsManager.Instance.OnTimerToHealPatinetEnd += ShowHeartParticle;
        }

        private void OnDisable()
        {
            EventsManager.Instance.OnTimerToHealPatinetEnd -= ShowHeartParticle;

        }
        
        private void ShowHeartParticle() => _heartParticle.Play();
    
    }
}