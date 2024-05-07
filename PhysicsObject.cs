using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using MillerInc.Convert.Classes;
using MillerInc.PhysicsEngine.Forces;
using MillerInc.PhysicsEngine.Types;

namespace MillerInc.PhysicsEngine
{
    public class PhysicsObject : Serialization<PhysicsObject>
    {

        #region Initializers

        /// <summary>
        /// Creates a new physics object at the origin and at default rotation
        /// </summary>
        public PhysicsObject() 
        {
            this.Position = new();
            this.Rotation = new();
            this.Velocity = new();
            this.Acceleration = new();
            this.Omega = new();
            this.Alpha = new();
            this.ActiveForces = new Vector3[0].ToList(); ; 
        }

        /// <summary>
        /// Creates a new object at the position and rotation given
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        public PhysicsObject(Vector3 position, Quaternion rotation)
        {
            this.Position = position;
            this.Rotation = rotation;
            this.Velocity = new();
            this.Acceleration = new();
            this.Omega = new();
            this.Alpha = new();
            this.ActiveForces = new();
        }

        /// <summary>
        /// creates a new instance of a physics object by copying the values 
        ///  (without linking them) from the other instance
        /// </summary>
        /// <param name="other"></param>
        public PhysicsObject(PhysicsObject other)
        {
            this.CopyFrom(other); 
        }

        /// <summary>
        /// Creates a new instance of a physics object but copied from another saved instance
        /// </summary>
        /// <param name="filePath">path to the file containing the saved (in json format) class</param>
        public PhysicsObject(string filePath)
        {
            this.CopyFrom(filePath);
        }

        #endregion

        #region Fields

        /// <summary>
        /// The position of the center of mass
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// The translational velocity of the object
        /// </summary>
        public Vector3 Velocity { get; set; }

        /// <summary>
        /// The translational acceleration of the object
        /// </summary>
        public Vector3 Acceleration { get; set; }

        /// <summary>
        /// The orientation of the object
        /// </summary>
        public Quaternion Rotation { get; set; }

        /// <summary>
        /// The mass of the object
        /// </summary>
        public float Mass { get; set; }
        
        public float Inertia { get; set; }

        /// <summary>
        /// The rotational velocity of the object
        /// </summary>
        public Quaternion Omega { get; set; }

        /// <summary>
        /// The rotational acceleration of the object
        /// </summary>
        public Quaternion Alpha { get; set; }

        /// <summary>
        /// This length, width, and height of the object
        /// </summary>
        public Vector3 Size { get; set; }

        /// <summary>
        /// The forces that are actively applied to the system;
        ///  if the force is dependent on time, call update force
        /// </summary>
        public List<Vector3> ActiveForces { get; set; } 

        /// <summary>
        /// Returns the net force
        /// </summary>
        public Vector3 NetForce { get { return this.GetNetForce(); } }

        /// <summary>
        /// The timestamp when the object has this state
        /// </summary>
        public double Time { get; set; } = 0;

        /// <summary>
        /// The shape of the object, set as one of the pre-specified supported shapes; 3D
        /// </summary>
        public Shape3D Shape3D { get; set; } = Shape3D.PointMass;

        /// <summary>
        /// The shape of the object, set as one of the pre-specified supported shapes; 2D
        /// </summary>
        public Shape2D Shape2D { get; set; } = Shape2D.PointMass;

        /// <summary>
        /// The net charge of the object in Coulombs (assumed uniformly distributed) 
        /// </summary>
        public float Charge { get; set; } = 1; 

        #endregion

        #region Forces

        public void ApplyForce(Vector3 force)
        {
            this.ActiveForces.Add(force);
        }

        /// <summary>
        /// NOT IMPLEMENTED: the plan is if there is a variable force, 
        /// there has to be a way for it to know that it needs to update, but that just doesn't work
        /// </summary>
        public void UpdateForces()
        {
            GetNetForce();
            this.Acceleration = new(this.NetForce.X / this.Mass, 
                this.NetForce.Y / this.Mass, this.NetForce.Z / this.Mass); 
        }

        /// <summary>
        /// Calculates for the net force
        /// </summary>
        /// <returns>the net force</returns>
        public Vector3 GetNetForce()
        {
            Vector3 output = new(0f, 0f, 0f); 
            foreach (var force in this.ActiveForces)
            {
                output += force; 
            }
            return output; 
        }

        public bool IsPointInside(Vector3 point)
        {
            if (Shape3D == Shape3D.Cube)
            {
                //if ()
            }
            return false; 
        }

        #endregion

        #region Private Copying Methods

        /// <summary>
        /// Copies from the other object to the current object
        /// </summary>
        /// <param name="other">the object to copy from </param>
        private void CopyFrom(PhysicsObject other)
        {
            this.Inertia = other.Inertia;
            this.Mass = other.Mass;
            this.Size = other.Size;
            this.Position = other.Position;
            this.Velocity = other.Velocity;
            this.Alpha = other.Alpha;
            this.Acceleration = other.Acceleration;
            this.ActiveForces = MillerInc.Methods.Lists.Copy.CopyFrom(other.ActiveForces);
            this.Omega = other.Omega; 
            this.Rotation = other.Rotation;
        }

        /// <summary>
        /// Copies from the object that was saved in the file specified
        /// </summary>
        /// <param name="filePath">the path to the file that the object is saved within</param>
        private void CopyFrom(string filePath)
        {
            PhysicsObject other = MillerInc.Convert.Files.JSON_Converter.Deserialize<PhysicsObject>(filePath); 
            this.CopyFrom(other); 
        }

        #endregion
    }
}
