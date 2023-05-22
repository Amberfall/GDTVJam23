using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private LayerMask _friendlyLayer = new LayerMask();

    private GameObject _currentHeldObject;

    private void Update() {
       QuitApplication();
       FoodPickUpInteraction();
       DropFood();
    }

    private void FoodPickUpInteraction() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, _friendlyLayer);

            if (hit.collider == null) { return; }

            Throwable throwable = hit.collider.gameObject.GetComponent<Throwable>();

            if (hit.collider != null && throwable)
            {
                throwable.IsActive = true;
                _currentHeldObject = throwable.gameObject;
            }
        }
    }

    private void DropFood() {
        if (Input.GetMouseButtonUp(0) && _currentHeldObject) {
            _currentHeldObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            _currentHeldObject = null;
        }
    }

    private void QuitApplication() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
