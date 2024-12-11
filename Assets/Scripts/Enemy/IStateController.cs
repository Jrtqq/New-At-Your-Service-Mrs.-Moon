using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyScripts
{
    public interface IStateController
    {
        void Switch<T>() where T : IState;
    }
}