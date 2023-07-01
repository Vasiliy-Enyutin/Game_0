using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.EnemyLogic
{
    [RequireComponent(typeof(Enemy))]
    [RequireComponent(typeof(AudioSource))]
    public class EnemyAudioController : MonoBehaviour
    {
        [SerializeField] private AudioClip _idleAudioClip = null!;
        [SerializeField] private AudioClip[] _attackAudioClips = null!;

        private AudioSource _audioSource;
        private Enemy _enemy;
        private bool _isPursuitSoundPlaying = false;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _enemy = GetComponent<Enemy>();
            PlayIdleSound();
        }

        private void Update()
        {
            if (_enemy.IsPursuingPlayer && !_isPursuitSoundPlaying)
            {
                StartPursuitSound();
            }
            else if (!_enemy.IsPursuingPlayer && _isPursuitSoundPlaying)
            {
                PlayIdleSound();
            }
        }

        private void StartPursuitSound()
        {
            if (_attackAudioClips.Length > 0 && !_isPursuitSoundPlaying)
            {
                _audioSource.Stop();
                _audioSource.loop = false;
                _isPursuitSoundPlaying = true;
                StartCoroutine(PlayAttackSounds());
            }
        }

        private IEnumerator PlayAttackSounds()
        {
            while (_isPursuitSoundPlaying)
            {
                int randomIndex = Random.Range(0, _attackAudioClips.Length);
                AudioClip audioClip = _attackAudioClips[randomIndex];
                _audioSource.PlayOneShot(audioClip);

                yield return new WaitForSeconds(audioClip.length);
            }
        }

        private void PlayIdleSound()
        {
            if (_audioSource.isPlaying && _audioSource.clip == _idleAudioClip)
            {
                return;
            }

            _isPursuitSoundPlaying = false;
            _audioSource.clip = _idleAudioClip;
            _audioSource.loop = true;
            _audioSource.Play();
        }
    }
}
