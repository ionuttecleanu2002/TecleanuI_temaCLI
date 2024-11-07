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
    internal class Point3D
    {
        private Vector3 position;
        private bool visibility;
        private Color color;
        private float size;

        public Point3D(RandomUtils r)
        {
            position = new Vector3(0,0,0);
            visibility = true;
            color = r.GetRandomColor();
            size = 10.0f;
        }

        public void SwitchVisibility()
        {
            visibility = !visibility;
        }

        public void DrawPoint()
        {
            GL.PointSize(size);
            GL.Begin(PrimitiveType.Points);
            GL.Color3(color);
            GL.Vertex3(position);
            GL.End();

        }
    }
}
