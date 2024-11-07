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
    internal class Object3d
    {
        bool visibility;
        Color color;
        List<Vector3> coordList;
        float gravity = 0.5f;

        bool isGrounded = false;

        public Object3d(RandomUtils r)
        {
            visibility = true;
            color = r.GetRandomColor();
            coordList = new List<Vector3>();
            

            int size_offset = r.GetRandomInt(4,6);
            int height_offset = r.GetRandomInt(5,20);
            int radial_offset = r.GetRandomInt(5,30);

            coordList.Add(new Vector3(0 * size_offset + radial_offset, 0 * size_offset + height_offset, 0 * size_offset + radial_offset));
            coordList.Add(new Vector3(0 * size_offset + radial_offset, 0 * size_offset + height_offset, 1 * size_offset + radial_offset));
            coordList.Add(new Vector3(1 * size_offset + radial_offset, 0 * size_offset + height_offset, 0 * size_offset + radial_offset));
            coordList.Add(new Vector3(1 * size_offset + radial_offset, 0 * size_offset + height_offset, 1 * size_offset + radial_offset));
            coordList.Add(new Vector3(1 * size_offset + radial_offset, 1 * size_offset + height_offset, 0 * size_offset + radial_offset));
            coordList.Add(new Vector3(1 * size_offset + radial_offset, 1 * size_offset + height_offset, 1 * size_offset + radial_offset));
            coordList.Add(new Vector3(0 * size_offset + radial_offset, 1 * size_offset + height_offset, 0 * size_offset + radial_offset));
            coordList.Add(new Vector3(0 * size_offset + radial_offset, 1 * size_offset + height_offset, 1 * size_offset + radial_offset));
            coordList.Add(new Vector3(0 * size_offset + radial_offset, 0 * size_offset + height_offset, 0 * size_offset + radial_offset));
            coordList.Add(new Vector3(0 * size_offset + radial_offset, 0 * size_offset + height_offset, 1 * size_offset + radial_offset));


        }

        public void Draw()
        {
            GL.Begin(PrimitiveType.QuadStrip);
            GL.Color3(color);
            foreach(Vector3 v in coordList)
            {
                GL.Vertex3(v);
            }
            GL.End();
        }

        public void ToggleVisibility()
        {
            visibility = !visibility;
        }

        public void ApplyGravity()
        {
            if (!isGrounded)
            {
                for (int i = 0; i < coordList.Count; i++)
                {
                    coordList[i] += new Vector3(0, -gravity, 0);
                }
            }
        }

        public bool GetGrounded()
        {
            return isGrounded;
        }

        public void GroundCheck()
        {
            foreach(Vector3 v in coordList)
            {
                if(v.Y <= 0)
                {
                    isGrounded = true;
                }
            }
        }
    }
}
