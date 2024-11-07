using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGC_Project
{
    internal class Triangle3D
    {
        private Vector3 pointA, pointB, pointC;
        private Color color;
        private bool visibility;
        private float lineWidth;
        RandomUtils random;

        public Triangle3D(RandomUtils random)
        {
            pointA = new Vector3(0, 0, 0);
            pointB = new Vector3(0, 1, 0);
            pointC = new Vector3(1, 0, 0);
            color = Color.Red;
            visibility = true;
            lineWidth = 1;
            this.random = random;
        }

        public void Draw()
        {
            if (visibility)
            {
                GL.Begin(PrimitiveType.Triangles);
                GL.Color3(color);
                GL.Vertex3(pointA);
                GL.Vertex3(pointB);
                GL.Vertex3(pointC);
                GL.End();
            }
        }

        public void ToggleVisibility()
        {
            visibility = !visibility;
        }

        public void ChangeColor()
        {
            color = random.GetRandomColor();
        }
    }
}
