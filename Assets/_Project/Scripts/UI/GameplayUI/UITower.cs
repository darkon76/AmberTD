using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UITower : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private TowerSO _towerSO;
    [SerializeField] private MaterialChanger GhostTowerPrefab;

    private Camera _mainCamera;
    private Plane _plane;
    [SerializeField] float Offset = 0;

    [SerializeField]
    private GameObject _ghostTower;

    private MaterialChanger _materialChanger;

    private void OnValidate()
    {
        var image = GetComponent<Image>();
        image.sprite = _towerSO?.Icon;
        _plane = new Plane(Vector3.up, -Offset);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_mainCamera == null)
        {
            _mainCamera = Camera.main;
        }
        var ray = _mainCamera.ScreenPointToRay(eventData.position);
        if (_plane.Raycast(ray, out var enter))
        {
            var rayPoint = ray.GetPoint(enter);
            rayPoint.RoundToInt();
            _ghostTower.transform.position = rayPoint;
            _materialChanger.Valid = MapHolder.Instance.IsValid(rayPoint);
        }
    }

    // Use this for initialization
    private void Awake()
    {
        _plane = new Plane(Vector3.up, -Offset);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _ghostTower = PoolManager.RequestGameObject(GhostTowerPrefab.gameObject);
        _materialChanger = _ghostTower.GetComponent<MaterialChanger>();
        _ghostTower.SetActive(true);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _ghostTower.SetActive(false);
        var ray = _mainCamera.ScreenPointToRay(eventData.position);
        if (_plane.Raycast(ray, out var enter))
        {
            var rayPoint = ray.GetPoint(enter);
            rayPoint.RoundToInt();
            _ghostTower.transform.position = rayPoint;
            if (MapHolder.Instance.TryPlace((int)rayPoint.x, (int)rayPoint.z, TileState.Blocked))
            {
                _towerSO.Create(rayPoint);
            }
        }
    }
}
