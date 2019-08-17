using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIItem : MonoBehaviour
{
    public enum UIEquippableTypes
    {
        Head,
        Chest,
        Hands,
        Waist,
        Feet,
        Ring,
        RangedWeapon,
        MeleeWeapon,
        Shield,
        Spear,
        Miscellaneous,
        NotEquippable
    }

    private Item _item;
    private bool _isDragging;

    private Action _grabAction;
    private Action _releaseAction;
    private Action _dropAction;

    private bool _isValid = true;

    private Vector2Int _dimensions;
    private Vector2 _scaleFactor;

    private RectTransform _rect;

    public Image _image;
    public Image _raretyImage;
    public Vector2 _pivot;
    private Image _backgroundImage;
    private Color _backgroundColor;
    private Color _foregroundColor;
    public Color _invalidColor;

    private BoxCollider2D _collider;
    private EventTrigger _eventTrigger;
    public Vector2 _colliderMargins;

    public int _numberOfIntersections = 0;

    private Rigidbody2D _rigidbody;

    void Start() {
    }

    void Update()
    {
        if (_isDragging)
        {
            Vector2 newPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            newPos += _scaleFactor * _rect.rect.size/2f  * new Vector2(-1, 1);
            transform.position = newPos;
        }
    }

    public void Initialize(Item item, Vector2 scaleFactor, Action grabAction, Action releaseAction, Action dropAction) {
        _item = item;
        _dimensions = _item._uiDimensions;
        _image.sprite = _item.UISprite;

        _backgroundImage = GetComponent<Image>();
        _backgroundColor = Item.GetRaretyColor(item.Rarety);
        _backgroundColor.a = 0.15f;
        _backgroundImage.color = _backgroundColor;

        _raretyImage.sprite = Item.GetRaretySprite(_item.Rarety);

        _foregroundColor = _image.color;

        _rigidbody = gameObject.AddComponent<Rigidbody2D>();
        _rigidbody.isKinematic = true;
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;

        _scaleFactor = scaleFactor;
        _rect = GetComponent<RectTransform>();
        float width = _dimensions.x * 64;
        float height = _dimensions.y * 64;
        _rect.sizeDelta = new Vector2(width, height);

        _collider = gameObject.AddComponent<BoxCollider2D>();
        _collider.offset = new Vector2(_rect.rect.width / 2f, -_rect.rect.height / 2f);
        _collider.size = _rect.rect.size - _colliderMargins;
        _collider.isTrigger = true;

        _eventTrigger = gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry mouseEnterEntry = new EventTrigger.Entry();
        mouseEnterEntry.eventID = EventTriggerType.PointerEnter;

        mouseEnterEntry.callback.AddListener((eventData) => {
            MouseEnter();
            EventTrigger.Entry mouseExitEntry = new EventTrigger.Entry();
            mouseExitEntry.eventID = EventTriggerType.PointerExit;
            mouseExitEntry.callback.AddListener((eventData2) =>
            {
                MouseExit();
                _eventTrigger.triggers[0] = mouseEnterEntry;
            });
            _eventTrigger.triggers[0] = mouseExitEntry;
        });

        _eventTrigger.triggers.Add(mouseEnterEntry);

        _grabAction = grabAction;
        _releaseAction = releaseAction;
        _dropAction = dropAction;
    }

    public Action GetDropAction() {
        return _dropAction;
    }

    public Item GetItem() {
        return _item;
    }

    public Vector2Int GetDimensions() {
        return _dimensions;
    }

    public void SetDragged(bool value) {
        Dragging = value;
        _backgroundImage.raycastTarget = !value;
        _image.raycastTarget = !value;
        if(value) {
            _grabAction();
        }
        else {
            _releaseAction();
        }
        TooltipController.GetInstance().HideTooltip();
    }

    public void ResetPivot() {
        _rect.pivot = _pivot;
    }

    public void SetValid(bool value) {
        _isValid = value;

        if(!value) {
            _backgroundImage.color = new Color(1f, 0f, 0f, _backgroundColor.a);
            _image.color = new Color(1f, 0f, 0f, _foregroundColor.a);
        }
        else {
            _backgroundImage.color = _backgroundColor;
            _image.color = _foregroundColor;
        }
        
        // _image.color = _isValid? Color.white :_invalidColor;
    }

    public bool IsIntersecting() {
        return _numberOfIntersections > 0;
    }

    public bool IsValid() {
        return _isValid;
    }

    public bool Dragging {
        get {return _isDragging;}
        set {_isDragging = value;}
    }

    private void OnTriggerEnter2D(Collider2D other) {
        _numberOfIntersections++;
    }

    private void OnTriggerExit2D(Collider2D other) {
        _numberOfIntersections --;
    }

    private void MouseEnter() {
        TooltipController.GetInstance().ShowTooltip(_item.Name, _item.Description, transform.position);
    }

    private void MouseExit() {
        TooltipController.GetInstance().HideTooltip();
    }

    public RectTransform GetRectTransform()
    {
        return _rect;
    }

    public bool Intersects(UIItem other) {
        return _collider.IsTouching(other._collider);
    }
    
    public Vector2 AnchoredPosition {
        get{ return _rect.anchoredPosition;}
        set {_rect.anchoredPosition = value;}
    }

    
}