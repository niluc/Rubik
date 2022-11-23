using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using OpenTK;                       // Matrix 4
using Tao.OpenGl;

namespace Rubik
{
    public partial class Learn : Form
    {
        //double theta = 5.0;
        private double startX, startY;
        private double endX, endY;
        bool track = false;
        bool isPress = false;
        bool isSolving = false;
        bool draw_axis = false;
        public uint selectedFace;
        public int selectedCube;
        //private double AngleX = 0;
        //private double AngleY = 0;
        //private double AngleZ = 0;
        private double Angle = 0;

        private Vector3d rot = new Vector3d(0.0, 0.0, 0.0);
        private Vector3d lastPos = new Vector3d(0.0, 0.0, 0.0);

        public static uint[][] textures = new uint[14][];

        public uint[] selectBuf = new uint[512];

        private Timer timer;
        /*
        Matrix4d mat = new Matrix4d(1.0, 0.0, 0.0, 0.0, 
                                    0.0, 1.0, 0.0, 0.0, 
                                    0.0, 0.0, 1.0, 0.0, 
                                    0.0, 0.0, 0.0, 1.0);*/

        private static Matrix4d mat = new Matrix4d(0.778246879577637, -0.335064768791199, 0.531095266342163, 0,
                                                   0.0402242317795753, 0.870613098144531, 0.490320920944214, 0,
                                                   -0.62666779756546, -0.360228270292282, 0.69103080034256, 0,
                                                   0, 0, 0, 1);
        private RubiksCube rubiksCube;

        public Dictionary<string, string> rotateM = new Dictionary<string, string>()
        {
            {"F'", "RLUD.9.6.3.12.21.24.27.18.9"},
            {"F", "RLUD.9.18.27.24.21.12.3.6.9"},
            {"D", "RLFB.1.2.3.12.21.20.19.10.1"},
            {"D'", "RLFB.1.10.19.20.21.12.3.2.1"},
            {"U'", "RLFB.7.8.9.18.27.26.25.16.7"},
            {"U", "RLFB.7.16.25.26.27.18.9.8.7"},
            {"L", "UDFB.7.8.9.6.3.2.1.4.7"},
            {"L'", "UDFB.7.4.1.2.3.6.9.8.7"},
            {"R", "UDFB.27.26.25.22.19.20.21.24.27"},
            {"R'", "UDFB.27.24.21.20.19.22.25.26.27"},
            {"B", "RLUD.7.4.1.10.19.22.25.16.7"},
            {"B'", "RLUD.7.16.25.22.19.10.1.4.7"},
            {"E", "RLFB.4.5.6.15.24.23.22.13.4"},
            {"E'", "RLFB.4.13.22.23.24.15.6.5.4"},
            {"M", "UDFB.10.13.16.17.18.15.12.11.10"},
            {"M'", "UDFB.10.11.12.15.18.17.16.13.10"},
            {"S", "UDRL.8.17.26.23.20.11.2.5.8"},
            {"S'", "UDRL.8.5.2.11.20.23.26.17.8"},
        };

        public int tabIndex = 0;

        public static int[][,] _rubikData =
        {

            new int[3,3]
            {
                {0,0,0},
                {0,0,0},           // left
                {0,0,0}
            },

            new int[3,3]
            {
                {1,1,1},
                {1,1,1},            // right
                {1,1,1}
            },
            new int[3,3]
            {
                {2,2,2},
                {2,2,2},            // top
                {2,2,2}
            },
            new int[3,3]
            {
                {3,3,3},
                {3,3,3},            // down
                {3,3,3}
            },
            new int[3,3]
            {
                {4,4,4},
                {4,4,4},            // front
                {4,4,4}
            },
            new int[3,3]
            {
                {5,5,5},
                {5,5,5},            // back
                {5,5,5}
            }
        };

        public static int[][,] testcase;

        // Rubik declard
        myRubik _newRubik;
        myRubik testcaseRubik;
        public Learn()
        {
            InitializeComponent();
            // Centers the form on the current screen
            CenterToScreen();
            //rubiksCube = new RubikCubeForm();
            rubiksCube = new RubiksCube(_rubikData);
            //newRubik = new myRubik(_rubikData);
            this.timer = new Timer();
            timer.Tick += Tick;
            timer.Enabled = true;
            timer.Interval = 1;
            timer.Start();

            //rubiksCube.BringToFront();
            //rubiksCube.Show();
            //rubiksCube.Activate();
            //var timer = new Timer();
            //timer.Tick += GameLoop;
            //timer.Interval = 1000 / 60;
            //timer.Start();

            //init data
            //set display member and value member for combobox
            glControl.Visible = false;
            buttonNext.Visible = false;
            buttonBack.Visible = false;
            pictureTestCase.Visible = false;
            lableGuide.Visible = false;
            pictureMain.Visible = false;
        }

        private void GameLoop(object sender, System.EventArgs e)
        {
            // Update coordinates of game entities
            // or check collisions
            FrameUpdate();

            // This method calls glControl_Paint
            glControl.Invalidate();
        }

        private void FrameUpdate()
        {
            //theta += 1.0;
            //case (theta > 360) theta -= 360;
        }

        private void Tick(object sender, EventArgs e)
        {
            this.glControl.Invalidate();
        }


