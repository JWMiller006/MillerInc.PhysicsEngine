using System;
using System.Collections.Generic;
using System.Text;

namespace MillerInc.PhysicsEngine.DisplayToolkit
{
    public class SimulateState
    {
        public SimulateState(List<PhysicsObject> objects)
        {
            this.Objects = objects;
        }

        public SimulateState()
        {
            this.Objects = [];
        }


        #region Fields 

        public List<PhysicsObject> Objects { get; set; }

        public static PrimaryProcessor RenderingProcessor { get; set; } = PrimaryProcessor.CPU;




        #endregion


        #region Methods

        public void Add(PhysicsObject obj)
        {
            this.Objects.Add(obj);
        }

        public void Remove(PhysicsObject obj)
        {
               this.Objects.Remove(obj);
        }

        public void ClearScene()
        {
            this.Objects.Clear();
        }

        public void Simulate(float timeStep)
        {
            if (PrimaryProcessor.CPU == RenderingProcessor)
            {
                foreach (PhysicsObject obj in this.Objects)
                {
                    obj.Simulate(timeStep);
                }
            }
            else if (PrimaryProcessor.GPU == RenderingProcessor)
            { 
                
            }
        }

        #endregion


    }

    public enum PrimaryProcessor
    {
        CPU,
        GPU, 
        Mixed, 
    }
}
