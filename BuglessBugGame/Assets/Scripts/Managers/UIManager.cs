using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [HideInInspector]public UIJourney journey;
    [HideInInspector]public UIChalet lobby;


    void Awake()
    {
        journey = GetComponent<UIJourney>();
        lobby = GetComponent<UIChalet>();
    }
}
