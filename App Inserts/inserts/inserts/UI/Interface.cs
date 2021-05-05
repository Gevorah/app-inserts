using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using System.Windows.Forms;

namespace inserts.UI
{
    public partial class Interface : Form
    {
        public Interface()
        {
            InitializeComponent();
            Console.WriteLine("Hola");
            Read("Employee");
            Console.WriteLine("PASA 1");
            Read("Department");
            Console.Write("PASA 2");
            Read("Project");
            Console.WriteLine("PASA 3");
            Read("WorksOn");
            Console.WriteLine("PASA 4");
            Write();
        }

        private const string OUTPUT = @"Data\INSERTS.sql";

        public void Read (string file)
        {
            try
            {
                string cd = Directory.GetCurrentDirectory();
                string extra = @"bin\Debug";
                string path = cd.Substring(0, cd.Length - extra.Length) + @"Data\" + file +".csv";
                Console.WriteLine(path);
                StreamReader sr = new StreamReader(path);
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] sl = line.Split(',');
                    Inserts(sl, file);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        
        }

        string insert = "";
        string update = "";
        string workson = "";
        public void Inserts(string [] info, string nameFile)
        {

            if (nameFile.Equals("Employee"))
            {
                insert +="INSERT INTO EMPLOYEE VALUES ('" +info[0]+ "','" + info [1] + "','" + info [2] + "','" + info [3] + "',TO_DATE('" + info[4] + "','dd/mm/yyyy'),'" + info[5] + "','" + info[6] +"'"+",''"+");\n";
                update += "UPDATE EMPLOYEE SET DEPTNO = '" + info[7] + "' WHERE EMPNO = '" + info[0] + "';\n";
            }
            else if (nameFile.Equals("Department"))
            {
                insert += "INSERT INTO DEPARTMENT VALUES ('" + info[0] + "','" + info[1] + "','');\n";
                update += "UPDATE DEPARTMENT SET EMPNO = '" + info[2] + "' WHERE DEPTNO = '" + info[0] + "';\n";
            }
            else if (nameFile.Equals("Project"))
            {
                insert += "INSERT INTO PROJECT VALUES ('" + info[0] + "','" + info[1] + "', '');\n";
                update += "UPDATE PROJECT SET DEPTNO = '" + info[2] + "' WHERE PROJNO = '" + info[0] + "';\n";
            }
            else if (nameFile.Equals("WorksOn"))
            {
                workson += "INSERT INTO WORKSON VALUES ('" + info[0] + "','" + info[1] +"',TO_DATE('" + info[2] + "','mm/dd/yyyy'),'" + info[3] + "');\n";
            }
            

        }

        public void Write()
        {
            try
            {
                string cd = Directory.GetCurrentDirectory();
                string extra = @"bin\Debug";
                string path = cd.Substring(0, cd.Length - extra.Length) + OUTPUT;
                Console.WriteLine(path);
                StreamWriter sw = new StreamWriter(path);
                sw.Write(insert);
                sw.Write(update);
                sw.Write(workson);
                sw.Flush();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}
