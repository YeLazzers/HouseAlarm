using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmCenter : MonoBehaviour
{
    private readonly int _maxVolume = 1;
    private readonly int _minVolume = 0;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<AlarmZone> _zones;
    [Range(0, 1)]
    [SerializeField] private float _volumeSpeed;

    private Coroutine _volumeCoroutine;
    private bool _isAlarmEnabled;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        if (_zones != null && _zones.Count > 0)
        {
            foreach (AlarmZone zone in _zones)
            {
                zone.RubberEntered += ToggleAlarm;
                zone.RubberExitted += ToggleAlarm;
            }
        }
    }

    private void OnDisable()
    {
        if (_zones != null && _zones.Count > 0)
        {
            foreach (AlarmZone zone in _zones)
            {
                zone.RubberEntered -= ToggleAlarm;
                zone.RubberExitted -= ToggleAlarm;
            }
        }
    }

    private void ToggleAlarm()
    {
        if (_volumeCoroutine != null)
        {
            StopCoroutine(_volumeCoroutine);
        }

        _volumeCoroutine = StartCoroutine(LerpVolume(_isAlarmEnabled ? _minVolume : _maxVolume));
        _isAlarmEnabled = !_isAlarmEnabled;
    }

    private void EnableAlarm()
    {
        if (_volumeCoroutine != null)
        {
            StopCoroutine( _volumeCoroutine);
        }
        StartCoroutine(LerpVolume(_maxVolume));
    }

    private void DisableAlarm()
    {
        if (_volumeCoroutine != null)
        {
            StopCoroutine(_volumeCoroutine);
        }
        StartCoroutine(LerpVolume(_minVolume));
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
