using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace MillerInc.PhysicsEngine.Kinematics
{
    /// <summary>
    /// All formulas listed in this section assume that the acceleration is constant
    /// </summary>
    public class KinematicsFormulas
    {
        /// <summary>
        /// Simulates ahead using constant acceleration and a set time
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="deltaTime"></param>
        /// <returns></returns>
        public static PhysicsObject SimulateAhead(PhysicsObject originalState, float deltaTime)
        {
            float t = deltaTime;
            PhysicsObject obj = new(originalState);
            Vector3 a = obj.Acceleration;
            Vector3 finalV = new(obj.Velocity.X + a.X * t,
                obj.Velocity.Y + a.Y * t, obj.Velocity.Y + a.Y * t);
            float xComp = GetComponentPos(obj.Velocity.X, t, obj.Acceleration.X);
            float yComp = GetComponentPos(obj.Velocity.Y, t, obj.Acceleration.Y);
            float zComp = GetComponentPos(obj.Velocity.Z, t, obj.Acceleration.Z);

            Vector3 deltaPos = new(xComp, yComp, zComp);
            Vector3 finalPos = obj.Position + deltaPos;

            obj.Velocity = finalV;
            obj.Position = finalPos;
            obj.Time += deltaTime; 
            return obj; 
        }

        /// <summary>
        /// Uses the formula pos = initV * t + 0.5 * a * t^2 to find a component of the velocity
        /// </summary>
        /// <param name="initVComp">the component of the velocity that is in the direction</param>
        /// <param name="t">difference in time</param>
        /// <param name="aComp">the constant acceleration for the object</param>
        /// <returns></returns>
        private static float GetComponentPos(float initVComp, float t, float aComp)
        {
            return (initVComp * t) + (0.5f * aComp * t * t); 
        }

        /// <summary>
        /// Gets a list of states that are occurring in the future
        /// </summary>
        /// <param name="originalState">the initial state of the object</param>
        /// <param name="timeStep">the time between each instance of the object</param>
        /// <param name="totalStates">the total amount of states calculated</param>
        /// <returns>a list of the states of the object over the interval</returns>
        public static List<PhysicsObject> GetObjectStates(PhysicsObject originalState, float timeStep, int totalStates)
        {
            List<PhysicsObject> outList = new();
            for (int i = 0; i < totalStates; i++)
            {
                outList.Add(SimulateAhead(originalState, timeStep * i)); 
            }
            return outList; 
        }
    }
}
