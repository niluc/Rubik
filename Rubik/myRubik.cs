using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using OpenTK.Graphics.OpenGL;
using OpenTK;                       // Matrix 4

namespace Rubik
{
    [Serializable]
    class myRubik
    {
        public int[][,] rubikFace;

        private double AngleX = 0;
        private double AngleY = 0;
        private double AngleZ = 0;

                  /*                                  
*                           .--------------.
*                           |BLU |    |BRU | 
*                           |--------------|
*                           |    | 2Y |    | 
*                           |--------------|
     *                      |FLU |    |FRU | 
     *       ,--------------.--------------.--------------.--------------.
     *  |    |BLU |    |FLU |FLU |    |FRU |FRU |    |BRU |BRU |    |BLU |
     *  |----|--------------|--------------|--------------|--------------|
     *  |    |    | 0B |    |    | 4R |    |    | 1G |    |    | 5O |    |
     *       |--------------|--------------|--------------|--------------|
     *    y3 |BDL |    |FDL |FDL |    |DFR |DFR |    |BDR |BDR |    |BDL |
     *  `    `--------------'--------------'--------------'--------------'
     *                      |FDL |    |DFR |
     *                      |--------------|
     *                      |    | 3W |    |
     *                      |--------------|
     *                      |BDL |    |BDR |
     *                      '--------------'
     *        
     *        */
        private Dictionary<string, int> FU = new Dictionary<string, int>();
        private Dictionary<string, int> FL = new Dictionary<string, int>();
        private Dictionary<string, int> FR = new Dictionary<string, int>();
        private Dictionary<string, int> DF = new Dictionary<string, int>();
        private Dictionary<string, int> LU = new Dictionary<string, int>();
        private Dictionary<string, int> DL = new Dictionary<string, int>();
        private Dictionary<string, int> BL = new Dictionary<string, int>();
        private Dictionary<string, int> BR = new Dictionary<string, int>();
        private Dictionary<string, int> BD = new Dictionary<string, int>();
        private Dictionary<string, int> BU = new Dictionary<string, int>();
        private Dictionary<string, int> DR = new Dictionary<string, int>();
        private Dictionary<string, int> RU = new Dictionary<string, int>();
        
        private List<Dictionary<string, int>> listEdges = new List<Dictionary<string, int>>();

        private Dictionary<string, int> FLU = new Dictionary<string, int>();
        private Dictionary<string, int> BRU = new Dictionary<string, int>();
        private Dictionary<string, int> FRU = new Dictionary<string, int>();
        private Dictionary<string, int> DFR = new Dictionary<string, int>();
        private Dictionary<string, int> BLU = new Dictionary<string, int>();
        private Dictionary<string, int> DFL = new Dictionary<string, int>();
        private Dictionary<string, int> BDL = new Dictionary<string, int>();
        private Dictionary<string, int> BDR = new Dictionary<string, int>();

        private List<Dictionary<string, int>> listCorners = new List<Dictionary<string, int>>();

        private Dictionary<string, int> F = new Dictionary<string, int>();
        private Dictionary<string, int> B = new Dictionary<string, int>();
        private Dictionary<string, int> L = new Dictionary<string, int>();
        private Dictionary<string, int> R = new Dictionary<string, int>();
        private Dictionary<string, int> U = new Dictionary<string, int>();
        private Dictionary<string, int> D = new Dictionary<string, int>();

        private List<Dictionary<string, int>> listCenters = new List<Dictionary<string, int>>();

        public myRubik(int[][,] rubikData)
        {
            rubikFace = rubikData;
            updateBlock();
        }

        public void updateBlock()
        {
            listEdges.Clear();
            FU["F"] = rubikFace[4][0, 1];  FU["U"] = rubikFace[2][2, 1];

            FL["F"] = rubikFace[4][1, 0];  FL["L"] = rubikFace[0][1, 2];

            FR["F"] = rubikFace[4][1, 2];  FR["R"] = rubikFace[1][1, 0];

            DF["D"] = rubikFace[3][0, 1];  DF["F"] = rubikFace[4][2, 1];

            LU["L"] = rubikFace[0][0, 1];  LU["U"] = rubikFace[2][1, 0];

            DL["D"] = rubikFace[3][1, 0];  DL["L"] = rubikFace[0][2, 1];

            BL["B"] = rubikFace[5][1, 2];  BL["L"] = rubikFace[0][1, 0];

            BR["B"] = rubikFace[5][1, 0];  BR["R"] = rubikFace[1][1, 2];

            BD["B"] = rubikFace[5][2, 1];  BD["D"] = rubikFace[3][2, 1];

            BU["B"] = rubikFace[5][0, 1];  BU["U"] = rubikFace[2][0, 1];

            DR["D"] = rubikFace[3][1, 2];  DR["R"] = rubikFace[1][2, 1];

            RU["R"] = rubikFace[1][0, 1];  RU["U"] = rubikFace[2][1, 2];
            
            listEdges.Add(FU);
            listEdges.Add(FL);
            listEdges.Add(FR);
            listEdges.Add(DF);
            listEdges.Add(LU);
            listEdges.Add(DL);
            listEdges.Add(BL);
            listEdges.Add(BR);
            listEdges.Add(BD);
            listEdges.Add(BU);
            listEdges.Add(DR); 
            listEdges.Add(RU);

            listCorners.Clear();
            FLU["F"] = rubikFace[4][0, 0];  FLU["L"] = rubikFace[0][0, 2];  FLU["U"] = rubikFace[2][2, 0];  FLU["name"] = 10;

            BRU["B"] = rubikFace[5][0, 0];  BRU["R"] = rubikFace[1][0, 2];  BRU["U"] = rubikFace[2][0, 2];  BRU["name"] = 20;

            FRU["F"] = rubikFace[4][0, 2];  FRU["R"] = rubikFace[1][0, 0];  FRU["U"] = rubikFace[2][2, 2];  FRU["name"] = 30;

            DFR["D"] = rubikFace[3][0, 2];  DFR["F"] = rubikFace[4][2, 2];  DFR["R"] = rubikFace[1][2, 0];  DFR["name"] = 40;

            BLU["B"] = rubikFace[5][0, 2];  BLU["L"] = rubikFace[0][0, 0];  BLU["U"] = rubikFace[2][0, 0];  BLU["name"] = 50;

            DFL["F"] = rubikFace[4][2, 0];  DFL["D"] = rubikFace[3][0, 0];  DFL["L"] = rubikFace[0][2, 2];  DFL["name"] = 60;

            BDL["B"] = rubikFace[5][2, 2];  BDL["D"] = rubikFace[3][2, 0];  BDL["L"] = rubikFace[0][2, 0];  BDL["name"] = 70;

            BDR["B"] = rubikFace[5][2, 0];  BDR["D"] = rubikFace[3][2, 2];  BDR["R"] = rubikFace[1][2, 2];  BDR["name"] = 80;

            listCorners.Add(FLU);
            listCorners.Add(BRU);
            listCorners.Add(FRU);
            listCorners.Add(DFR);
            listCorners.Add(BLU);
            listCorners.Add(DFL);
            listCorners.Add(BDL);
            listCorners.Add(BDR);

            listCenters.Clear();

            F["F"] = rubikFace[4][1, 1]; F["name"] = 40;
            R["R"] = rubikFace[1][1, 1]; R["name"] = 10;
            L["L"] = rubikFace[0][1, 1]; L["name"] = 100;
            B["B"] = rubikFace[5][1, 1]; B["name"] = 50;
            U["U"] = rubikFace[2][1, 1]; U["name"] = 20;
            D["D"] = rubikFace[3][1, 1]; D["name"] = 30;
            //solveWhiteCross();

            listCenters.Add(F);
            listCenters.Add(R);
            listCenters.Add(L);
            listCenters.Add(B);
            listCenters.Add(U);
            listCenters.Add(D);
        }

