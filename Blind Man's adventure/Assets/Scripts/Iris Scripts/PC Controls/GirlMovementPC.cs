using UnityEngine;
using UnityEngine.EventSystems;

public class GirlMovementPC : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    bool isFlipped = false;

    private Camera mainCamera;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    Vector3 target;

    private void OnEnable()
    {
        GirlControl.OnStateChanged += Reset;
    }

    private void OnDisable()
    {
        GirlControl.OnStateChanged -= Reset;
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;
        target = transform.position;
    }

    void Update()
    {
        if (!IsPointerOverUI())
        {
            Move();

            if (Vector3.Distance(transform.position, target) > 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
                anim.SetBool("isWalking", true);
            }
            else
            {
                anim.SetBool("isWalking", false);
                OnTargetReached();
            }
        }
    }

    private void Move()
    {
        if (Input.GetMouseButton(0))
        {
            target = new Vector3(mainCamera.ScreenToWorldPoint(Input.mousePosition).x, transform.position.y, transform.position.z);
            OnTargetSet();
        }
    }

    private void OnTargetReached()
    {

    }

    private void OnTargetSet()
    {
        CheckSpriteFlip();
    }

    void CheckSpriteFlip()
    {
        if (target.x < transform.position.x)
            spriteRenderer.flipX = true;
        else if (target.x > transform.position.x)
            spriteRenderer.flipX = false;
    }

    private void Reset()
    {
        target = transform.position;
        anim.SetBool("isWalking", false);
    }

    bool IsPointerOverUI()
    {

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }
        return false;
    }
}
