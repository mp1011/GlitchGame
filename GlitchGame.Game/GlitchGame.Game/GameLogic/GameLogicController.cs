using GlitchGame.GameMain.Memory;

namespace GlitchGame.GameMain.GameLogic
{
    public class GameLogicController
    {
        private readonly SystemMemory _systemMemory;
        private BouncingBall[] _bouncingBalls;

        public GameLogicController(SystemMemory systemMemory)
        {
            _systemMemory = systemMemory;
            _bouncingBalls = new BouncingBall[]
            {
                new BouncingBall(),
                new BouncingBall(),
                new BouncingBall(),
                new BouncingBall()
            };

            _bouncingBalls[0].SpriteIndex = 0;
            _bouncingBalls[1].SpriteIndex = 1;
            _bouncingBalls[2].SpriteIndex = 2;
            _bouncingBalls[3].SpriteIndex = 3;

            _bouncingBalls[0].XSpeed = 132;
            _bouncingBalls[0].YSpeed = 132;

            _bouncingBalls[1].XSpeed = 129;
            _bouncingBalls[1].YSpeed = 130;

            _bouncingBalls[2].XSpeed = 140;
            _bouncingBalls[2].YSpeed = 120;

            _bouncingBalls[3].XSpeed = 130;
            _bouncingBalls[3].YSpeed = 125;
        }

        public void UpdateFrame()
        {
            for (int i = 0; i < _bouncingBalls.Length; i++)
                _bouncingBalls[i].Update(_systemMemory);
        }
    }
}
