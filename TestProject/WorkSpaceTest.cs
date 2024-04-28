using HiveAPI.Controllers;
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

        private readonly Mock<IWorksService> workSpaceServiceMock;

        public WorkSpaceTests()
        {
            workSpaceServiceMock = new Mock<IWorksService>();

        }

        

        [Fact]
        
        public async  System.Threading.Tasks.Task GetWorkSpaceTest() { 
            var mockWorkSpaces = GetFakeWorkSpaces();

            workSpaceServiceMock.Setup(x => x.GetWorkSpaces()).Returns(mockWorkSpaces);
            var workSpaceController = new WorkSpacesController(workSpaceServiceMock.Object);

            var result = await workSpaceController.GetWorkSpaces();

            Assert.NotNull(result);
        }

       
        
        [Fact]

        public async System.Threading.Tasks.Task GetWorkSpaceByIdTest(){
            int mockId = 3;
            workSpaceServiceMock.Setup(x => x.GetWorkSpaceById(mockId)).Returns((System.Threading.Tasks.Task.FromResult((WorkSpace)null)));

            var workSpaceController = new WorkSpacesController(workSpaceServiceMock.Object);

            var result = await workSpaceController.GetWorkSpaceById(mockId);

            Assert.NotNull(result);
        }

        public static System.Threading.Tasks.Task<List<WorkSpace>> GetFakeWorkSpaces()
        {

            var workspaces = new List<WorkSpace>();
            workspaces.Add(new WorkSpace());
            var task = System.Threading.Tasks.Task.FromResult(workspaces);


            //        var workSpaces = new System.Threading.Tasks.Task<List<WorkSpace>>()
            //      {
            //        new()
            //      {
            //        WorkspaceName = "Test",
            //      WorkspaceDescription = "Test"
            // }
            //};
            return task;
        }

       

    }
}