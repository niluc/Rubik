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
    public partial class Practice : Form
    {
        //double theta = 5.0;
        int time = 1;
        uint curFace;
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

        private Timer timer;
  
        public uint[] selectBuf = new uint[512];
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

        public Dictionary<string, string> rotateM = new Dictionary<string ,string>()
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

        // Rubik declard
        myRubik _newRubik;

        //RubikCubeForm rubiksCube;
        public Practice()
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
            //GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);
            Matrix4 matrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), 1.0f, 0.9f,
                        1000f);
            
            GL.LoadMatrix(ref matrix);
            //Glu.gluPerspective(45.0, 1.0, 0.9, 1000.0);
            GL.Translate(0,0,-2.5);
            
            GL.Ortho(   -Constants._orthor * Constants._rubikSize,
                        Constants._orthor * Constants._rubikSize,
                        -Constants._orthor * Constants._rubikSize,
                        Constants._orthor * Constants._rubikSize,
                        2*Constants._orthor * Constants._rubikSize,
                        -2*Constants._orthor * Constants._rubikSize);
            
            
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
                
                double size = 200.0/9.0;
                double dis = 150;

                GL.Enable(EnableCap.Texture2D);

                GL.BindTexture(TextureTarget.Texture2D, Practice.textures[7][0]);
                GL.Begin(PrimitiveType.Quads);
            
                GL.TexCoord2(0f,1f);
                GL.Vertex3(-size, size, size + dis);
                GL.TexCoord2(0f,0f);
                GL.Vertex3(-size, -size, size + dis);
                GL.TexCoord2(1f,0f);
                GL.Vertex3(size, -size, size + dis);
                GL.TexCoord2(1f,1f);
                GL.Vertex3(size, size, size + dis);
                GL.End();

                GL.BindTexture(TextureTarget.Texture2D, Practice.textures[8][0]);
                GL.Begin(PrimitiveType.Quads);
            
                GL.TexCoord2(0f,1f);
                GL.Vertex3(size, size, -size - dis);
                GL.TexCoord2(0f,0f);
                GL.Vertex3(size, -size, -size - dis);
                GL.TexCoord2(1f,0f);
                GL.Vertex3(-size, -size, -size - dis);
                GL.TexCoord2(1f,1f);
                GL.Vertex3(-size, size, -size - dis);
                GL.End();

                GL.BindTexture(TextureTarget.Texture2D, Practice.textures[9][0]);
                GL.Begin(PrimitiveType.Quads);
            
                GL.TexCoord2(0f,1f);
                GL.Vertex3(size + dis, size , size);
                GL.TexCoord2(0f,0f);
                GL.Vertex3(size + dis, -size , size);
                GL.TexCoord2(1f,0f);
                GL.Vertex3(size + dis, -size , -size);
                GL.TexCoord2(1f,1f);
                GL.Vertex3(size + dis, size , -size);
                GL.End();

                GL.BindTexture(TextureTarget.Texture2D, Practice.textures[10][0]);
                GL.Begin(PrimitiveType.Quads);
            
                GL.TexCoord2(0f,1f);
                GL.Vertex3(-size - dis, size , -size);
                GL.TexCoord2(0f,0f);
                GL.Vertex3(-size - dis, -size , -size);
                GL.TexCoord2(1f,0f);
                GL.Vertex3(-size - dis, -size , size);
                GL.TexCoord2(1f,1f);
                GL.Vertex3(-size - dis, size , size);
                GL.End();

                GL.BindTexture(TextureTarget.Texture2D, Practice.textures[11][0]);
                GL.Begin(PrimitiveType.Quads);
            
                GL.TexCoord2(0f,1f);
                GL.Vertex3(-size, size + dis , -size);
                GL.TexCoord2(0f,0f);
                GL.Vertex3(-size, size + dis, size);
                GL.TexCoord2(1f,0f);
                GL.Vertex3(size, size + dis, size);
                GL.TexCoord2(1f,1f);
                GL.Vertex3(size, size + dis, -size);
                GL.End();

                GL.BindTexture(TextureTarget.Texture2D, Practice.textures[12][0]);
                GL.Begin(PrimitiveType.Quads);
            
                GL.TexCoord2(0f,0f);
                GL.Vertex3(-size, -size - dis , -size);
                GL.TexCoord2(1f,0f);
                GL.Vertex3(size, -size - dis, -size);
                GL.TexCoord2(1f,1f);
                GL.Vertex3(size, -size - dis, size);
                GL.TexCoord2(0f,1f);
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
                    for(i = 0; i < hits; i++)
                    {
                        names = *cur;
                        cur++;
                        //Console.WriteLine(*ptr);
                        //Console.WriteLine(*cur);
                        //Console.WriteLine(*cur < minZ);
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

        // button
        private void buttonRotate_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            RubikCubeMoviment moviment = null;
            
            switch(btn.Text)
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
                /*
                case "Y":
                    moviment = new RubikCubeMoviment(Depth.First, Spin.Clockwise, Axis.Y, 3);
                    _newRubik.rotateAllY();
                    break;

                case "Y'":
                    //btn.Enabled = false;
                    moviment = new RubikCubeMoviment(Depth.First, Spin.Anticlockwise, Axis.Y, 3);
                    _newRubik.rotateAllY();
                    _newRubik.rotateAllY();
                    _newRubik.rotateAllY();
                    break;

                case "X":
                    moviment = new RubikCubeMoviment(Depth.First, Spin.Anticlockwise, Axis.X, 3);
                    _newRubik.rotateAllX();
                    break;

                case "Z":
                    moviment = new RubikCubeMoviment(Depth.First, Spin.Clockwise, Axis.Z, 3);
                    _newRubik.rotateAllZ();
                    break;
                */
            }
            
            rubiksCube.Manipulate(moviment);

        }

        public double p2i_coord(double x)
        {
            double x_i = (x - (((double)glControl.Width - 1.0)/2.0))*2.0/(double)glControl.Width;
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
                
                double r = 1.0 - startx*startx - starty*starty;
                if (r > 0)
                {
                    lastPos.X = startx;
                    lastPos.Y = starty;
                    lastPos.Z = Math.Sqrt(r);
                }
                else
                {
                    double d = Math.Sqrt(startx*startx + starty*starty);
                    lastPos.X = startx/d;
                    lastPos.Y = starty/d;
                    lastPos.Z = 0;
                }

                r = 1.0 - endx*endx - endy*endy;
                if (r > 0)
                {
                    curPos.X = endx;
                    curPos.Y = endy;
                    curPos.Z = Math.Sqrt(r);
                }
                else
                {
                    double d = Math.Sqrt(endx*endx + endy*endy);
                    curPos.X = endx/d;
                    curPos.Y = endy/d;
                    curPos.Z = 0;
                }

            
                //Vector3d sub = Vector3d.Subtract(curPos, lastPos);
                //Angle = 0.8 * 90 * sub.Length;
                Angle = 180/3.14 * Math.Acos(Math.Min(1, Vector3d.Dot(lastPos.Normalized(), curPos.Normalized())));
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
                if (selectedCube != (i = doPicking(e.X, e.Y)) )//&& curFace == selectedFace)
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
            //Console.WriteLine(face);
            //Console.WriteLine(selectedFace);
            //Console.WriteLine(code);

            Console.WriteLine(dir);
            
            //Console.WriteLine(dir);
            //Console.WriteLine();
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
            if (moviment != null) { 
                rubiksCube.Manipulate(moviment);
            }
        }


        private void glControl_MouseUp(object sender, MouseEventArgs e)
        {
            isPress = false;
            track = false;
            //Angle = 0;
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        _rubikData[i][j, k] = i;
                    }
                }
            }
            rubiksCube = new RubiksCube(_rubikData);
        }

        private void Scramble_Click(object sender, EventArgs e)
        {
            RubikCubeMoviment moviment = null;
            Random r = new Random();
            string s = "";

            for(int i = 0; i < 25; i++)
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
                        moviment = new RubikCubeMoviment(Depth.First, Spin.Anticlockwise, Axis.X, 1);
                        _newRubik.RotateRubik("L");
                        break;

                    case "l":
                        moviment = new RubikCubeMoviment(Depth.First, Spin.Clockwise, Axis.X, 1);
                        _newRubik.RotateRubik("L'");
                        break;

                    case "R":
                        moviment = new RubikCubeMoviment(Depth.Third, Spin.Clockwise, Axis.X, 1);
                        _newRubik.RotateRubik("R");
                        break;

                    case "r":
                        moviment = new RubikCubeMoviment(Depth.Third, Spin.Anticlockwise, Axis.X, 1);
                        _newRubik.RotateRubik("R'");
                        break;

                    case "F":
                        moviment = new RubikCubeMoviment(Depth.First, Spin.Clockwise, Axis.Z, 1);
                        _newRubik.RotateRubik("F");
                        break;

                    case "f":
                        moviment = new RubikCubeMoviment(Depth.First, Spin.Anticlockwise, Axis.Z, 1);
                        _newRubik.RotateRubik("F'");
                        break;

                    case "B":
                        moviment = new RubikCubeMoviment(Depth.Third, Spin.Anticlockwise, Axis.Z, 1);
                        _newRubik.RotateRubik("B");
                        break;

                    case "b":
                        moviment = new RubikCubeMoviment(Depth.Third, Spin.Clockwise, Axis.Z, 1);
                        _newRubik.RotateRubik("B'");
                        break;

                    case "U":
                        moviment = new RubikCubeMoviment(Depth.First, Spin.Clockwise, Axis.Y, 1);
                        _newRubik.RotateRubik("U");
                        break;

                    case "u":
                        moviment = new RubikCubeMoviment(Depth.First, Spin.Anticlockwise, Axis.Y, 1);
                        _newRubik.RotateRubik("U'");
                        break;

                    case "D":
                        moviment = new RubikCubeMoviment(Depth.Third, Spin.Anticlockwise, Axis.Y, 1);
                        _newRubik.RotateRubik("D");
                        break;

                    case "d":
                        moviment = new RubikCubeMoviment(Depth.Third, Spin.Clockwise, Axis.Y, 1);
                        _newRubik.RotateRubik("D'");
                        break;
                }
                rubiksCube.Manipulate(moviment);
            }
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
                    curFace = selectedFace;
                }
                //Console.WriteLine(i);
                
            }
        }

        //Solve
        private void button1_Click(object sender, EventArgs e)
        {
            isSolving = true;
            
            List<string> path1 = _newRubik.solveWhiteCross();
            List<string> path2 = _newRubik.solveWhiteFace();
            List<string> path3 = _newRubik.solveSecondLayer();
            List<string> path4 = _newRubik.yellowCross();
            List<string> path5 = _newRubik.yellowApplyEdges();
            List<string> path6 = _newRubik.yellowApplyCorners();
            List<string> path7 = _newRubik.yellowCornerOrient();
            List<string> path8 = _newRubik.preSolve();

            path1.AddRange(path2);
            path1.AddRange(path3);
            path1.AddRange(path4);
            path1.AddRange(path5);
            path1.AddRange(path6);
            path1.AddRange(path7);
            path1.AddRange(path8);
            
            //List<string> path1 = _newRubik.preSolve();
            string step = "";

            foreach (var item in path1)
            {
                switch (item)
                {
                    case "L'":
                        step += "l" ;
                        break;
                    case "R'":
                        step += "r" ;
                        break;
                    case "F'":
                        step += "f" ;
                        break;
                    case "B'":
                        step += "b" ;
                        break;
                    case "U'":
                        step += "u";
                        break;
                    case "D'":
                        step += "d";
                        break;
                    default:
                        step += item;
                        break;
                }
            }
            
            step = step.Replace("YYYY", "");
            step = step.Replace("FFFF", "");
            step = step.Replace("Rr","").Replace("rR","").Replace("lL","").Replace("Ll","");
            step = step.Replace("UUU","u").Replace("YuYU","YY").Replace("uYYU","YY").Replace("yuyU","yy").Replace("uyyU","yy");
            step = step.Replace("FFF","f").Replace("YUYu","YY").Replace("UYYu","YY").Replace("yUyu","yy").Replace("Uyyu","yy");
            step = step.Replace("XXX","x").Replace("YUyu","").Replace("yuYU","").Replace("yuYu","uu").Replace("yuuY","uu");
            step = step.Replace("ZZZ","z").Replace("YYUUYY","UU").Replace("YYuuYY","uu").Replace("YYUYY","U").Replace("YYuYY","u");
            step = step.Replace("yyUyy","U").Replace("UUYYU","uYY").Replace("YYY", "y").Replace("YuYY","yu").Replace("YUYY","yU");
            step = step.Replace("Yy","").Replace("yY","").Replace("Ff","").Replace("fF","").Replace("YUyU","UU").Replace("YUUy","UU");
            step = step.Replace("YUYU", "YYUU").Replace("YuYu","YYuu").Replace("yUyU","yyUU").Replace("yuyu","yyuu").Replace("yuY","u");
            step = step.Replace("Yuy","u").Replace("YUy","U").Replace("yUY","U").Replace("uYU","Y");
            step = step.Replace("UUUU", "").Replace("UUU", "u").Replace("YUUYYU","yu").Replace("Uu","").Replace("uU","");
            step = step.Replace("YYY", "y").Replace("YUYu","YY").Replace("uYu","Yuu").Replace("UYU","YUU").Replace("yuY","u").Replace("UYu","Y");
            step = step.Replace("yUY","U").Replace("Yuy","u").Replace("YUy","U").Replace("Yy","").Replace("yY","");

            Console.WriteLine("");
            Console.WriteLine(step);
            Console.WriteLine(step.Length);
            RubikCubeMoviment moviment = null;

            for(int i = 0; i < step.Length; i++)
            {
                switch (step[i].ToString())
                {
                    case "L":
                        moviment = new RubikCubeMoviment(Depth.First, Spin.Anticlockwise, Axis.X, 1);
                        break;
            
                    case "l":
                        moviment = new RubikCubeMoviment(Depth.First, Spin.Clockwise, Axis.X, 1);
                        break;

                    case "R":
                        moviment = new RubikCubeMoviment(Depth.Third, Spin.Clockwise, Axis.X, 1);
                        break;
            
                    case "r":
                        moviment = new RubikCubeMoviment(Depth.Third, Spin.Anticlockwise, Axis.X, 1);
                        break;
            
                    case "F":
                        moviment = new RubikCubeMoviment(Depth.First, Spin.Clockwise, Axis.Z, 1);
                        break;
            
                    case "f":
                        moviment = new RubikCubeMoviment(Depth.First, Spin.Anticlockwise, Axis.Z, 1);
                        break;

                    case "B":
                        moviment = new RubikCubeMoviment(Depth.Third, Spin.Anticlockwise, Axis.Z, 1);
                        break;

                    case "b":
                        moviment = new RubikCubeMoviment(Depth.Third, Spin.Clockwise, Axis.Z, 1);
                        break;
            
                    case "U":
                        moviment = new RubikCubeMoviment(Depth.First, Spin.Clockwise, Axis.Y, 1);
                        break;
            
                    case "u":
                        moviment = new RubikCubeMoviment(Depth.First, Spin.Anticlockwise, Axis.Y, 1);
                        break;
            
                    case "D":
                        moviment = new RubikCubeMoviment(Depth.Third, Spin.Anticlockwise, Axis.Y, 1);
                        break;
            
                    case "d":
                        moviment = new RubikCubeMoviment(Depth.Third, Spin.Clockwise, Axis.Y, 1);
                        break;

                    case "Y":
                        moviment = new RubikCubeMoviment(Depth.First, Spin.Clockwise, Axis.Y, 3);
                        break;

                    case "y":
                        moviment = new RubikCubeMoviment(Depth.First, Spin.Anticlockwise, Axis.Y, 3);
                        break;

                    case "X":
                        moviment = new RubikCubeMoviment(Depth.First, Spin.Anticlockwise, Axis.X, 3);
                        break;

                    case "x":
                        moviment = new RubikCubeMoviment(Depth.First, Spin.Clockwise, Axis.X, 3);
                        break;

                    case "Z":
                        moviment = new RubikCubeMoviment(Depth.First, Spin.Clockwise, Axis.Z, 3);
                        break;

                    case "z":
                        moviment = new RubikCubeMoviment(Depth.First, Spin.Anticlockwise, Axis.Z, 3);
                        break;

                }
                rubiksCube.Manipulate(moviment);
            }
            
            List<string> sol = new List<string>();

            step = step.Replace("UU","A").Replace("uu","a");
            step = step.Replace("LL","K").Replace("ll","k");
            step = step.Replace("RR","H").Replace("rr","h");
            step = step.Replace("FF","N").Replace("ff","n");
            step = step.Replace("BB","V").Replace("bb","v");
            step = step.Replace("DD","S").Replace("dd","s");
            step = step.Replace("YY","G").Replace("yy","g");

            for(int i = 0; i < step.Length; i++)
            {
                switch (step[i].ToString())
                {
                    case "l":
                        sol.Add("L'");
                        break;

                    case "r":
                        sol.Add("R'");
                        break;
            
                    case "f":
                        sol.Add("F'");
                        break;

                    case "b":
                        sol.Add("B'");
                        break;
            
                    case "u":
                        sol.Add("U'");
                        break;
            
                    case "d":
                        sol.Add("D'");
                        break;

                    case "y":
                        sol.Add("Y'");
                        break;

                    case "x":
                        sol.Add("X'");
                        break;

                    case "z":
                        sol.Add("Z'");
                        break;

                    case "a":
                        sol.Add("U'2");
                        break;

                    case "A":
                        sol.Add("U2");
                        break;

                    case "k":
                        sol.Add("L'2");
                        break;

                    case "K":
                        sol.Add("L2");
                        break;

                    case "h":
                        sol.Add("R'2");
                        break;

                    case "H":
                        sol.Add("R2");
                        break;

                    case "n":
                        sol.Add("F'2");
                        break;
                    
                    case "N":
                        sol.Add("F2");
                        break;

                    case "G":
                        sol.Add("Y2");
                        break;

                    case "g":
                        sol.Add("Y'2");
                        break;

                    case "V":
                        sol.Add("B2");
                        break;

                    case "v":
                        sol.Add("B'2");
                        break;

                    case "S":
                        sol.Add("D2");
                        break;

                    case "s":
                        sol.Add("D'2");
                        break;

                    default:
                        sol.Add(step[i].ToString());
                        break;
                }
                
            }
            step = step.Replace("Y","").Replace("y","");
            step = step.Replace("X","").Replace("x","");
            step = step.Replace("Z","").Replace("z","");
            step = step.Replace("G","").Replace("g","");
            
            foreach(var item in sol)
            {
                Console.Write(item + ", ");
            }
            
            Console.WriteLine(step.Length);
            isSolving = false;
        }
        
    }
}
