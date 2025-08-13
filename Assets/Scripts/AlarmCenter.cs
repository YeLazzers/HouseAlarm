using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmCenter : MonoBehaviour
{
    private readonly int _maxVolume = 1;
    private readonly int _minVolume = 0;

    [Range(0, 1)]
    [SerializeField] private float _volumeSpeed;
    [SerializeField] private List<AlarmZone> _zones;

    private Coroutine _volumeCoroutine;
    private AudioSource _audioSource;

    public void Alarm()
    {
        _volumeCoroutine = StartCoroutine(LerpVolume(_maxVolume));
    }

    public void Disarm()
    {
        _volumeCoroutine = StartCoroutine(LerpVolume(_minVolume));
    }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_zones.Count > 0)
        {
            _zones.ForEach(zone => zone.Initilize(this));
        }
    }

    private IEnumerator LerpVolume(float target)
    {
        if (_volumeCoroutine != null)
        {
            StopCoroutine(_volumeCoroutine);
        }

        while (_audioSource.volume != target)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, target, _volumeSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
