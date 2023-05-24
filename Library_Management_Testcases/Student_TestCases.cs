using Library_Management_Application;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace Library_Management_Testcases
{
    public class Student_TestCases
    {

        [Fact]
        public void Add_StudentDetails_Whencalled_returnsvalue()
        {
            var Addmock = new Mock<StudentInterface>();
            Addmock.Setup(x => x.Add_StudentDetails()).Returns(1);
            var res = Addmock.Object.Add_StudentDetails();
            res.Should().Be(1);
        }
        [Fact]
        public void Add_StudentDetails_Whencalled_retunsvalue_Failed()
        {
            var Addmock = new Mock<StudentInterface>();
            Addmock.Setup(x => x.Add_StudentDetails()).Returns(0);
            var res = Addmock.Object.Add_StudentDetails();
            res.Should().Be(0);
        }
        [Fact]
        public void Edit_StudentDetails_Whencalled_returnsvalues()
        {
            var Editmock = new Mock<StudentInterface>();
            Editmock.Setup(x => x.Edit_StudentDetails()).Returns(1);
            var res = Editmock.Object.Edit_StudentDetails();
            res.Should().Be(1);
        }

    }
}
