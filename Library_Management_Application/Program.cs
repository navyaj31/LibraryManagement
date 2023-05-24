using Library_Management_Application;
using Spectre.Console;
using System.Data;
using System.Data.SqlClient;
namespace Library_Management_Application
{
        internal class Program
        {
            static void Main(string[] args)
            {
                AnsiConsole.Write(new FigletText("Welcome to Library Management").Centered().Color(Color.PaleVioletRed1));
                
                Login login = new Login();
                Students students = new Students();
                Book book = new Book();
                bool Logged_In = login.Login_Page();
                while (!Logged_In)
                {
                Logged_In = login.Login_Page();

                }
                Console.WriteLine();
                

            
                while (Logged_In)
                {
                    Console.WriteLine();
                    var choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
                        .Title("[hotpink3_1] Select your choice : [/]")
                        .AddChoices(new[]
                        {
                        "Add_Book",
                        "Edit_Book",
                        "Delete_Book",
                        "ViewBookById",
                        "ViewAllBooks",
                        "Add_StudentDetails",
                        "Edit_StudentDetails",
                        "Delete_StudentDetails",
                        "ViewStudentById",
                        "ViewAllStudents",
                        "Book_Issued",
                        "Book_Return",
                        "StudentsHavingBooks"
                        }));
                    switch (choice)
                    {
                        
                        case "Add_Book":
                            {
                               book.Add_Book();
                                break;
                            
                            }
                        case "Edit_Book":
                            {
                                book.Edit_books();
                                break;
                            }
                        case "Delete_Book":
                            {
                                book.Delete_Books();
                                break;
                            }
                        case "ViewBookById":
                            {
                                book.ViewBookById();
                                break;

                            }
                        case "ViewAllBooks":
                            {
                                book.ViewAllBooks();
                                break;

                            }
                        case "Add_StudentDetails":
                            {
                                students.Add_StudentDetails();
                                break;

                            }
                        case "Edit_StudentDetails":
                            {
                                students.Edit_StudentDetails();
                                break;
                            }
                        case "Delete_StudentDetails":
                            {
                                students.Delete_StudentDetails();
                                break;
                            }
                        case "ViewStudentById":
                            {
                                students.View_Student_By_Id();
                                break;
                            }
                        case "ViewAllStudents":
                            {
                                students.View_All_Students();
                                break;
                            }
                        case "Book_Issued":
                            {
                                book.Book_Issue_Status();
                                break;
                            }
                        case "Book_Return":
                            {
                                book.Return_Book();
                                break;
                            }
                        
                        case "StudentsHavingBooks":
                            {
                                students.StudentHavingBooks();
                                break;
                            }

                    }
                }


            }
        }
    }



