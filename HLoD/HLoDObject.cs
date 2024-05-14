using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

namespace MillerInc.PhysicsEngine.HLoD
{
    public class HLoDObject : PhysicsEngine.PhysicsObject
    {
        public HLoDObject() 
        {
            
        }

        public void FromPhysicsObject(PhysicsObject obj)
        {
            this.Position = obj.Position;
            this.Rotation = obj.Rotation;
            this.Velocity = obj.Velocity;
            this.Acceleration = obj.Acceleration;
            this.AngularVelocity = obj.AngularVelocity;
            this.AngularAcceleration = obj.AngularAcceleration;
            this.ActiveForces = obj.ActiveForces;
            this.Mass = obj.Mass;
            this.MomentOfInertia = obj.MomentOfInertia; 
            // this.CollisionBounds = obj.CollisionBounds;
        }

        #region Attributes

        #region Electricity & Magnetism

        /// <summary>
        /// Represents the placement of charge per millimeter cubed (meter * 10^(-4)) (volume); 
        /// default is 1 millimeter cubed of neutral charge (in Coulombs)
        /// </summary>
        List<List<List<float>>> ChargeDistribution { get; set; } = DefaultChargeDist(1, 1, 1);

        /// <summary>
        /// Creates a rectangular prism of neutral charge
        /// </summary>
        /// <param name="x">width</param>
        /// <param name="y">height</param>
        /// <param name="z">depth</param>
        /// <returns>the default distribution of 0 charge</returns>
        public static List<List<List<float>>> DefaultChargeDist(int x, int y, int z)
        {
            List<List<List<float>>> output = [];
            for (int width = 0; width < x; width++)
            {
                output.Add([]);
                for (int height = 0; height < y; height++)
                {
                    output[width].Add([]); 
                    for (int depth = 0; depth < z; depth++)
                    {
                        output[width][height].Add(0); 
                    }
                }
            }
            return output; 
        }

        /// <summary>
        /// Calculates the net charge based on the current charge distribution
        /// </summary>
        /// <returns>the net charge</returns>
        public float CalcNetCharge()
        {
            float netCharge = 0f; 
            foreach (var listX in ChargeDistribution)
            {
                foreach (var listY in listX)
                {
                    netCharge += listY.Sum(); 
                }
            }
            this.Charge = netCharge; 
            return netCharge; 
        }

        /// <summary>
        /// Gets the electric field at 
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public float GetElectricField(Vector3 relativePos)
        {
            // if (IsPointIn())
            //{

            //}
            return 0f; 
        }

        #endregion

        #endregion
    }
}
