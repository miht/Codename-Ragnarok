using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.UI
{
    public class UIView : MonoBehaviour
    {
        private RectTransform _rect;

        private bool _isFocused;

        public UIBackpack _uiBackpack;

        void Start() {
            _rect = GetComponent<RectTransform>();
        }

        void OnMouseEnter() {
            _isFocused = true;
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
