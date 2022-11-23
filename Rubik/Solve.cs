using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using OpenTK;                       // Matrix 4
using Tao.OpenGl;


namespace Rubik
{
    public partial class Solve : Form
    {
        [DllImport(@"../../RubikCamera/x64/Debug/RubikCamera.dll", CallingConvention = CallingConvention.Cdecl)]
        //[DllImport("C:\\Users\\phatn\\OneDrive\\Desktop\\RubikCamera.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe int* cameraInput();

        private double startX, startY;
        private double endX, endY;
        private bool track = false;
        private bool isPress = false;
        bool draw_axis = false;
        public static uint selectedFace;
        public static int selectedCube;
        private int currentColor;
        List<string> sol = new List<string>();
        private bool isAuto = false;
        //private double AngleX = 0;
        //private double AngleY = 0;
        //private double AngleZ = 0;

        private string solution = null;
        private string reverse = null;
        private int index = 0;
        private double Angle = 0;

        private Vector3d rot = new Vector3d(0.0, 0.0, 0.0);
        private Vector3d lastPos = new Vector3d(0.0, 0.0, 0.0);

        public static uint[][] textures = new uint[14][];
        private Timer timer;
        private uint[] selectBuf = new uint[512];
        private static Matrix4d mat = new Matrix4d(0.778246879577637, -0.335064768791199, 0.531095266342163, 0,
                                                   0.0402242317795753, 0.870613098144531, 0.490320920944214, 0,
                                                   -0.62666779756546, -0.360228270292282, 0.69103080034256, 0,
                                                   0, 0, 0, 1);

