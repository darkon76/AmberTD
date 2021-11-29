using UnityEngine;
using UnityEngine.EventSystems;
public class LoadSceneButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int _levelIndex;


    public void OnPointerClick(PointerEventData eventData)
    {
        ProjectSceneManager.Instance.LoadScene(_levelIndex);
    }
}