        public List<string> preSolve()
        {
            updateBlock();
            List<string> solution = new List<string>();

            Dictionary<string, int> position = new Dictionary<string, int>();
            //int check = 0;
            foreach(var item in listCenters)
            {
                if(item.ContainsValue(3))
                {
                    position = item;
                    //check = 1;
                }
            }
            if(position.Count() == 0)
            {
                return null;
            }
            
            var whiteFace = position.FirstOrDefault(x => x.Value == 3).Key;
            //Console.WriteLine(whiteFace);
            switch (whiteFace)
            {
                case "F":
                    this.rotateAllX();
                    solution.Add("X");
                    break;

                case "U":
                    this.rotateAllX();
                    this.rotateAllX();
                    solution.Add("X");
                    solution.Add("X");
                    break;

                case "B":
                    this.rotateAllX();  solution.Add("X");
                    this.rotateAllX();  solution.Add("X");
                    this.rotateAllX();  solution.Add("X");
                    break;

                case "L":
                    this.rotateAllZ();  solution.Add("Z");
                    this.rotateAllZ();  solution.Add("Z");
                    this.rotateAllZ();  solution.Add("Z");
                    break;

                case "R":
                    this.rotateAllZ();  solution.Add("Z");
                    break;

                case "D":
                    break;
            }

            //int check1 = 0;
            //Console.WriteLine(check1);
            Dictionary<string, int> position1 = new Dictionary<string, int>();
            //Console.WriteLine(1);
            foreach(var item in listCenters)
            {
                if(item.ContainsValue(4))
                {
                    position1 = item;
                    //check1 = 1;
                }
            }
            //Console.WriteLine(check1);

            if(position1.Count() == 0)
            {
                return null;
            }
            //Console.WriteLine(check1);
            //Console.WriteLine(1);
            var redFace = position1.FirstOrDefault(x => x.Value == 4).Key;
            //Console.WriteLine(redFace);
            switch (redFace)
            {
                case "F":
                    break;

                case "B":
                    this.rotateAllY(); solution.Add("Y");
                    this.rotateAllY(); solution.Add("Y");
                    break;

                case "L":
                    this.rotateAllY();  solution.Add("Y");
                    this.rotateAllY();  solution.Add("Y");
                    this.rotateAllY();  solution.Add("Y");
                    break;

                case "R":
                    this.rotateAllY();  solution.Add("Y");
                    break;

            }
            //Console.WriteLine(1);
            return solution;

        }
        