        private RubiksCube rubiksCube;
        public int[][,] subData = {

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
        Dictionary<string, string> color = new Dictionary<string, string>()
        {
            {"01","020"},
            {"02","021"},
            {"03","022"},
            {"04","010"},
            {"05","011"},
            {"06","012"},
            {"07","000"},
            {"08","001"},
            {"09","002"},
            {"119","122"},
            {"120","121"},
            {"121","120"},
            {"122","112"},
            {"123","111"},
            {"124","110"},
            {"125","102"},
            {"126","101"},
            {"127","100"},
            {"27","200"},
            {"28","210"},
            {"29","220"},
            {"216","201"},
            {"217","211"},
            {"218","221"},
            {"225","202"},
            {"226","212"},
            {"227","222"},
            {"31","320"},
            {"32","310"},
            {"33","300"},
            {"310","321"},
            {"311","311"},
            {"312","301"},
            {"319","322"},
            {"320","312"},
            {"321","302"},
            {"43","420"},
            {"46","410"},
            {"49","400"},
            {"412","421"},
            {"415","411"},
            {"418","401"},
            {"421","422"},
            {"424","412"},
            {"427","402"},
            {"525","500"},
            {"522","510"},
            {"519","520"},
            {"516","501"},
            {"513","511"},
            {"510","521"},
            {"57","502"},
            {"54","512"},
            {"51","522"},
        };
        
        public Solve()
        {
            //InitializeComponent();
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
        }

        private void GameLoop(object sender, System.EventArgs e)
        {
            // Update coordinates of game entities
            // or check collisions
            FrameUpdate();

            // This method calls glControl_Paint
            glControl1.Invalidate();
        }

        private void FrameUpdate()
        {
            //theta += 1.0;
            //case (theta > 360) theta -= 360;
        }

        private void Tick(object sender, EventArgs e)
        {
            this.glControl1.Invalidate();
            
            if (isAuto && !rubiksCube.isAnimate && solution != null)
            {
                RubikCubeMoviment moviment = null;
                if (index == -1)
                { 
                    index = 0;
                }
                if (index < solution.Length && index >= 0)
                {
                    
                    string step = "";
                    switch (solution[index].ToString())
                    {
                        case "L":
                            moviment = new RubikCubeMoviment(Depth.First, Spin.Anticlockwise, Axis.X, 1);
                            step = "L";
                            break;

                        case "l":
                            moviment = new RubikCubeMoviment(Depth.First, Spin.Clockwise, Axis.X, 1);
                            step = "L'";
                            break;

                        case "R":
                            moviment = new RubikCubeMoviment(Depth.Third, Spin.Clockwise, Axis.X, 1);
                            step = "R";
                            break;

                        case "r":
                            moviment = new RubikCubeMoviment(Depth.Third, Spin.Anticlockwise, Axis.X, 1);
                            step = "R'";
                            break;

                        case "F":
                            moviment = new RubikCubeMoviment(Depth.First, Spin.Clockwise, Axis.Z, 1);
                            step = "F";
                            break;

                        case "f":
                            moviment = new RubikCubeMoviment(Depth.First, Spin.Anticlockwise, Axis.Z, 1);
                            step = "F'";
                            break;

                        case "B":
                            moviment = new RubikCubeMoviment(Depth.Third, Spin.Anticlockwise, Axis.Z, 1);
                            step = "B";
                            break;

                        case "b":
                            moviment = new RubikCubeMoviment(Depth.Third, Spin.Clockwise, Axis.Z, 1);
                            step = "B'";
                            break;

                        case "U":
                            moviment = new RubikCubeMoviment(Depth.First, Spin.Clockwise, Axis.Y, 1);
                            step = "U";
                            break;

                        case "u":
                            moviment = new RubikCubeMoviment(Depth.First, Spin.Anticlockwise, Axis.Y, 1);
                            step = "U'";
                            break;

                        case "D":
                            moviment = new RubikCubeMoviment(Depth.Third, Spin.Anticlockwise, Axis.Y, 1);
                            step = "D";
                            break;

                        case "d":
                            moviment = new RubikCubeMoviment(Depth.Third, Spin.Clockwise, Axis.Y, 1);
                            step = "D'";
                            break;

                        case "Y":
                            moviment = new RubikCubeMoviment(Depth.First, Spin.Clockwise, Axis.Y, 3);
                            step = "Y";
                            break;

                        case "y":
                            moviment = new RubikCubeMoviment(Depth.First, Spin.Anticlockwise, Axis.Y, 3);
                            step = "Y'";
                            break;

                        case "X":
                            moviment = new RubikCubeMoviment(Depth.First, Spin.Anticlockwise, Axis.X, 3);
                            step = "X";
                            break;

                        case "x":
                            moviment = new RubikCubeMoviment(Depth.First, Spin.Clockwise, Axis.X, 3);
                            step = "X'";
                            break;

                        case "Z":
                            moviment = new RubikCubeMoviment(Depth.First, Spin.Clockwise, Axis.Z, 3);
                            step = "Z";
                            break;

                        case "z":
                            moviment = new RubikCubeMoviment(Depth.First, Spin.Anticlockwise, Axis.Z, 3);
                            step = "Z'";
                            break;

                    }
                    curstep.Text = step;
                    
                    rubiksCube.Manipulate(moviment);

                    index++;
                    
                    if (index <= solution.Length - 1)
                    {
                        curstep.Text = sol[index];
                        if (index >= 2 && index <= solution.Length - 3)
                        {
                            pre.Text = sol[index - 2] + "  " + sol[index - 1];
                            nex.Text = sol[index + 1] + "  " + sol[index + 2];
                        }
                        else
                        {
                            pre.Text = "";
                            nex.Text = "";
                        }
                    }
                    
                }

            }
        }

        private void glcontrol_Load(object sender, EventArgs e)
        {
            
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
            
            glControl1.Size = new System.Drawing.Size(2 * Constants._rubikSize, 2 * Constants._rubikSize);
            //glControl.Size = new System.Drawing.Size(2 * 210, 2 * 210);
            glControl1.MakeCurrent();
            GL.Viewport(0, 0, 2 * Constants._rubikSize, 2 * Constants._rubikSize);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            GL.ClearColor(ColorTranslator.FromHtml("#222223"));
            //GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);
            Matrix4 matrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), 1, 0.9f,
                        1000f);
            //GL.Translate(0,0,-2.5);
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

            glControl1.SwapBuffers();
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

            if (draw_axis)
            {
               
                double size = 200.0/9.0;
                double dis = 150;
                GL.Enable(EnableCap.Texture2D);

                GL.BindTexture(TextureTarget.Texture2D, Solve.textures[7][0]);
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

                GL.BindTexture(TextureTarget.Texture2D, Solve.textures[8][0]);
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

                GL.BindTexture(TextureTarget.Texture2D, Solve.textures[9][0]);
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

                GL.BindTexture(TextureTarget.Texture2D, Solve.textures[10][0]);
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

                GL.BindTexture(TextureTarget.Texture2D, Solve.textures[11][0]);
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

                GL.BindTexture(TextureTarget.Texture2D, Solve.textures[12][0]);
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

        private void glcontrol_Paint(object sender, PaintEventArgs e)
        {
            GL.Enable(EnableCap.DepthTest);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);
            GL.MatrixMode(MatrixMode.Modelview);
            //Gl.glMatrixMode(Gl.GL_MODELVIEW);

            renderObjs();
            //GL.PopMatrix();
            glControl1.SwapBuffers();
        }

