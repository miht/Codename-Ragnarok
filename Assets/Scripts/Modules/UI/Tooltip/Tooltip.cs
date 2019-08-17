using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public Text _name;
    public Text _description;

    private RectTransform _rect;

    // Start is called before the first frame update

    void Awake() {
        _rect = GetComponent<RectTransform>();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Configure(string name, string description, Vector2 position) {
        _name.text = name;
        _description.text = description;
        _rect.transform.position = position;
    }

    void OnEnable() {
    }
}