        public List<string> solveWhiteCross()
        {
            updateBlock();
            List <string> solution = preSolve();
            if(solution == null)
            {
                return null;
            }
            Dictionary<string, List<string>> allStep = new Dictionary<string, List<string>>()
            {
                {"UR", new List<string>(){""}}, 
                {"UL", new List<string>(){""}}, 
                {"UF", new List<string>(){""}}, 
                {"UB", new List<string>(){""}},

                {"DR", new List<string>(){"R", "R"}}, 
                {"DL", new List<string>(){"L", "L"}}, 
                {"DF", new List<string>(){"F", "F"}}, 
                {"DB", new List<string>(){"B", "B"}},

                {"FU", new List<string>(){"F", "R", "U'", "R'", "F'"}},
                {"FD", new List<string>(){"F'", "R", "U'", "R'"}},
                {"FR", new List<string>(){"R", "U", "R'"}},
                {"FL", new List<string>(){"L'", "U'", "L"}},

                {"BU", new List<string>(){"B", "L", "U'", "L'", "B'"}},
                {"BD", new List<string>(){"B", "R'", "U", "R"}},
                {"BR", new List<string>(){"R'", "U", "R"}},
                {"BL", new List<string>(){"L", "U'", "L'"}},

                {"LU", new List<string>(){"L", "F", "U'", "F'", "L'"}},
                {"LD", new List<string>(){"L'", "F", "U'", "F'"}},
                {"LF", new List<string>(){"F", "U'", "F'"}},
                {"LB", new List<string>(){"B'", "U", "B"}},

                {"RU", new List<string>(){"R'", "F'", "U", "F", "R"}},
                {"RD", new List<string>(){"R", "F'", "U", "F"}},
                {"RF", new List<string>(){"F'","U", "F"}},
                {"RB", new List<string>(){"B", "U'", "B'"}}
                
            };
            updateBlock();
            //List <string> solution = new List<string>();
            int[] color = {4, 1, 5, 0};
            for(int i = 0; i < 4; i++)
            {
                Dictionary<string, int> position = new Dictionary<string, int>();
                foreach(var item in listEdges)
                {
                    if(item.ContainsValue(3) && item.ContainsValue(color[i]))
                    {
                        position = item;
                    }
                }
                var whiteFace = position.FirstOrDefault(x => x.Value == 3).Key;
                var colorFace = position.FirstOrDefault(x => x.Value == color[i]).Key;
                
                if(whiteFace == null || colorFace == null)
                {
                    return null;
                }
                
                string start = whiteFace + colorFace;
                var path = allStep[start];
                if(start == "DR" && L["L"] != DL["L"])
                {
                    this.RotateRubik("D'");
                    solution.Add("D'");
                }
                else if(start == "DL" && L["L"] != DL["L"]){
                    this.RotateRubik("D");
                    solution.Add("D");
                }
                else if(start == "DB" && L["L"] != DL["L"]){
                    this.RotateRubik("D");
                    this.RotateRubik("D");
                    solution.Add("D");
                    solution.Add("D");
                }
                else { 
                    foreach(var item in path)
                    {
                        this.RotateRubik(item);
                        solution.Add(item);
                        //Console.Write(item);
                    }
                    int k = 0;
                    while(FU["U"] != 3 || FU["F"] != color[i])
                    {
                        k++;
                        if(k == 200)
                        {
                            return null;
                        }
                        solution.Add("U");
                        this.RotateRubik("U");
                    }

                    this.RotateRubik("F");solution.Add("F");
                    this.RotateRubik("F");solution.Add("F");
                }

                this.rotateAllY();
                
                solution.Add("Y");
            
            }

            return solution;
        }

        public List<string> solveWhiteFace()
        {
            Dictionary<string, List<string>> step1 = new Dictionary<string, List<string>>()
            {
                {"DFRF", new List<string>(){"R", "U'", "R'"}}, 
                {"DFRR", new List<string>(){"R", "U", "R'", "U'"}}, 

                {"DFLF", new List<string>(){"L'", "U", "L", "U'"}}, 
                {"DFLL", new List<string>(){"L'", "U'", "L"}}, 
                {"DFLD", new List<string>(){"L'", "U'", "L"}}, 

                {"BDLB", new List<string>(){"B'", "U", "U", "B"}},
                {"BDLD", new List<string>(){"B'", "U", "U", "B"}},
                {"BDLL", new List<string>(){"B'", "U", "B", "U", "U"}},

                {"BDRB", new List<string>(){"B", "U", "B'"}},
                {"BDRD", new List<string>(){"B", "U", "B'"}},
                {"BDRR", new List<string>(){"B", "U'", "B'", "U"}},

                {"BRUB", new List<string>(){"U"}},
                {"BRUR", new List<string>(){"U"}},
                {"BRUU", new List<string>(){"U"}},

                {"BLUB", new List<string>(){"U", "U"}},
                {"BLUL", new List<string>(){"U", "U"}},
                {"BLUU", new List<string>(){"U", "U"}},

                {"FLUF", new List<string>(){"U'"}},
                {"FLUL", new List<string>(){"U'"}},
                {"FLUU", new List<string>(){"U'"}},
                
            };

            Dictionary<string, List<string>> step2 = new Dictionary<string, List<string>>()
            {
                {"F", new List<string>(){"F'","U'", "F"}},
                {"R", new List<string>(){"R", "U", "R'"}},
                {"U", new List<string>(){"R", "U", "U", "R'", "U'", "R", "U", "R'"}},
            };

            updateBlock();
            List <string> solution = new List<string>();
            for(int i = 0; i < 4; i++)
            {
                int frontColor = rubikFace[4][1, 1];
                int rightColor = rubikFace[1][1, 1];
                //Console.WriteLine(frontColor);
                //Console.WriteLine(rightColor);
                Dictionary<string, int> position = new Dictionary<string, int>();
                foreach(var item in listCorners)
                {
                    if(item.ContainsValue(3) && item.ContainsValue(frontColor) && item.ContainsValue(rightColor))
                    {
                        position = item;
                    }
                }

                if (position.ContainsKey("name") == false)
                {
                    return null;
                }
                //Console.WriteLine(position["name"]);
                var whiteFace = position.FirstOrDefault(x => x.Value == 3).Key;
                string positionName = "";

                switch (position["name"])
                {
                    case 10:
                        positionName = "FLU";
                        break;
                    case 20:
                        positionName = "BRU";
                        break;
                    case 30:
                        positionName = "FRU";
                        break;
                    case 40:
                        positionName = "DFR";
                        break;
                    case 50:
                        positionName = "BLU";
                        break;
                    case 60:
                        positionName = "DFL";
                        break;
                    case 70:
                        positionName = "BDL";
                        break;
                    case 80:
                        positionName = "BDR";
                        break;
                }

                string start = positionName + whiteFace;
                //Console.WriteLine("");
                //Console.WriteLine(start);
                List <string> solution1 = new List<string>();
                List <string> solution2 = new List<string>();
                if (step1.ContainsKey(start))
                {
                    var path1 = step1[start];
                    foreach(var item in path1)
                    {
                        this.RotateRubik(item);
                        solution1.Add(item);
                    }
                }

                if (solution1.Count() > 0 || positionName != "DFR")
                {
                    foreach(var item in solution1)
                    {
                        solution.Add(item);
                    }
                    var whiteFaceFRU = FRU.FirstOrDefault(x => x.Value == 3).Key;
                    if (step2.ContainsKey(whiteFaceFRU))
                    {
                        solution2 = step2[whiteFaceFRU];
                    }
                    
                    foreach(var item in solution2)
                    {
                        this.RotateRubik(item);
                        solution.Add(item);
                    }
                    //updateBlock();
                }
                this.rotateAllY();
                solution.Add("Y");   
            }

            Console.WriteLine("");

            return solution;
        }

