using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class HelperSpriteRenderer
{
    public static string CleanName(string name) => name.Replace("(Instance)", "").Trim();
}
