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

        public Force(Vector3 forceVector, float timeApplied)
        {
            this.ForceVector = forceVector;
            this.TimeRemaining = timeApplied;
        }

        /// <summary>
        /// creates a new force with the given vector and time applied and the point applied, if the point applied is not given, it is assumed to be the origin
        /// </summary>
        /// <param name="forceVector"></param>
        /// <param name="timeApplied"></param>
        /// <param name="pointApplied"></param>
        public Force(Vector3 forceVector, float timeApplied, Vector3 pointApplied)
        {
            this.ForceVector = forceVector;
            this.TimeRemaining = timeApplied;
            this.PointApplied = pointApplied;
        }

        #endregion

        #region Fields 

        /// <summary>
        /// The full force vector
        /// </summary>
        public Vector3 ForceVector { get; set; }

        /// <summary>
        /// The magnitude of the force
        /// </summary>
        public float Magnitiude { get { return this.ForceVector.Length(); } }

        /// <summary>
        /// The amount of time the force has left to be applied
        /// </summary>
        public float TimeRemaining { get; set; } = float.PositiveInfinity;

        /// <summary>
        /// The point on the object where the force is applied
        /// </summary>
        public Vector3 PointApplied { get; set; } = new();

        /// <summary>
        /// The torque applied by the force
        /// </summary>
        public Vector3 Torque { get { return Vector3.Cross(this.PointApplied, this.ForceVector); } }


        #endregion

        #region Operators

        public static Force operator +(Force a, Force b)
        {
            return new(a.ForceVector + b.ForceVector);
        }

        public static Force operator -(Force a, Force b)
        {
            return new(a.ForceVector - b.ForceVector);
        }

        public static Force operator *(Force a, float b)
        {
            return new(a.ForceVector * b);
        }

        public static Force operator /(Force a, float b)
        {
            return new(a.ForceVector / b);
        }

        public static Force operator *(float a, Force b)
        {
            return new(a * b.ForceVector);
        }

        public static bool operator ==(Force a, Force b)
        {
            return a.ForceVector == b.ForceVector;
        }

        public static bool operator !=(Force a, Force b)
        {
            return a.ForceVector != b.ForceVector;
        }

        public static bool operator >(Force a, Force b)
        {
            return a.Magnitiude > b.Magnitiude;
        }

        public static bool operator <(Force a, Force b)
        {
            return a.Magnitiude < b.Magnitiude;
        }

        public static bool operator >=(Force a, Force b)
        {
            return a.Magnitiude >= b.Magnitiude;
        }

        public static bool operator <=(Force a, Force b)
        {
            return a.Magnitiude <= b.Magnitiude;
        }

        public static implicit operator Vector3(Force a)
        {
            return a.ForceVector;
        }

        #endregion

    }
}
