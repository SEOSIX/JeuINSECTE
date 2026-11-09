using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TransitionManager : MonoBehaviour
{
    static TransitionManager instance;
    public static TransitionManager Instance
    {
        get
        {
            if (instance == null) instance = new GameObject("TransitionManager").AddComponent<TransitionManager>();
            return instance;
        }
    }

    public float duration = 0.8f;

    GameObject canvas;
    RectTransform hole;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        canvas = new GameObject("IrisCanvas", typeof(Canvas), typeof(GraphicRaycaster));
        canvas.transform.SetParent(transform);
        canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.GetComponent<Canvas>().sortingOrder = 999;
        
        hole = NewImage(canvas.transform);
        hole.GetComponent<Image>().sprite = CreateHoleSprite();
        
        foreach (var dir in new[] { Vector2.left, Vector2.right, Vector2.up, Vector2.down })
        {
            var panel = NewImage(hole);
            panel.anchorMin = panel.anchorMax = new Vector2(0.5f, 0.5f) + dir / 2;
            panel.pivot = new Vector2(0.5f, 0.5f) - dir / 2;
            panel.sizeDelta = Vector2.one * 20000;
        }

        canvas.SetActive(false);
    }

    static RectTransform NewImage(Transform parent)
    {
        var go = new GameObject("Img", typeof(Image));
        go.transform.SetParent(parent, false);
        go.GetComponent<Image>().color = Color.black;
        return go.GetComponent<RectTransform>();
    }

    static Sprite CreateHoleSprite()
    {
        const int s = 256;
        var tex = new Texture2D(s, s);
        for (int y = 0; y < s; y++)
            for (int x = 0; x < s; x++)
                tex.SetPixel(x, y, new Color(1, 1, 1, Vector2.Distance(new Vector2(x, y), Vector2.one * s / 2) > s / 2 ? 1 : 0));
        tex.Apply();
        return Sprite.Create(tex, new Rect(0, 0, s, s), Vector2.one / 2);
    }

    public Coroutine IrisOut() => StartCoroutine(Animate(1, 0));
    public Coroutine IrisIn() => StartCoroutine(Animate(0, 1));

    public void LoadScene(string scene, Action onLoaded = null) => StartCoroutine(Load(scene, onLoaded));

    IEnumerator Load(string scene, Action onLoaded)
    {
        yield return Animate(1, 0);
        yield return SceneManager.LoadSceneAsync(scene);
        onLoaded?.Invoke();
        yield return Animate(0, 1);
    }

    IEnumerator Animate(float from, float to)
    {
        canvas.SetActive(true);
        float max = canvas.GetComponent<RectTransform>().rect.size.magnitude;

        for (float t = 0; t < 1; t += Time.unscaledDeltaTime / duration)
        {
            hole.sizeDelta = Vector2.one * Mathf.SmoothStep(from, to, t) * max;
            yield return null;
        }

        hole.sizeDelta = Vector2.one * to * max;
        canvas.SetActive(to == 0);
    }
}