using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerApp
{
    // Перечисление состояний конечного автомата для синтаксического анализа
    internal enum State
    {
        START,
        SPACE,
        IDFUNC,
        IDFUNCREM,
        EMPTY,
        PARAMETERS,
        SPACE2,
        IDPARAM,
        IDPARAMREM,
        END,
        ERROR
    }
}
