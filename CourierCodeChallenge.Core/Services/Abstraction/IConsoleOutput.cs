using CourierCodeChallenge.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierCodeChallenge.Core.Services.Abstraction
{
    public interface IConsoleOutput
    {
        void WriteLine(List<Package> packages);
    }
}
