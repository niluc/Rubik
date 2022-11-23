using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using OpenTK;

namespace Rubik
{
    class RubiksCube : IDraw
    {
        List<Cube> composingCubes;
        Queue<Animation> pendingAnimation;
        public Animation current;
        public float AngleX, AngleY, AngleZ;
        public bool isAnimate = false;
        public int[][,] data;//= Practice._rubikData;
        Dictionary<string, List<int>> faceColor;

        public RubiksCube(int[][,] rubikData)
        {
            data = rubikData;
            /*
            LDB
            LDE
            LDF
            LEB
            LEE
            LEF
            LUB
            LUE
            LUF
            EDB
            EDE
            EDF
            EEB
            EEE
            EEF
            EUB
            EUE
            EUF
            RDB
            RDE
            RDF
            REB
            REE
            REF
            RUB
            RUE
            RUF 
            */
            
            faceColor = new Dictionary<string, List<int>>()
            {
                {"LDB", new List<int>(){6, data[5][2, 2], 6, data[3][2, 0], data[0][2, 0], 6}}, 
                {"LDE", new List<int>(){6, 6, 6, data[3][1, 0], data[0][2, 1], 6}}, 
                {"LDF", new List<int>(){data[4][2, 0], 6, 6, data[3][0, 0], data[0][2, 2], 6}},
                {"LEB", new List<int>(){6, data[5][1, 2], 6, 6, data[0][1, 0], 6}},
                {"LEE", new List<int>(){6, 6, 6, 6, data[0][1, 1], 6}},
                {"LEF", new List<int>(){data[4][1, 0], 6, 6, 6, data[0][1, 2], 6}},
                {"LUB", new List<int>(){6, data[5][0, 2], data[2][0, 0], 6, data[0][0, 0], 6}},
                {"LUE", new List<int>(){6, 6, data[2][1, 0], 6, data[0][0, 1], 6}},
                {"LUF", new List<int>(){data[4][0, 0], 6, data[2][2, 0], 6, data[0][0, 2], 6}},
                {"EDB", new List<int>(){6, data[5][2, 1], 6, data[3][2, 1], 6, 6}},
                {"EDE", new List<int>(){6, 6, 6, data[3][1, 1], 6, 6}},
                {"EDF", new List<int>(){data[4][2, 1], 6, 6, data[3][0, 1], 6, 6}},
                {"EEB", new List<int>(){6, data[5][1, 1], 6, 6, 6, 6}},
                {"EEE", new List<int>(){6, 6, 6, 6, 6, 6}},
                {"EEF", new List<int>(){data[4][1, 1], 6, 6, 6, 6, 6}},
                {"EUB", new List<int>(){6, data[5][0, 1], data[2][0, 1], 6, 6, 6}},
                {"EUE", new List<int>(){6, 6, data[2][1, 1], 6, 6, 6}},
                {"EUF", new List<int>(){data[4][0, 1], 6, data[2][2, 1], 6, 6, 6}},
                {"RDB", new List<int>(){6, data[5][2, 0], 6, data[3][2, 2], 6, data[1][2, 2]}},
                {"RDE", new List<int>(){6, 6, 6, data[3][1, 2], 6, data[1][2, 1]}},
                {"RDF", new List<int>(){data[4][2, 2], 6, 6, data[3][0, 2], 6, data[1][2, 0]}},
                {"REB", new List<int>(){6, data[5][1, 0], 6, 6, 6, data[1][1, 2]}},
                {"REE", new List<int>(){6, 6, 6, 6, 6, data[1][1, 1]}},
                {"REF", new List<int>(){data[4][1, 2], 6, 6, 6, 6, data[1][1, 0]}},
                {"RUB", new List<int>(){6, data[5][0, 0], data[2][0, 2], 6, 6, data[1][0, 2]}},
                {"RUE", new List<int>(){6, 6, data[2][1, 2], 6, 6, data[1][0, 1]}},
                {"RUF", new List<int>(){data[4][0, 2], 6, data[2][2, 2], 6, 6, data[1][0, 0]}},
            };
            
            pendingAnimation = new Queue<Animation>();
            composingCubes = new List<Cube>();

            double blockSpace = 200.0/3.0;
            int id = 1;
            for (double x = -blockSpace; x < 2 * blockSpace; x += blockSpace)
            {
                for (double y = -blockSpace; y < 2 * blockSpace; y += blockSpace)
                {
                    for (double z = -blockSpace; z < 2 * blockSpace; z += blockSpace)
                    {
                        var cubeColor = this.GenerateCubeColor(x, y, z);
                        composingCubes.Add(new Cube(200.0/6.0, x, y, z, id, cubeColor));
                        id++;
                    }
                }
            }

        }

