using Library_Management_Application;
using Moq;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_Testcases
{
    public class BookTestcases
    {
        [Fact]
        public void AddBook_Whencalled_returnsvalue()
        {
            var Addmock = new Mock<BookInterface>();
            Addmock.Setup(x => x.Add_Book()).Returns(1);
            var res = Addmock.Object.Add_Book();
            res.Should().Be(1);
        }
        [Fact]
        public void EditBook_Whencalled_returnsvalues()
        {
            var Editmock = new Mock<BookInterface>();
            Editmock.Setup(x => x.Edit_books()).Returns(1);
            var res = Editmock.Object.Edit_books();
            res.Should().Be(1);
        }
        
        
    }
}



