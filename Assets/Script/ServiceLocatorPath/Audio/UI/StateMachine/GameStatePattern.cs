using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace GameAudio
{
    public class GameStatePattern : Singleton<GameStatePattern>, IGameContext
    {
        private IGameState currentState = new MenuState();

        public void Gameplay() => currentState.Gameplay(this);
        public void GameplayDanger() => currentState.GameplayDanger(this);
        public void Pause() => currentState.Pause(this);

        void IGameContext.SetState(IGameState newState)
        {
            currentState = newState;
        }
    }
}