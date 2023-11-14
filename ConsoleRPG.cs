using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using static ConsoleRPG.ConsoleRPG;


namespace ConsoleRPG
{
    internal class ConsoleRPG
    {
        public static Player player;
        public static Item[] items;
        public static Monster[] companys;


        public static void Main(string[] args)
        {
            GameManager.DataSetting();
            GameManager.DisplayHome();
        }

    }
}


   



