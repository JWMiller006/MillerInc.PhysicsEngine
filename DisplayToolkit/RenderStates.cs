using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using MillerInc.PhysicsEngine.Types;
using System.Drawing.Drawing2D;

namespace MillerInc.PhysicsEngine.DisplayToolkit
{
    /// <summary>
    /// In development, may not be included in this library
    /// </summary>
    public class RenderStates
    {
        /// <summary>
        /// Generates a list of bitmaps that contain 
        /// </summary>
        /// <param name="states"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static List<Bitmap> GenerateDisplayList(List<PhysicsObject> states, Color color, float penSize = 2.5f)
        {
            List<Bitmap> images = new();
            int width, height;
            float smallestX, maxY;
            ((width, height), (smallestX, maxY)) = GetMaxFrameSize(states);
            foreach (PhysicsObject state in states)
            {
                images.Add(RenderState(state, width, height, color, penSize, smallestX)); 
            }
            return images;
        }

        public static Bitmap RenderState(PhysicsObject state, int width, int height, 
            Color color, float penSize, float smallestXPoint = 0)
        {
            Bitmap bits = new(width, height);
            Graphics canvas = Graphics.FromImage(bits);
            Pen drawingUtensil = new(color, penSize);
            if (state.Shape3D == Shape3D.PointMass)
            {
                if (state.Shape2D == Shape2D.PointMass)
                {
                    float x, y;
                    (x, y) = GetRelPos(state, smallestXPoint, height);
                    try
                    {
                        bits.SetPixel((int)x, (int)y, color);
                    }
                    catch { }
                    try
                    {
                        bits.SetPixel((int)x + 1, (int)y, color);
                    }
                    catch { }
                    try
                    {
                        bits.SetPixel((int)x + 1, (int)y + 1, color);
                    }
                    catch { }
                    try
                    {
                        bits.SetPixel((int)x, (int)y + 1, color);
                    }
                    catch
                    {

                    }
                }
            }
            return bits; 
        }

        public static (float, float) GetRelPos(PhysicsObject state, float minX, float maxY)
        {
            return (state.Position.X - minX, maxY - state.Position.Y); 
        }

        /// <summary>
        /// Gets the max size of the frame necessary to fit all of the objs of the object
        /// </summary>
        /// <param name="states">the collection of states of the object</param>
        /// <returns>the integer values of the maximum necessary size of the frame</returns>
        public static ((int, int), (float, float)) GetMaxFrameSize(IEnumerable<PhysicsObject> states)
        {
            float maxX, maxY, minX, minY, difX, difY;
            ((minX, minY), (maxX, maxY)) = GetBounds(states); 
            difX = maxX - minX;
            difY = maxY - minY;
            difX *= 1.1f;
            difY *= 1.1f;
            if (difX < 1)
            {
                difX = 2; 
            }
            if (difY < 1)
            {
                difY = 2;
            }
            return (((int)difX, (int)difY), (minX, minY)); 
        }

        /// <summary>
        /// Finds the maximum and minimum x and y coordinates
        /// </summary>
        /// <param name="states">the collection of states</param>
        /// <returns>(minimum X, minimum Y), (maximum X, maximum Y)</returns>
        public static ((float, float), (float, float)) GetBounds(IEnumerable<PhysicsObject> states) 
        {
            float maxX, maxY, minX, minY;
            List<PhysicsObject> objs = states.ToList();  
            maxX = objs[0].Position.X;
            minX = objs[0].Position.X; 
            maxY = objs[0].Position.Y; 
            minY = objs[0].Position.Y;

            for (int i = 1; i < objs.Count; i++)
            {
                if (objs[i].Position.X > maxX)
                {
                    maxX = objs[i].Position.X; 
                }
                if (objs[i].Position.X < minX)
                {
                    minX = objs[i].Position.X;
                }
                if (objs[i].Position.Y > maxY)
                {
                    maxY = objs[i].Position.Y; 
                }
                if (objs[i].Position.Y < minY)
                {
                    minY = objs[i].Position.Y;
                }
            }

            return ((minX, minY), (maxX, maxY));
        }
    }
}
