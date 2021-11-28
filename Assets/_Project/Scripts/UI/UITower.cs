using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UITower : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private TowerSO _towerSO;
    [SerializeField] private MaterialChanger GhostTower;

    private Camera _mainCamera;
    private Plane _plane;
    [SerializeField] float Offset = 0;

    [SerializeField]
    private GameObject _ghostTower;

    private void OnValidate()
    {
        var image = GetComponent<Image>();
        image.sprite = _towerSO?.Icon;
        _plane = new Plane(Vector3.up, -Offset);
    }

    public void OnDrag(PointerEventData eventData)
    {
        var ray = _mainCamera.ScreenPointToRay(eventData.position);
        float enter;
        if (_plane.Raycast(ray, out enter))
        {
            var rayPoint = ray.GetPoint(enter);

            rayPoint.x -= .5f;
            rayPoint.z -= .5f;

            rayPoint.x = Mathf.RoundToInt(rayPoint.x);
            rayPoint.y = Mathf.RoundToInt(rayPoint.y);
            rayPoint.z = Mathf.RoundToInt(rayPoint.z);

            rayPoint.x += .5f;
            rayPoint.z += .5f;


            _ghostTower.transform.position = rayPoint;
        }
    }

    // Use this for initialization
    private void Awake()
    {
        _plane = new Plane(Vector3.up, -Offset);
        _mainCamera = Camera.main; //Cache the camera, micro optimization.
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _ghostTower = PoolManager.RequestGameObject(GhostTower.gameObject);
        _ghostTower.SetActive(true);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _ghostTower.SetActive(false);
    }
}
