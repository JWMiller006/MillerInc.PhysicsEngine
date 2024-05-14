using ILGPU;
using ILGPU.Runtime;
using ILGPU.Runtime.Cuda;
using ILGPU.SharpDX.Cuda;
using System;
using System.Collections.Generic;
using System.Text;

namespace MillerInc.PhysicsEngine.DisplayToolkit
{
    public class GPUAccelerator
    {
        public static void MyKernel(Index1D index, ArrayView1D<float, Stride1D.Dense> a, ArrayView1D<float, Stride1D.Dense> b, ArrayView1D<float, Stride1D.Dense> result)
        {
            result[index] = a[index] * b[index];
        }

        public static void TestGPU()
        {
            Context context = Context.CreateDefault();
            foreach (var device in context.Devices)
            {
                Console.WriteLine(device.Name);
                Console.WriteLine();
                device.PrintInformation(Console.Out);

            }
            return; 
        }

        public static void RunGPU(string[] args)
        {
            using Context context = Context.Create(builder => builder.AllAccelerators().Default());
            foreach (Device device in context.Devices)
            {
                Console.WriteLine(device.Name);
            }
            //using var accelerator = new CudaAccelerator(context);

            var a = new float[1024];
            var b = new float[1024];
            var result = new float[1024];

            for (int i = 0; i < a.Length; i++)
            {
                a[i] = i;
                b[i] = i;
            }
            /*
            using var bufferA = accelerator.Allocate1D<float>(a.Length);
            using var bufferB = accelerator.Allocate1D<float>(b.Length);
            using var bufferResult = accelerator.Allocate1D<float>(result.Length);

            bufferA.CopyFromCPU(a.AsSpan());
            bufferB.CopyFromCPU(b.AsSpan());

            var myKernel = accelerator.LoadAutoGroupedStreamKernel<Index1, ArrayView1D<float, Stride1D.Dense>, ArrayView1D<float, Stride1D.Dense>, ArrayView1D<float, Stride1D.Dense>>(MyKernel);
            myKernel(bufferA.Length, bufferA.View, bufferB.View, bufferResult.View);

            accelerator.Synchronize();

            bufferResult.CopyToCPU(result.AsSpan());

            for (int i = 0; i < result.Length; i++)
            {
                Console.WriteLine(result[i]);
            }
            */
        }
    }
}
