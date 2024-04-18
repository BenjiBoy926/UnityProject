using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbstractHumanoidToy
{
    public class HeroState
    {
        protected Hero Hero => _hero;
        protected float TimeSinceStateStart => Time.time - _timeOfStateStart;

        private Hero _hero;
        private float _timeOfStateStart;

        public HeroState(Hero hero)
        {
            _hero = hero;
        }
        public virtual void Enter()
        {
            _timeOfStateStart = Time.time;
        }
        public virtual void Update(float dt)
        {

        }
        public virtual void Exit()
        {

        }
    }
}