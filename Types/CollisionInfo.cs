using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace MillerInc.PhysicsEngine.Types
{
    /// <summary>
    /// An object that contains information about a collision, which is used to determine the results of a collision
    /// </summary>
    public class CollisionInfo
    {
        public bool Collided { get; set; }

        public Vector3 CollsionPoint { get; set; }

        public Vector3 Impulse { get; set; }

        public float Duration { get; set; }

        public PhysicsObject Object1 { get; set; }

        public PhysicsObject Object2 { get; set; }

        public CollisionInfo() { }

        public CollisionInfo(bool collided, Vector3 collisionPoint, Vector3 impulse)
        {
            this.Collided = collided;
            this.CollsionPoint = collisionPoint;
            this.Impulse = impulse;
        }

        /// <summary>
        /// NEEDS WORK
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        public void GetCollisionInfo(PhysicsObject obj1, PhysicsObject obj2, CollisionType collisionType)
        {
            this.Object1 = obj1;
            this.Object2 = obj2;

            // Not figured out until the bounds of the objects are figured out
            // Calculate the collision point
            
            // this.CollsionPoint = obj1.Position + obj1.RelativePosition;

            // Calculate the impulse
            Vector3 initialMomentum1 = obj1.Mass * obj1.Velocity;
            Vector3 initialMomentum2 = obj2.Mass * obj2.Velocity;
            Vector3 totalMomentum = initialMomentum1 + initialMomentum2;
            
            if (collisionType == CollisionType.Bounce)
            {
                this.Impulse = totalMomentum;

            }
            else if (collisionType == CollisionType.Slide)
            {
                this.Impulse = totalMomentum;
            }
            else if (collisionType == CollisionType.Stick)
            {
                // The only implemented collision type currently 5/7/2024
                this.Impulse = totalMomentum;
                Vector3 finalVelocity = this.Impulse / (obj1.Mass + obj2.Mass);
                obj1.Velocity = finalVelocity;
                obj2.Velocity = finalVelocity;
            }
            else
            {
                this.Impulse = Vector3.Zero;
            }
        }


        #region Overrides
        public override string ToString()
        {
            return $"Collided: {this.Collided}, Collision Point: {this.CollsionPoint}, Impulse: {this.Impulse}";
        }

        public override bool Equals(object obj)
        {
            if (obj is CollisionInfo)
            {
                CollisionInfo other = obj as CollisionInfo;
                return this.Collided == other.Collided && this.CollsionPoint == other.CollsionPoint && this.Impulse == other.Impulse;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(CollisionInfo a, CollisionInfo b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(CollisionInfo a, CollisionInfo b)
        {
            return !a.Equals(b);
        }

        #endregion

    }

    public enum CollisionType
    {
        None,
        Bounce,
        Slide,
        Stick
    }
}