        public bool secondLayerSolved()
        {
            updateBlock();
            int frontColor = rubikFace[4][1, 1];
            int rightColor = rubikFace[1][1, 1];
            int backColor = rubikFace[5][1, 1];
            int leftColor = rubikFace[0][1, 1];
            bool F = (FL["F"] == frontColor && FL["L"] == leftColor);
            bool R = (FR["F"] == frontColor && FR["R"] == rightColor);
            bool L = (BL["B"] == backColor && BL["L"] == leftColor);
            bool B = (BR["B"] == backColor && BR["R"] == rightColor);

            return (F && R && L && B);
        }

        public List<string> solveSecondLayer()
        {
            List <string> solution = new List<string>();
            int step = 0;
            int k = 0;
            updateBlock();
            while (true)
            {
                step++;
                k++;
                if(k == 200)
                {
                    return null;
                }
                if (secondLayerSolved())
                {
                    break;
                }

                if (step > 6)
                {
                    return null;
                }
                Dictionary<string, int> current = new Dictionary<string, int>();
                current = FU;
                
                if (!current.ContainsValue(2) && !current.ContainsValue(13))
                {
                    step = 0;
                    int frontColor = current["F"];
                    //Console.WriteLine(frontColor);
                    Dictionary<string, int> position = new Dictionary<string, int>();
                    foreach(var item in listCenters)
                    {
                        if(item.ContainsValue(frontColor))
                        {
                            position = item;
                        }
                    }

                    if(position.Count() == 0)
                    {
                        return null;
                    }

                    if (position["name"] == 100)
                    {
                        this.RotateRubik("U"); solution.Add("U");
                        this.rotateAllY(); solution.Add("Y");
                        this.rotateAllY(); solution.Add("Y");
                        this.rotateAllY(); solution.Add("Y");
                    }
                    else if(position["name"] == 10)
                    {
                        this.RotateRubik("U'"); solution.Add("U'");
                        this.rotateAllY();  solution.Add("Y");
                    }
                    else if(position["name"] == 50)
                    {
                        this.RotateRubik("U"); solution.Add("U");
                        this.RotateRubik("U"); solution.Add("U");
                        this.rotateAllY(); solution.Add("Y");
                        this.rotateAllY(); solution.Add("Y");
                    }

                    if (FU["U"] == R["R"])
                    {
                        this.RotateRubik("U"); solution.Add("U");
                        this.RotateRubik("R"); solution.Add("R");
                        this.RotateRubik("U'"); solution.Add("U'");
                        this.RotateRubik("R'"); solution.Add("R'");
                        this.RotateRubik("U'"); solution.Add("U'");
                        this.RotateRubik("F'"); solution.Add("F'");
                        this.RotateRubik("U"); solution.Add("U");
                        this.RotateRubik("F"); solution.Add("F");
                    }
                    else
                    {
                        this.RotateRubik("U'"); solution.Add("U'");
                        this.RotateRubik("L'"); solution.Add("L'");
                        this.RotateRubik("U"); solution.Add("U");
                        this.RotateRubik("L"); solution.Add("L");
                        this.RotateRubik("U"); solution.Add("U");
                        this.RotateRubik("F"); solution.Add("F");
                        this.RotateRubik("U'"); solution.Add("U'");
                        this.RotateRubik("F'"); solution.Add("F'");
                    }

                }
                else
                {
                    int frontColor = F["F"];
                    int rightColor = R["R"];
                    bool Front = (FR["F"] != frontColor);
                    bool right = (Front || FR["R"] != rightColor);
                    bool check = (right && !FR.ContainsValue(2));
                    if (check)
                    {
                        this.RotateRubik("U"); solution.Add("U");
                        this.RotateRubik("R"); solution.Add("R");
                        this.RotateRubik("U'"); solution.Add("U'");
                        this.RotateRubik("R'"); solution.Add("R'");
                        this.RotateRubik("U'"); solution.Add("U'");
                        this.RotateRubik("F'"); solution.Add("F'");
                        this.RotateRubik("U"); solution.Add("U");
                        this.RotateRubik("F"); solution.Add("F");
                    }
                }
                this.rotateAllY();
                solution.Add("Y");
            }
            Console.WriteLine("");

            return solution;
        }

        public List<string> yellowCross()
        {
            List<string> step = new List<string>{"F", "R", "U", "R'", "U'", "F'"};
            List <string> solution = new List<string>();
            List <string> upYellows = new List<string>();
            updateBlock();

            if (FU.FirstOrDefault(x => x.Value == 2).Key == "U")
            {
                upYellows.Add("FU");
            }
            if (RU.FirstOrDefault(x => x.Value == 2).Key == "U")
            {
                upYellows.Add("RU");
            }
            if (LU.FirstOrDefault(x => x.Value == 2).Key == "U")
            {
                upYellows.Add("LU");
            }
            if (BU.FirstOrDefault(x => x.Value == 2).Key == "U")
            {
                upYellows.Add("BU");
            }
            //Console.WriteLine(upYellows.Count());
            if (upYellows.Count() == 0)
            {
                foreach(var item in step)
                {
                    this.RotateRubik(item);
                    solution.Add(item);
                }
                this.RotateRubik("U"); solution.Add("U");
                this.RotateRubik("U"); solution.Add("U");
                foreach(var item in step)
                {
                    this.RotateRubik(item);
                    solution.Add(item);
                }
                foreach(var item in step)
                {
                    this.RotateRubik(item);
                    solution.Add(item);
                }
            }
            else if (upYellows.Count() == 2)
            {
                if (!(upYellows.Contains("FU") && upYellows.Contains("BU")) && !(upYellows.Contains("RU") && upYellows.Contains("LU")))
                {
                    int k = 0;
                    while (!(BU.FirstOrDefault(x => x.Value == 2).Key == "U" && LU.FirstOrDefault(x => x.Value == 2).Key == "U"))
                    {
                        k++;
                        if(k == 200)
                        {
                            return null;
                        }
                        this.RotateRubik("U");
                        solution.Add("U");
                    }

                    foreach(var item in step)
                    {
                        this.RotateRubik(item);
                        solution.Add(item);
                    }
                }

                int i = 0;
                while (!(RU.FirstOrDefault(x => x.Value == 2).Key == "U" && LU.FirstOrDefault(x => x.Value == 2).Key == "U"))
                {
                    i += 1;
                    if (i == 300)
                    {
                        return null;
                    }
                    this.RotateRubik("U");
                    solution.Add("U");
                }

                foreach(var item in step)
                {
                    this.RotateRubik(item);
                    solution.Add(item);
                }
            }

            Console.WriteLine("");
           
            return solution;
        }

