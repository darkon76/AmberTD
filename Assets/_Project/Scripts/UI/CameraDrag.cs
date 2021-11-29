using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Really simple camera control.
/// </summary>
public class CameraDrag : MonoBehaviour, IDragHandler
{
    private Camera _mainCamera;
    [SerializeField] private Vector2 multiplier; 
    

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    public void OnDrag(PointerEventData eventData)
    {
        var delta = eventData.delta;
        delta.x *= multiplier.x;
        delta.y *= multiplier.y;

        var translation = new Vector3(delta.x, 0, delta.y);
        _mainCamera.transform.Translate(translation, Space.World);
    }
}
