using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmCenter : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<AlarmZone> _zones;
    [Range(0, 1)]
    [SerializeField] private float _volumeSpeed;

    private Coroutine _volumeCoroutine;
    private bool _isAlarmEnabled;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        StartCoroutine(LerpVolume());
    }

    private void OnEnable()
    {
        if (_zones != null && _zones.Count > 0)
        {
            foreach (AlarmZone zone in _zones)
            {
                zone.RubberEntered += EnableAlarm;
                zone.RubberExitted += DisableAlarm;
            }
        }
    }

    private void OnDisable()
    {
        if (_zones != null && _zones.Count > 0)
        {
            foreach (AlarmZone zone in _zones)
            {
                zone.RubberEntered -= EnableAlarm;
                zone.RubberExitted -= DisableAlarm;
            }
        }
    }

    private void EnableAlarm()
    {
        _isAlarmEnabled = true;
    }

    private void DisableAlarm()
    {
        _isAlarmEnabled = false;
    }


    private IEnumerator LerpVolume()
    {
        while (enabled)
        {
            float volumeDirection = _isAlarmEnabled ? 1 : 0;
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, volumeDirection, _volumeSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
