using SystemOfExtras;
using UnityEngine;
using UnityEngine.Events;

namespace GameAudio
{
    public class AudioEventWrapper : MonoBehaviour
    {

        [SerializeField] private UnityEvent OnAnimationEvent;
        public void PlaySound()
        {
           OnAnimationEvent.Invoke();
        }
    }
}
