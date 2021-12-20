using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Vehicles
{
    /// <summary>
    /// Vehicle controller
    /// </summary>
    public abstract class Vehicle: MonoBehaviour
    {
        /// <summary>
        /// floating point speed
        /// </summary>
        public float speed;

        /// <summary>
        /// image of vehicle
        /// </summary>
        public Sprite image
        {
            get
            {
                if(_image != null)
                {
                    return _image.sprite;
                }
                else
                {
                    return null;
                }
            }

            set
            {
                if(_image == null)
                {
                    _image = GetComponent<Image>();
                }

                _image.sprite = value;
            }
        }

        Image _image;

        /// <summary>
        /// virtual method to move right this vehicle
        /// </summary>
        public virtual void Drive()
        {
            transform.position += transform.right * Time.fixedDeltaTime * speed;
        }
    }
}
