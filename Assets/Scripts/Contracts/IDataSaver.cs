using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shooter.Models;

namespace AssemblyCSharp.Assets.Scripts.Contracts
{
    public interface IDataSaver
    {
        void Save( Player player );
        Player Load();
    }
}
