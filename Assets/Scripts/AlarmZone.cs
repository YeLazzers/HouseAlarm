using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class AlarmZone : MonoBehaviour
{
    private AlarmCenter _alarmCenter;
    private bool isRubberStay;

    public void Initilize(AlarmCenter alarmCenter)
    {
        _alarmCenter = alarmCenter;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Rubber rubber))
        {
            isRubberStay = true;
            _alarmCenter?.Alarm();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent(out Rubber rubber))
        {
            isRubberStay = false;
            _alarmCenter?.Disarm();
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = isRubberStay ? Color.red : Color.green;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
