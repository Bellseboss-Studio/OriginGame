using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Gameplay.UsoDeCartas
{
    [RequireComponent(typeof(BoxCollider), typeof(Rigidbody))]
    public class DragComponent : MonoBehaviour
    {
        [SerializeField] private bool isDetachable;
        private Vector3 posicionInicial;
        private Transform _canvasRectTransform;
        private EventTrigger.Entry entry_0;
        private EventTrigger.Entry entry_1;
        private EventTrigger.Entry entry_2;
        private Camera _camera;
        private bool _isInUse;
        public bool canUseComponent;
        public Action OnDragging;
        public Action OnFinishDragging;
        public Action OnDropCompleted;
        public bool estaSeleccionadaLaCarta;
        [SerializeField] private GameObject colision;
        private IDeckForGame _deckForGame;
        private Vector2 _pointInScreen;
        private IGameLogic _gameLogic;
        private OriginGame inputs;
        private bool _startDragging;

        public void PointMouseTouch(InputAction.CallbackContext context)
        {
            _pointInScreen = context.ReadValue<Vector2>();
            Debug.Log($"point {_pointInScreen}");
        }
        public void PointMouseTouchStart(InputAction.CallbackContext context)
        {
            var perfomed = context.performed;
            Debug.Log($"start {perfomed}");
        }
        public void PointMouseTouchCancel(InputAction.CallbackContext context)
        {
            Debug.Log($"Start {context.started} cancel {context.canceled}");
            if (context.started)
            {
                //shoot raycast if the gameObject is a card start dragging
                _startDragging = true;
                posicionInicial = transform.localPosition;
            }else if (context.canceled)
            {
                _startDragging = false;
                Dropping();
            }
        }

        private void Awake()
        {
            inputs = new OriginGame();
        }

        private void Start()
        {
            canUseComponent = true;
        }

        private void OnEnable()
        {
            inputs.Enable();
        }

        private void OnDisable()
        {
            inputs.Disable();
        }

        public void CreateEventForDragAndDrop()
        {
            var trigger_Object = gameObject.AddComponent<EventTrigger>();

            entry_0 = new EventTrigger.Entry();
            entry_0.eventID = EventTriggerType.BeginDrag;
            entry_0.callback.AddListener((data) => { BeginDrag_((PointerEventData)data); });

            entry_1 = new EventTrigger.Entry();
            entry_1.eventID = EventTriggerType.Drag;
            entry_1.callback.AddListener((data) => { ObjectDragging_((PointerEventData)data); });

            entry_2 = new EventTrigger.Entry();
            entry_2.eventID = EventTriggerType.EndDrag;
            entry_2.callback.AddListener((data) => { ObjectDrop_((PointerEventData)data); });

            trigger_Object.triggers.Add(entry_0);
            trigger_Object.triggers.Add(entry_1);
            trigger_Object.triggers.Add(entry_2);

            inputs.Player.TouchPress.started += PointMouseTouchCancel;
            inputs.Player.TouchPress.canceled += PointMouseTouchCancel;
        }

        private void ObjectDrop_(PointerEventData data)
        {
            Dropping();
        }

        private void Dropping()
        {
            OnFinishDragging?.Invoke();
            if (colision == null)
            {
                RestartPosition();
                return;
            }

            switch (colision.name)
            {
                case "Place":
                    _deckForGame.PlaceCard();
                    break;
                case "Pass":
                    _deckForGame.PassTurn();
                    RestartPosition();
                    break;
            }
            OnDropCompleted?.Invoke();
        }

        public void RestartPosition()
        {
            transform.position = posicionInicial;
        }

        private void BeginDrag_(PointerEventData data)
        {
            posicionInicial = transform.position;
            OnDragging?.Invoke();
        }


        private void ObjectDragging_(PointerEventData data)
        {
            Dragging();
        }

        private void Dragging()
        {
            Vector3 mousePosition = Application.platform == RuntimePlatform.Android
                ? inputs.Player.TouchPosition.ReadValue<Vector2>()
                : inputs.Player.TouchPosition.ReadValue<Vector2>();
                //: Mouse.current.position.ReadValue();

            mousePosition.z = 80;
            Debug.DrawRay(_camera.transform.position, (_camera.ScreenToWorldPoint(mousePosition)), Color.cyan);

            RaycastHit[] hits;
            //Debug.Log($"mouse is in {mousePosition}");
            var screenPointToRay = _camera.ViewportPointToRay(mousePosition);
            hits = Physics.RaycastAll(_camera.transform.position, _camera.ScreenToWorldPoint(mousePosition),
                Mathf.Infinity);

            //Debug.Log($"origin {screenPointToRay.origin} - direction {screenPointToRay.direction}");
            Debug.DrawRay(screenPointToRay.origin, screenPointToRay.direction * 100, Color.yellow);
            foreach (var hit in hits)
            {
                if (hit.collider.gameObject.TryGetComponent<DropComponent>(out var drop))
                {
                    drop.Drop(this);
                    //Debug.Log("did hit");
                    transform.position = hit.point;
                    break;
                }
            }
        }

        public void Configure(Camera camera, IDeckForGame deckForGame, IGameLogic gameLogic)
        {
            _camera = camera;
            _deckForGame = deckForGame;
            _gameLogic = gameLogic;
            //_factoriaCarta = factoriaCartas;
            CreateEventForDragAndDrop();
        }

        private void OnTriggerEnter(Collider other)
        {
            colision = other.gameObject;
        }

        private void OnTriggerExit(Collider other)
        {
            if (colision != null && other.name == colision.name)
            {
                colision = null;   
            }
        }

        private void Update()
        {
            if (_startDragging)
            {
                Dragging();
            }
        }
    }
}
