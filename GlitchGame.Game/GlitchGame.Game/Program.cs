using System;

namespace GlitchGame.GameMain
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new GameEngine())
                game.Run();
        }
    }
}
