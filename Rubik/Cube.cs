using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using OpenTK;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rubik
{
    class Cube : IDraw
    {
        private double size;
        
        public int ID;
        public double X { get; private set; }
        public double Y { get; private set; }
        public double Z { get; private set; }

        private FaceCube<int> faceColors;
        private uint[][] textures;

        private double AngleX = 0;
        private double AngleY = 0;
        private double AngleZ = 0;

        private Dictionary<string, int> position = new Dictionary<string, int>()
        {
                {"LDB", 1}, 
                {"LDE", 2}, 
                {"LDF", 3},
                {"LEB", 4},
                {"LEE", 5},
                {"LEF", 6},
                {"LUB", 7},
                {"LUE", 8},
                {"LUF", 9},
                {"EDB", 10},
                {"EDE", 11},
                {"EDF", 12},
                {"EEB", 13},
                {"EEE", 14},
                {"EEF", 15},
                {"EUB", 16},
                {"EUE", 17},
                {"EUF", 18},
                {"RDB", 19},
                {"RDE", 20},
                {"RDF", 21},
                {"REB", 22},
                {"REE", 23},
                {"REF", 24},
                {"RUB", 25},
                {"RUE", 26},
                {"RUF", 27},
        };

        public Cube(double size, double X, double Y, double Z)
        {
            this.size = size;
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            this.faceColors = new FaceCube<int>(0, 0, 0, 0, 0, 0);
        }

        public Cube(double size, double X, double Y, double Z, int ID, FaceCube<int> faceColors)
        {
            this.size = size;
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            this.faceColors = faceColors;
            this.ID = ID;

        }

        public Cube(double size, double X, double Y, double Z, FaceCube<Texture> faceColors)
        {
            this.size = size;
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            //this.faceColors = faceColors;
        }

        public void Rotate(double AngleAxisX, double AngleAxisY, double AngleAxisZ)
        {
            this.AngleX += AngleAxisX;
            this.AngleY += AngleAxisY;
            this.AngleZ += AngleAxisZ;
        }

        public void UpdateName()
        {
            string s1 = "",s2 = "",s3 = "";
            if(this.X < 0)
            {
                s1 = "L";
            }
            else if(this.X == 0)
            {
                s1 = "E";
            }
            else if(this.X > 0)
            {
                s1 = "R";
            }

            if(this.Y < 0)
            {
                s2 = "D";
            }
            else if(this.Y == 0)
            {
                s2 = "E";
            }
            else if(this.Y > 0)
            {
                s2 = "U";
            }

            if(this.Z < 0)
            {
                s3 = "B";
            }
            else if(this.Z == 0)
            {
                s3 = "E";
            }
            else if(this.Z > 0)
            {
                s3 = "F";
            }
            string name = s1 + s2 + s3;
            this.ID = position[name];
        }

        public void Draw()
        {

            GL.Color3(Color.White);
            GL.Enable(EnableCap.Texture2D);
            
            GL.PushMatrix();
            GL.Rotate(AngleX, 1, 0, 0);
            GL.Rotate(AngleY, 0, 1, 0);
            GL.Rotate(AngleZ, 0, 0, 1);          
           
            //GL.LineWidth(3.4f);
            GL.InitNames();
            GL.PushName(this.ID);
            /*
            if (Home.check == 1)
            {
                this.textures = Practice.textures;
            }
            else if(Home.check == 2)
            {
                this.textures = Solve.textures;
            }
            */
            this.textures = Practice.textures;

            GL.BindTexture(TextureTarget.Texture2D, this.textures[faceColors.Back][0]);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0f,1f);
            GL.Vertex3(this.size + X, this.size + Y, -this.size + Z);
            GL.TexCoord2(0f,0f);
            GL.Vertex3(this.size + X, -this.size + Y, -this.size + Z);
            GL.TexCoord2(1f,0f);
            GL.Vertex3(-this.size + X, -this.size + Y, -this.size + Z);
            GL.TexCoord2(1f,1f);
            GL.Vertex3(-this.size + X, this.size + Y, -this.size + Z);
            GL.End();

            //Bottom
            
            GL.BindTexture(TextureTarget.Texture2D, this.textures[faceColors.Bottom][0]);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0f,0f);
            GL.Vertex3(-this.size + X, -this.size + Y, -this.size + Z);
            GL.TexCoord2(1f,0f);
            GL.Vertex3(this.size + X, -this.size + Y, -this.size + Z);
            GL.TexCoord2(1f,1f);
            GL.Vertex3(this.size + X, -this.size + Y, this.size + Z);
            GL.TexCoord2(0f,1f);
            GL.Vertex3(-this.size + X, -this.size + Y, this.size + Z);
            GL.End();

            //Left
            GL.BindTexture(TextureTarget.Texture2D, this.textures[faceColors.Left][0]);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0f,1f);
            GL.Vertex3(-this.size + X, this.size + Y, -this.size + Z);
            GL.TexCoord2(0f,0f);
            GL.Vertex3(-this.size + X, -this.size + Y, -this.size + Z);
            GL.TexCoord2(1f,0f);
            GL.Vertex3(-this.size + X, -this.size + Y, this.size + Z);
            GL.TexCoord2(1f,1f);
            GL.Vertex3(-this.size + X, this.size + Y, this.size + Z);
            GL.End();

            //Right
            GL.BindTexture(TextureTarget.Texture2D, this.textures[faceColors.Right][0]);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0f,1f);
            GL.Vertex3(this.size + X, this.size + Y, this.size + Z);
            GL.TexCoord2(0f,0f);
            GL.Vertex3(this.size + X, -this.size + Y, this.size + Z);
            GL.TexCoord2(1f,0f);
            GL.Vertex3(this.size + X, -this.size + Y, -this.size + Z);
            GL.TexCoord2(1f,1f);
            GL.Vertex3(this.size + X, this.size + Y, -this.size + Z);
            GL.End();

            ///UP
            ///
            GL.BindTexture(TextureTarget.Texture2D, this.textures[faceColors.Top][0]);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0f,1f);
            GL.Vertex3(-this.size + X, this.size + Y, -this.size + Z);
            GL.TexCoord2(0f,0f);
            GL.Vertex3(-this.size + X, this.size + Y, this.size + Z);
            GL.TexCoord2(1f,0f);
            GL.Vertex3(this.size + X, this.size + Y, this.size + Z);
            GL.TexCoord2(1f,1f);
            GL.Vertex3(this.size + X, this.size + Y, -this.size + Z);
            GL.End();

            //Front

            GL.BindTexture(TextureTarget.Texture2D, this.textures[faceColors.Front][0]);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0f,1f);
            GL.Vertex3(-this.size + X, this.size + Y, this.size + Z);
            GL.TexCoord2(0f,0f);
            GL.Vertex3(-this.size + X, -this.size + Y, this.size + Z);
            GL.TexCoord2(1f,0f);
            GL.Vertex3(this.size + X, -this.size + Y, this.size + Z);
            GL.TexCoord2(1f,1f);
            GL.Vertex3(this.size + X, this.size + Y, this.size + Z);     
            GL.End();

            GL.Disable(EnableCap.Texture2D);

            GL.PopName();
            GL.PopMatrix();
        }

        public void Place(RubikCubeMoviment moviment)
        {
            //this.Transform(moviment);
            this.faceColors.Rotate(moviment);
            this.Transform(moviment);
            this.UpdateName();
            this.ResetTransforms();
        }

        private void Transform(RubikCubeMoviment moviment)
        {
            if(moviment.Axis == Axis.X)
            {
                if(moviment.Spin == Spin.Clockwise) //Up
                {
                    //Edges
                    if(this.Y < 0 && this.Z < 0)
                    {
                        this.Z = -this.Z;
                    }
                    else if(this.Y < 0 && this.Z > 0)
                    {
                        this.Y = -this.Y;
                    }
                    else if(this.Y > 0 && this.Z < 0)
                    {
                        this.Y = -this.Y;
                    }
                    else if(this.Y > 0 && this.Z > 0)
                    {
                        this.Z = -this.Z;
                    }

                    //Middles
                    else if(this.Y == 0 && this.Z < 0)
                    {
                        this.Y = this.Z;
                        this.Z = 0;
                    }
                    else if (this.Y == 0 && this.Z > 0)
                    {
                        this.Y = this.Z;
                        this.Z = 0;
                    }
                    else if (this.Z == 0 && this.Y < 0)
                    {
                        this.Z = -this.Y;
                        this.Y = 0;
                    }
                    else if (this.Z == 0 && this.Y > 0)
                    {
                        this.Z = -this.Y;
                        this.Y = 0;
                    }
                }
                else //Down
                {
                    //Edges
                    if (this.Y < 0 && this.Z < 0)
                    {
                        this.Y = -this.Y;
                    }
                    else if (this.Y < 0 && this.Z > 0)
                    {
                        this.Z = -this.Z;
                    }
                    else if (this.Y > 0 && this.Z < 0)
                    {
                        this.Z = -this.Z;
                    }
                    else if (this.Y > 0 && this.Z > 0)
                    {
                        this.Y = -this.Y;
                    }

                    //Middles
                    else if (this.Y == 0 && this.Z < 0)
                    {
                        this.Y = -this.Z;
                        this.Z = 0;
                    }
                    else if (this.Y == 0 && this.Z > 0)
                    {
                        this.Y = -this.Z;
                        this.Z = 0;
                    }
                    else if (this.Z == 0 && this.Y < 0)
                    {
                        this.Z = this.Y;
                        this.Y = 0;
                    }
                    else if (this.Z == 0 && this.Y > 0)
                    {
                        this.Z = this.Y;
                        this.Y = 0;
                    }
                }
            }

            if(moviment.Axis == Axis.Y)
            {
                if (moviment.Spin == Spin.Clockwise) //Left
                {
                    if (this.X < 0 && this.Z < 0)
                    {
                        this.X = -this.X;
                    }
                    else if (this.X < 0 && this.Z > 0)
                    {
                        this.Z = -this.Z;
                    }
                    else if (this.X > 0 && this.Z < 0)
                    {
                        this.Z = -this.Z;
                    }
                    else if (this.X > 0 && this.Z > 0)
                    {
                        this.X = -this.X;
                    }

                    //Middle
                    else if(this.X == 0 && this.Z < 0)
                    {
                        this.X = -this.Z;
                        this.Z = 0;
                    }
                    else if(this.X == 0 && this.Z > 0)
                    {
                        this.X = -this.Z;
                        this.Z = 0;
                    }
                    else if(this.Z == 0 && this.X < 0)
                    {
                        this.Z = this.X;
                        this.X = 0;
                    }
                    else if(this.Z == 0 && this.X > 0)
                    {
                        this.Z = this.X;
                        this.X = 0;
                    }
                }
                else
                {
                    if (this.X < 0 && this.Z < 0)
                    {
                        this.Z = -this.Z;
                    }
                    else if (this.X < 0 && this.Z > 0)
                    {
                        this.X = -this.X;
                    }
                    else if (this.X > 0 && this.Z < 0)
                    {
                        this.X = -this.X;
                    }
                    else if (this.X > 0 && this.Z > 0)
                    {
                        this.Z = -this.Z;
                    }

                    //Middle
                    else if (this.X == 0 && this.Z < 0)
                    {
                        this.X = this.Z;
                        this.Z = 0;
                    }
                    else if (this.X == 0 && this.Z > 0)
                    {
                        this.X = this.Z;
                        this.Z = 0;
                    }
                    else if (this.Z == 0 && this.X < 0)
                    {
                        this.Z = -this.X;
                        this.X = 0;
                    }
                    else if (this.Z == 0 && this.X > 0)
                    {
                        this.Z = -this.X;
                        this.X = 0;
                    }
                }
            }

            if(moviment.Axis == Axis.Z)
            {
                if(moviment.Spin == Spin.Clockwise)
                {
                    if (this.X < 0 && this.Y < 0)
                    {
                        this.Y = -this.Y;
                    }
                    else if (this.X < 0 && this.Y > 0)
                    {
                        this.X = -this.X;
                    }
                    else if (this.X > 0 && this.Y < 0)
                    {
                        this.X = -this.X;
                    }
                    else if (this.X > 0 && this.Y > 0)
                    {
                        this.Y = -this.Y;
                    }

                    //Middle
                    else if (this.X == 0 && this.Y < 0)
                    {
                        this.X = this.Y;
                        this.Y = 0;
                    }
                    else if (this.X == 0 && this.Y > 0)
                    {
                        this.X = this.Y;
                        this.Y = 0;
                    }
                    else if (this.Y == 0 && this.X < 0)
                    {
                        this.Y = -this.X;
                        this.X = 0;
                    }
                    else if (this.Y == 0 && this.X > 0)
                    {
                        this.Y = -this.X;
                        this.X = 0;
                    }
                }
                else
                {
                    if (this.X < 0 && this.Y < 0)
                    {
                        this.X = -this.X;
                    }
                    else if (this.X < 0 && this.Y > 0)
                    {
                        this.Y = -this.Y;
                    }
                    else if (this.X > 0 && this.Y < 0)
                    {
                        this.Y = -this.Y;
                    }
                    else if (this.X > 0 && this.Y > 0)
                    {
                        this.X = -this.X;
                    }

                    //Middle
                    else if (this.X == 0 && this.Y < 0)
                    {
                        this.X = -this.Y;
                        this.Y = 0;
                    }
                    else if (this.X == 0 && this.Y > 0)
                    {
                        this.X = -this.Y;
                        this.Y = 0;
                    }
                    else if (this.Y == 0 && this.X < 0)
                    {
                        this.Y = this.X;
                        this.X = 0;
                    }
                    else if (this.Y == 0 && this.X > 0)
                    {
                        this.Y = this.X;
                        this.X = 0;
                    }
                }
            }
            
            this.ResetTransforms();
        }

        private void ResetTransforms()
        {
            this.AngleX = 0;
            this.AngleY = 0;
            this.AngleZ = 0;
        }
    }
}
