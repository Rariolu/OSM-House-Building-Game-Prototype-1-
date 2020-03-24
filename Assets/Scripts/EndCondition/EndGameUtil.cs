using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class EndGameUtil //: NullableInstanceClassSingleton<EndGameUtil>
{
    EXIT_STATE state;
    public EXIT_STATE ExitState
    {
        get
        {
            return state;
        }
    }
    EndGameUtil(EXIT_STATE exitState)
    {
        state = exitState;
    }
    public static void SetExitState(EXIT_STATE exitState)
    {
        SingletonUtil.SetInstance(new EndGameUtil(exitState));
    }
}