        private void glcontrol_KeyDown(object sender, KeyEventArgs e)
        {
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

        private void glcontrol_KeyUp(object sender, KeyEventArgs e)
        {
            track = false;
        }

        private void glcontrol_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isPress = true;
                startX = (double)(e.X);
                startY = (double)glControl1.Width - (double)e.Y - 1.0;
                //Console.WriteLine(doPicking(e.X, e.Y));
                //Console.WriteLine(selectedFace);
                int i = doPicking(e.X, e.Y);
                if (i == 0)
                {
                    track = true;
                    //isClick = true;
                }
                
                else
                {
                    selectedCube = i;
                    string code = selectedFace.ToString() + i.ToString();
                    if (color.ContainsKey(code))
                    {
                        string pos = color[code];
                        var x = int.Parse(pos[0].ToString());
                        var y = int.Parse(pos[1].ToString());
                        var z = int.Parse(pos[2].ToString());
                        //Console.WriteLine(pos);
                        //rubiksCube.data[pos / 100][(pos % 100) / 100, ((pos % 100) % 100) / 100] = currentColor;
                        _rubikData[x][y, z] = currentColor;
                        //rubiksCube = null;
                        rubiksCube = new RubiksCube(_rubikData);
                        //Console.WriteLine(_rubikData[x][y, z]);
                        //subData = _rubikData;
                        //isClick = false;
                    }

                }
                
                //Console.WriteLine(i);

            }
        }

        public double p2i_coord(double x)
        {
            double x_i = (x - (((double)glControl1.Width - 1.0) / 2.0)) * 2.0 / (double)glControl1.Width;
            return x_i;
        }

        private void glcontrol_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (isPress && track)
            {
                Vector3d curPos = new Vector3d(0.0, 0.0, 0.0);
                endX = (double)e.X;
                endY = glControl1.Height - (double)e.Y - 1.0;

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
                Angle = 180/3.14 * Math.Acos(Math.Min(1, Vector3d.Dot(lastPos.Normalized(), curPos.Normalized())));
                rot = Vector3d.Cross(lastPos, curPos);

                if (Angle < -180 || Angle > 180)
                {
                    Angle = 0.0;
                }

                startX = endX;
                startY = endY;

            }
            
            /*
            if (isPress && !track && !isSolving)
            {
                int i;
                if (selectedCube != (i = doPicking(e.X, e.Y)))
                {
                    //rotateByMouse(selectedCube, i);
                    isPress = false;
                }
            }
            */

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
            glControl1.SwapBuffers();

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


        private void glcontrol_MouseUp(object sender, MouseEventArgs e)
        {
            isPress = false;
            track = false;
            selectedCube = -1;
            selectedFace = 10;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            //comboBox1.Text = "Blue";
            ComboBox cb = sender as ComboBox;
            switch (cb.SelectedItem.ToString())
            {
                case "Blue":
                    currentColor = 0;
                    break;

                case "Red":
                    currentColor = 4;
                    break;

                case "Green":
                    currentColor = 1;
                    break;

                case "Orange":
                    currentColor = 5;
                    break;

                case "Yellow":
                    currentColor = 2;
                    break;

                case "White":
                    currentColor = 3;
                    break;
            }


        }