        public bool edgesPlaced()
        {
            updateBlock();
            string actualOrdered = FU["F"].ToString() + RU["R"].ToString() + BU["B"].ToString() + LU["L"].ToString();
            //Console.WriteLine(actualOrdered);
            return (actualOrdered == "4150" || actualOrdered == "0415" || actualOrdered == "5041" || actualOrdered == "1504");
        }

        public bool cornersIsPlaced(Dictionary<string, int> T)
        {
            updateBlock();
            Dictionary<string, int> newT = new Dictionary<string, int>();
            newT = T;
            if(newT["name"] == 10) // FLU
            {
                if (!newT.ContainsValue(FU["F"]) || !newT.ContainsValue(LU["L"])) { return false;}
            }
            if(newT["name"] == 30)
            {
                if (!newT.ContainsValue(FU["F"]) || !newT.ContainsValue(RU["R"])) { return false;}
            }
            if(newT["name"] == 20)
            {
                if (!newT.ContainsValue(BU["B"]) || !newT.ContainsValue(RU["R"])) { return false;}
            }
            if(newT["name"] == 50)
            {
                if (!newT.ContainsValue(BU["B"]) || !newT.ContainsValue(LU["L"])) { return false;}
            }
            return true;
        }

        public List<string> CornerPlaced()
        {
            updateBlock();
            List<string> T = new List<string> ();
            if (cornersIsPlaced(FRU)) { T.Add("FRU");}
            if (cornersIsPlaced(FLU)) { T.Add("FLU");}
            if (cornersIsPlaced(BRU)) { T.Add("BRU");}
            if (cornersIsPlaced(BLU)) { T.Add("BLU");}
            return T;
        }

        public List<string> yellowApplyEdges()
        {
            List<string> applyEdges = new List<string>{"R", "U", "R'", "U", "R", "U", "U", "R'"};
            List<string> applyCorner = new List<string>{"U", "R", "U'", "L'", "U", "R'", "U'", "L"};
            List<string> applyCornerOrient = new List<string>{"R'", "D'", "R", "D"};

            List <string> solution = new List<string>();
            int i = 0;
            int turns = 0;
            updateBlock();
            while (!edgesPlaced())
            {
                i += 1;
                if (i == 300)
                {
                    return null;
                }
                turns += 1;
                if (turns >= 4)
                {
                    turns = 0;
                    foreach(var item in applyEdges)
                    {
                        this.RotateRubik(item);
                        solution.Add(item);
                    }
                }
                //Console.WriteLine(turns);
                foreach(var item in applyEdges)
                {
                    this.RotateRubik(item);
                    solution.Add(item);
                }
                this.rotateAllY(); solution.Add("Y");
                this.rotateAllY(); solution.Add("Y");
                this.rotateAllY(); solution.Add("Y");

            }

            int k = 0;
            while(F["F"] != FU["F"])
            {
                k++;
                if(k == 100)
                {
                    return null;
                }
                this.RotateRubik("U");
                solution.Add("U");
            }

            return solution;

        }

        public List<string> yellowApplyCorners()
        {
            List<string> applyEdges = new List<string>{"R", "U", "R'", "U", "R", "U", "U", "R'"};
            List<string> applyCorner = new List<string>{"U", "R", "U'", "L'", "U", "R'", "U'", "L"};
            List<string> applyCornerOrient = new List<string>{"R'", "D'", "R", "D"};

            List <string> solution = new List<string>();
            int i = 0;
            while (true)
            {
                i += 1;
                if (i == 300)
                {
                    return null;
                }
                //updateBlock();
                //List<string> placed = this.CornerPlaced();
                //Console.WriteLine(placed.Count());
                //Console.WriteLine(placed.IndexOf("FRU"));
                if (CornerPlaced().Count() == 4) { break;}
                else if (CornerPlaced().Count() == 1)
                {
                    int k = 0;
                    while (this.CornerPlaced().IndexOf("FRU") != 0)
                    {
                        k++;
                        if(k == 200)
                        {
                            return null;
                        }
                        //Console.WriteLine(placed.IndexOf("FRU"));
                        this.RotateRubik("U");
                        solution.Add("U");
                        
                    }
                    foreach(var item in applyCorner)
                    {
                        this.RotateRubik(item);
                        solution.Add(item);
                    }
                }
                else
                {
                    foreach(var item in applyCorner)
                    {
                        this.RotateRubik(item);
                        solution.Add(item);
                    }
                }
            }

            return solution;
        }

        public List<string> yellowCornerOrient()
        {
            List<string> applyEdges = new List<string>{"R", "U", "R'", "U", "R", "U", "U", "R'"};
            List<string> applyCorner = new List<string>{"U", "R", "U'", "L'", "U", "R'", "U'", "L"};
            List<string> applyCornerOrient = new List<string>{"R'", "D'", "R", "D"};

            List <string> solution = new List<string>();
            int n = 0, m = 0;
            for(int i = 0; i < 4; i++)
            {
                while(FRU["U"] != 2)
                {
                    n += 1;
                    if (n == 300)
                    {
                        return null;
                    }
                    foreach(var item in applyCornerOrient)
                    {
                        this.RotateRubik(item);
                        solution.Add(item);
                    }
                }

                this.RotateRubik("U");
                solution.Add("U");
            }

            while(F["F"] != FU["F"])
            {
                m += 1;
                if (m == 300)
                {
                    return null;
                }
                this.RotateRubik("U");
                solution.Add("U");
            }

            Console.WriteLine("");
            
            return solution;

        }

