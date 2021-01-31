using System;
using System.Collections.Generic;
using System.Text;

namespace GlitchGame.GameMain.Memory
{
    public interface IDataBlock
    {
        int Address { get; }
        int BitWidth { get; }
    }
}
