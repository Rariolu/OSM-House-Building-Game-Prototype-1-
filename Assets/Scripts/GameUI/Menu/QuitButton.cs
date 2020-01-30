using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class QuitButton : UIButton
{
    protected override void Start()
    {
        base.Start();
        Click += QuitButton_Click;
    }
    void QuitButton_Click(UIButton sender)
    {
        Util.Quit();
    }
}