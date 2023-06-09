using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UserInputs.Signals;
using UserInputs.UI.Signals;
using Zenject;

namespace UserInputs
{
    public class InputsHandler : IDisposable
    {
        private SignalBus _signalBus;
        private UserControls _controls;

        [Inject]
        public void Init(SignalBus signalBus, UserControls controls)
        {
            _signalBus = signalBus;
            _controls = controls;

            _controls.Player.Enable();

            _controls.Player.LookAround.performed += OnLookAround;
            _controls.Player.Move.performed += OnMove;
            _controls.Player.Fire.performed += OnFire;
            _controls.Player.Ulta.performed += OnUlta;
        }

        public void Dispose()
        {
            _controls.Player.Disable();
        }

        private void OnLookAround(InputAction.CallbackContext context)
        {
            var lookDelta = context.ReadValue<Vector2>();
            var lookAroundSignal = new LookAroundSignal(lookDelta);
            _signalBus.Fire(lookAroundSignal);
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            var moveDelta = context.ReadValue<Vector2>();
            var moveSignal = new MoveSignal(moveDelta);
            _signalBus.Fire(moveSignal);
        }

        private void OnFire(InputAction.CallbackContext context)
        {
            var fireSignal = new FireSignal(isUlta: false);
            _signalBus.Fire(fireSignal);
        }

        private void OnUlta(InputAction.CallbackContext context)
        {
            var fireSignal = new FireSignal(isUlta: true);
            _signalBus.Fire(fireSignal);
        }
    }
}