        public void DrawRubik()
        {
            GL.LineWidth(Constants._lineWidth);

            //Front
            GL.Color3(Color.Black);
            GL.InitNames();
            GL.PushName(4);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(-Constants.Size * 1.5, -Constants.Size * 1.5, Constants.Size * 1.5);
            GL.Vertex3(-Constants.Size * 1.5, Constants.Size * 1.5, Constants.Size * 1.5);
            GL.Vertex3(Constants.Size * 1.5, Constants.Size * 1.5, Constants.Size * 1.5);
            GL.Vertex3(Constants.Size * 1.5, -Constants.Size * 1.5, Constants.Size * 1.5);
            GL.End();
            GL.PopName();

            //Back
            GL.PushName(5);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(-Constants.Size * 1.5, -Constants.Size * 1.5, -Constants.Size * 1.5);
            GL.Vertex3(-Constants.Size * 1.5, Constants.Size * 1.5, -Constants.Size * 1.5);
            GL.Vertex3(Constants.Size * 1.5, Constants.Size * 1.5, -Constants.Size * 1.5);
            GL.Vertex3(Constants.Size * 1.5, -Constants.Size * 1.5, -Constants.Size * 1.5);
            GL.End();
            GL.PopName();

            //Left
            GL.PushName(0);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(-Constants.Size * 1.5, -Constants.Size * 1.5, Constants.Size * 1.5);
            GL.Vertex3(-Constants.Size * 1.5, -Constants.Size * 1.5, -Constants.Size * 1.5);
            GL.Vertex3(-Constants.Size * 1.5, Constants.Size * 1.5, -Constants.Size * 1.5);
            GL.Vertex3(-Constants.Size * 1.5, Constants.Size * 1.5, Constants.Size * 1.5);
            GL.End();
            GL.PopName();

            //Right
            GL.PushName(1);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(Constants.Size * 1.5, -Constants.Size * 1.5, Constants.Size * 1.5);
            GL.Vertex3(Constants.Size * 1.5, -Constants.Size * 1.5, -Constants.Size * 1.5);
            GL.Vertex3(Constants.Size * 1.5, Constants.Size * 1.5, -Constants.Size * 1.5);
            GL.Vertex3(Constants.Size * 1.5, Constants.Size * 1.5, Constants.Size * 1.5);
            GL.End();
            GL.PopName();

            //Top
            GL.PushName(2);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(-Constants.Size * 1.5, Constants.Size * 1.5, Constants.Size * 1.5);
            GL.Vertex3(-Constants.Size * 1.5, Constants.Size * 1.5, -Constants.Size * 1.5);
            GL.Vertex3(Constants.Size * 1.5, Constants.Size * 1.5, -Constants.Size * 1.5);
            GL.Vertex3(Constants.Size * 1.5, Constants.Size * 1.5, Constants.Size * 1.5);
            GL.End();
            GL.PopName();

            //Bottom
            GL.PushName(3);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(-Constants.Size * 1.5, -Constants.Size * 1.5, Constants.Size * 1.5);
            GL.Vertex3(-Constants.Size * 1.5, -Constants.Size * 1.5, -Constants.Size * 1.5);
            GL.Vertex3(Constants.Size * 1.5, -Constants.Size * 1.5, -Constants.Size * 1.5);
            GL.Vertex3(Constants.Size * 1.5, -Constants.Size * 1.5, Constants.Size * 1.5);
            GL.End();
            GL.PopName();

        }
        

        public void RotateRubik(string Face)
        {
            switch (Face)
            {
                case "L":
                    this.rotateX(0, true);
                    break;
                case "L'":
                    this.rotateX(0, false);
                    break;
                case "R":
                    this.rotateX(1, true);
                    break;
                case "R'":
                    this.rotateX(1, false);
                    break;
                case "U":
                    this.rotateY(2, true);
                    break;
                case "U'":
                    this.rotateY(2, false);
                    break;
                case "D":
                    this.rotateY(3, true);
                    break;
                case "D'":
                    this.rotateY(3, false);
                    break;
                case "F":
                    this.rotateZ(4, true);
                    break;
                case "F'":
                    this.rotateZ(4, false);
                    break;
                case "B":
                    this.rotateZ(5, true);
                    break;
                case "B'":
                    this.rotateZ(5, false);
                    break;
            }
        }

        public void rotateAllY()
        {
            this.rotateY(2, true);
            this.rotateE();
            this.rotateY(3, false);
        }

        public void rotateAllX()
        {
            this.rotateX(0, true);
            this.rotateM();
            this.rotateX(1, false);
        }

        public void rotateAllZ()
        {
            this.rotateZ(4, true);
            this.rotateS();
            this.rotateZ(5, false);
        }

        public void rotateS()
        {
            int a = rubikFace[1][0, 1];
            int b = rubikFace[1][1, 1];
            int c = rubikFace[1][2, 1];
            for (int i = 0; i < 3; i++)
            {
                rubikFace[1][i, 1] = rubikFace[2][1, i];
            }
            for (int i = 0; i < 3; i++)
            {
                rubikFace[2][1, i] = rubikFace[0][2 - i, 1];
            }
            for (int i = 0; i < 3; i++)
            {
                rubikFace[0][i, 1] = rubikFace[3][1, i];
            }
            rubikFace[3][1, 0] = c;
            rubikFace[3][1, 1] = b;
            rubikFace[3][1, 2] = a;
            updateBlock();
        }

        public void rotateM()
        {
            int a = rubikFace[2][0, 1];
            int b = rubikFace[2][1, 1];
            int c = rubikFace[2][2, 1];
            for (int i = 0; i < 3; i++)
            {
                rubikFace[2][i, 1] = rubikFace[5][2 - i, 1];
            }
            for (int i = 0; i < 3; i++)
            {
                rubikFace[5][i, 1] = rubikFace[3][2 - i, 1];
            }
            for (int i = 0; i < 3; i++)
            {
                rubikFace[3][i, 1] = rubikFace[4][i, 1];
            }
            rubikFace[4][0, 1] = a;
            rubikFace[4][1, 1] = b;
            rubikFace[4][2, 1] = c;
            updateBlock();
        }

