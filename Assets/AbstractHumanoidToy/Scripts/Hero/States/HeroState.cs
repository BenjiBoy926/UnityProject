using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbstractHumanoidToy
{
    public class HeroState
    {
        protected Hero Hero => _hero;

        private Hero _hero;
        private float _timeOfStateStart;

        public HeroState(Hero hero)
        {
            _hero = hero;
        }
        public virtual void Enter(float time)
        {
            _timeOfStateStart = time;
        }
        public virtual void Update(float dt)
        {

        }
        public virtual void Exit()
        {

        }
    }
}