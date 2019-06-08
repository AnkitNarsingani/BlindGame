using UnityEngine;
using System.Collections;

public class InteractObject : MonoBehaviour
{
    protected delegate void OnInputAction();
    protected event OnInputAction OnTouchDown;

    private bool canCheckPlayerDistance;

    GameObject player;

    void OnEnable()
    {
        OnTouchDown += OnTouched;
    }

    void OnDisable()
    {
        OnTouchDown -= OnTouched;
    }

    protected virtual void Start()
    {
#if UNITY_EDITOR
        player = GameObject.FindGameObjectWithTag("Player");
#elif UNITY_ANDROID
        player = GameObject.FindGameObjectWithTag("Player");
#endif
    }

    protected virtual void Update()
    {
        StartCoroutine(CheckGameObjectTouched());

        if (canCheckPlayerDistance)
        {
#if UNITY_EDITOR
            if (player.GetComponent<GirlMovementPC>().TargetReached)
            {
                OnTouchDown.Invoke();
                canCheckPlayerDistance = false;
            }
#elif UNITY_ANDROID
            if (player.GetComponent<GirlMovement>().TargetReached)
            {
                OnTouchDown.Invoke();
                canCheckPlayerDistance = false;
            }
#endif
        }

    }

    protected virtual void OnTouched()
    {
        Destroy(gameObject);
    }

    IEnumerator CheckGameObjectTouched()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
        {
#if UNITY_EDITOR
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
#elif UNITY_ANDROID
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
#endif
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            yield return new WaitForEndOfFrame();

            if (hit.collider != null && hit.collider.name.Equals(gameObject.name))
                canCheckPlayerDistance = true;
            else
                canCheckPlayerDistance = false;     
        }
    }
}