        private void Next_Click(object sender, EventArgs e)
        {
            if (solution != null)
            {
                RubikCubeMoviment moviment = null;
                if (index <= solution.Length - 1 && index >= 0)
                {
                    string step = "";
                    switch (solution[index].ToString())
                    {
                        case "L":
                            moviment = new RubikCubeMoviment(Depth.First, Spin.Anticlockwise, Axis.X, 1);
                            step = "L";
                            break;

                        case "l":
                            moviment = new RubikCubeMoviment(Depth.First, Spin.Clockwise, Axis.X, 1);
                            step = "L'";
                            break;

                        case "R":
                            moviment = new RubikCubeMoviment(Depth.Third, Spin.Clockwise, Axis.X, 1);
                            step = "R";
                            break;

                        case "r":
                            moviment = new RubikCubeMoviment(Depth.Third, Spin.Anticlockwise, Axis.X, 1);
                            step = "R'";
                            break;

                        case "F":
                            moviment = new RubikCubeMoviment(Depth.First, Spin.Clockwise, Axis.Z, 1);
                            step = "F";
                            break;

                        case "f":
                            moviment = new RubikCubeMoviment(Depth.First, Spin.Anticlockwise, Axis.Z, 1);
                            step = "F'";
                            break;

                        case "B":
                            moviment = new RubikCubeMoviment(Depth.Third, Spin.Anticlockwise, Axis.Z, 1);
                            step = "B";
                            break;

                        case "b":
                            moviment = new RubikCubeMoviment(Depth.Third, Spin.Clockwise, Axis.Z, 1);
                            step = "B'";
                            break;

                        case "U":
                            moviment = new RubikCubeMoviment(Depth.First, Spin.Clockwise, Axis.Y, 1);
                            step = "U";
                            break;

                        case "u":
                            moviment = new RubikCubeMoviment(Depth.First, Spin.Anticlockwise, Axis.Y, 1);
                            step = "U'";
                            break;

                        case "D":
                            moviment = new RubikCubeMoviment(Depth.Third, Spin.Anticlockwise, Axis.Y, 1);
                            step = "D";
                            break;

                        case "d":
                            moviment = new RubikCubeMoviment(Depth.Third, Spin.Clockwise, Axis.Y, 1);
                            step = "D'";
                            break;

                        case "Y":
                            moviment = new RubikCubeMoviment(Depth.First, Spin.Clockwise, Axis.Y, 3);
                            step = "Y";
                            break;

                        case "y":
                            moviment = new RubikCubeMoviment(Depth.First, Spin.Anticlockwise, Axis.Y, 3);
                            step = "Y'";
                            break;

                        case "X":
                            moviment = new RubikCubeMoviment(Depth.First, Spin.Anticlockwise, Axis.X, 3);
                            step = "X";
                            break;

                        case "x":
                            moviment = new RubikCubeMoviment(Depth.First, Spin.Clockwise, Axis.X, 3);
                            step = "X'";
                            break;

                        case "Z":
                            moviment = new RubikCubeMoviment(Depth.First, Spin.Clockwise, Axis.Z, 3);
                            step = "Z";
                            break;

                        case "z":
                            moviment = new RubikCubeMoviment(Depth.First, Spin.Anticlockwise, Axis.Z, 3);
                            step = "Z'";
                            break;

                    }
                    
                    if (index >= 2 && index <= solution.Length - 4)
                    {
                        curstep.Text = sol[index + 1];
                        pre.Text = sol[index - 1] + "  " + sol[index];
                        nex.Text = sol[index + 2] + "  " + sol[index + 3];
                    }
                    else
                    {
                        pre.Text = "";
                        nex.Text = "";
                    }
                    rubiksCube.Manipulate(moviment);
                }
               
                index++;
                
                Previous.Enabled = true;
                if (index >= solution.Length)
                {
                    Next.Enabled = false;
                }
            }
            else
            {
                 Previous.Enabled = true;
                 Next.Enabled = false;
            }
        }

