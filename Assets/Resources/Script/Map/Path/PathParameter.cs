using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathFindingName
{
    public class PathParam : MonoBehaviour
    {
        public WalkableParam walkableParam { get; private set; }

        public string start;
        public string end;

        public bool shortPath { get; private set; } = false;
        
        public int shortPathPower { get; private set; } = 2;

        public bool DiagonalSearch { get; private set; } = false;

        public PathParam(string start, string end, WalkableParam walkableParam = null)
        {
            this.start = start;
            this.end = end;

            if (walkableParam is null)
                this.walkableParam = WalkableParam.GetCommonParam();
            else
                this.walkableParam = walkableParam;
        }

        public PathParam SetDiagonalSearch()
        {
            DiagonalSearch = true;
            return this;
        }

    }
}