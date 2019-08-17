using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipController : MonoBehaviour
{
    public Tooltip _tooltipPrefab;
    private Tooltip _tooltip;

    static TooltipController _instance;

    public static TooltipController GetInstance() {
        if(_instance == null) {
            _instance = GameObject.Find("UI Canvas").GetComponent<TooltipController>();
        }
        
        return _instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        _tooltip = Instantiate(_tooltipPrefab, transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowTooltip(string name, string description, Vector2 position) {
        _tooltip.gameObject.SetActive(true);
        _tooltip.Configure(name, description, position);
    }

    public void HideTooltip() {
        _tooltip.gameObject.SetActive(false);
    }
}
