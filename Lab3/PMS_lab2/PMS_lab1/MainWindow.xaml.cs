using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SharpGL.SceneGraph;
using SharpGL;

namespace PMS_lab1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool rotate, stop, pause;
        double a;
        int ang = 0;
        double shrink = 1;
        double shrinkStep = -0.01;
        double shrinkBound = 1;
        double r, g, b;
        Random rnd = new Random();
        MediaPlayer player;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            r = rnd.NextDouble();
            g = rnd.NextDouble();
            b = rnd.NextDouble();
            player = new MediaPlayer();
            player.Open(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"music.wav", UriKind.Relative));
            player.MediaEnded += player_MediaEnded;
        }

        /// <summary>
        /// Handles the OpenGLDraw event of the openGLControl1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="SharpGL.SceneGraph.OpenGLEventArgs"/> instance containing the event data.</param>
        private void openGLControl_OpenGLDraw(object sender, OpenGLEventArgs args)
        {
            //  Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;

            //  Clear the color and depth buffer.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            
            DrawAxis(gl);

            DrawGraphic(gl);

            #region Clear
            if (stop == true)
            {
                ClearCanvas(gl);
            }
            #endregion
        }

        private void ClearCanvas(OpenGL gl)
        {
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            rotate = stop = false;
            ang = 0;
        }

        private void Color()
        {
            if (LighterRadioButton.IsChecked == true)
                    if (r < 0.8)
                        r += 0.01;
                    else
                        if (b < 0.99)
                            b += 0.01;
                        else
                            if (g < 0.8)
                                g += 0.01;
                            else
                            {
                                r = rnd.NextDouble();
                                g = rnd.NextDouble();
                                b = rnd.NextDouble();
                            }
            else
                if (r >= 0)
                    r -= 0.01;
                else
                    if (b >= 0)
                        b -= 0.01;
                    else
                        if (g >= 0)
                            g -= 0.01;
                        else
                        {
                            r = rnd.NextDouble();
                            g = rnd.NextDouble();
                            b = rnd.NextDouble();
                        }
        }

        private void DrawGraphic(OpenGL gl)
        {
            // Graphic
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();

            int lastAng = ang;
            double lastShrink = shrink;

            if (ang < 360)
                ang++;
            else
                ang = 0;

            shrink += shrinkStep;

            if ((shrink < -shrinkBound) || (shrink > shrinkBound))
                shrinkStep *= -1;

            if (!rotate)
            {
                gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
                DrawAxis(gl);
                gl.Rotate(0, 0, lastAng);
                gl.Scale(lastShrink, 1, 1);
                ang = lastAng;
                shrink = lastShrink;
            }
            else
            {
                gl.Rotate(0, 0, ang);
                gl.Scale(shrink, 1, 1);
            }

            double x = -8, y;

            gl.Begin(OpenGL.GL_LINE_STRIP);
            gl.Color((float)r, (float)g, (float)b);

            do
            {
                y = Math.Pow(x, 2) * a;
                gl.Vertex(x / 10, y / 10);
                x += 0.01;
            }
            while (x <= 8);
            gl.End();

            if (rotate)
                Color();
        }

        private void player_MediaEnded(object sender, EventArgs e)
        {
            player.Stop();
            player.Play();
        }

        private void DrawAxis(OpenGL gl)
        {
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            //gl.Ortho(10f, 10f, 10f, 10f, -1f, 1f);
            //gl.MatrixMode(OpenGL.GL_MODELVIEW);

            gl.Begin(OpenGL.GL_LINES);
            gl.Color(0, 0, 0);
            gl.Vertex(0f, 1f);
            gl.Vertex(0f, -1f);
            gl.End();

            // X axis
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            //gl.Ortho(10f, 10f, 10f, 10f, -1f, 1f);
            //gl.MatrixMode(OpenGL.GL_MODELVIEW);
            //gl.Disable(OpenGL.GL_DEPTH_TEST);

            gl.Begin(OpenGL.GL_LINES);
            //gl.Color(0, 0, 0);
            gl.Vertex(-1f, 0f);
            gl.Vertex(1f, 0f);
            gl.End();

            // Arrow Y axis
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();

            gl.Begin(OpenGL.GL_LINE_STRIP);
            //gl.Color(0, 0, 0);
            gl.Vertex(-0.03f, 0.9f);
            gl.Vertex(0f, 1f);
            gl.Vertex(0.03f, 0.9f);
            gl.End();

            //Arrow X axis
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();

            gl.Begin(OpenGL.GL_LINE_STRIP);
            //gl.Color(0, 0, 0);
            gl.Vertex(0.93f, 0.05f);
            gl.Vertex(1f, 0f);
            gl.Vertex(0.93f, -0.05f);
            gl.End();

            // Text 1 -1
            gl.LoadIdentity();
            int a = (int)(openGLControl.ActualHeight / 2 + ((openGLControl.ActualHeight / 2) * 0.1));
            gl.DrawText((int)openGLControl.ActualWidth / 2 - (int)openGLControl.ActualWidth / 30, a - 4, 0, 0, 0, "", 10, "1");
            gl.End();

            gl.LoadIdentity();
            a = (int)(openGLControl.ActualHeight / 2 - ((openGLControl.ActualHeight / 2) * 0.1));
            gl.DrawText((int)openGLControl.ActualWidth / 2 + (int)openGLControl.ActualWidth / 40, a - 4, 0, 0, 0, "", 10, "-1");
            gl.End();

            gl.LoadIdentity();
            a = (int)(openGLControl.ActualWidth / 2 - ((openGLControl.ActualWidth / 2) * 0.1));
            gl.DrawText(a - 4, (int)openGLControl.ActualHeight / 2 - (int)openGLControl.ActualHeight / 20, 0, 0, 0, "", 10, "-1");
            gl.End();

            gl.LoadIdentity();
            a = (int)(openGLControl.ActualWidth / 2 + ((openGLControl.ActualWidth / 2) * 0.1));
            gl.DrawText(a - 4, (int)openGLControl.ActualHeight / 2 + (int)openGLControl.ActualHeight / 40, 0, 0, 0, "", 10, "1");
            gl.End();

            // Text 5 -5
            gl.LoadIdentity();
            a = (int)(openGLControl.ActualHeight / 2 + ((openGLControl.ActualHeight / 2) * 0.1) * 5);
            gl.DrawText((int)openGLControl.ActualWidth / 2 - (int)openGLControl.ActualWidth / 30, a - 4, 0, 0, 0, "", 10, "5");
            gl.End();

            gl.LoadIdentity();
            a = (int)(openGLControl.ActualHeight / 2 - ((openGLControl.ActualHeight / 2) * 0.1) * 5);
            gl.DrawText((int)openGLControl.ActualWidth / 2 + (int)openGLControl.ActualWidth / 40, a - 4, 0, 0, 0, "", 10, "-5");
            gl.End();

            gl.LoadIdentity();
            a = (int)(openGLControl.ActualWidth / 2 - ((openGLControl.ActualWidth / 2) * 0.1) * 5);
            gl.DrawText(a - 4, (int)openGLControl.ActualHeight / 2 - (int)openGLControl.ActualHeight / 20, 0, 0, 0, "", 10, "-5");
            gl.End();

            gl.LoadIdentity();
            a = (int)(openGLControl.ActualWidth / 2 + ((openGLControl.ActualWidth / 2) * 0.1) * 5);
            gl.DrawText(a - 4, (int)openGLControl.ActualHeight / 2 + (int)openGLControl.ActualHeight / 40, 0, 0, 0, "", 10, "5");
            gl.End();


            // Text Axis
            gl.LoadIdentity();
            a = (int)(openGLControl.ActualWidth / 2 - ((openGLControl.ActualWidth / 2) * 0.1));
            gl.DrawText(a - 2, (int)openGLControl.ActualHeight - 10, 0, 0, 0, "", 10, "Y");
            gl.End();

            gl.LoadIdentity();
            a = (int)(openGLControl.ActualHeight / 2 - ((openGLControl.ActualHeight / 2) * 0.1));
            gl.DrawText((int)openGLControl.ActualWidth - 10, a - 2, 0, 0, 0, "", 10, "X");
            gl.End();


            //gl.LoadIdentity();
            //gl.DrawText((int)(0.1 * (1 / openGLControl.ActualWidth)), (int)(-0.3 * (1 / openGLControl.ActualHeight)), 0, 0, 0, "", 10, "1");
            //gl.End();

            double i = -1;
            do
            {
                gl.MatrixMode(OpenGL.GL_PROJECTION);
                gl.LoadIdentity();

                gl.Begin(OpenGL.GL_LINES);
                gl.Color(0, 0, 0);
                gl.Vertex(i, 0.02);
                gl.Vertex(i, -0.03);
                gl.End();

                i += 0.1;
            }
            while (i < 0.9);

            i = -1;
            do
            {
                gl.MatrixMode(OpenGL.GL_PROJECTION);
                gl.LoadIdentity();

                gl.Begin(OpenGL.GL_LINES);
                gl.Color(0, 0, 0);
                gl.Vertex(-0.02, i);
                gl.Vertex(0.02, i);
                gl.End();

                i += 0.1;
            }
            while (i < 0.9);

            i = -1;
            do
            {
                gl.MatrixMode(OpenGL.GL_PROJECTION);
                gl.LoadIdentity();

                gl.Begin(OpenGL.GL_LINES);
                gl.Color(0, 0, 0);
                gl.Vertex(-0.02, i);
                gl.Vertex(0.02, i);
                gl.End();

                i += 0.1;
            }
            while (i < 0.9);

        }

        /// <summary>
        /// Handles the OpenGLInitialized event of the openGLControl1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="SharpGL.SceneGraph.OpenGLEventArgs"/> instance containing the event data.</param>
        private void openGLControl_OpenGLInitialized(object sender, OpenGLEventArgs args)
        {
            //  TODO: Initialise OpenGL here.

            //  Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;

            //  Set the clear color.
            gl.ClearColor(1, 1, 1, 1);
        }

        /// <summary>
        /// Handles the Resized event of the openGLControl1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="SharpGL.SceneGraph.OpenGLEventArgs"/> instance containing the event data.</param>
        private void openGLControl_Resized(object sender, OpenGLEventArgs args)
        {
            //  TODO: Set the projection matrix here.

            //  Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;

            //  Set the projection matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);

            //  Load the identity.
            gl.LoadIdentity();

            //  Create a perspective transformation.
            gl.Perspective(60.0f, (double)Width / (double)Height, 0.01, 100.0);

            //  Use the 'look at' helper function to position and aim the camera.
            gl.LookAt(-5, 5, -5, 0, 0, 0, 0, 1, 0);

            //  Set the modelview matrix.
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }

        /// <summary>
        /// The current rotation.
        /// </summary>
        private float rotation = 0.0f;


        private void DrawGraphicButton_Click(object sender, RoutedEventArgs e)
        {
            if (InputATextBox.Text.Length != 0)
            {
                rotate = true;
                a = Convert.ToDouble(InputATextBox.Text);
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Do you want a = 1?", "You didn't input parametr a", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.OK)
                {
                    rotate = true;
                    a = 1;
                    InputATextBox.Text = "1";
                }
            }
            player.Play();
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            rotate = false;
            player.Pause();
        }

        private void openGLControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            stop = true;
            player.Stop();
        }
    }
}
