using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Physics_fighter.Src
{
    public class World
    {
        public int Frame = 0;
        public List<String> PlayerList = new List<String>();
        public List<String> ReadyList = new List<String>();
        public PointMass[] PointMassList;
        public Connection[] ConnectionList;
        public World(int PointMassCount, int ConnectionCount)
        {
            PointMassList = new PointMass[PointMassCount];
            for (int i = 0; i < PointMassList.Length; ++i)
            {
                PointMassList[i] = null;
            }
            ConnectionList = new Connection[ConnectionCount];
            for (int i = 0; i < ConnectionList.Length; ++i)
            {
                ConnectionList[i] = null;
            }
            AddPointMass(new PointMass(new Vector_2d(50,400)));
            AddPointMass(new PointMass(new Vector_2d(100,400)));
            AddConnection(new Connection(this, 0, 1));
        }
        public int AddPointMass(PointMass point)
        {
            for (int i = 0; i < PointMassList.Length; ++i)
            {
                if (PointMassList[i] == null)
                {
                    point.Id = i;
                    PointMassList[i] = point;
                    return i;
                }
            }
            return -1;
        }
        public int AddConnection(Connection connection)
        {
            for (int i = 0; i < ConnectionList.Length; ++i)
            {
                if (ConnectionList[i] == null)
                {
                    connection.Id = i;
                    ConnectionList[i] = connection;
                    return i;
                }
            }
            return -1;
        }
        public void Update()
        {
            for (int i = 0; i < PointMassList.Length; ++i)
            {
                if (PointMassList[i] != null)
                {
                    PointMassList[i].Update(this);
                }
            }
            for (int j = 0; j < 4; ++j)
            {
                for (int i = 0; i < ConnectionList.Length; ++i)
                {
                    if (ConnectionList[i] != null)
                    {
                        ConnectionList[i].Update(this);
                    }
                }
            }
            ++Frame;
        }
    }
}