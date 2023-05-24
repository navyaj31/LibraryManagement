using Spectre.Console;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_Application
{
    internal class Login : LoginInterface
    {
        public bool Login_Page()
        {
            AnsiConsole.MarkupLine("[Purple]---------Login----------[/]");
            Console.WriteLine();
            SqlConnection con = new SqlConnection("server=IN-8HRQ8S3; database=LibraryManagement; Integrated Security = True");
            DataSet ds = new DataSet();
            string User_Name = AnsiConsole.Ask<string>("[Green]Enter User_Name:[/]");
            string Password = AnsiConsole.Ask<string>("[Green]Enter Password: [/]");
            SqlDataAdapter adp = new SqlDataAdapter($"SELECT * FROM Login WHERE User_Name = '{User_Name}' and Password = '{Password}'", con);
            
            adp.Fill(ds);
            int Count= (int)ds.Tables[0].Rows.Count;
            if(Count > 0)
            {
                AnsiConsole.MarkupLine("[Green]Login successful [/]");
                SqlCommandBuilder builder1 = new SqlCommandBuilder(adp);
                adp.Update(ds);
                return true;
            }
            else
            {
                AnsiConsole.MarkupLine("[Red] Login Failed [/]");
                return false;

            }
            




        }


    }
}
