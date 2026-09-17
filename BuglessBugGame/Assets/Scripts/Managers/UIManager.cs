using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [HideInInspector]public UIJourney journey;


    void Awake()
    {
        journey = GetComponent<UIJourney>();
    }
}
