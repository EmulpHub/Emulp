using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace PathFindingName
{
    public delegate bool Condition(string pos);

    public class Path : MonoBehaviour
    {
        public static int CalcH(string pos, PathParam param)
        {
            return F.DistanceBetweenTwoPos(pos, param.end);
        }

        public static PathResult Find(PathParam param)
        {
            var prePath = new PreExecution();

            prePath.Treat_param(param);

            var listForbideenPos = param.walkableParam.listForbideenPos;

            if (listForbideenPos.Contains(param.start))
                listForbideenPos.Remove(param.start);
            if (listForbideenPos.Contains(param.end))
                listForbideenPos.Remove(param.end);

            var path = MakePath(param);

            prePath.Treat_path(path, param);

            return new PathResult(path, param.start,param.walkableParam);
        }

        private static List<string> MakePath(PathParam param)
        {
            //create grid
            var info = new PathInfo(param,param.walkableParam);

            var listOpen = info.listOpen;
            var listClose = info.listClose;

            listOpen.Add(param.start, new squareInfo(param.start, 0, CalcH(param.end, param))); //Adding starting point

            while (listOpen.Count > 0)
            {
                squareInfo square = listOpen.Values.OrderBy(a => a.f).ThenBy(a => a.g).ThenBy(a => a.h).First();

                listOpen.Remove(square.pos);
                listClose.Add(square.pos, square);

                int g = 10;

                if (square.ManageNearPos("1_0", info, g)) break;
                if (square.ManageNearPos("-1_0", info, g)) break;
                if (square.ManageNearPos("0_1", info, g)) break;
                if (square.ManageNearPos("0_-1", info, g)) break;

                if (param.DiagonalSearch)
                {
                    g = 14;

                    if (square.ManageNearPos("1_1", info, g)) break;
                    if (square.ManageNearPos("-1_1", info, g)) break;
                    if (square.ManageNearPos("1_-1", info, g)) break;
                    if (square.ManageNearPos("-1_-1", info, g)) break;
                }
            }

            //ONLY FOR DEBUG
            /*
            foreach (var s in new List<squareInfo>(listClose.Values))
            {
                Debug_test.prefab.GetComponent<Debug_test>().focusMode = Debug_test.FocusMode.All;

                Debug_test.prefab.GetComponent<Debug_test>().G = s.g;
                Debug_test.prefab.GetComponent<Debug_test>().H = s.h;
                Debug_test.prefab.GetComponent<Debug_test>().pos = s.pos;

                Instantiate(Debug_test.prefab, Debug_test.prefabParent.transform).GetComponent<Debug_test>();
            }*/

            //Calc path
            var path = new List<string>();

            if (!listClose.ContainsKey(param.end)) return path;

            squareInfo current = listClose[param.end];

            int resultWanted = 0;
            int i = 0;
            while (current.pos != param.start)
            {
                listClose.Remove(current.pos);

                current = current.Next(info, i % 2 == resultWanted);

                if (i == 0)
                {
                    var dir = DirectionData.GetDirection(param.end, current.pos);

                    if (dir == DirectionData.Direction.right || dir == DirectionData.Direction.left)
                        resultWanted = 1;
                }

                path.Add(current.pos);

                i++;
            }

            path.Reverse();

            path.Add(param.end);

            path.RemoveAt(0);

            return path;
        }

    }

    public class PathResult
    {
        public WalkableParam walkableParam;

        public string start;

        public string endOfPath
        {
            get
            {
                if (path.Count == 0)
                    return "no path";

                return path.Last();
            }
        }

        public List<string> path;

        private int _lengthInSquare = -1;

        public int LengthInSquare
        {
            get
            {
                if (_lengthInSquare == -1)
                {
                    int amount = 0;

                    int i = 0;
                    while (i < path.Count)
                    {
                        string pos = path[i];

                        if (i == path.Count - 1)
                        {
                            if (i == 0 && F.DistanceBetweenTwoPos(pos, start) > 1)
                                amount += 2;
                            else
                                amount++;

                            break;
                        }

                        string pos1 = path[i + 1];

                        if (F.DistanceBetweenTwoPos(pos, pos1) > 1)
                            amount += 2;
                        else
                            amount++;

                        i++;
                    }

                    _lengthInSquare = amount;
                }

                return _lengthInSquare;
            }
        }

        public PathResult(List<string> path, string start, WalkableParam walkableParam)
        {
            this.path = path;
            this.start = start;
            this.walkableParam = walkableParam;

        }
    }

    public class PathInfo
    {
        public WalkableParam walkableParam;
        public PathParam param;
        public Dictionary<string, squareInfo> listOpen;
        public Dictionary<string, squareInfo> listClose;

        public PathInfo(PathParam param,WalkableParam walkableParam)
        {
            this.param = param;
            this.walkableParam = walkableParam;

            listOpen = new Dictionary<string, squareInfo>();
            listClose = new Dictionary<string, squareInfo>();
        }

        public bool containBoth(string pos, out squareInfo square)
        {
            if (containOpen(pos, out square)) return true;
            if (containClose(pos, out square)) return true;

            return false;
        }


        public bool containOpen(string pos, out squareInfo square)
        {
            if (listOpen.TryGetValue(pos, out square)) return true;

            return false;
        }

        public bool containClose(string pos, out squareInfo square)
        {
            if (listClose.TryGetValue(pos, out square)) return true;

            return false;
        }
    }

    public class squareInfo
    {
        public string pos;

        public int g, h;

        public int f { get => g + h; }

        public squareInfo(string pos, int g, int h)
        {
            this.pos = pos;
            this.g = g;
            this.h = h;
        }

        public bool ManageNearPos(string posToManage, PathInfo info, int addG)
        {
            string newPos = F.AdditionPos(posToManage, pos);

            if (!Walkable.Check(newPos, info.walkableParam))
                return false;

            if (F.DistanceBetweenTwoPos(newPos, pos) == 2 && !F.IsTileCrossable(newPos, pos))
                return false;

            if (info.containBoth(newPos, out squareInfo square))
            {
                int newG = g + addG;

                if (square.g > newG)
                {
                    square.g = newG;
                }
            }
            else
            {
                var newSquare = new squareInfo(newPos, g + addG, Path.CalcH(newPos, info.param));

                if (info.param.end == newPos)
                {
                    info.listClose.Add(newPos, newSquare);
                    return true;
                }
                else
                {
                    info.listOpen.Add(newPos, newSquare);
                }
            }

            return false;
        }

        public squareInfo Next(PathInfo info, bool YFirst)
        {
            int minF = int.MaxValue;
            int minG = int.MaxValue;
            squareInfo square = null;

            void Check(string posToCheck, bool diagonal = false)
            {
                if (info.containClose(posToCheck, out squareInfo value) && value.f <= minF)
                {
                    if (value.f == minF && value.g > minG) return;

                    if (diagonal && !F.IsTileCrossable(posToCheck, pos)) return;

                    minF = value.f;
                    minG = value.g;
                    square = value;
                }
            }

            if (YFirst)
            {
                Check(F.AdditionPos("1_0", pos));
                Check(F.AdditionPos("-1_0", pos));
                Check(F.AdditionPos("0_1", pos));
                Check(F.AdditionPos("0_-1", pos));

            }
            else
            {
                Check(F.AdditionPos("0_1", pos));
                Check(F.AdditionPos("0_-1", pos));
                Check(F.AdditionPos("1_0", pos));
                Check(F.AdditionPos("-1_0", pos));
            }

            if (info.param.DiagonalSearch)
            {
                Check(F.AdditionPos("1_1", pos), true);
                Check(F.AdditionPos("1_-1", pos), true);
                Check(F.AdditionPos("-1_1", pos), true);
                Check(F.AdditionPos("-1_-1", pos), true);
            }

            return square;
        }
    }
}