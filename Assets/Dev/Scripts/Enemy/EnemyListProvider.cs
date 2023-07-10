using System.Collections.Generic;
using UnityEngine;

namespace Dev.Scripts.Enemy
{
    public interface IEnemyListProvider
    {
        List<GameObject> GetEnemyList();
    }
}