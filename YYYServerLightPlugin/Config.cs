using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YYYServerLightPlugin
{
    public class Config
    {
        [Description("是否开启Debug")]
        public bool Debug { get; set; } = true;
    }
}
