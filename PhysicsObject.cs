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
            this.AngularVelocity = new();
            this.AngularAcceleration = new();
            this.ActiveForces = []; 
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
            this.AngularVelocity = new();
            this.AngularAcceleration = new();
            this.ActiveForces = [];
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
        /// The name of the object
        /// </summary>
        public string ObjectName { get; set; } = "Default Object";

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
        
        public float MomentOfInertia { get; set; }

        /// <summary>
        /// The rotational velocity of the object
        /// </summary>
        public Vector3 AngularVelocity { get; set; }

        /// <summary>
        /// The rotational acceleration of the object
        /// </summary>
        public Vector3 AngularAcceleration { get; set; }

        /// <summary>
        /// This length, width, and height of the object
        /// </summary>
        public Vector3 Size { get; set; }

        /// <summary>
        /// The forces that are actively applied to the system;
        ///  if the force is dependent on time, call update force
        /// </summary>
        public List<Force> ActiveForces { get; set; } 

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

        /// <summary>
        /// Applies the force to the object for infinite time
        /// </summary>
        /// <param name="force"></param>
        public void ApplyForce(Vector3 force)
        {
            this.ActiveForces.Add(new(force));
        }
        
        /// <summary>
        /// Applies a force to the object for a certain amount of time
        /// </summary>
        /// <param name="force"></param>
        /// <param name="timeApplied"></param>
        public void ApplyForce(Vector3 force, float timeApplied)
        {
            this.ActiveForces.Add(new(force, timeApplied));
        }

        /// <summary>
        /// Update the forces as of current, should be called when a force is added or removed
        /// </summary>
        public void UpdateForces(float timeStep)
        {
            GetNetForce();
            this.Acceleration = new(this.NetForce.X / this.Mass, 
                this.NetForce.Y / this.Mass, this.NetForce.Z / this.Mass); 
            for (int i = 0; i < this.ActiveForces.Count; i++)
            {
                this.ActiveForces[i].TimeRemaining -= timeStep;
            }
        }

        /// <summary>
        /// Calculates for the net force
        /// </summary>
        /// <returns>the net force</returns>
        public Vector3 GetNetForce()
        {
            for (int i = 0; i < this.ActiveForces.Count; i++)
            {
                if (this.ActiveForces[i].ForceVector.Length() == 0)
                {
                    this.ActiveForces.RemoveAt(i);
                }
                else if (this.ActiveForces[i].TimeRemaining <= 0)
                {
                    this.ActiveForces.RemoveAt(i);
                }
            }
            Vector3 output = new(0f, 0f, 0f); 
            foreach (var force in this.ActiveForces)
            {
                output += force; 
            }
            return output; 
        }

        public Vector3 GetNetTorque()
        {
            Vector3 output = new(0f, 0f, 0f);
            foreach (var force in this.ActiveForces)
            {
                output += force.Torque;
            }
            return output;
        }
        
        /// <summary>
        /// NOT FINISHED YET, IN DEVELOPMENT
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool IsPointInside(Vector3 point)
        {
            if (Shape3D == Shape3D.Cube)
            {
                //if ()
            }
            return false; 
        }

        /// <summary>
        /// NOT FINISHED YET, IN DEVELOPMENT
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public CollisionInfo Collide(PhysicsObject other)
        {
            CollisionInfo collisionInfo = new();
            collisionInfo.Collided = true; 
            collisionInfo.GetCollisionInfo(this, other, CollisionType.Stick);
            return collisionInfo;
        }

        #endregion

        #region Simulation

        /// <summary>
        /// Simulates one time step of the object (WARNING: THIS IS MEANT ONLY FOR SMALL TIME STEPS SINCE THE FORCES 
        /// ARE ASSUMED AS CONSTANT OVER THE TIMEFRAME; To avoid this, use HLoD object, this will require more processing power, 
        /// but will be more accurate)
        /// </summary>
        /// <param name="timeStep"></param>
        /// <returns>this object in its new state; is not necessary since it does save its state into itself</returns>
        public PhysicsObject Simulate(float timeStep)
        {
            this.Time += timeStep;
            this.UpdateForces(timeStep);
            this.Acceleration = this.NetForce / this.Mass;
            this.Velocity += this.Acceleration * timeStep;
            this.Position += this.Velocity * timeStep;
            this.AngularAcceleration = this.GetNetTorque() / this.MomentOfInertia; 
            this.AngularVelocity += this.AngularAcceleration * timeStep;
            Vector3 temp = new(this.AngularVelocity.X, this.AngularVelocity.Y, this.AngularVelocity.Z);
            this.Rotation = Quaternion.CreateFromAxisAngle(temp, this.AngularVelocity.Length() * timeStep) * this.Rotation;
            return this; 
        }

        #endregion

        #region Private Copying Methods

        /// <summary>
        /// Copies from the other object to the current object
        /// </summary>
        /// <param name="other">the object to copy from </param>
        private void CopyFrom(PhysicsObject other)
        {
            this.MomentOfInertia = other.MomentOfInertia;
            this.Mass = other.Mass;
            this.Size = other.Size;
            this.Position = other.Position;
            this.Velocity = other.Velocity;
            this.AngularAcceleration = other.AngularAcceleration;
            this.Acceleration = other.Acceleration;
            this.ActiveForces = MillerInc.Methods.Lists.Copy.CopyFrom(other.ActiveForces);
            this.AngularVelocity = other.AngularVelocity; 
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


        public override string ToString()
        {
            return $"{this.ObjectName}: at position {this.Position} at rotation of {this.Rotation} moving with velocity {this.Velocity} at an acceleration of " +
                $"{this.Acceleration}. This object as a total of {this.ActiveForces.Count} forces acting on it with a net force of " +
                $"{this.NetForce}, and is rotating at an angular speed of {this.AngularVelocity} at an angular acceleration of {this.AngularAcceleration}." +
                $" Mass of {this.Mass} and net charge of {this.Charge} in Coulombs "; 
        }
        #endregion
    }
}
