using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIItem : MonoBehaviour
{

    private EventTrigger _eventTrigger;

    private bool _isDragging;

    void Start() {
        _eventTrigger = gameObject.AddComponent<EventTrigger>();

        Initialize();
    }

    void Update()
    {
        if (_isDragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
    }

    void Initialize() {
        EventTrigger.Entry beginDragEntry = new EventTrigger.Entry();
        beginDragEntry.eventID = EventTriggerType.BeginDrag;
        beginDragEntry.callback.AddListener((data) => { BeginDrag((PointerEventData) data);});
        _eventTrigger.triggers.Add(beginDragEntry);

        EventTrigger.Entry endDragEntry = new EventTrigger.Entry();
        endDragEntry.eventID = EventTriggerType.EndDrag;
        endDragEntry.callback.AddListener((data) => { EndDrag((PointerEventData) data); });
        _eventTrigger.triggers.Add(endDragEntry);
    }

    private void BeginDrag(PointerEventData data) {
        _isDragging = true;
    }

    private void EndDrag(PointerEventData data)
    {
        _isDragging = false;
    }
}