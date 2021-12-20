using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.View
{
    /// <summary>
    /// Vehicle name view in main view
    /// </summary>
    public class VehicleNameView: MonoBehaviour
    {
        /// <summary>
        /// Reference to vehicle name label
        /// </summary>
        [SerializeField] Text _nameLabel;

        /// <summary>
        /// Set text name of vehicle
        /// </summary>
        /// <param name="name">vehicle name string</param>
        public void SetVehicleName(string name)
        {
            _nameLabel.text = name;
        }
    }
}