        public void rotateZ(int x, bool y)
        {
            this.rotateFace(x, y);
            if (x == 4 && y == true) // F
            {
                int a = rubikFace[1][0, 0];
                int b = rubikFace[1][1, 0];
                int c = rubikFace[1][2, 0];
                for (int i = 0; i < 3; i++)
                {
                    rubikFace[1][i, 0] = rubikFace[2][2, i];
                }
                for (int i = 0; i < 3; i++)
                {
                    rubikFace[2][2, i] = rubikFace[0][2 - i, 2];
                }
                for (int i = 0; i < 3; i++)
                {
                    rubikFace[0][i, 2] = rubikFace[3][0, i];
                }
                rubikFace[3][0, 0] = c;
                rubikFace[3][0, 1] = b;
                rubikFace[3][0, 2] = a;
            }
            if (x == 4 && y == false) // F'
            {
                int a = rubikFace[1][0, 0];
                int b = rubikFace[1][1, 0];
                int c = rubikFace[1][2, 0];
                for (int i = 0; i < 3; i++)
                {
                    rubikFace[1][i, 0] = rubikFace[3][0, 2 - i];
                }
                for (int i = 0; i < 3; i++)
                {
                    rubikFace[3][0, i] = rubikFace[0][i, 2];
                }
                for (int i = 0; i < 3; i++)
                {
                    rubikFace[0][2 - i, 2] = rubikFace[2][2, i];
                }
                rubikFace[2][2, 0] = a;
                rubikFace[2][2, 1] = b;
                rubikFace[2][2, 2] = c;
            }
            if (x == 5 && y == true) // B
            {
                int a = rubikFace[1][0, 2];
                int b = rubikFace[1][1, 2];
                int c = rubikFace[1][2, 2];
                for (int i = 0; i < 3; i++)
                {
                    rubikFace[1][i, 2] = rubikFace[3][2, 2 - i];
                }
                for (int i = 0; i < 3; i++)
                {
                    rubikFace[3][2, i] = rubikFace[0][i, 0];
                }
                for (int i = 0; i < 3; i++)
                {
                    rubikFace[0][i, 0] = rubikFace[2][0, 2 - i];
                }
                rubikFace[2][0, 0] = a;
                rubikFace[2][0, 1] = b;
                rubikFace[2][0, 2] = c;
            }
            if (x == 5 && y == false) // B'
            {
                int a = rubikFace[1][0, 2];
                int b = rubikFace[1][1, 2];
                int c = rubikFace[1][2, 2];
                for (int i = 0; i < 3; i++)
                {
                    rubikFace[1][i, 2] = rubikFace[2][0, i];
                }
                for (int i = 0; i < 3; i++)
                {
                    rubikFace[2][0, i] = rubikFace[0][2 - i, 0];
                }
                for (int i = 0; i < 3; i++)
                {
                    rubikFace[0][i, 0] = rubikFace[3][2, i];
                }
                rubikFace[3][2, 0] = c;
                rubikFace[3][2, 1] = b;
                rubikFace[3][2, 2] = a;
            }
            updateBlock();
        }

        public void rotateX(int x, bool y)
        {
            this.rotateFace(x, y);
            if (x == 0 && y == true) // L
            {
                int a = rubikFace[2][0, 0];
                int b = rubikFace[2][1, 0];
                int c = rubikFace[2][2, 0];
                for (int i = 0; i < 3; i++)
                {
                    rubikFace[2][i, 0] = rubikFace[5][2 - i, 2];
                }
                for (int i = 0; i < 3; i++)
                {
                    rubikFace[5][i, 2] = rubikFace[3][2 - i, 0];
                }
                for (int i = 0; i < 3; i++)
                {
                    rubikFace[3][i, 0] = rubikFace[4][i, 0];
                }
                rubikFace[4][0, 0] = a;
                rubikFace[4][1, 0] = b;
                rubikFace[4][2, 0] = c;
            }
            if (x == 0 && y == false) // L'
            {
                int a = rubikFace[4][0, 0];
                int b = rubikFace[4][1, 0];
                int c = rubikFace[4][2, 0];
                for (int i = 0; i < 3; i++)
                {
                    rubikFace[4][i, 0] = rubikFace[3][i, 0];
                }
                for (int i = 0; i < 3; i++)
                {
                    rubikFace[3][i, 0] = rubikFace[5][2 - i, 2];
                }
                for (int i = 0; i < 3; i++)
                {
                    rubikFace[5][i, 2] = rubikFace[2][2 - i, 0];
                }
                rubikFace[2][0, 0] = a;
                rubikFace[2][1, 0] = b;
                rubikFace[2][2, 0] = c;
            }
            if (x == 1 && y == true) // R
            {
                int a = rubikFace[2][0, 2];
                int b = rubikFace[2][1, 2];
                int c = rubikFace[2][2, 2];
                for (int i = 0; i < 3; i++)
                {
                    rubikFace[2][i, 2] = rubikFace[4][i, 2];
                }
                for (int i = 0; i < 3; i++)
                {
                    rubikFace[4][i, 2] = rubikFace[3][i, 2];
                }
                for (int i = 0; i < 3; i++)
                {
                    rubikFace[3][i, 2] = rubikFace[5][2 - i, 0];
                }
                rubikFace[5][0, 0] = c;
                rubikFace[5][1, 0] = b;
                rubikFace[5][2, 0] = a;
            }
            if (x == 1 && y == false) // R'
            {
                int a = rubikFace[2][0, 2];
                int b = rubikFace[2][1, 2];
                int c = rubikFace[2][2, 2];
                for (int i = 0; i < 3; i++)
                {
                    rubikFace[2][i, 2] = rubikFace[5][2 - i, 0];
                }
                for (int i = 0; i < 3; i++)
                {
                    rubikFace[5][i, 0] = rubikFace[3][2 - i, 2];
                }
                for (int i = 0; i < 3; i++)
                {
                    rubikFace[3][i, 2] = rubikFace[4][i, 2];
                }
                rubikFace[4][0, 2] = a;
                rubikFace[4][1, 2] = b;
                rubikFace[4][2, 2] = c;
            }
            updateBlock();
        }

