using UnityEngine;

public class Cupboard : InteractObject
{
    [Range(0, 1)] [SerializeField] float openDelay = 0.5f;
    [Range(0, 1)] [SerializeField] float closeDelay = 0.5f;
    [Range(0, 0.5f)] [SerializeField] float fadeSpeed = 0.25f;
    [SerializeField] private GameObject cupboardInteractableUI;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnTouched()
    {
        StartCoroutine("OpenCupboard");
    }

    public void StartClosingCupboard()
    {
        StartCoroutine("CloseCupboard");
    }

    System.Collections.IEnumerator OpenCupboard()
    {
        yield return new WaitForSeconds(openDelay);
        cupboardInteractableUI.SetActive(true);
    }

    System.Collections.IEnumerator CloseCupboard()
    {
        yield return new WaitForSeconds(closeDelay);
        UnityEngine.UI.Image background = cupboardInteractableUI.GetComponentInChildren<UnityEngine.UI.Image>();
        Color backgroundColor = background.color;
        while(backgroundColor.a > 0)
        {
            backgroundColor.a -= fadeSpeed;
            background.color = backgroundColor;
            yield return null;
        }
        cupboardInteractableUI.SetActive(false);
    }
}
