using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class House : MonoBehaviour
{
    [SerializeField] private List<AlarmZone> _alarmZones;
    [SerializeField] private AlarmVoice _alarmVoice;

    private void OnEnable()
    {
        _alarmZones.ForEach(zone =>
        {
            zone.Triggered += OnAlarmZoneTriggered;
            zone.Released += OnAlarmZoneReleased;
        });
    }

    private void OnDisable()
    {
        _alarmZones.ForEach(zone =>
        {
            zone.Triggered -= OnAlarmZoneTriggered;
            zone.Released -= OnAlarmZoneReleased;
        });
    }

    private void OnAlarmZoneTriggered(Collider collider)
    {
        if (collider.TryGetComponent(out Rubber rubber))
        {
            _alarmVoice?.TurnOn();
        }
    }

    private void OnAlarmZoneReleased(Collider collider)
    {
        _alarmVoice?.TurnOff();
    }
}