        public void rotateE()
        {
            int a = rubikFace[0][1, 0];
            int b = rubikFace[0][1, 1];
            int c = rubikFace[0][1, 2];
            for (int i = 0; i < 3; i++)
            {
                rubikFace[0][1, i] = rubikFace[4][1, i];
            }
            for (int i = 0; i < 3; i++)
            {
                rubikFace[4][1, i] = rubikFace[1][1, i];
            }
            for (int i = 0; i < 3; i++)
            {
                rubikFace[1][1, i] = rubikFace[5][1, i];
            }
            rubikFace[5][1, 0] = a;
            rubikFace[5][1, 1] = b;
            rubikFace[5][1, 2] = c;
            updateBlock();
        }

        public void rotateY(int x, bool y)
        {
            this.rotateFace(x, y);
            if (x == 2 && y == true) // U
            {
                int a = rubikFace[0][0, 0];
                int b = rubikFace[0][0, 1];
                int c = rubikFace[0][0, 2];
                for (int i = 0; i < 3; i++)
                {
                    rubikFace[0][0, i] = rubikFace[4][0, i];
                }
                for (int i = 0; i < 3; i++)
                {
                    rubikFace[4][0, i] = rubikFace[1][0, i];
                }
                for (int i = 0; i < 3; i++)
                {
                    rubikFace[1][0, i] = rubikFace[5][0, i];
                }
                rubikFace[5][0, 0] = a;
                rubikFace[5][0, 1] = b;
                rubikFace[5][0, 2] = c;
            }
            if (x == 2 && y == false) // U'
            {
                int a = rubikFace[0][0, 0];
                int b = rubikFace[0][0, 1];
                int c = rubikFace[0][0, 2];
                for (int i = 0; i < 3; i++)
                {
                    rubikFace[0][0, i] = rubikFace[5][0, i];
                }
                for (int i = 0; i < 3; i++)
                {
                    rubikFace[5][0, i] = rubikFace[1][0, i];
                }
                for (int i = 0; i < 3; i++)
                {
                    rubikFace[1][0, i] = rubikFace[4][0, i];
                }
                rubikFace[4][0, 0] = a;
                rubikFace[4][0, 1] = b;
                rubikFace[4][0, 2] = c;
            }
            if (x == 3 && y == true) // D
            {
                int a = rubikFace[0][2, 0];
                int b = rubikFace[0][2, 1];
                int c = rubikFace[0][2, 2];
                for (int i = 0; i < 3; i++)
                {
                    rubikFace[0][2, i] = rubikFace[5][2, i];
                }
                for (int i = 0; i < 3; i++)
                {
                    rubikFace[5][2, i] = rubikFace[1][2, i];
                }
                for (int i = 0; i < 3; i++)
                {
                    rubikFace[1][2, i] = rubikFace[4][2, i];
                }
                rubikFace[4][2, 0] = a;
                rubikFace[4][2, 1] = b;
                rubikFace[4][2, 2] = c;
            }
            if (x == 3 && y == false) // D'
            {
                int a = rubikFace[0][2, 0];
                int b = rubikFace[0][2, 1];
                int c = rubikFace[0][2, 2];
                for (int i = 0; i < 3; i++)
                {
                    rubikFace[0][2, i] = rubikFace[4][2, i];
                }
                for (int i = 0; i < 3; i++)
                {
                    rubikFace[4][2, i] = rubikFace[1][2, i];
                }
                for (int i = 0; i < 3; i++)
                {
                    rubikFace[1][2, i] = rubikFace[5][2, i];
                }
                rubikFace[5][2, 0] = a;
                rubikFace[5][2, 1] = b;
                rubikFace[5][2, 2] = c;
            }
            updateBlock();
        }

        public void rotateFace(int x, bool y)
        {
            int tmp;
            if (y == true)
            {
                tmp = rubikFace[x][0, 0];
                rubikFace[x][0, 0] = rubikFace[x][2, 0];
                rubikFace[x][2, 0] = rubikFace[x][2, 2];
                rubikFace[x][2, 2] = rubikFace[x][0, 2];
                rubikFace[x][0, 2] = tmp;
                tmp = rubikFace[x][0, 1];
                rubikFace[x][0, 1] = rubikFace[x][1, 0];
                rubikFace[x][1, 0] = rubikFace[x][2, 1];
                rubikFace[x][2, 1] = rubikFace[x][1, 2];
                rubikFace[x][1, 2] = tmp;
            }
            else
            {
                tmp = rubikFace[x][0, 0];
                rubikFace[x][0, 0] = rubikFace[x][0, 2];
                rubikFace[x][0, 2] = rubikFace[x][2, 2];
                rubikFace[x][2, 2] = rubikFace[x][2, 0];
                rubikFace[x][2, 0] = tmp;
                tmp = rubikFace[x][0, 1];
                rubikFace[x][0, 1] = rubikFace[x][1, 2];
                rubikFace[x][1, 2] = rubikFace[x][2, 1];
                rubikFace[x][2, 1] = rubikFace[x][1, 0];
                rubikFace[x][1, 0] = tmp;
            }

        }
        public static myRubik DeepClone<myRubik>(myRubik obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (myRubik)formatter.Deserialize(ms);
            }
        }

        public static bool operator ==(myRubik a, myRubik b)
        { 
            for (int i = 0; i < 6; i++)
            {
                for (int m = 0; m < 3; m++)
                {
                    for (int n = 0; n < 3; n++)
                    {
                        if (a.rubikFace[i][m, n] != b.rubikFace[i][m, n]) return false;
                    }
                }
            }
            return true;
        }
        public static bool operator !=(myRubik a, myRubik b)
        {
            for (int i = 0; i < 6; i++)
            {
                for (int m = 0; m < 6; m++)
                {
                    for (int n = 0; n < 6; n++)
                    {
                        if (a.rubikFace[i][m, n] != b.rubikFace[i][m, n]) return true;
                    }
                }
            }
            return false;
        }

    }

}
