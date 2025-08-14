using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class AlarmZone : MonoBehaviour
{
    public event UnityAction<Collider> Triggered;
    public event UnityAction<Collider> Released;

    private bool isColliderStay;


    private void OnTriggerEnter(Collider collider)
    {
        isColliderStay = true;
        Triggered?.Invoke(collider);
    }

    private void OnTriggerExit(Collider collider)
    {
        isColliderStay = false;
        Released?.Invoke(collider);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = isColliderStay ? Color.red : Color.green;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
