using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace MillerInc.PhysicsEngine.Forces
{
    public class Force
    {
        #region Initializers

        public Force()
        {
            this.ForceVector = new(); 
        }

        public Force(Vector3 forceVector)
        {
            this.ForceVector = forceVector;
        }

        #endregion

        #region

        public Vector3 ForceVector { get; set; }

        public float Magnitiude { get { return this.ForceVector.Length(); } }

        #endregion

    }
}
