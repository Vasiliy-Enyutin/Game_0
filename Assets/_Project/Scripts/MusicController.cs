using System.Collections;
using System.Linq;
using _Project.Scripts.Factories;
using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicController : MonoBehaviour
    {
        [SerializeField]
        private AudioClip[] _idleAudioClips;
        [SerializeField]
        private AudioClip[] _pursuitAudioClips;
        [SerializeField]
        private float _clipsTransitionDuration;

        [Inject]
        private GameFactoryService _gameFactoryService;

        private AudioSource _audioSource;
        private bool _isPursuitMusicPlaying;
        private bool _isIdleMusicPlaying;
        private Coroutine _transitionCoroutine;
        private float _startVolume;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.loop = false;
            _audioSource.playOnAwake = false;
            _audioSource.clip = null;
            _startVolume = _audioSource.volume;
            PlayIdleMusic();
        }

        private void Update()
        {
            if (!_audioSource.isPlaying && _transitionCoroutine == null)
            {
                if (_isPursuitMusicPlaying)
                {
                    PlayPursuitMusic();
                }
                else if (_isIdleMusicPlaying)
                {
                    PlayIdleMusic();
                }
            }

            bool isAnyEnemyPursuingPlayer = CheckEnemyPursuitState();
            if (isAnyEnemyPursuingPlayer && !_isPursuitMusicPlaying)
            {
                PlayPursuitMusic();
            }
            else if (!isAnyEnemyPursuingPlayer && !_isIdleMusicPlaying)
            {
                PlayIdleMusic();
            }
        }

        private bool CheckEnemyPursuitState()
        {
            return _gameFactoryService.Enemies != null && _gameFactoryService.Enemies.Any(enemy => enemy.IsPursuingPlayer);
        }

        private void PlayIdleMusic()
        {
            if (_transitionCoroutine != null)
            {
                StopCoroutine(_transitionCoroutine);
            }

            _transitionCoroutine = StartCoroutine(TransitionToNewClip(GetRandomClip(_idleAudioClips), _clipsTransitionDuration));

            _isIdleMusicPlaying = true;
            _isPursuitMusicPlaying = false;
        }

        private void PlayPursuitMusic()
        {
            if (_transitionCoroutine != null)
            {
                StopCoroutine(_transitionCoroutine);
            }

            _transitionCoroutine = StartCoroutine(TransitionToNewClip(GetRandomClip(_pursuitAudioClips), _clipsTransitionDuration));

            _isIdleMusicPlaying = false;
            _isPursuitMusicPlaying = true;
        }

        private AudioClip GetRandomClip(AudioClip[] clips)
        {
            if (clips.Length == 0)
            {
                return null;
            }

            int randomIndex = Random.Range(0, clips.Length);
            return clips[randomIndex];
        }

        private IEnumerator TransitionToNewClip(AudioClip newClip, float transitionDuration)
        {
            float time = 0;

            while (time < transitionDuration)
            {
                time += Time.deltaTime;
                _audioSource.volume = Mathf.Lerp(_startVolume, 0, time / transitionDuration);
                yield return null;
            }

            _audioSource.Stop();
            _audioSource.clip = newClip;
            _audioSource.volume = _startVolume;
            _audioSource.Play();

            time = 0;
            while (time < transitionDuration)
            {
                time += Time.deltaTime;
                _audioSource.volume = Mathf.Lerp(0, _startVolume, time / transitionDuration);
                yield return null;
            }

            _transitionCoroutine = null;
        }
    }
}