        private void glControl_Load(object sender, EventArgs e)
        {
            //var key = rotateM.FirstOrDefault(x => x.Key.Contains("4+3+12")).Value;
            //Console.WriteLine(key);
            Bitmap[] stickers = new Bitmap[14];

            stickers[0] = new Bitmap(@"../../Resources/blue.bmp");
            stickers[1] = new Bitmap(@"../../Resources/green.bmp");
            stickers[2] = new Bitmap(@"../../Resources/yellow.bmp");
            stickers[3] = new Bitmap(@"../../Resources/white.bmp");
            stickers[4] = new Bitmap(@"../../Resources/red.bmp");
            stickers[5] = new Bitmap(@"../../Resources/orange.bmp");
            stickers[6] = new Bitmap(@"../../Resources/black.bmp");
            stickers[7] = new Bitmap(@"../../Resources/F.bmp");
            stickers[8] = new Bitmap(@"../../Resources/B.bmp");
            stickers[9] = new Bitmap(@"../../Resources/R.bmp");
            stickers[10] = new Bitmap(@"../../Resources/L.bmp");
            stickers[11] = new Bitmap(@"../../Resources/U.bmp");
            stickers[12] = new Bitmap(@"../../Resources/D.bmp");
            stickers[13] = new Bitmap(@"../../Resources/gray.bmp");


            glControl.Size = new System.Drawing.Size(2 * Constants._rubikSize, 2 * Constants._rubikSize);
            //glControl.Size = new System.Drawing.Size(2 * 210, 2 * 210);
            glControl.MakeCurrent();
            GL.Viewport(0, 0, 2 * Constants._rubikSize, 2 * Constants._rubikSize);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            GL.ClearColor(ColorTranslator.FromHtml("#222223"));
            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);
            Matrix4 matrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), 1.0f, 0.9f,
                        1000f);

            GL.LoadMatrix(ref matrix);
            //Glu.gluPerspective(45.0, 1.0, 0.9, 1000.0);
            GL.Translate(0, 0, -2.5);

            GL.Ortho(-Constants._orthor * Constants._rubikSize,
                        Constants._orthor * Constants._rubikSize,
                        -Constants._orthor * Constants._rubikSize,
                        Constants._orthor * Constants._rubikSize,
                        2 * Constants._orthor * Constants._rubikSize,
                        -2 * Constants._orthor * Constants._rubikSize);


            // https://www.khronos.org/registry/OpenGL-Refpages/es2.0/xhtml/glViewport.xml

            //GL.Viewport(0, 0, 100, 100);
            //GL.Viewport(0, 0, 2 * Constants._rubikSize, 2 * Constants._rubikSize);
            // Make objects show right position
            GL.Enable(EnableCap.DepthTest);

            GL.ShadeModel(ShadingModel.Smooth);
            //GL.Enable(EnableCap.Texture2D);
            System.Drawing.Imaging.BitmapData bitmapdata;
            Bitmap image;
            Rectangle rect;
            for (uint i = 0; i < stickers.Length; i++)
            {
                image = stickers[i];
                image.RotateFlip(RotateFlipType.RotateNoneFlipY);
                rect = new Rectangle(0, 0, image.Width, image.Height);
                bitmapdata = image.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly,
                    System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                textures[i] = new uint[2];
                GL.GenTextures(1, textures[i]);
                GL.BindTexture(TextureTarget.Texture2D, textures[i][0]);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height,
                    0, PixelFormat.Bgr, PixelType.UnsignedByte, bitmapdata.Scan0);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);		// Linear Filtering
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.Linear);

                image.UnlockBits(bitmapdata);
                image.Dispose();
            }
            //GL.Disable(EnableCap.Texture2D);
            //GL.MatrixMode(MatrixMode.Modelview);

            glControl.SwapBuffers();
        }

        private void glControl_Paint(object sender, PaintEventArgs e)
        {
            GL.Enable(EnableCap.DepthTest);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);
            GL.MatrixMode(MatrixMode.Modelview);
            //Gl.glMatrixMode(Gl.GL_MODELVIEW);

            renderObjs();
            //GL.PopMatrix();
            glControl.SwapBuffers();

        }

        private void renderObjs()
        {
            GL.LoadIdentity();

            //GL.PushMatrix();

            if (track)
            {
                GL.PushMatrix();
                GL.LoadIdentity();
                GL.Rotate(Angle, rot.X, rot.Y, rot.Z);
                GL.MultMatrix(ref mat);
                GL.GetDouble(GetPName.ModelviewMatrix, out mat);
                GL.PopMatrix();
            }

            Angle = 0;
            GL.MultMatrix(ref mat);
            //Console.WriteLine(mat);
            //Console.WriteLine("");
            if (draw_axis)
            {
                double size = 200.0 / 9.0;
                double dis = 150;

                GL.Enable(EnableCap.Texture2D);

                GL.BindTexture(TextureTarget.Texture2D, Practice.textures[7][0]);
                GL.Begin(PrimitiveType.Quads);

                GL.TexCoord2(0f, 1f);
                GL.Vertex3(-size, size, size + dis);
                GL.TexCoord2(0f, 0f);
                GL.Vertex3(-size, -size, size + dis);
                GL.TexCoord2(1f, 0f);
                GL.Vertex3(size, -size, size + dis);
                GL.TexCoord2(1f, 1f);
                GL.Vertex3(size, size, size + dis);
                GL.End();

                GL.BindTexture(TextureTarget.Texture2D, Practice.textures[8][0]);
                GL.Begin(PrimitiveType.Quads);

                GL.TexCoord2(0f, 1f);
                GL.Vertex3(size, size, -size - dis);
                GL.TexCoord2(0f, 0f);
                GL.Vertex3(size, -size, -size - dis);
                GL.TexCoord2(1f, 0f);
                GL.Vertex3(-size, -size, -size - dis);
                GL.TexCoord2(1f, 1f);
                GL.Vertex3(-size, size, -size - dis);
                GL.End();

                GL.BindTexture(TextureTarget.Texture2D, Practice.textures[9][0]);
                GL.Begin(PrimitiveType.Quads);

                GL.TexCoord2(0f, 1f);
                GL.Vertex3(size + dis, size, size);
                GL.TexCoord2(0f, 0f);
                GL.Vertex3(size + dis, -size, size);
                GL.TexCoord2(1f, 0f);
                GL.Vertex3(size + dis, -size, -size);
                GL.TexCoord2(1f, 1f);
                GL.Vertex3(size + dis, size, -size);
                GL.End();

                GL.BindTexture(TextureTarget.Texture2D, Practice.textures[10][0]);
                GL.Begin(PrimitiveType.Quads);

                GL.TexCoord2(0f, 1f);
                GL.Vertex3(-size - dis, size, -size);
                GL.TexCoord2(0f, 0f);
                GL.Vertex3(-size - dis, -size, -size);
                GL.TexCoord2(1f, 0f);
                GL.Vertex3(-size - dis, -size, size);
                GL.TexCoord2(1f, 1f);
                GL.Vertex3(-size - dis, size, size);
                GL.End();

                GL.BindTexture(TextureTarget.Texture2D, Practice.textures[11][0]);
                GL.Begin(PrimitiveType.Quads);

                GL.TexCoord2(0f, 1f);
                GL.Vertex3(-size, size + dis, -size);
                GL.TexCoord2(0f, 0f);
                GL.Vertex3(-size, size + dis, size);
                GL.TexCoord2(1f, 0f);
                GL.Vertex3(size, size + dis, size);
                GL.TexCoord2(1f, 1f);
                GL.Vertex3(size, size + dis, -size);
                GL.End();

                GL.BindTexture(TextureTarget.Texture2D, Practice.textures[12][0]);
                GL.Begin(PrimitiveType.Quads);

                GL.TexCoord2(0f, 0f);
                GL.Vertex3(-size, -size - dis, -size);
                GL.TexCoord2(1f, 0f);
                GL.Vertex3(size, -size - dis, -size);
                GL.TexCoord2(1f, 1f);
                GL.Vertex3(size, -size - dis, size);
                GL.TexCoord2(0f, 1f);
                GL.Vertex3(-size, -size - dis, size);
                GL.End();

                GL.Disable(EnableCap.Texture2D);
            }

            _newRubik = null;
            _newRubik = new myRubik(_rubikData);

            //rubiksCube.Draw();

            if (!rubiksCube.isAnimate)
            {
                _newRubik.DrawRubik();
                rubiksCube.Draw();
            }

            rubiksCube.Draw();

        }

        public unsafe int doPicking(int x, int y)
        {
            int[] viewport = new int[4];
            GL.SelectBuffer(512, selectBuf);
            GL.RenderMode(RenderingMode.Select);

            GL.MatrixMode(MatrixMode.Projection);
            GL.PushMatrix();
            GL.LoadIdentity();
            GL.GetInteger(GetPName.Viewport, viewport);

            Glu.gluPickMatrix((double)x, (double)viewport[3] - y, 5, 5, viewport);

            Glu.gluPerspective(45.0, 1.0, 0.9, 1000.0);
            GL.Translate(0, 0, -2.5);

            GL.Ortho(-Constants._orthor * Constants._rubikSize,
                        Constants._orthor * Constants._rubikSize,
                        -Constants._orthor * Constants._rubikSize,
                        Constants._orthor * Constants._rubikSize,
                        2 * Constants._orthor * Constants._rubikSize,
                        -2 * Constants._orthor * Constants._rubikSize);
            GL.MatrixMode(MatrixMode.Modelview);

            renderObjs();

            GL.MatrixMode(MatrixMode.Projection);
            GL.PopMatrix();
            GL.MatrixMode(MatrixMode.Modelview);
            glControl.SwapBuffers();

            int hits = GL.RenderMode(RenderingMode.Render);

            if (hits != 0)
            {
                uint i, numberOfNames;
                uint names = 0;
                uint* cur;
                fixed (uint* ptr = selectBuf)
                {
                    cur = ptr;
                    uint* ptrNames = cur;
                    uint minZ = 0xffffffff;
                    for (i = 0; i < hits; i++)
                    {
                        names = *cur;
                        cur++;
                        if (*cur < minZ)
                        {
                            selectedFace = *ptrNames;
                            numberOfNames = names;
                            minZ = *cur;
                            ptrNames = cur + 2;
                        }

                        cur += names + 2;
                    }
                    return (int)(*ptrNames);
                }
            }
            return 0;
        }

        private void glControl_KeyUp(object sender, KeyEventArgs e)
        {
            track = false;
        }

        private void glControl_KeyDown(object sender, KeyEventArgs e)
        {
            //   case(e.Alt && e.KeyCode == Keys.E)
            track = true;
            switch (e.KeyCode)
            {
                case Keys.S:
                    Angle = 3;
                    rot = new Vector3d(1.0, 0.0, 0.0);
                    break;

                case Keys.W:
                    Angle = -3;
                    rot = new Vector3d(1.0, 0.0, 0.0);
                    break;

                case Keys.A:
                    Angle = -3;
                    rot = new Vector3d(0.0, 1.0, 0.0);
                    break;

                case Keys.D:
                    Angle = 3;
                    rot = new Vector3d(0.0, 1.0, 0.0);
                    break;

                case Keys.C:
                    if (draw_axis)
                    {
                        draw_axis = false;
                    }
                    else
                    {
                        draw_axis = true;
                    }
                    break;
            }

        }
        public double p2i_coord(double x)
        {
            double x_i = (x - (((double)glControl.Width - 1.0) / 2.0)) * 2.0 / (double)glControl.Width;
            return x_i;
        }

        private void glControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (isPress && track)
            {
                Vector3d curPos = new Vector3d(0.0, 0.0, 0.0);
                endX = (double)e.X;
                endY = glControl.Height - (double)e.Y - 1.0;

                //track = true;

                double startx = p2i_coord(startX);
                double starty = p2i_coord(startY);

                double endx = p2i_coord(endX);
                double endy = p2i_coord(endY);

                double r = 1.0 - startx * startx - starty * starty;
                if (r > 0)
                {
                    lastPos.X = startx;
                    lastPos.Y = starty;
                    lastPos.Z = Math.Sqrt(r);
                }
                else
                {
                    double d = Math.Sqrt(startx * startx + starty * starty);
                    lastPos.X = startx / d;
                    lastPos.Y = starty / d;
                    lastPos.Z = 0;
                }

                r = 1.0 - endx * endx - endy * endy;
                if (r > 0)
                {
                    curPos.X = endx;
                    curPos.Y = endy;
                    curPos.Z = Math.Sqrt(r);
                }
                else
                {
                    double d = Math.Sqrt(endx * endx + endy * endy);
                    curPos.X = endx / d;
                    curPos.Y = endy / d;
                    curPos.Z = 0;
                }


                //Vector3d sub = Vector3d.Subtract(curPos, lastPos);
                //Angle = 0.8 * 90 * sub.Length;
                Angle = 180 / 3.14 * Math.Acos(Math.Min(1, Vector3d.Dot(lastPos.Normalized(), curPos.Normalized())));
                rot = Vector3d.Cross(lastPos, curPos);
                //Vector3d huhu = Vector3d.Normalize(rot);
                //Console.WriteLine(huhu);
                //Console.WriteLine(Angle);

                if (Angle < -180 || Angle > 180)
                {
                    Angle = 0.0;
                }

                startX = endX;
                startY = endY;
            }

            if (isPress && !track && !isSolving && !rubiksCube.isAnimate)
            {
                int i;
                if (selectedCube != (i = doPicking(e.X, e.Y)))
                {
                    rotateByMouse(selectedCube, i);
                    isPress = false;
                }
            }

        }

        // mouse rotate slice
        private void rotateByMouse(int x, int y)
        {
            RubikCubeMoviment moviment = null;
            string face = "";

            switch (selectedFace)
            {
                case 0:
                    face = "L";
                    break;
                case 1:
                    face = "R";
                    break;
                case 2:
                    face = "U";
                    break;
                case 3:
                    face = "D";
                    break;
                case 4:
                    face = "F";
                    break;
                case 5:
                    face = "B";
                    break;
            }
            var code = x.ToString() + "." + y.ToString();
            var dk1 = "." + x.ToString() + ".";
            var dk2 = "." + y.ToString() + ".";
            var dir = rotateM.FirstOrDefault(n => (n.Value.Contains(face)
                                                    && n.Value.Contains(code)
                                                    && n.Value.Contains(dk1)
                                                    && n.Value.Contains(dk2))).Key;

            Console.WriteLine(dir);
            switch (dir)
            {
                case "L":
                    moviment = new RubikCubeMoviment(Depth.First, Spin.Anticlockwise, Axis.X, 1);
                    _newRubik.RotateRubik("L");
                    break;

                case "L'":
                    moviment = new RubikCubeMoviment(Depth.First, Spin.Clockwise, Axis.X, 1);
                    _newRubik.RotateRubik("L'");
                    break;

                case "R":
                    moviment = new RubikCubeMoviment(Depth.Third, Spin.Clockwise, Axis.X, 1);
                    _newRubik.RotateRubik("R");
                    break;

                case "R'":
                    moviment = new RubikCubeMoviment(Depth.Third, Spin.Anticlockwise, Axis.X, 1);
                    _newRubik.RotateRubik("R'");
                    break;

                case "F":
                    moviment = new RubikCubeMoviment(Depth.First, Spin.Clockwise, Axis.Z, 1);
                    _newRubik.RotateRubik("F");
                    break;

                case "F'":
                    moviment = new RubikCubeMoviment(Depth.First, Spin.Anticlockwise, Axis.Z, 1);
                    _newRubik.RotateRubik("F'");
                    break;

                case "B":
                    moviment = new RubikCubeMoviment(Depth.Third, Spin.Anticlockwise, Axis.Z, 1);
                    _newRubik.RotateRubik("B");
                    break;

                case "B'":
                    moviment = new RubikCubeMoviment(Depth.Third, Spin.Clockwise, Axis.Z, 1);
                    _newRubik.RotateRubik("B'");
                    break;

                case "U":
                    moviment = new RubikCubeMoviment(Depth.First, Spin.Clockwise, Axis.Y, 1);
                    _newRubik.RotateRubik("U");
                    break;

                case "U'":
                    moviment = new RubikCubeMoviment(Depth.First, Spin.Anticlockwise, Axis.Y, 1);
                    _newRubik.RotateRubik("U'");
                    break;

                case "D":
                    moviment = new RubikCubeMoviment(Depth.Third, Spin.Anticlockwise, Axis.Y, 1);
                    _newRubik.RotateRubik("D");
                    break;

                case "D'":
                    moviment = new RubikCubeMoviment(Depth.Third, Spin.Clockwise, Axis.Y, 1);
                    _newRubik.RotateRubik("D'");
                    break;

                case "E":
                    moviment = new RubikCubeMoviment(Depth.Second, Spin.Anticlockwise, Axis.Y, 1);
                    _newRubik.rotateE();
                    _newRubik.rotateE();
                    _newRubik.rotateE();
                    break;

                case "E'":
                    moviment = new RubikCubeMoviment(Depth.Second, Spin.Clockwise, Axis.Y, 1);
                    _newRubik.rotateE();
                    break;

                case "M":
                    moviment = new RubikCubeMoviment(Depth.Second, Spin.Anticlockwise, Axis.X, 1);
                    _newRubik.rotateM();
                    break;

                case "M'":
                    moviment = new RubikCubeMoviment(Depth.Second, Spin.Clockwise, Axis.X, 1);
                    _newRubik.rotateM();
                    _newRubik.rotateM();
                    _newRubik.rotateM();
                    break;

                case "S":
                    moviment = new RubikCubeMoviment(Depth.Second, Spin.Clockwise, Axis.Z, 1);
                    _newRubik.rotateS();
                    break;

                case "S'":
                    moviment = new RubikCubeMoviment(Depth.Second, Spin.Anticlockwise, Axis.Z, 1);
                    _newRubik.rotateS();
                    _newRubik.rotateS();
                    _newRubik.rotateS();
                    break;

            }
            if (isRubiksEqual(_newRubik, testcaseRubik)) MessageBox.Show("Goodjob!");
            
            if (moviment != null)
            {
                rubiksCube.Manipulate(moviment);
            }
        }

        private void glControl_MouseUp(object sender, MouseEventArgs e)
        {
            isPress = false;
            track = false;
            //Angle = 0;
        }

        private void glControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isPress = true;
                startX = (double)(e.X);
                startY = (double)glControl.Width - (double)e.Y - 1.0;
                //Console.WriteLine(doPicking(e.X, e.Y));
                //Console.WriteLine(selectedFace);
                int i;
                if ((i = doPicking(e.X, e.Y)) == 0)
                {
                    track = true;
                }
                else
                {
                    selectedCube = i;
                }
                Console.WriteLine(i);

            }
        }

        private void Scramble(myRubik scrambleRubik)
        {
            Random r = new Random();
            string s = "";

            for (int i = 0; i < 25; i++)
            {
                switch ((r.Next(1, 100)) % 12)
                {
                    case 0:
                        s += "L";
                        break;

                    case 1:
                        s += "l";
                        break;

                    case 2:
                        s += "R";
                        break;

                    case 3:
                        s += "r";
                        break;

                    case 4:
                        s += "F";
                        break;

                    case 5:
                        s += "f";
                        break;

                    case 6:
                        s += "B";
                        break;

                    case 7:
                        s += "b";
                        break;

                    case 8:
                        s += "U";
                        break;

                    case 9:
                        s += "u";
                        break;

                    case 10:
                        s += "D";
                        break;

                    case 11:
                        s += "d";
                        break;
                }
            }

            s = s.Replace("Ll", "").Replace("Rr", "").Replace("Uu", "").Replace("Dd", "").Replace("Ff", "").Replace("Bb", "");
            s = s.Replace("lL", "").Replace("rR", "").Replace("uU", "").Replace("dD", "").Replace("fF", "").Replace("bB", "");

            for (int j = 0; j < s.Length; j++)
            {
                switch (s[j].ToString())
                {
                    case "L":
                        scrambleRubik.RotateRubik("L");
                        break;

                    case "l":
                        scrambleRubik.RotateRubik("L'");
                        break;

                    case "R":
                        scrambleRubik.RotateRubik("R");
                        break;

                    case "r":
                        scrambleRubik.RotateRubik("R'");
                        break;

                    case "F":
                        scrambleRubik.RotateRubik("F");
                        break;

                    case "f":
                        scrambleRubik.RotateRubik("F'");
                        break;

                    case "B":
                        scrambleRubik.RotateRubik("B");
                        break;

                    case "b":
                        scrambleRubik.RotateRubik("B'");
                        break;

                    case "U":
                        scrambleRubik.RotateRubik("U");
                        break;

                    case "u":
                        scrambleRubik.RotateRubik("U'");
                        break;

                    case "D":
                        scrambleRubik.RotateRubik("D");
                        break;

                    case "d":
                        scrambleRubik.RotateRubik("D'");
                        break;
                }
            }
        }

        private static bool isRubiksEqual(myRubik rubik, myRubik testcase)
        {
            int[][,] face1 = rubik.rubikFace;
            int[][,] face2 = testcase.rubikFace;
            myRubik new1 = myRubik.DeepClone(rubik);
            myRubik new2 = myRubik.DeepClone(testcase);
            new1.preSolve();
            new2.preSolve();
            //bool h = (new1 == new2);
            //Console.WriteLine(new1.rubikFace);
            //Console.WriteLine(h);
            return new1 == new2;
        }


        private void stepComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (stepComboBox.SelectedItem != null)
            {
                switch (stepComboBox.SelectedItem)
                {
                    case "White Cross":
                        tabIndex = 11;
                        break;
                    case "White Face":
                        tabIndex = 21;
                        break;
                    case "Second Layer Edges":
                        tabIndex = 31;
                        break;
                    case "Yellow Cross":
                        tabIndex = 41;
                        break;
                    case "Yellow Apply Edges":
                        tabIndex = 51;
                        break;
                    case "Yellow Apply Corners":
                        tabIndex = 61;
                        break;
                    case "Yellow Corner Orient":
                        tabIndex = 71;
                        break;
                }
                stepDo(tabIndex);
            }
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            tabIndex++;
            stepDo(tabIndex);
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            tabIndex--;
            stepDo(tabIndex);
        }

        private void stepDo(int index)
        {
            switch (index)
            {
                case 11:
                    glControl.Visible = true;
                    buttonBack.Visible = false;
                    buttonNext.Visible = true;
                    pictureTestCase.Visible = true;
                    lableGuide.Visible = true;
                    labelHint.Visible = false;
                    pictureMain.Visible = false;
                    pictureTestCase.Image = Image.FromFile("../../Resources/step1_1.png");
                    lableGuide.Text = Constants.language == 1 ? "Hãy thử hoàn thành khối này:" : "Try to do this without going to next page:";
                    testcase = new []
                    {
                        new int[3,3]
                        {
                            {13,13,13},
                            {13,0,13},           // left
                            {13,0,13}
                        },

                        new int[3,3]
                        {
                            {13,13,13},
                            {13,1,13},            // right
                            {13,1,13}
                        },
                        new int[3,3]
                        {
                            {13,13,13},
                            {13,13,13},            // top
                            {13,13,13}
                        },
                        new int[3,3]
                        {
                            {13,3,13},
                            {3,3,3},            // down
                            {13,3,13}
                        },
                        new int[3,3]
                        {
                            {13,13,13},
                            {13,4,13},            // front
                            {13,4,13}
                        },
                        new int[3,3]
                        {
                            {13,13,13},
                            {13,5,13},            // back
                            {13,5,13}
                        }
                    };
                    _rubikData = new[]
                    {
                        new int[3,3]
                        {
                            {13,13,13},
                            {13,0,13},           // left
                            {13,0,13}
                        },

                        new int[3,3]
                        {
                            {13,13,13},
                            {13,1,13},            // right
                            {13,1,13}
                        },
                        new int[3,3]
                        {
                            {13,13,13},
                            {13,13,13},            // top
                            {13,13,13}
                        },
                        new int[3,3]
                        {
                            {13,3,13},
                            {3,3,3},            // down
                            {13,3,13}
                        },
                        new int[3,3]
                        {
                            {13,13,13},
                            {13,4,13},            // front
                            {13,4,13}
                        },
                        new int[3,3]
                        {
                            {13,13,13},
                            {13,5,13},            // back
                            {13,5,13}
                        }
                    };
                    testcaseRubik = new myRubik(testcase);
                    _newRubik = null;
                    _newRubik = new myRubik(_rubikData);
                    Scramble(_newRubik);
                    rubiksCube = new RubiksCube(_rubikData);
                    break;
                case 12:
                    glControl.Visible = false;
                    buttonNext.Visible = false;
                    buttonBack.Visible = true;
                    pictureTestCase.Visible = false;
                    lableGuide.Visible = true;
                    labelHint.Visible = false;
                    pictureMain.Visible = true;
                    lableGuide.Text = Constants.language == 1 ? "Một số mẹo:" : "Some Tricks:";
                    pictureMain.Image = Image.FromFile("../../Resources/step1_2.png");
                    break;
                case 21:
                    glControl.Visible = true;
                    buttonBack.Visible = false;
                    buttonNext.Visible = true;
                    pictureTestCase.Visible = true;
                    lableGuide.Visible = true;
                    labelHint.Visible = false;
                    pictureMain.Visible = false;
                    pictureTestCase.Image = Image.FromFile("../../Resources/step2_1.png");
                    lableGuide.Text = Constants.language == 1 ? "Hãy thử hoàn thành khối này:" : "Try to do this without going to next page:";
                    testcase = new[]
                    {
                        new int[3,3]
                        {
                            {13,13,13},
                            {13,0,13},           // left
                            {0,0,0}
                        },

                        new int[3,3]
                        {
                            {13,13,13},
                            {13,1,13},            // right
                            {1,1,1}
                        },
                        new int[3,3]
                        {
                            {13,13,13},
                            {13,13,13},            // top
                            {13,13,13}
                        },
                        new int[3,3]
                        {
                            {3,3,3},
                            {3,3,3},            // down
                            {3,3,3}
                        },
                        new int[3,3]
                        {
                            {13,13,13},
                            {13,4,13},            // front
                            {4,4,4}
                        },
                        new int[3,3]
                        {
                            {13,13,13},
                            {13,5,13},            // back
                            {5,5,5}
                        }
                    };
                    _rubikData = new[]
                    {
                        new int[3,3]
                        {
                            {13,13,13},
                            {13,0,13},           // left
                            {0,0,0}
                        },

                        new int[3,3]
                        {
                            {13,13,13},
                            {13,1,13},            // right
                            {1,1,1}
                        },
                        new int[3,3]
                        {
                            {13,13,13},
                            {13,13,13},            // top
                            {13,13,13}
                        },
                        new int[3,3]
                        {
                            {3,3,3},
                            {3,3,3},            // down
                            {3,3,3}
                        },
                        new int[3,3]
                        {
                            {13,13,13},
                            {13,4,13},            // front
                            {4,4,4}
                        },
                        new int[3,3]
                        {
                            {13,13,13},
                            {13,5,13},            // back
                            {5,5,5}
                        }
                    };
                    testcaseRubik = new myRubik(testcase);
                    _newRubik = null;
                    _newRubik = new myRubik(_rubikData);
                    Scramble(_newRubik);
                    _newRubik.solveWhiteCross();
                    rubiksCube = new RubiksCube(_rubikData);
                    break;
                case 22:
                    glControl.Visible = false;
                    buttonNext.Visible = true;
                    buttonBack.Visible = true;
                    pictureTestCase.Visible = false;
                    lableGuide.Visible = false;
                    labelHint.Visible = false;
                    pictureMain.Visible = true;
                    pictureMain.Image = Constants.language == 1 ? Image.FromFile("../../Resources/step2_2v.png") : Image.FromFile("../../Resources/step2_2.png");
                    break;
                case 23:
                    glControl.Visible = false;
                    buttonNext.Visible = true;
                    buttonBack.Visible = true;
                    pictureTestCase.Visible = false;
                    lableGuide.Visible = false;
                    labelHint.Visible = false;
                    pictureMain.Visible = true;
                    pictureMain.Image = Constants.language == 1 ? Image.FromFile("../../Resources/step2_3v.png") : Image.FromFile("../../Resources/step2_3.png");
                    break;
                case 24:
                    glControl.Visible = false;
                    buttonNext.Visible = false;
                    buttonBack.Visible = true;
                    pictureTestCase.Visible = false;
                    lableGuide.Visible = false;
                    labelHint.Visible = false;
                    pictureMain.Visible = true;
                    pictureMain.Image = Constants.language == 1 ? Image.FromFile("../../Resources/step2_4v.png") : Image.FromFile("../../Resources/step2_4.png");
                    break;
                case 31:
                    glControl.Visible = false;
                    buttonNext.Visible = true;
                    buttonBack.Visible = false;
                    pictureTestCase.Visible = false;
                    lableGuide.Visible = false;
                    labelHint.Visible = false;
                    pictureMain.Visible = true;
                    pictureMain.Image = Constants.language == 1 ? Image.FromFile("../../Resources/step3_1v.png") : Image.FromFile("../../Resources/step3_1.png");
                    break;
                case 32:
                    glControl.Visible = false;
                    buttonNext.Visible = true;
                    buttonBack.Visible = true;
                    pictureTestCase.Visible = false;
                    lableGuide.Visible = false;
                    labelHint.Visible = false;
                    pictureMain.Visible = true;
                    pictureMain.Image = Constants.language == 1 ? Image.FromFile("../../Resources/step3_2v.png") : Image.FromFile("../../Resources/step3_2.png");
                    break;
                case 33:
                    glControl.Visible = true;
                    buttonBack.Visible = true;
                    buttonNext.Visible = false;
                    pictureTestCase.Visible = true;
                    lableGuide.Visible = true;
                    labelHint.Visible = true;
                    pictureMain.Visible = false;
                    pictureTestCase.Image = Image.FromFile("../../Resources/step3_3.png");
                    lableGuide.Text = Constants.language == 1 ? "Cùng luyện tập nào:" : "Let's practice:";
                    labelHint.Text = Constants.language == 1 ? "Mẹo: Dùng U R U' R' U' F' U F" : "Hint: Using U R U' R' U' F' U F";
                    testcase = new[]
                    {
                        new int[3,3]
                        {
                            {13,13,13},
                            {0,0,0},           // left
                            {0,0,0}
                        },

                        new int[3,3]
                        {
                            {13,13,13},
                            {1,1,1},            // right
                            {1,1,1}
                        },
                        new int[3,3]
                        {
                            {13,13,13},
                            {13,2,13},            // top
                            {13,13,13}
                        },
                        new int[3,3]
                        {
                            {3,3,3},
                            {3,3,3},            // down
                            {3,3,3}
                        },
                        new int[3,3]
                        {
                            {13,13,13},
                            {4,4,4},            // front
                            {4,4,4}
                        },
                        new int[3,3]
                        {
                            {13,13,13},
                            {5,5,5},            // back
                            {5,5,5}
                        }
                    };
                    _rubikData = new[]
                    {
                        new int[3,3]
                        {
                            {13,13,13},
                            {0,0,0},           // left
                            {0,0,0}
                        },

                        new int[3,3]
                        {
                            {13,13,13},
                            {1,1,1},            // right
                            {1,1,1}
                        },
                        new int[3,3]
                        {
                            {13,13,13},
                            {13,2,13},            // top
                            {13,13,13}
                        },
                        new int[3,3]
                        {
                            {3,3,3},
                            {3,3,3},            // down
                            {3,3,3}
                        },
                        new int[3,3]
                        {
                            {13,13,13},
                            {4,4,4},            // front
                            {4,4,4}
                        },
                        new int[3,3]
                        {
                            {13,13,13},
                            {5,5,5},            // back
                            {5,5,5}
                        }
                    };
                    testcaseRubik = new myRubik(testcase);
                    _newRubik = null;
                    _newRubik = new myRubik(_rubikData);
                    Scramble(_newRubik);
                    _newRubik.solveWhiteCross();
                    _newRubik.solveWhiteFace();
                    rubiksCube = new RubiksCube(_rubikData);
                    break;
                case 41:
                    glControl.Visible = false;
                    buttonNext.Visible = true;
                    buttonBack.Visible = false;
                    pictureTestCase.Visible = false;
                    lableGuide.Visible = false;
                    labelHint.Visible = false;
                    pictureMain.Visible = true;
                    pictureMain.Image = Constants.language == 1 ? Image.FromFile("../../Resources/step4_1v.png") : Image.FromFile("../../Resources/step4_1.png");
                    break;
                case 42:
                    glControl.Visible = false;
                    buttonNext.Visible = true;
                    buttonBack.Visible = true;
                    pictureTestCase.Visible = false;
                    lableGuide.Visible = false;
                    labelHint.Visible = false;
                    pictureMain.Visible = true;
                    pictureMain.Image = Constants.language == 1 ? Image.FromFile("../../Resources/step4_2v.png") : Image.FromFile("../../Resources/step4_2.png");
                    break;
                case 43:
                    glControl.Visible = true;
                    buttonBack.Visible = true;
                    buttonNext.Visible = false;
                    pictureTestCase.Visible = true;
                    lableGuide.Visible = true;
                    labelHint.Visible = true;
                    pictureMain.Visible = false;
                    pictureTestCase.Image = Image.FromFile("../../Resources/step4_3.png");
                    lableGuide.Text = Constants.language == 1 ? "Cùng luyện tập nào:" : "Let's practice:";
                    labelHint.Text = Constants.language == 1 ? "Mẹo: Dùng F U R U' R' F'" : "Hint: Using F U R U' R' F'";
                    testcase = new[]
                    {
                        new int[3,3]
                        {
                            {13,13,13},
                            {0,0,0},           // left
                            {0,0,0}
                        },

                        new int[3,3]
                        {
                            {13,13,13},
                            {1,1,1},            // right
                            {1,1,1}
                        },
                        new int[3,3]
                        {
                            {13,2,13},
                            {2,2,2},            // top
                            {13,2,13}
                        },
                        new int[3,3]
                        {
                            {3,3,3},
                            {3,3,3},            // down
                            {3,3,3}
                        },
                        new int[3,3]
                        {
                            {13,13,13},
                            {4,4,4},            // front
                            {4,4,4}
                        },
                        new int[3,3]
                        {
                            {13,13,13},
                            {5,5,5},            // back
                            {5,5,5}
                        }
                    };
                    _rubikData = new[]
                    {
                        new int[3,3]
                        {
                            {13,13,13},
                            {0,0,0},           // left
                            {0,0,0}
                        },

                        new int[3,3]
                        {
                            {13,13,13},
                            {1,1,1},            // right
                            {1,1,1}
                        },
                        new int[3,3]
                        {
                            {13,2,13},
                            {2,2,2},            // top
                            {13,2,13}
                        },
                        new int[3,3]
                        {
                            {3,3,3},
                            {3,3,3},            // down
                            {3,3,3}
                        },
                        new int[3,3]
                        {
                            {13,13,13},
                            {4,4,4},            // front
                            {4,4,4}
                        },
                        new int[3,3]
                        {
                            {13,13,13},
                            {5,5,5},            // back
                            {5,5,5}
                        }
                    };
                    testcaseRubik = new myRubik(testcase);
                    _newRubik = null;
                    _newRubik = new myRubik(_rubikData);
                    Scramble(_newRubik);
                    _newRubik.solveWhiteCross();
                    _newRubik.solveWhiteFace();
                    _newRubik.solveSecondLayer();
                    rubiksCube = new RubiksCube(_rubikData);
                    break;
                case 51:
                    glControl.Visible = false;
                    buttonNext.Visible = true;
                    buttonBack.Visible = false;
                    pictureTestCase.Visible = false;
                    lableGuide.Visible = false;
                    labelHint.Visible = false;
                    pictureMain.Visible = true;
                    pictureMain.Image = Constants.language == 1 ? Image.FromFile("../../Resources/step5_1v.png") : Image.FromFile("../../Resources/step5_1.png");
                    break;
                case 52:
                    glControl.Visible = true;
                    buttonBack.Visible = true;
                    buttonNext.Visible = false;
                    pictureTestCase.Visible = true;
                    lableGuide.Visible = true;
                    labelHint.Visible = true;
                    pictureMain.Visible = false;
                    pictureTestCase.Image = Image.FromFile("../../Resources/step5_2.png");
                    lableGuide.Text = Constants.language == 1 ? "Cùng luyện tập nào:" : "Let's practice:";
                    labelHint.Text = Constants.language == 1 ? "Mẹo: Dùng R U R' U R U2 R' U" : "Hint: Using R U R' U R U2 R' U";
                    testcase = new[]
                    {
                        new int[3,3]
                        {
                            {13,0,13},
                            {0,0,0},           // left
                            {0,0,0}
                        },

                        new int[3,3]
                        {
                            {13,1,13},
                            {1,1,1},            // right
                            {1,1,1}
                        },
                        new int[3,3]
                        {
                            {13,2,13},
                            {2,2,2},            // top
                            {13,2,13}
                        },
                        new int[3,3]
                        {
                            {3,3,3},
                            {3,3,3},            // down
                            {3,3,3}
                        },
                        new int[3,3]
                        {
                            {13,4,13},
                            {4,4,4},            // front
                            {4,4,4}
                        },
                        new int[3,3]
                        {
                            {13,5,13},
                            {5,5,5},            // back
                            {5,5,5}
                        }
                    };
                    _rubikData = new[]
                    {
                        new int[3,3]
                        {
                            {13,0,13},
                            {0,0,0},           // left
                            {0,0,0}
                        },

                        new int[3,3]
                        {
                            {13,1,13},
                            {1,1,1},            // right
                            {1,1,1}
                        },
                        new int[3,3]
                        {
                            {13,2,13},
                            {2,2,2},            // top
                            {13,2,13}
                        },
                        new int[3,3]
                        {
                            {3,3,3},
                            {3,3,3},            // down
                            {3,3,3}
                        },
                        new int[3,3]
                        {
                            {13,4,13},
                            {4,4,4},            // front
                            {4,4,4}
                        },
                        new int[3,3]
                        {
                            {13,5,13},
                            {5,5,5},            // back
                            {5,5,5}
                        }
                    };
                    testcaseRubik = new myRubik(testcase);
                    _newRubik = null;
                    _newRubik = new myRubik(_rubikData);
                    Scramble(_newRubik);
                    _newRubik.solveWhiteCross();
                    _newRubik.solveWhiteFace();
                    _newRubik.solveSecondLayer();
                    _newRubik.yellowCross();
                    rubiksCube = new RubiksCube(_rubikData);
                    break;
                case 61:
                    glControl.Visible = false;
                    buttonNext.Visible = false;
                    buttonBack.Visible = false;
                    pictureTestCase.Visible = false;
                    lableGuide.Visible = false;
                    labelHint.Visible = false;
                    pictureMain.Visible = true;
                    pictureMain.Image = Constants.language == 1 ? Image.FromFile("../../Resources/step6_1v.png") : Image.FromFile("../../Resources/step6_1.png");
                    break;
                case 71:
                    glControl.Visible = false;
                    buttonNext.Visible = true;
                    buttonBack.Visible = false;
                    pictureTestCase.Visible = false;
                    lableGuide.Visible = false;
                    labelHint.Visible = false;
                    pictureMain.Visible = true;
                    pictureMain.Image = Constants.language == 1 ? Image.FromFile("../../Resources/step7_1v.png") : Image.FromFile("../../Resources/step7_1.png");
                    break;
                case 72:
                    glControl.Visible = true;
                    buttonBack.Visible = true;
                    buttonNext.Visible = false;
                    pictureTestCase.Visible = true;
                    lableGuide.Visible = true;
                    labelHint.Visible = true;
                    pictureMain.Visible = false;
                    pictureTestCase.Image = Image.FromFile("../../Resources/step7_2.png");
                    lableGuide.Text = Constants.language == 1 ? "Cùng luyện tập nào:" : "Let's practice:";
                    labelHint.Text = Constants.language == 1 ? "Mẹo: Dùng U R U' L' U R' U' L rồi R' D' R D" : "Hint: Using U R U' L' U R' U' L then R' D' R D";
                    testcase = new[]
                    {
                        new int[3,3]
                        {
                            {0,0,0},
                            {0,0,0},           // left
                            {0,0,0}
                        },

                        new int[3,3]
                        {
                            {1,1,1},
                            {1,1,1},            // right
                            {1,1,1}
                        },
                        new int[3,3]
                        {
                            {2,2,2},
                            {2,2,2},            // top
                            {2,2,2}
                        },
                        new int[3,3]
                        {
                            {3,3,3},
                            {3,3,3},            // down
                            {3,3,3}
                        },
                        new int[3,3]
                        {
                            {4,4,4},
                            {4,4,4},            // front
                            {4,4,4}
                        },
                        new int[3,3]
                        {
                            {5,5,5},
                            {5,5,5},            // back
                            {5,5,5}
                        }
                    };
                    _rubikData = new[]
                    {
                        new int[3,3]
                        {
                            {0,0,0},
                            {0,0,0},           // left
                            {0,0,0}
                        },

                        new int[3,3]
                        {
                            {1,1,1},
                            {1,1,1},            // right
                            {1,1,1}
                        },
                        new int[3,3]
                        {
                            {2,2,2},
                            {2,2,2},            // top
                            {2,2,2}
                        },
                        new int[3,3]
                        {
                            {3,3,3},
                            {3,3,3},            // down
                            {3,3,3}
                        },
                        new int[3,3]
                        {
                            {4,4,4},
                            {4,4,4},            // front
                            {4,4,4}
                        },
                        new int[3,3]
                        {
                            {5,5,5},
                            {5,5,5},            // back
                            {5,5,5}
                        }
                    };
                    testcaseRubik = new myRubik(testcase);
                    _newRubik = null;
                    _newRubik = new myRubik(_rubikData);
                    Scramble(_newRubik);
                    _newRubik.solveWhiteCross();
                    _newRubik.solveWhiteFace();
                    _newRubik.solveSecondLayer();
                    _newRubik.yellowCross();
                    _newRubik.yellowApplyEdges();
                    rubiksCube = new RubiksCube(_rubikData);
                    break;
            }
        }
    }
}
