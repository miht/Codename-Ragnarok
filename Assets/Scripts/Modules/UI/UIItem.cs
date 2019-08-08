using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIItem : MonoBehaviour
{
    private Item _item;
    private bool _isDragging;

    private Action _dropAction;

    private bool _isValid = true;

    private Vector2Int _dimensions;

    private RectTransform _rect;

    public Image _image;
    private Image _backgroundImage;
    private Color _backgroundColor;
    private Color _foregroundColor;
    public Color _invalidColor;

    private BoxCollider2D _collider;
    public Vector2 _colliderMargins;

    public int _numberOfIntersections = 0;

    private Rigidbody2D _rigidbody;

    void Start() {
    }

    void Update()
    {
        if (_isDragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        UIUtilities.DebugRect(_rect.rect, Color.green);
    }

    public void Initialize(Item item, Action dropAction) {
        _item = item;
        _dimensions = _item._uiDimensions;
        _image.sprite = _item.UISprite;

        _backgroundImage = GetComponent<Image>();
        _backgroundColor = Item._raretyColors[_item.Rarety];
        _backgroundColor.a = 0.15f;
        _backgroundImage.color = _backgroundColor;

        _foregroundColor = _image.color;

        _rigidbody = gameObject.AddComponent<Rigidbody2D>();
        _rigidbody.isKinematic = true;
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;

        _rect = GetComponent<RectTransform>();
        float width = _dimensions.x * 64;
        float height = _dimensions.y * 64;
        _rect.sizeDelta = new Vector2(width, height);

        _collider = gameObject.AddComponent<BoxCollider2D>();
        _collider.size = _rect.rect.size - _colliderMargins;
        _collider.isTrigger = true;

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
        Debug.Log("Intersecting!");
    }

    private void OnTriggerExit2D(Collider2D other) {
        _numberOfIntersections --;
    }

    public RectTransform GetRectTransform()
    {
        return _rect;
    }

    public bool Intersects(UIItem other) {
        return _collider.IsTouching(other._collider);
    }
}