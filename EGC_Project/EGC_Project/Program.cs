using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGC_Project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (Window3d window = new Window3d())
            {
                window.Run();
            }
        }
    }
}
