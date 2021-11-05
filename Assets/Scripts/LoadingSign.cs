using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LoadingSign : MonoBehaviour
{

    private Image image;
    [SerializeField] private float rotationSpeed = 1;
    private Tweener tw;
    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void Load()
    {
        image.DOFade(1, 0.2f);
        tw?.Kill();
        gameObject.SetActive(true);
        tw = transform.DORotate(Vector3.forward, 1f / rotationSpeed).SetLoops(-1, LoopType.Incremental);
        tw.Play();
    }

    public void Unload()
    {
        image.DOFade(0, 1).OnComplete(()=>{tw?.Kill();});
    }
}