        private void Previous_Click(object sender, EventArgs e)
        {
            RubikCubeMoviment moviment = null;
            if (index > 0 && reverse != "" && index <= solution.Length - 1)
            {
                string step = "";
                switch (reverse[index - 1].ToString())
                {
                    case "L":
                        moviment = new RubikCubeMoviment(Depth.First, Spin.Anticlockwise, Axis.X, 1);
                        step = "L";
                        break;

                    case "l":
                        moviment = new RubikCubeMoviment(Depth.First, Spin.Clockwise, Axis.X, 1);
                        step = "L'";
                        break;

                    case "R":
                        moviment = new RubikCubeMoviment(Depth.Third, Spin.Clockwise, Axis.X, 1);
                        step = "R";
                        break;

                    case "r":
                        moviment = new RubikCubeMoviment(Depth.Third, Spin.Anticlockwise, Axis.X, 1);
                        step = "R'";
                        break;

                    case "F":
                        moviment = new RubikCubeMoviment(Depth.First, Spin.Clockwise, Axis.Z, 1);
                        step = "F";
                        break;

                    case "f":
                        moviment = new RubikCubeMoviment(Depth.First, Spin.Anticlockwise, Axis.Z, 1);
                        step = "F'";
                        break;

                    case "B":
                        moviment = new RubikCubeMoviment(Depth.Third, Spin.Anticlockwise, Axis.Z, 1);
                        step = "B";
                        break;

                    case "b":
                        moviment = new RubikCubeMoviment(Depth.Third, Spin.Clockwise, Axis.Z, 1);
                        step = "B'";
                        break;

                    case "U":
                        moviment = new RubikCubeMoviment(Depth.First, Spin.Clockwise, Axis.Y, 1);
                        step = "U";
                        break;

                    case "u":
                        moviment = new RubikCubeMoviment(Depth.First, Spin.Anticlockwise, Axis.Y, 1);
                        step = "U'";
                        break;

                    case "D":
                        moviment = new RubikCubeMoviment(Depth.Third, Spin.Anticlockwise, Axis.Y, 1);
                        step = "D";
                        break;

                    case "d":
                        moviment = new RubikCubeMoviment(Depth.Third, Spin.Clockwise, Axis.Y, 1);
                        step = "D'";
                        break;

                    case "Y":
                        moviment = new RubikCubeMoviment(Depth.First, Spin.Clockwise, Axis.Y, 3);
                        step = "Y";
                        break;

                    case "y":
                        moviment = new RubikCubeMoviment(Depth.First, Spin.Anticlockwise, Axis.Y, 3);
                        step = "Y'";
                        break;

                    case "X":
                        moviment = new RubikCubeMoviment(Depth.First, Spin.Anticlockwise, Axis.X, 3);
                        step = "X";
                        break;

                    case "x":
                        moviment = new RubikCubeMoviment(Depth.First, Spin.Clockwise, Axis.X, 3);
                        step = "X'";
                        break;

                    case "Z":
                        moviment = new RubikCubeMoviment(Depth.First, Spin.Clockwise, Axis.Z, 3);
                        step = "Z";
                        break;

                    case "z":
                        moviment = new RubikCubeMoviment(Depth.First, Spin.Anticlockwise, Axis.Z, 3);
                        step = "Z'";
                        break;

                }
                curstep.Text = sol[index - 1];
                if (index >= 2 && index <= solution.Length - 2)
                {
                    pre.Text = sol[index - 2] + "  " + sol[index - 2];
                    nex.Text = sol[index] + "  " + sol[index + 1];
                }
                else
                {
                    pre.Text = "";
                    nex.Text = "";
                }
                rubiksCube.Manipulate(moviment);
                Next.Enabled = true;
                
            }
            index--;

            
            if (index < 0)
            {
                Previous.Enabled = false;
                Next.Enabled = true;
            }
            Console.WriteLine(index);
        }

        private void Auto_Click(object sender, EventArgs e)
        {
            isAuto = true;
            Next.Enabled = false;
            Previous.Enabled = false;
            //camera.Enabled = false;
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            isAuto = false;
            Next.Enabled = true;
            Previous.Enabled = true;
            camera.Enabled = true;
            /*
            if(sol.Count() != 0 && index <= solution.Length - 3)
            {
                curstep.Text = sol[index];
                pre.Text = sol[index - 2] + "  " + sol[index - 1];
                nex.Text = sol[index + 1] + "  " + sol[index + 2];
            }
            */
            
        }

        private unsafe void camera_Click(object sender, EventArgs e)
        {
            //Next.Enabled = true;
            //Previous.Enabled = true;
            camera.Enabled = false;
            int[] map = new int[54];
            
            int* m = Solve.cameraInput();

            if (m != null)
            {
                for(int i = 0; i < 54; i++)
                {
                    //Console.WriteLine(*(m + i));
                    //p++;
                    map[i] = *(m + i);
                
                }
                //Console.WriteLine(*p);

                int[] stt = {4, 0, 5, 1, 2, 3};
                int p = 0;

                for(int k = 0; k < 6; k++)
                {
                    int t = stt[k];
                    for(int i = 0; i < 3; i++)
                    {
                        for(int j = 0; j < 3; j++)
                        {
                            _rubikData[t][i, j] = map[p];
                            p++;
                        }
                    }
                }

                rubiksCube = new RubiksCube(_rubikData);
            }
            camera.Enabled = true;
            //Console.WriteLine(map);
        }

