using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.UI
{
    public class UIView : MonoBehaviour
    {
        public RectTransform[] _panes;
        private RectTransform _rect;

        private bool _isFocused;

        public UIBackpack _uiBackpack;

        void Start() {
            _rect = GetComponent<RectTransform>();
        }

        void OnMouseEnter() {
            _isFocused = true;
        }

        public RectTransform[] GetPanes() {
            return _panes;
        }

        void OnMouseExit() {
            _isFocused = false;
        }

        public bool IsFocused() {
            return _isFocused;
        }

        public void OnClick() {

        }
    }
}
