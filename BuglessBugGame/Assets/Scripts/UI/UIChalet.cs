using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIChalet : MonoBehaviour
{
    [Header("RefUI")]
    [SerializeField] public Button _goWalkButton;
    [SerializeField] public GameObject _parentLobbyUI;
    [SerializeField] public GameObject _parentMapUI;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _goWalkButton.onClick.AddListener(GoToMap);
    }

    private void GoToMap()
    {
        //a modifier plus tard pour aller sur l'UI map
        GameManager.instance.player.playerData.playerInventoryData.insects.Clear();
        _parentMapUI.SetActive(true);
        _parentLobbyUI.SetActive(false);
    }
}
