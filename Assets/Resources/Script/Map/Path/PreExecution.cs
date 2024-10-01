using System.Collections.Generic;
using UnityEngine;

namespace PathFindingName
{
    public class PreExecution
    {
        public static bool Isreverse(string start, string end)
        {
            var startV2 = F.ConvertToVector2Int(start);
            var endV2 = F.ConvertToVector2Int(end);

            int totalStart = startV2.x + startV2.y;
            int totalEnd = endV2.x + endV2.y;

            if (totalStart == totalEnd)
                return endV2.x > startV2.x;
            else
                return totalEnd > totalStart;
        }

        bool isReverse = false;

        public void Treat_param(PathParam param)
        {
            string start = param.start;
            string end = param.end;

            isReverse = Isreverse(start, end);

            if (!isReverse) return;

            param.start = end;
            param.end = start;

            var entity = EntityByPos.TryGet(start);

            if (entity != null)
                param.listIgnoreEntity.Add(entity);
        }

        public void Treat_path(List<string> path, PathParam param)
        {
            if (!isReverse) return;

            string start = param.start;
            string end = param.end;

            param.start = end;
            param.end = start;

            if (path.Count == 0) return;

            path.RemoveAt(path.Count - 1);

            path.Reverse();

            path.Add(start);
        }
    }
}
