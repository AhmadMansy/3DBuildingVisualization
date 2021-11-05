using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{

    [SerializeField] private Button resetBtn;
    [SerializeField] private Button reloadBtn;
    [SerializeField] private Text fps;
    [SerializeField] private Transform[] transforms;
    [SerializeField] private List<TransformCopy> cameraTransformCopies;

    private void Start()
    {
        foreach (var transform1 in transforms)
        {
            cameraTransformCopies.Add(new TransformCopy(transform1));
        }
        
        resetBtn.onClick.AddListener(ModelReset); 
        reloadBtn.onClick.AddListener(Reload); 
        StartCoroutine(FPSCounter());
    }

    private IEnumerator FPSCounter()
    {
        while (true)
        {
            double f = (1 / Time.unscaledDeltaTime);
            f = Math.Round(f, 1);
            fps.text = "FPS: " + f.ToString(CultureInfo.InvariantCulture);
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void Update()
    {
        return;
        double f = (1 / Time.unscaledDeltaTime);
        f = Math.Round(f, 1);
        fps.text = "FPS: " + f.ToString(CultureInfo.InvariantCulture);

    }

    private void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void ModelReset()
    {
        for (var i = 0; i < transforms.Length; i++)
        {
            transforms[i].GetCopyData(cameraTransformCopies[i]);
        }
    }
}

[Serializable]
public class TransformCopy
{
    public Vector3 Position;
    public Quaternion Rotation;
    public Vector3 LocalScale;
 
    public TransformCopy (Vector3 newPosition, Quaternion newRotation, Vector3 newLocalScale)
    {
        Position = newPosition;
        Rotation = newRotation;
        LocalScale = newLocalScale;
    }
 
    public TransformCopy (Transform transform)
    {
        CopyFrom (transform);
    }
 
    private void CopyFrom (Transform transform)
    {
        Position = transform.position;
        Rotation = transform.rotation;
        LocalScale = transform.localScale;
    }
}

public static class TransformCopyHelper{
    public static void GetCopyData(this Transform original, TransformCopy copy)
    {
        original.position = copy.Position;
        original.rotation = copy.Rotation;
        original.localScale = copy.LocalScale;
    }
}