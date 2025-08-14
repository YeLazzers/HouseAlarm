using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmVoice : MonoBehaviour
{
    private readonly int _maxVolume = 1;
    private readonly int _minVolume = 0;

    [Range(0, 1)]
    [SerializeField] private float _volumeSpeed;

    private Coroutine _volumeCoroutine;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void TurnOn()
    {
        StopCoroutines();
        _volumeCoroutine = StartCoroutine(LerpVolume(_maxVolume));
    }

    public void TurnOff()
    {
        StopCoroutines();
        _volumeCoroutine = StartCoroutine(LerpVolume(_minVolume));
    }

    private void StopCoroutines()
    {
        if (_volumeCoroutine != null)
        {
            StopCoroutine(_volumeCoroutine);
        }
    }

    private IEnumerator LerpVolume(float target)
    {

        while (_audioSource.volume != target)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, target, _volumeSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
