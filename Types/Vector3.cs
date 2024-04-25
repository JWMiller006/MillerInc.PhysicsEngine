using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MillerInc.PhysicsEngine.Types
{
    public struct Vector3
    {
        double x;
        double y;
        double z;
        double length;

        public double X
        {
            readonly get { return x; }
            set { x = value; ComputeLength(); }
        }

        public double Y
        {
            readonly get { return y; }
            set { y = value; ComputeLength(); }
        }

        public double Z
        {
            readonly get { return z; }
            set { z = value; ComputeLength(); }
        }

        public double I { 
            readonly get { return x; }
            set { x = value; ComputeLength(); }
        }

        public double J {
            readonly get { return y; }
            set { y = value; ComputeLength(); }
        }

        public double K { readonly get { return z; }
            set { z = value; ComputeLength(); }
        }

        public double Length
        {
            get { return length; }
        }

        // Constructors

        public Vector3(double X, double Y, double Z)
        {
            x = X;
            y = Y;
            z = Z;
            ComputeLength();
        }

        public Vector3() : this(0, 0, 0) { }

        public Vector3(Vector3 v) : this(v.X, v.Y, v.Z) { }
        //


        /// <summary>

        /// Computes the length of the vector and stores it.

        /// This function is automatically called whenever one of

        /// the vector components is changed.

        /// </summary>

        void ComputeLength()
        {
            length = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(z, 2));
        }

        // Overloaded Operators

        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        public static Vector3 operator +(Vector3 v)
        {
            return new Vector3(v);
        }

        public static Vector3 operator -(Vector3 v)
        {
            return new Vector3(-v.X, -v.Y, -v.Z);
        }

        public static Vector3 operator *(Vector3 v1, double x)
        {
            return new Vector3(v1.X * x, v1.Y * x, v1.Z * x);
        }

        public static bool operator ==(Vector3 v1, Vector3 v2)
        {
            if (v1.X == v2.X && v1.Y == v2.Y && v1.Z == v2.Z)
                return true;
            else
                return false;
        }

        public override readonly bool Equals(object obj)
        {
            return (this == (Vector3)obj);
        }

        public override readonly int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator !=(Vector3 v1, Vector3 v2)
        {
            if (!(v1 == v2))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Computes the dot (scalar)-product of two 3-dimensional vectors
        /// </summary>
        public static double operator %(Vector3 v1, Vector3 v2)
        {
            return (v1.X * v2.X) + (v1.Y * v2.Y) + (v1.Z * v2.Z);
        }

        /// <summary>
        /// Computes the cross (vector)-product of two 3-dimensional vectors
        /// </summary>
        public static Vector3 operator *(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.Y * v2.Z - v1.Z * v2.Y, v1.X * v2.Z - v1.Z * v2.X, v1.X * v2.Y -
                v1.Y * v2.X);
        }

        /// <summary>
        /// Returns the unit vector of a given vector.
        /// </summary>
        public static Vector3 operator ~(Vector3 v)
        {
            return new Vector3(v.X / v.Length, v.Y / v.Length, v.Z / v.Length);
        }
        //


        // Basis vectors for 3d-coordinate system

        public static Vector3 BasisI
        {
            get { return new Vector3(1, 0, 0); }
        }
        public static Vector3 BasisJ
        {
            get { return new Vector3(0, 1, 0); }
        }
        public static Vector3 BasisK
        {
            get { return new Vector3(0, 0, 1); }
        }
        //


        public double this[long index]
        {
            readonly get
            {
                return index switch
                {
                    0 => x,
                    1 => y,
                    2 => z,
                    3 => length,
                    _ => throw new System.IndexOutOfRangeException
                                                ("Index must fall between 0 and 3"),
                };
            }
            set
            {
                switch (index)
                {
                    case 0:
                        x = value;
                        break;
                    case 1:
                        y = value;
                        break;
                    case 2:
                        z = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException
                            ("Index must fall between 0 and 2");
                }
                ComputeLength();
            }
        }

        public override string ToString()
        {
            return x.ToString() + " " + y.ToString() + " " + z.ToString();
        }
    }
}
