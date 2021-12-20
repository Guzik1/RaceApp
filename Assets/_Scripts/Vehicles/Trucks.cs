using System.Collections;
using UnityEngine;

namespace Assets._Scripts.Vehicles
{
    /// <summary>
    /// Class represent Truck vehicle, implement Vehicle
    /// </summary>
    public class Trucks : Vehicle
    {
        /// <summary>
        /// Flag of trailer, true if trailer be assigned
        /// </summary>
        public bool hasTrailer = false;

        void Start()
        {
            speed = Random.Range(70, 90);

            StartCoroutine(TrailerSimulation());
        }

        /// <summary>
        /// attrach trailer
        /// </summary>
        public void attrachTrailer()
        {
            if (!hasTrailer)
            {
                hasTrailer = true;

                speed -= 30f;
            }
        }

        /// <summary>
        /// deatach trailer
        /// </summary>
        public void detachTrailer()
        {
            if (hasTrailer)
            {
                hasTrailer = false;

                speed += 30f;
            }
        }

        /// <summary>
        /// trailer simulation, attrach trailer, wait for 7 second and deatach trailer
        /// </summary>
        /// <returns></returns>
        IEnumerator TrailerSimulation()
        {
            attrachTrailer();

            yield return new WaitForSeconds(7);

            detachTrailer();
        }
    }
}
