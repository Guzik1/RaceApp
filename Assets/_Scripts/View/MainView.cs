using UnityEngine;
using UnityEngine.UI;

public class MainView : MonoBehaviour
{
    /// <summary>
    /// Image of truck
    /// </summary>
    [SerializeField] Image _image01 = null;

    /// <summary>
    /// Image of bus
    /// </summary>
    [SerializeField] Image _image02 = null;

    /// <summary>
    /// Image of race car
    /// </summary>
    [SerializeField] Image _image03 = null;

    /// <summary>
    /// start race button
    /// </summary>
    [SerializeField] Button _raceButton = null;

    /// <summary>
    /// race view controller class
    /// </summary>
    [SerializeField] RaceView _raceView = null;

    void Start()
    {
        _raceButton.onClick.AddListener(RaceClick);
    }

    private void OnDestroy()
    {
        _raceButton.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// Call on start race button click
    /// </summary>
    private void RaceClick()
    {
        _raceView.Open(_image01.sprite, _image02.sprite, _image03.sprite);
    }
}
