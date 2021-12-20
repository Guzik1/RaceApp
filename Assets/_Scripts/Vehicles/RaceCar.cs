using System.Collections;
using UnityEngine;

namespace Assets._Scripts.Vehicles
{
    /// <summary>
    /// Class represent Race Car vehicle, implement Vehicle
    /// </summary>
    public class RaceCar : Vehicle
    {
        /// <summary>
        /// Flag represent electrical car, true if race car is electric
        /// </summary>
        public bool isElectric;

        /// <summary>
        /// Flag represent nitro available status, true if nitro is available to use
        /// </summary>
        public bool hasNitro;

        void Start()
        {
            speed = Random.Range(80, 170);

            ActivateNitro();
        }

        /// <summary>
        /// Activate nitro if is available
        /// </summary>
        public void ActivateNitro()
        {
            if (hasNitro)
            {
                hasNitro = false;

                StartCoroutine(NitroLoop());
            }
        }

        /// <summary>
        /// Nitro loop, set speed to +40, wait for 4 seconds and set speed to -40
        /// </summary>
        /// <returns></returns>
        IEnumerator NitroLoop()
        {
            speed += 40f;

            yield return new WaitForSeconds(4f);

            speed -= 40f;
        }
    }
}
