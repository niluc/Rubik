using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubik
{
    class Animation
    {
        private List<Cube> animationCubes;
        public bool AnimationEnded;
        private RubikCubeMoviment moviment;
        double leftDegree;

        public Animation(List<Cube> animationCubes, double degree, RubikCubeMoviment moviment)
        {
            this.animationCubes = animationCubes;
            this.leftDegree = degree;
            this.moviment = moviment;
            this.AnimationEnded = false;
        }

        public void Animate()
        {
            List<Cube> movimentingPieces = new List<Cube>();

            if (moviment.n == 3)
            {
                movimentingPieces = this.animationCubes;
            }

            else if (moviment.Axis == Axis.X)
            {
                if (moviment.Depth == Depth.First)
                {
                    movimentingPieces = this.animationCubes.FindAll(pieces => pieces.X < 0);
                }
                else if (moviment.Depth == Depth.Second)
                {
                    movimentingPieces = this.animationCubes.FindAll(pieces => pieces.X == 0);
                }
                else if (moviment.Depth == Depth.Third)
                {
                    movimentingPieces = this.animationCubes.FindAll(pieces => pieces.X > 0);
                }

            }
            else if (moviment.Axis == Axis.Y)
            {
                if (moviment.Depth == Depth.First)
                {
                    movimentingPieces = this.animationCubes.FindAll(pieces => pieces.Y > 0);
                }
                else if (moviment.Depth == Depth.Second)
                {
                    movimentingPieces = this.animationCubes.FindAll(pieces => pieces.Y == 0);
                }
                else if (moviment.Depth == Depth.Third)
                {
                    movimentingPieces = this.animationCubes.FindAll(pieces => pieces.Y < 0);
                }

            }
            else if (moviment.Axis == Axis.Z)
            {
                if (moviment.Depth == Depth.First)
                {
                    movimentingPieces = this.animationCubes.FindAll(pieces => pieces.Z > 0);
                }
                else if (moviment.Depth == Depth.Second)
                {
                    movimentingPieces = this.animationCubes.FindAll(pieces => pieces.Z == 0);
                }
                else if (moviment.Depth == Depth.Third)
                {
                    movimentingPieces = this.animationCubes.FindAll(pieces => pieces.Z < 0);
                }
                
            }

            double rotateFactor = 45.0/19.0;

            if (moviment.Axis == Axis.X)
            {
                if (moviment.Spin == Spin.Clockwise)
                {
                    rotateFactor = -rotateFactor;
                }
                foreach (var item in movimentingPieces)
                {
                    item.Rotate(rotateFactor, 0, 0);
                }
            }
            if (moviment.Axis == Axis.Y)
            {
                if (moviment.Spin == Spin.Clockwise)
                {
                    rotateFactor = -rotateFactor;
                }
                foreach (var item in movimentingPieces)
                {
                    item.Rotate(0, rotateFactor, 0);
                }
            }
            if (moviment.Axis == Axis.Z)
            {
                if (moviment.Spin == Spin.Clockwise)
                {
                    rotateFactor = -rotateFactor;
                }
                foreach (var item in movimentingPieces)
                {
                    item.Rotate(0, 0, rotateFactor);
                }
            }

            leftDegree -= Math.Abs(rotateFactor);

            if (leftDegree <= 0)
            {
                foreach (var item in movimentingPieces)
                {
                    item.Place(moviment);
                }
                this.AnimationEnded = true;
                
            }
        }
        
    }
}

