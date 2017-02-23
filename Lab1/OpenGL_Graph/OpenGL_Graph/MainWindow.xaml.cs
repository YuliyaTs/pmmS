using System;
using System.Windows;
using SharpGL;
using SharpGL.SceneGraph;

namespace OpenGL_Graph
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields

        float rotatePyramid = 0;
        float rquad = 0;

        #endregion

        #region Constructors

        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Mehods

        private void OpenGLControl_OpenGLInitialized(object sender, OpenGLEventArgs args)
        {
            //  Enable the OpenGL depth testing functionality.
            args.OpenGL.Enable(OpenGL.GL_DEPTH_TEST);
        }

        private void OpenGLControl_Resized(object sender, OpenGLEventArgs args)
        {
            // Get the OpenGL instance.
            OpenGL gl = args.OpenGL;

            // Load and clear the projection matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();

            // Perform a perspective transformation
            gl.Perspective(45.0f, (float)gl.RenderContextProvider.Width /
                (float)gl.RenderContextProvider.Height,
                0.1f, 100.0f);

            // Load the modelview.
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }

        private void DrawGraphics(object sender, OpenGLEventArgs args)
        {
            #region Setting Open GL
            //  Get the OpenGL instance that's been passed to us.
            OpenGL gl = args.OpenGL;

            //  Clear the color and depth buffers.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            gl.ShadeModel(OpenGL.GL_SMOOTH);         // Set Smooth Shading                 
            gl.ClearColor(1.0f, 1.0f, 1.0f, 0.5f);     // BackGround Color

            //  Reset the modelview matrix.
            gl.LoadIdentity();

            //  Move the geometry into a fairly central position.
            gl.Translate(0.0f, 0.0f, -40.0f);

            #endregion

            #region Drawing grid

            gl.LineWidth(0.5f);
            gl.Begin(OpenGL.GL_LINES);
            for (double i = -35; i < 35; i += 1)
            {
                gl.Color(0.7f, 0.7f, 0.7f);
                gl.Vertex(i, -18f);
                gl.Vertex(i, 18f);
            }
            for (double i = -20; i < 20; i += 1)
            {
                gl.Vertex(-32, i);
                gl.Vertex(32, i);
            }
            gl.End();

            #endregion

            #region Drawing axes

            gl.LineWidth(2f);
            gl.Begin(OpenGL.GL_LINES);

            gl.Color(0.0f, 0.0f, 0.0f);
            gl.Vertex(0, -15f);
            gl.Vertex(0, 15f);

            gl.Vertex(-30f, 0);
            gl.Vertex(30f, 0);

            gl.Vertex(-0.3f, 1);
            gl.Vertex(0.3f, 1);

            gl.Vertex(1, -0.3);
            gl.Vertex(1, 0.3);

            gl.End();

            #endregion

            #region Start drawing triangles.

            gl.Begin(OpenGL.GL_TRIANGLES);

            gl.Vertex(0, 16f);
            gl.Vertex(0.5, 15f);
            gl.Vertex(-0.5, 15f);

            gl.Vertex(31f, 0);
            gl.Vertex(30f, -0.5);
            gl.Vertex(30f, 0.5);

            gl.End();

            #endregion

            #region Drawing labels

            gl.DrawText((int)ActualWidth/2 + 10, (int)ActualHeight  - 80, 0f, 0f, 0f, "Arial", 23f, "Y");
            gl.DrawText((int)ActualWidth - 60, (int)ActualHeight / 2, 0f, 0f, 0f, "Arial", 23f, "X");
            gl.DrawText((int)ActualWidth/2 - 25, (int)ActualHeight / 2 - 40, 0f, 0f, 0f, "Arial", 18f, "0");

            gl.DrawText((int)ActualWidth / 2 + 7, (int)ActualHeight / 2 - 42, 0f, 0f, 0f, "Arial", 18f, "1");
            gl.DrawText((int)ActualWidth / 2, (int)ActualHeight / 2 - 2, 0f, 0f, 0f, "Arial", 18f, "1");

            #endregion

            #region Drawing function
            gl.PointSize(3);
            gl.Begin(OpenGL.GL_POINTS);
            for (double i = -8; i < 8; i += 0.001)
            {
                var x = i;
                var y = Math.Pow(x, 3.0) + 3;
                if (y > 0)
                    gl.Color(1.0f, 0.0f, 0.0f);
                else
                    gl.Color(0.0f, 0.0f, 1.0f);

                gl.Vertex(x, y);
            }
            gl.End();
            #endregion

            gl.Flush();

            #region Help
            /*
             // Move the geometry into a fairly central position.
            gl.Translate(-1.5f, 0.0f, -6.0f);

             // Draw a pyramid. First, rotate the modelview matrix.
            gl.Rotate(rotatePyramid, 0.0f, 1.0f, 0.0f);

             // Start drawing triangles.
            gl.Begin(OpenGL.GL_TRIANGLES);


            gl.Vertex(0.0f, 1.0f, 0.0f);
            gl.Color(0.0f, 1.0f, 0.0f);
            gl.Vertex(-1.0f, -1.0f, 1.0f);
            gl.Color(0.0f, 0.0f, 1.0f);
            gl.Vertex(1.0f, -1.0f, 1.0f);

            gl.Color(1.0f, 0.0f, 0.0f);
            gl.Vertex(0.0f, 1.0f, 0.0f);
            gl.Color(0.0f, 0.0f, 1.0f);
            gl.Vertex(1.0f, -1.0f, 1.0f);
            gl.Color(0.0f, 1.0f, 0.0f);
            gl.Vertex(1.0f, -1.0f, -1.0f);

            gl.Color(1.0f, 0.0f, 0.0f);
            gl.Vertex(0.0f, 1.0f, 0.0f);
            gl.Color(0.0f, 1.0f, 0.0f);
            gl.Vertex(1.0f, -1.0f, -1.0f);
            gl.Color(0.0f, 0.0f, 1.0f);
            gl.Vertex(-1.0f, -1.0f, -1.0f);

            gl.Color(1.0f, 0.0f, 0.0f);
            gl.Vertex(0.0f, 1.0f, 0.0f);
            gl.Color(0.0f, 0.0f, 1.0f);
            gl.Vertex(-1.0f, -1.0f, -1.0f);
            gl.Color(0.0f, 1.0f, 0.0f);
            gl.Vertex(-1.0f, -1.0f, 1.0f);

            gl.End();
            
            //  Reset the modelview.
            gl.LoadIdentity();

            //  Move into a more central position.
            gl.Translate(1.5f, 0.0f, -7.0f);

            //  Rotate the cube.
            gl.Rotate(rquad, 1.0f, 1.0f, 1.0f);

            //  Provide the cube colors and geometry.
            gl.Begin(OpenGL.GL_QUADS);

            gl.Color(0.0f, 1.0f, 0.0f);
            gl.Vertex(1.0f, 1.0f, -1.0f);
            gl.Vertex(-1.0f, 1.0f, -1.0f);
            gl.Vertex(-1.0f, 1.0f, 1.0f);
            gl.Vertex(1.0f, 1.0f, 1.0f);

            gl.Color(1.0f, 0.5f, 0.0f);
            gl.Vertex(1.0f, -1.0f, 1.0f);
            gl.Vertex(-1.0f, -1.0f, 1.0f);
            gl.Vertex(-1.0f, -1.0f, -1.0f);
            gl.Vertex(1.0f, -1.0f, -1.0f);

            gl.Color(1.0f, 0.0f, 0.0f);
            gl.Vertex(1.0f, 1.0f, 1.0f);
            gl.Vertex(-1.0f, 1.0f, 1.0f);
            gl.Vertex(-1.0f, -1.0f, 1.0f);
            gl.Vertex(1.0f, -1.0f, 1.0f);

            gl.Color(1.0f, 1.0f, 0.0f);
            gl.Vertex(1.0f, -1.0f, -1.0f);
            gl.Vertex(-1.0f, -1.0f, -1.0f);
            gl.Vertex(-1.0f, 1.0f, -1.0f);
            gl.Vertex(1.0f, 1.0f, -1.0f);

            gl.Color(0.0f, 0.0f, 1.0f);
            gl.Vertex(-1.0f, 1.0f, 1.0f);
            gl.Vertex(-1.0f, 1.0f, -1.0f);
            gl.Vertex(-1.0f, -1.0f, -1.0f);
            gl.Vertex(-1.0f, -1.0f, 1.0f);

            gl.Color(1.0f, 0.0f, 1.0f);
            gl.Vertex(1.0f, 1.0f, -1.0f);
            gl.Vertex(1.0f, 1.0f, 1.0f);
            gl.Vertex(1.0f, -1.0f, 1.0f);
            gl.Vertex(1.0f, -1.0f, -1.0f);

            gl.End();

            //  Flush OpenGL.
            gl.Flush();

            //  Rotate the geometry a bit.
            rotatePyramid += 3.0f;
            rquad -= 3.0f; */

            #endregion
        }

        #endregion
    }
}
