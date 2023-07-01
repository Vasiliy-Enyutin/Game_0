using UnityEngine;
using Zenject;

namespace _Project.Scripts.PlayerLogic
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayerAudioController : MonoBehaviour
    {
        [SerializeField]
        private AudioClip[] _footstepsAudioClips;
        
        [Inject]
        private PlayerInputService _playerInputService = null!;
        
        private AudioSource _audioSource;


        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (_playerInputService.MoveDirection != Vector3.zero)
            {
                PlayFootstepSound();
            }
        }

        private void PlayFootstepSound()
        {
            if (_footstepsAudioClips.Length > 0 && !_audioSource.isPlaying)
            {
                int randomIndex = Random.Range(0, _footstepsAudioClips.Length);
                AudioClip audioClip = _footstepsAudioClips[randomIndex];
                _audioSource.PlayOneShot(audioClip);
            }
        }
    }
}