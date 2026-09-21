using TMPro;
using UnityEngine;

public class DebugMenu : MonoBehaviour
{
    public GameObject debugMenuRoot;
    [SerializeField] private TextMeshProUGUI fpsText;

    private float timer = 0f;
    private float updateInterval = 1f;
    private int frameCount = 0;
    
    
    void Start()
    {
        
    }
    void Update()
    {
        FpsCounter();
    }

    private void FpsCounter()
    {
        frameCount++;
        timer += Time.deltaTime;

        if (timer >= updateInterval)
        {
            float fps = 1.0f / Time.deltaTime;
            fpsText.text = (int)fps + "FPS: ";
            
            frameCount = 0;
            timer = 0f;
        }
    }
}