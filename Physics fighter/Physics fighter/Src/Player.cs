using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Physics_fighter.Src
{
    public class Player
    {
        public int Team = -1;
        public string Name = "Spec";
        public float Score = 0;//This is damadge delt to player
        public List<int[]> JointActuators = new List<int[]>();//0 is the id of actuator, 1 is the PointMass said acutator is assined to
        public List<int> Connctions = new List<int>();//The parts owned
        public List<int> PointMasses = new List<int>();//dito
        public List<int[]> JointsId = new List<int[]>();//A,Mid,B
        public void GetScore(World world)
        {
            for (int i = 0; i < Connctions.Count;++i )
            {
                if (world.ConnectionList[Connctions[i]] != null)
                {
                    Score += 100 - world.ConnectionList[Connctions[i]].Damadge;
                }
                else
                {
                    Score += 150;//The penilty for total anhalation
                }
            }
        }
        public void Spawn(World world,Vector_2d pos)
        {
            new SpawnPlayer(world,pos,this);
        }
    }
}