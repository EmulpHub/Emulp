using System.Collections.Generic;
using UnityEngine;

namespace MonsterRandom
{
    public class MonsterStatic_Random : MonoBehaviour
    {
        static List<TypeCaracteristic> listTypeCarac = new()
        {
            new TypeCaracteristic(0, MonsterInfo.MonsterType.normal, TypeCaracteristic.Condition.normal),
            new TypeCaracteristic(0, MonsterInfo.MonsterType.grassy, TypeCaracteristic.Condition.support),
            new TypeCaracteristic(2, MonsterInfo.MonsterType.archer, TypeCaracteristic.Condition.cantBeAlone),
            new TypeCaracteristic(1, MonsterInfo.MonsterType.funky, TypeCaracteristic.Condition.cantBeAlone),
            new TypeCaracteristic(2, MonsterInfo.MonsterType.shark, TypeCaracteristic.Condition.normal),
            new TypeCaracteristic(1, MonsterInfo.MonsterType.magnetic, TypeCaracteristic.Condition.normal),
        };

        static int mostExpensiveMonster = 2;

        public static MonsterInfo.MonsterType RandomType(ref int availablePoint, bool support, bool firstMonster)
        {
            if (availablePoint == 0) return MonsterInfo.MonsterType.normal;

            int targetPoint = availablePoint;

            if (availablePoint >= mostExpensiveMonster) targetPoint = mostExpensiveMonster;

            List<TypeCaracteristic> typeCaracteristics_sameValue = new List<TypeCaracteristic>();

            List<TypeCaracteristic> typeCaracteristics_belowValue = new List<TypeCaracteristic>();

            foreach (TypeCaracteristic type in listTypeCarac)
            {
                if (support && type.condition != TypeCaracteristic.Condition.support) continue;

                if (firstMonster && type.condition == TypeCaracteristic.Condition.cantBeAlone) continue;

                if (type.cost == targetPoint) typeCaracteristics_sameValue.Add(type);
                else typeCaracteristics_belowValue.Add(type);
            }

            availablePoint -= targetPoint;

            List<TypeCaracteristic> finalList = typeCaracteristics_sameValue;

            if (finalList.Count == 0) finalList = typeCaracteristics_belowValue;

            return finalList[Random.Range(0, finalList.Count)].type;
        }

        public static void Randomize(MonsterInMap listMonster, int availablePoint)
        {
            int index = 0;

            foreach (MonsterListInfo monsterInfo in listMonster.list)
            {
                monsterInfo.type = RandomType(ref availablePoint, index == 2, index == 0);

                index++;
            }
        }
    }

    public class TypeCaracteristic
    {
        public int cost;

        public MonsterInfo.MonsterType type;

        public enum Condition { normal, support, cantBeAlone }

        public Condition condition;

        public TypeCaracteristic(int pointRequired, MonsterInfo.MonsterType type, Condition condition)
        {
            this.cost = pointRequired;
            this.type = type;
            this.condition = condition;
        }
    }
}