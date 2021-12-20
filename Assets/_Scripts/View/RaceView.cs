using Assets._Scripts.Vehicles;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class manages race view
/// </summary>
public class RaceView : MonoBehaviour
{
    /// <summary>
    /// back button
    /// </summary>
    [SerializeField] Button _backButton = null;

    /// <summary>
    /// vehicle names text
    /// </summary>
    [SerializeField] Text _namesText = null;

    /// <summary>
    /// vehicle prefab to spawn on race start
    /// </summary>
    [SerializeField] GameObject vehiclePrefab;

    /// <summary>
    /// spawned vehicle parent
    /// </summary>
    [SerializeField] Transform vehicleParent;

    /// <summary>
    /// restart race button game object
    /// </summary>
    [SerializeField] GameObject _restartRaceButtonObject = null;

    /// <summary>
    /// restart race button
    /// </summary>
    [SerializeField] Button _restartRaceButton = null;

    /// <summary>
    /// true if race started
    /// </summary>
    bool _raceStarted;

    /// <summary>
    /// List of vehicles controllers.
    /// </summary>
    List<Vehicle> _vehicles = new List<Vehicle>(3);

    private void Start()
    {
        _restartRaceButton = _restartRaceButtonObject.GetComponent<Button>();

        _backButton.onClick.AddListener(BackClick);
        _restartRaceButton.onClick.AddListener(OnRestartRaceButtonClick);
    }

    private void OnDestroy()
    {
        _backButton.onClick.RemoveAllListeners();
        _restartRaceButton.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// On back button click.
    /// </summary>
    private void BackClick()
    {
        gameObject.SetActive(false);
        _restartRaceButtonObject.SetActive(false);
    }

    /// <summary>
    /// On restart race button clicked method
    /// </summary>
    private void OnRestartRaceButtonClick()
    {
        Sprite sprite1 = _vehicles[0] != null ? _vehicles[0].image : null;
        Sprite sprite2 = _vehicles[1] != null ? _vehicles[1].image : null;
        Sprite sprite3 = _vehicles[2] != null ? _vehicles[2].image : null;

        Open(sprite1, sprite2, sprite3);
    }

    /// <summary>
    /// Open race window, create wehicles and starte race.
    /// </summary>
    /// <param name="sprite1">image for Truck</param>
    /// <param name="sprite2">image for Bus</param>
    /// <param name="sprite3">image for RaceCar</param>
    public void Open(Sprite sprite1, Sprite sprite2, Sprite sprite3)
    {
        _restartRaceButtonObject.SetActive(false);

        if (_vehicles.Count > 0)
        {
            foreach (var vehicle in _vehicles)
            {
                if(vehicle != null)
                    Destroy(vehicle.gameObject);
            }

            _vehicles.Clear();
        }

        var names = "";

        if (sprite1 != null)
        {
            names += $"{ sprite1.name }, ";

            InstantiateVehicle(typeof(Trucks), sprite1, -300);
        }
        else
        {
            _vehicles.Add(null);
        }

        if (sprite2 != null)
        {
            names += $"{ sprite2.name }, ";

            InstantiateVehicle(typeof(Bus), sprite2, 0);
        }
        else
        {
            _vehicles.Add(null);
        }

        if (sprite3 != null)
        {
            names += sprite3.name;

            InstantiateVehicle(typeof(RaceCar), sprite3, 300);
        }
        else
        {
            _vehicles.Add(null);
        }

        _namesText.text = names;

        gameObject.SetActive(true);

        _raceStarted = true;
    }

    void FixedUpdate()
    {
        if (_raceStarted)
        {
            for (int i = 0; i < _vehicles.Count; i++)
            {
                if(_vehicles[i] != null)
                {
                    _vehicles[i].Drive();
                }
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        _raceStarted = false;

        Vehicle vehicle = col.GetComponent<Vehicle>();
        if(vehicle != null)
        {
            Debug.Log($"{ vehicle.image.name } won");
        }

        _restartRaceButtonObject.SetActive(true);
    }

    /// <summary>
    /// Instantiate vehicle of specify type and image. Adds vehicle to vehicles list.
    /// </summary>
    /// <param name="vehicleType">Type of vehicle</param>
    /// <param name="image">Image for vehicle</param>
    /// <param name="yOffest">Y offset</param>
    /// <returns>Vehicle controller</returns>
    Vehicle InstantiateVehicle(Type vehicleType, Sprite image, int yOffest)
    {
        GameObject newObject = Instantiate(vehiclePrefab, new Vector2(0, vehicleParent.position.y + yOffest), Quaternion.identity, vehicleParent);

        Vehicle vehicleController = newObject.AddComponent<Trucks>();
        vehicleController.image = image;

        _vehicles.Add(vehicleController);

        return vehicleController;
    }
}
