using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using System.Security.Cryptography;

namespace Library_Management_Application
{
    internal class Book : BookInterface
    {
        SqlConnection con = new SqlConnection("server=IN-8HRQ8S3; database=LibraryManagement; Integrated Security = True");
        public int Add_Book()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter("Select * from Books", con);
            adp.Fill(ds);
            string Book_Name = AnsiConsole.Ask<string>("[purple]Enter Book_Name[/]");
            string Author_Name = AnsiConsole.Ask<string>("[purple]Enter Author_Name[/]");
            string Publication_Name = AnsiConsole.Ask<string>("[purple]Enter Publication_Name[/]");
            int quantity = AnsiConsole.Ask<int>("[purple]Enter Book Quantity : [/]");
            var row = ds.Tables[0].NewRow();
            
            row["Book_Name"] = Book_Name;
            row["Author_Name"] = Author_Name;
            row["Publication_Name"] = Publication_Name;
            row["Quantity"] = quantity;
            ds.Tables[0].Rows.Add(row);
            SqlCommandBuilder builder1 = new SqlCommandBuilder(adp);
            int res=adp.Update(ds);
            AnsiConsole.Write(new Markup("[deepskyblue2]Book is successfully added[/]"));
            return res;


        }
        public int Edit_books()
        {
            int Id = AnsiConsole.Ask<int>("[lightsteelblue]Enter Id: [/]");
            if (Id < 0)
            {
                AnsiConsole.MarkupLine("[Red]Id should not be negative values[/]");
                return 0;

            }
            SqlDataAdapter adp = new SqlDataAdapter($"Select * from Books where Book_Id={Id}", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            string Book_Name = AnsiConsole.Ask<string>("[purple]Enter Book_Name[/]");
            string Author_Name = AnsiConsole.Ask<string>("[purple]Enter Author_Name[/]");
            string Publication_Name = AnsiConsole.Ask<string>("[purple]Enter Publication_Name[/]");
            int quantity = AnsiConsole.Ask<int>("[purple]Enter Book Quantity : [/]");

            ds.Tables[0].Rows[0]["Book_Name"] = Book_Name;
            ds.Tables[0].Rows[0]["Author_Name"] = Author_Name;
            ds.Tables[0].Rows[0]["Publication_Name"] = Publication_Name;
            ds.Tables[0].Rows[0]["Quantity"] = quantity;
            SqlCommandBuilder builder1 = new SqlCommandBuilder(adp);
            int res=adp.Update(ds);
            AnsiConsole.Write(new Markup("[deepskyblue2]Book is successfully Updated[/]"));
            return res;

        }
        public int Delete_Books()
        {

            int Id = AnsiConsole.Ask<int>("[lightsteelblue]Enter Id: [/]");
            if (Id < 0)
            {
                AnsiConsole.MarkupLine("[Red]Id should not be negative values[/]");
                return 0;

            }
            SqlDataAdapter adp = new SqlDataAdapter($"Select * from Books where Book_Id = {Id}", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            ds.Tables[0].Rows[0].Delete();
            SqlCommandBuilder builder = new SqlCommandBuilder(adp);
            int res=adp.Update(ds);
            AnsiConsole.Write(new Markup("[tan] Book is Successfully deleted [/]"));
            return res;
        }


        public void ViewBookById()
        {

            int Id = AnsiConsole.Ask<int>("[lightsteelblue]Enter Book_Id: [/]");
            SqlDataAdapter adp = new SqlDataAdapter($"Select * from Books where Book_Id = {Id}", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            Table table = new Table();

            table.AddColumn("Book_Name");
            table.AddColumn("Author_Name");
            table.AddColumn("Publication_Name");
            table.AddColumn("Available");


            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                table.AddRow(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString(), ds.Tables[0].Rows[i][3].ToString());
            }
            AnsiConsole.Write(table);
            

        }
        public void ViewAllBooks()
        {
            
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter("Select * from Books", con);
            adp.Fill(ds);
            Table table = new Table();
            table.AddColumn("Book_Id");
            table.AddColumn("Book_Name");
            table.AddColumn("Author_Name");
            table.AddColumn("Publication_Name");
            table.AddColumn("Quantity");
           

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                table.AddRow(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString(), ds.Tables[0].Rows[i][3].ToString(), ds.Tables[0].Rows[i][4].ToString());

            }
            AnsiConsole.Write(table);
            


        }
        public void Book_Issue_Status()
        { 
            
            //Checking if student taken book or not

            int studentId = AnsiConsole.Ask<int>("[lightsteelblue]Enter Student_Id: [/]");
            SqlDataAdapter adp = new SqlDataAdapter($"Select * from Students where Student_Id = {studentId}", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            if (ds.Tables[0].Rows.Count == 0)
            {
                AnsiConsole.MarkupLine("[lightsteelblue] Student doesnot Exists [/]");
                return;
            }

            SqlDataAdapter adp1 = new SqlDataAdapter($"Select * from Issue_Book where Student_Id = {studentId}", con);
            DataSet ds1 = new DataSet();
            adp1.Fill(ds1);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                AnsiConsole.MarkupLine("[Red] Student already Taken Book [/]");
                return;
            }
            //Checking book available or not
            int book_Id = AnsiConsole.Ask<int>("[lightsteelblue]Enter Book_Id: [/]");
            SqlDataAdapter adp2 = new SqlDataAdapter($"Select * from Books where Book_Id = {book_Id}", con);
            DataSet ds2 = new DataSet();
            adp2.Fill(ds2);
            if (ds2.Tables[0].Rows.Count == 0)
            {
                AnsiConsole.MarkupLine("[lightsteelblue] Book doesnot Exits [/]");
                return;

            }
            int quantity = (int)ds2.Tables[0].Rows[0]["Quantity"];
            if (quantity == 0)
            {
                AnsiConsole.MarkupLine("[Red] Book is not available[/]");
                return;
            }
            ds2.Tables[0].Rows[0]["Quantity"] = quantity - 1;
            SqlCommandBuilder builder = new SqlCommandBuilder(adp2);
            adp2.Update(ds2);

            //Update Book issue table
            SqlDataAdapter adp3 = new SqlDataAdapter($"Select * from Issue_Book", con);
            DataSet ds3 = new DataSet();
            adp3.Fill(ds3);
            var row = ds3.Tables[0].NewRow();
            row["Student_Id"] = studentId;
            row["Book_Id"] = book_Id;
            ds3.Tables[0].Rows.Add(row);
            SqlCommandBuilder builder1 = new SqlCommandBuilder(adp3);
            adp3.Update(ds3);
            AnsiConsole.MarkupLine($"[Green]Book Issued to student with Student_Id = {studentId}[/]");

        }
        public void Return_Book()
        {   //Checking student is existing Book_Issue table or not
            int studentId = AnsiConsole.Ask<int>("[lightsteelblue]Enter Student_Id: [/]");
            SqlDataAdapter adp = new SqlDataAdapter($"Select * from Issue_Book where Student_Id = {studentId}", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            if (ds.Tables[0].Rows.Count == 0)
            {
                AnsiConsole.MarkupLine("[lightsteelblue] Student had not issued any Book [/]");
                return;
            }
            int bookId = (int)ds.Tables[0].Rows[0]["Book_Id"];
            ds.Tables[0].Rows[0].Delete();
            SqlCommandBuilder builder = new SqlCommandBuilder(adp);
            adp.Update(ds);




            //Incrementing the quantity after returning the book       
            SqlDataAdapter adp2 = new SqlDataAdapter($"Select * from Books where Book_Id = {bookId}", con);
            DataSet ds2 = new DataSet();
            adp2.Fill(ds2);
            int quantity = (int)ds2.Tables[0].Rows[0]["Quantity"];
            ds2.Tables[0].Rows[0]["Quantity"] = quantity + 1;
            SqlCommandBuilder builder1 = new SqlCommandBuilder(adp2);
            adp2.Update(ds2);

            AnsiConsole.MarkupLine("[Green]Book Returned[/]");

        }
    }
}

