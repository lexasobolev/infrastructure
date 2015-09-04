using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    public abstract class Phase
    { }

    public abstract class Preparing : Phase
    { }

    public abstract class Unhandled : Phase
    { }

    public abstract class Succeeded : Phase
    { }

    public abstract class Failed : Phase
    { }
}
