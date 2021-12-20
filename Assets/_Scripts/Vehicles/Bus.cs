using UnityEngine;

namespace Assets._Scripts.Vehicles
{    
    /// <summary>
     /// Class represent Bus vehicle, implement Vehicle
     /// </summary>
    public class Bus : Vehicle
    {
        /// <summary>
        /// number of bus line
        /// </summary>
        public int lineNumber;

        void Start()
        {
            speed = Random.Range(40, 90);
            lineNumber = Random.Range(1, 100);
        }

        /// <summary>
        /// Write line number on console.
        /// </summary>
        public void Board()
        {
            Debug.Log(lineNumber);
        }
    }
}
