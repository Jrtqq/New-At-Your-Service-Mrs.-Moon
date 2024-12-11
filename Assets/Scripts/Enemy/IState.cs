using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyScripts
{
    public interface IState
    {
        void Enter();
        void Exit();
        void FixedUpdate();
    }
}