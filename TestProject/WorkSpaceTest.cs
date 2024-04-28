/*using HiveAPI.Controllers;
using HiveAPI;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiveAPI.Services.WorkSpaceServices;
using HiveAPI.Models;

namespace TestProject
{
    public class WorkSpaceTests
    {

        private readonly Mock<WorksService> workSpaceServiceMock;

        public WorkSpaceTests()
        {
            workSpaceServiceMock = new Mock<WorksService>();

        }

        [Fact]
        public async  System.Threading.Tasks.Task GetWorkSpaceTest() { 
            var mockWorkSpaces = GetFakeWorkSpaces();

            workSpaceServiceMock.Setup(x => x.GetWorkSpaces()).Returns(mockWorkSpaces);
            var workSpaceController = new WorkSpacesController(workSpaceServiceMock.Object);

            var result = await workSpaceController.GetWorkSpaces();

            Assert.NotNull(result);
            
        }

        public static List<WorkSpace> GetFakeWorkSpaces()
        {
            var workSpaces = new List<WorkSpace>()
            {
                new()
                {
                    WorkspaceName = "Test",
                    WorkspaceDescription = "Test"
                }
            };
            return workSpaces;
        }

       

    }
}*/