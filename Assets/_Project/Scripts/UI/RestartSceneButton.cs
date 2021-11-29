
using UnityEngine;
using UnityEngine.EventSystems;

public class RestartSceneButton : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        ProjectSceneManager.Instance.Restart();
    }
}



