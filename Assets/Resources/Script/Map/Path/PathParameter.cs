using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathFindingName
{
    public class PathParam : MonoBehaviour
    {
        public List<string> listForcePos = new List<string>();
        public List<Entity> listIgnoreEntity = new List<Entity>();
        public string start, end;

        public bool shortPath = false;
        public int shortPathPower = 2;

        public bool DiagonalSearch = false;

        public PathParam(string start, string end)
        {
            this.start = start;
            this.end = end;
        }

        public PathParam SetDiagonalSearch()
        {
            DiagonalSearch = true;
            return this;
        }


        public PathParam AddListIgnoreEntity(Entity entity)
        {
            this.listIgnoreEntity.Add(entity);
            return this;
        }

        public PathParam AddListIgnoreEntity(List<Entity> listIgnoreEntity)
        {
            this.listIgnoreEntity.AddRange(listIgnoreEntity);
            return this;
        }

        public PathParam AddListForcePos(string forcePos)
        {
            this.listForcePos.Add(forcePos);
            return this;
        }
        public PathParam AddListForcePos(List<string> listForcePos)
        {
            this.listForcePos.AddRange(listForcePos);
            return this;
        }
    }
}