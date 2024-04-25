using System;
using System.Collections.Generic;
using System.Text;

namespace MillerInc.PhysicsEngine.Types
{
    /// <summary>
    /// The shape of an object (for 3D simulations); the number associated with each object 
    /// is the number of vertices for that object
    /// </summary>
    public enum Shape3D
    {
        Sphere = 0, 
        PointMass = 1, 
        UniformThinRod = 2,
        TriangularPrism = 4,
        Pyramid = 5,
        RectangularPrism = 8, 
        Cube = 8, 
    }

    /// <summary>
    /// The shape of an object (for 2D simulations); the number associated with each object 
    /// is the number of vertices for that object
    /// </summary>
    public enum Shape2D
    {
        Circle = 0, 
        PointMass = 1, 
        UniformThinRod = 2, 
        Triangle = 3, 
        EquilateralTriangle = 3, 
        Quadrilateral = 4,
        Rectangle = 4,
        Square = 4,
        Pentagon = 5,
        Hexagon = 6,
        Heptagon = 7, 
        Octagon = 8,
        Nonagon = 9,

    }
}
