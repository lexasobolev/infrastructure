﻿using Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class AppStartup : ILoggable<Before>, ILoggable<Failed>
    {
    }
}