        private FaceCube<int> GenerateCubeColor(double x, double y, double z)
        {
            string s1 ="",s2 = "",s3 ="";
            if(x < 0)
            {
                s1 = "L";
            }
            else if(x == 0)
            {
                s1 = "E";
            }
            else if(x > 0)
            {
                s1 = "R";
            }

            if(y < 0)
            {
                s2 = "D";
            }
            else if(y == 0)
            {
                s2 = "E";
            }
            else if(y > 0)
            {
                s2 = "U";
            }

            if(z < 0)
            {
                s3 = "B";
            }
            else if(z == 0)
            {
                s3 = "E";
            }
            else if(z > 0)
            {
                s3 = "F";
            }

            string name = s1 + s2 + s3;
            //var cubeColor = new FaceCube<int>(4, 5, 2, 3, 0, 1);
            var cubeColor = new FaceCube<int>(faceColor[name][0], 
                                              faceColor[name][1],
                                              faceColor[name][2], 
                                              faceColor[name][3], 
                                              faceColor[name][4], 
                                              faceColor[name][5]);
            if (x < 0)
            {
                cubeColor.Right = 6;
            }
            if (x == 0)
            {
                cubeColor.Left = 6;
                cubeColor.Right = 6;
            }
            if (x > 0)
            {
                cubeColor.Left = 6;
            }
            if (y < 0)
            {
                cubeColor.Top = 6;
            }
            if (y == 0)
            {
                cubeColor.Top = 6;
                cubeColor.Bottom = 6;
            }
            if (y > 0)
            {
                cubeColor.Bottom = 6;
            }
            if (z < 0)
            {
                cubeColor.Front = 6;
            }
            if (z == 0)
            {
                cubeColor.Front = 6;
                cubeColor.Back = 6;
            }
            if (z > 0)
            {
                cubeColor.Back = 6;
            }

            return cubeColor;
        }

        public void Manipulate(RubikCubeMoviment moviment)
        {
            Animation animation = new Animation(composingCubes, 90, moviment);
            this.pendingAnimation.Enqueue(animation);
        }
        
        public void Rotate(float AngleAxisX, float AngleAxisY, float AngleAxisZ)
        {
            this.AngleX += AngleAxisX;
            this.AngleY += AngleAxisY;
            this.AngleZ += AngleAxisZ;
        }
        
        public void Draw()
        {
            DoAnimation();
            AdjustRotation();
            foreach (var item in composingCubes)
            {
                item.Draw();
                //Console.WriteLine(item.ID);
            }
        }

        private void DoAnimation()
        {
            if (current == null)
            {
                if (this.pendingAnimation.Count > 0)
                {
                    this.current = this.pendingAnimation.Dequeue();
                }
                else return;
            }
            else
            {
                if (current.AnimationEnded)
                {
                    current = null;
                    isAnimate = false;
                }
                else
                {
                    current.Animate();
                    isAnimate = true;
                }
            }
        }

        private void AdjustRotation()
        {
            GL.Rotate(AngleX, 1, 0, 0);
            GL.Rotate(AngleY, 0, 1, 0);
            GL.Rotate(AngleZ, 0, 0, 1);
        }
    }
}
