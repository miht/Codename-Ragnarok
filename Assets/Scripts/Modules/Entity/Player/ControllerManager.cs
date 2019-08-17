using System.Collections;
using System.Collections.Generic;
using Modules.UI;
using UnityEngine;

namespace Modules.Entity.Player
{
    public class ControllerManager : MonoBehaviour
    {
        public UIView _ui;
        private Controller[] _controllers;
        // Start is called before the first frame update
        void Start()
        {
            _controllers = gameObject.GetComponentsInChildren<Controller>();
            foreach(Controller controller in _controllers) {
                controller.Initialize(this,
                () => {
                    YieldControl(controller);
                },
                () => {
                    ReleaseControl();
                });

            }
        }

        public bool IsMouseOverUI() {
            bool mouseOverUI = true;
            foreach(RectTransform rt in _ui.GetPanes()) {
                mouseOverUI &= RectTransformUtility.RectangleContainsScreenPoint(rt, Input.mousePosition);
            }

            Debug.Log(mouseOverUI);
            return mouseOverUI;
        }

        void YieldControl(Controller controller) {
            foreach(Controller contr in _controllers) {
                contr.enabled = false;
            }
            controller.enabled = true;
        }

        void ReleaseControl() {
            foreach(Controller contr in _controllers) {
                contr.enabled = true;
            }
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}