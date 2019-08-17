using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Modules.Entity.Player
{
    public class Controller : MonoBehaviour
    {
        private ControllerManager _controllerManager;
        private Action _onRequestControl;
        private Action _onReleaseControl;

        public void Initialize(ControllerManager manager, Action onRequestControl, Action onReleaseControl) {
            _controllerManager = manager;
            _onRequestControl = onRequestControl;
            _onReleaseControl = onReleaseControl;
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public bool IsMouseOverUI()
        {
            return _controllerManager.IsMouseOverUI();
        }

        public virtual void RequestControl() {
            _onRequestControl();
        }

        public virtual void ReleaseControl() {
            _onReleaseControl();
        }
    }
}