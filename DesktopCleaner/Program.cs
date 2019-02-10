using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCleaner
{
    class Program
    {
        static void Main(string[] args)
        {
            FileMover fileMover =new FileMover();
            fileMover.Move();
        }
    }
}
