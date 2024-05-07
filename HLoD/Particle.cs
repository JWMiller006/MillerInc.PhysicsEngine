using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics; 

namespace MillerInc.PhysicsEngine.HLoD
{
    public class Particle
    {
        /// <summary>
        /// Creates a new default particle
        /// </summary>
        public Particle()
        {

        }

        #region

        /// <summary>
        /// The position relative to the origin
        /// </summary>
        public Vector3 Position { get; set; } = default;

        /// <summary>
        /// The charge of a particle in eV
        /// </summary>
        public int Charge { get; set; } = 0; 


        
        #endregion
    }
}
