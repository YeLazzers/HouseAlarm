using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class AlarmZone : MonoBehaviour
{
    public UnityAction RubberEntered;
    public UnityAction RubberExitted;

    private bool isRubberStay;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Rubber rubber))
        {
            isRubberStay = true;
            RubberEntered?.Invoke();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent(out Rubber rubber))
        {
            isRubberStay = false;
            RubberExitted?.Invoke();
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = isRubberStay ? Color.red : Color.green;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
