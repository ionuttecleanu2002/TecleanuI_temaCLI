using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGC_Project
{
    class Window3d : GameWindow
    {
        RandomUtils random;
        KeyboardState previouseKeyboard;
        //Point3D point;
        //Triangle3D triangle;

        Vector3 cameraPosition = new Vector3(50, 20, 50); 
        Vector3 cameraTarget = Vector3.Zero;
        float cameraSpeed = 15f;

        List<Object3d> objList;


        public Window3d() : base(800, 600, new GraphicsMode(32, 24, 0, 8))
        {
            VSync = VSyncMode.On;
            random = new RandomUtils();
            objList = new List<Object3d>();

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(Color.DeepSkyBlue);

            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);

            GL.Hint(HintTarget.PointSmoothHint, HintMode.Nicest);
            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            double aspect_ratio = Width / (double)Height;

            //Viewport
            GL.Viewport(0, 0, Width, Height);

            //Perspective
            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)aspect_ratio , 1, 256);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            //LOGIC BLOCK
            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();

            if (keyboard[Key.Escape])
            {
                Exit();
            }
            if (keyboard[Key.F] && !previouseKeyboard[Key.F])
            {
                Object3d obj = new Object3d(random);
                objList.Add(obj);
            }
            //Camera
            Vector3 forward = Vector3.Normalize(cameraTarget - cameraPosition); 
            Vector3 right = Vector3.Normalize(Vector3.Cross(forward, Vector3.UnitY)); 

            if (keyboard[Key.W])
            {
                cameraPosition += forward * cameraSpeed * (float)e.Time;
                cameraTarget += forward * cameraSpeed * (float)e.Time;
            }
            if (keyboard[Key.S])
            {
                cameraPosition -= forward * cameraSpeed * (float)e.Time;
                cameraTarget -= forward * cameraSpeed * (float)e.Time;
            }
            if (keyboard[Key.A])
            {
                cameraPosition -= right * cameraSpeed * (float)e.Time;
                cameraTarget -= right * cameraSpeed * (float)e.Time;
            }
            if (keyboard[Key.D])
            {
                cameraPosition += right * cameraSpeed * (float)e.Time;
                cameraTarget += right * cameraSpeed * (float)e.Time;
            }
            if (keyboard[Key.E])
            {
                cameraPosition += Vector3.UnitY * cameraSpeed * (float)e.Time;
                cameraTarget += Vector3.UnitY * cameraSpeed * (float)e.Time;
            }
            if (keyboard[Key.Q])
            {
                cameraPosition -= Vector3.UnitY * cameraSpeed * (float)e.Time;
                cameraTarget -= Vector3.UnitY * cameraSpeed * (float)e.Time;
            }

            // Update the previous keyboard state
            previouseKeyboard = keyboard;

            // Update camera lookat matrix
            Matrix4 lookat = Matrix4.LookAt(cameraPosition, cameraTarget, Vector3.UnitY);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);

        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);

            //RENDER BLOCK
            foreach(Object3d obj in objList)
            {
                obj.Draw();
                obj.GroundCheck();
                obj.ApplyGravity();
            }
            //RENDER END

            SwapBuffers();
        }


    }
}
