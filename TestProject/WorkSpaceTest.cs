/*using HiveAPI.Controllers;
using HiveAPI;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class WorkSpaceTests
    {

        private readonly Mock<WorkSpacesController> _workSpaceController;
        private readonly Mock<APIDbContext> _context;

        public WorkSpaceTests()
        {
            _workSpaceController = new Mock<WorkSpacesController>();
            _context = new Mock<APIDbContext>();

        }
        [Fact]
        public void GetUserReturnsUserWhenUserExists()
        {
            // Arrange
            var mockController = new Mock<WorkSpacesController>();
            //var existingUser = new User { UserId = 1, name = "John", email = "john@example.com" };
            //mockController.Setup(c => c.Create(existingUser));

            var result = mockController.Setup(c => c.GetWorkSpaces());

            //var controller = new UserController(null, mockController.Object);

            // Act


            // Assert
            Assert.NotEmpty(result);
        }

    }
}*/