        private void reset_Click(object sender, EventArgs e)
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
            curstep.Text = "...";
            rubiksCube = new RubiksCube(_rubikData);
            button1.Enabled = true;
            camera.Enabled = true;
            Next.Enabled = true;
            Previous.Enabled = true;
        }

        /*
private void optimal_Click(object sender, EventArgs e)
{
   solution = null;
   int[] stt = { 2, 1, 4, 3, 0, 5 };
   int p = 0;
   string config = "";
   for (int k = 0; k < 6; k++)
   {
       int t = stt[k];
       for (int i = 0; i < 3; i++)
       {
           for (int j = 0; j < 3; j++)
           {
               string c = "";
               if (_rubikData[t][i, j] == _rubikData[0][1, 1])
               {
                   c = "L";
               }
               if (_rubikData[t][i, j] == _rubikData[1][1, 1])
               {
                   c = "R";
               }
               if (_rubikData[t][i, j] == _rubikData[2][1, 1])
               {
                   c = "U";
               }
               if (_rubikData[t][i, j] == _rubikData[3][1, 1])
               {
                   c = "D";
               }
               if (_rubikData[t][i, j] == _rubikData[4][1, 1])
               {
                   c = "F";
               }
               if (_rubikData[t][i, j] == _rubikData[5][1, 1])
               {
                   c = "B";
               }
               config += c;
               p++;
           }
       }
   }

   var psi = new ProcessStartInfo();
   psi.FileName = @"C:/New Folder/python.exe";
   var script = @"C:/Users/phatn/test.py";
   var s = config;

   psi.Arguments = $"{script} {s}";
   psi.UseShellExecute = false;
   psi.CreateNoWindow = true;
   psi.RedirectStandardOutput = true;
   psi.RedirectStandardError = true;
   var errors = "";
   var s1 = "";

   using (var process = Process.Start(psi))
   {
       errors = process.StandardError.ReadToEnd();
       s1 = process.StandardOutput.ReadToEnd();
   }
   Console.WriteLine(s1);
   s1 = s1.Replace(" ", "");
   s1 = s1.Replace("F'", "f");
   s1 = s1.Replace("B'", "b");
   s1 = s1.Replace("R'", "r");
   s1 = s1.Replace("L'", "l");
   s1 = s1.Replace("U'", "u");
   s1 = s1.Replace("D'", "d");

   s1 = s1.Replace("F2", "FF");
   s1 = s1.Replace("B2", "BB");
   s1 = s1.Replace("R2", "RR");
   s1 = s1.Replace("L2", "LL");
   s1 = s1.Replace("U2", "UU");
   s1 = s1.Replace("D2", "DD");
   solution = s1;
   Console.WriteLine(solution);
}


*/

        private void glControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                int i = doPicking(e.X, e.Y);
                if (i > 0 && !track)
                {
                    //selectedCube = i;
                
                    string code = selectedFace.ToString() + selectedCube.ToString();
                    if (color.ContainsKey(code))
                    {
                        string pos = color[code];
                        var x = int.Parse(pos[0].ToString());
                        var y = int.Parse(pos[1].ToString());
                        var z = int.Parse(pos[2].ToString());
                        //Console.WriteLine(pos);
                        Console.WriteLine(selectedCube);
                        //rubiksCube.data[pos / 100][(pos % 100) / 100, ((pos % 100) % 100) / 100] = currentColor;
                        _rubikData[x][y, z] = currentColor;
                        //rubiksCube = null;
                        rubiksCube = new RubiksCube(_rubikData);
                        //subData = _rubikData;
                        //Console.WriteLine(_rubikData[x][y, z]);
                    }

                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //isSolving = true;
            //subData = _rubikData;
            for(int i = 0; i < 6; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    for(int k = 0; k < 3; k++)
                    {
                        subData[i][j,k] = _rubikData[i][j,k];
                    }
                }
            }
            
            Next.Enabled = true;
            index = 0;
            solution = null;
            reverse = null;
            int check = 0;
            
            List<string> path1 = _newRubik.solveWhiteCross();
            
            if(path1 == null)
            {
                check = 1;
                goto invalid;
            }

            List<string> path2 = _newRubik.solveWhiteFace();
            
            if(path2 == null)
            {
                check = 1;
                goto invalid;
            }

            List<string> path3 = _newRubik.solveSecondLayer();
 
            if(path3 == null)
            {
                check = 1;
                goto invalid;
            }

            List<string> path4 = _newRubik.yellowCross();
 
            if(path4 == null)
            {
                check = 1;
                goto invalid;
            }

            List<string> path5 = _newRubik.yellowApplyEdges();

            if(path5 == null)
            {
                check = 1;
                goto invalid;
            }

            List<string> path6 = _newRubik.yellowApplyCorners();
 
            if(path6 == null)
            {
                check = 1;
                goto invalid;
            }

            List<string> path7 = _newRubik.yellowCornerOrient();

            if(path7 == null)
            {
                check = 1;
                goto invalid;
            }

            List<string> path8 = _newRubik.preSolve();

            if(path8 == null)
            {
                check = 1;
                goto invalid;
            }
            
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    { 
                        if (_rubikData[i][j, k] != i)
                        {
                            goto invalid;
                        }
                    }
                }
            }
            
            path1.AddRange(path2);
            path1.AddRange(path3);
            path1.AddRange(path4);
            path1.AddRange(path5);
            path1.AddRange(path6);
            path1.AddRange(path7);
            path1.AddRange(path8);

            //List<string> path1 = _newRubik.preSolve();
            string step = "";
            string reverseStep = "";

            foreach (var item in path1)
            {
                switch (item)
                {
                    case "L'":
                        step += "l";
                        break;
                    case "R'":
                        step += "r";
                        break;
                    case "F'":
                        step += "f";
                        break;
                    case "B'":
                        step += "b";
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

            solution = step;
            sol.Clear();
            foreach (var item in solution)
            {
                switch (item.ToString())
                {
                    case "L":
                        sol.Add("L");
                        break;

                    case "l":
                        sol.Add("L'");
                        break;

                    case "R":
                        sol.Add("R");
                        break;

                    case "r":
                        sol.Add("R'");
                        break;

                    case "F":
                        sol.Add("F");
                        break;

                    case "f":
                        sol.Add("F'");
                        break;

                    case "B":
                        sol.Add("B");
                        break;

                    case "b":
                        sol.Add("B'");
                        break;

                    case "U":
                        sol.Add("U");
                        break;

                    case "u":
                        sol.Add("U'");
                        break;

                    case "D":
                        sol.Add("D");
                        break;

                    case "d":
                        sol.Add("D'");
                        break;

                    case "Y":
                        sol.Add("Y");
                        break;

                    case "y":
                        sol.Add("Y'");
                        break;

                    case "X":
                        sol.Add("X");
                        break;

                    case "x":
                        sol.Add("X'");
                        break;

                    case "Z":
                        sol.Add("Z");
                        break;

                    case "z":
                        sol.Add("Z'");
                        break;

                }
            }

            if(sol.Count() != 0)
            {
                curstep.Text = sol[0];
            }
            

            string s = "";
            foreach (var item in step)
            {
                s = item.ToString().ToUpper();
                if (item.ToString() != s)
                {
                    reverseStep += s;
                }
                else
                {
                    reverseStep += s.ToLower();
                }
            }

            reverse = reverseStep;
            //Console.WriteLine("");
            //Console.WriteLine(step);
            //Console.WriteLine(reverseStep);

            step = step.Replace("UU","A").Replace("uu","a");
            step = step.Replace("LL","K").Replace("ll","k");
            step = step.Replace("RR","H").Replace("rr","h");
            step = step.Replace("FF","N").Replace("ff","n");
            step = step.Replace("BB","V").Replace("bb","v");
            step = step.Replace("DD","S").Replace("dd","s");
            step = step.Replace("YY","G").Replace("yy","g");

            step = step.Replace("Y","").Replace("y","");
            step = step.Replace("X","").Replace("x","");
            step = step.Replace("Z","").Replace("z","");
            step = step.Replace("G","").Replace("g","");

            Console.WriteLine(step.Length);
            //Console.WriteLine(step.Length);
            //isSolving = false;
            //solution = null;

            invalid:
            if(check == 1)
            {
                for(int i = 0; i < 6; i++)
                {
                    for(int j = 0; j < 3; j++)
                    {
                        for(int k = 0; k < 3; k++)
                        {
                            _rubikData[i][j,k] = subData[i][j,k];
                        }
                    }
                }
                //Console.WriteLine(subData[4][0, 1]);
                //Console.WriteLine(subData[4][1, 1]);
                MessageBox.Show("Invalid rubik ! Try again", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

    }
}

        
