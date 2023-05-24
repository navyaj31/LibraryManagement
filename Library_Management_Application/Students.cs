using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_Application
{
    internal class Students
    {
        SqlConnection con = new SqlConnection("server=IN-8HRQ8S3; database=LibraryManagement; Integrated Security = True");

        public int Add_StudentDetails()
        {

            SqlDataAdapter adp = new SqlDataAdapter("Select * from Students", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);
           
            
            string Student_Name = AnsiConsole.Ask<string>("[purple]Enter Student_Name:[/]");
            
            var row = ds.Tables[0].NewRow();
            //row["Student_Id"] = Student_Id;
            row["Student_Name"] = Student_Name;
            //row["Book_Issued"] = Book_Issued;
            ds.Tables[0].Rows.Add(row);
            SqlCommandBuilder builder1 = new SqlCommandBuilder(adp);
            int res=adp.Update(ds);
            AnsiConsole.Write(new Markup("[deepskyblue2]StudentDetails is successfully added[/]"));
            return res;
        }
        public int Edit_StudentDetails()
        {

            DataSet ds = new DataSet();
            int Id = AnsiConsole.Ask<int>("[lightsteelblue]Enter Id: [/]");
            if (Id < 0)
            {
                AnsiConsole.MarkupLine("[Red]Id should not be negative values[/]");
                return 0;
            }
            SqlDataAdapter adp = new SqlDataAdapter($"Select * from Students where Student_Id={Id} ", con);
            adp.Fill(ds);

            string Student_Name = AnsiConsole.Ask<string>("[purple]Enter Student_Name[/]");
            //ds.Tables[0].Rows[0]["Student_Id"] = Student_Id;
            ds.Tables[0].Rows[0]["Student_Name"] = Student_Name;
            //ds.Tables[0].Rows[0]["Book_Issued"] = Book_Issued;
            SqlCommandBuilder builder1 = new SqlCommandBuilder(adp);
            int res= adp.Update(ds);
            AnsiConsole.Write(new Markup("[deepskyblue2]StudentDetails is successfully Updated[/]"));
            return res;
        }
        public int  Delete_StudentDetails()
        {
            DataSet ds = new DataSet();
            int Id = AnsiConsole.Ask<int>("[lightsteelblue]Enter Id: [/]");
            if (Id < 0)
            {
                AnsiConsole.MarkupLine("[Red]Id should not be negative values[/]");
                return 0;

            }
            SqlDataAdapter adp = new SqlDataAdapter($"Select *from Students where Student_Id = {Id}", con);
            adp.Fill(ds);
            ds.Tables[0].Rows[0].Delete();
            SqlCommandBuilder builder = new SqlCommandBuilder(adp);
            int res = adp.Update(ds);
            AnsiConsole.Write(new Markup("[tan] StudentDetails is Successfully deleted [/]"));
            return res;

        }
        public void View_Student_By_Id()
        {
            int Id = AnsiConsole.Ask<int>("[lightsteelblue]Enter Student Id: [/]");
            SqlDataAdapter adp = new SqlDataAdapter($"Select * from Students where Student_Id = {Id}", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            Table table = new Table();
            table.AddColumn("Student_Id");
            table.AddColumn("Student_Name");
            

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                table.AddRow(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString());
            }
            AnsiConsole.Write(table);
        }
        public void View_All_Students()
        {
            SqlDataAdapter adp = new SqlDataAdapter($"Select * from Students", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            Table table = new Table();
            table.AddColumn("Student_Id");
            table.AddColumn("Student_Name");
            

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                table.AddRow(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString());
            }
            AnsiConsole.Write(table);

        }
        public void StudentHavingBooks()
        {
            SqlDataAdapter adp = new SqlDataAdapter("Select Students.Student_Id, Students.Student_Name, Books.Book_Id, Books.Book_Name from Issue_Book join Students on Issue_Book.Student_Id = Students.Student_Id join Books on Issue_Book.Book_Id = Books.Book_Id", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            Table table = new Table();
            table.AddColumn("Student_Id");
            table.AddColumn("Student Name");
            table.AddColumn("Book_Id");
            table.AddColumn("Book_Name");

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                table.AddRow(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString(), ds.Tables[0].Rows[i][3].ToString());

            }
            AnsiConsole.Write(table);
            AnsiConsole.MarkupLine($"[Green]Students having Books =[/] [Purple] {ds.Tables[0].Rows.Count}[/]");
        }
      
      

    }
}


