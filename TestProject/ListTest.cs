using HiveAPI.Controllers;
using HiveAPI.Models;
using HiveAPI.Services.ListServices;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class ListTest
    {
        readonly Mock<IListService> listServiceMock;

        public ListTest()
        {
            listServiceMock = new Mock<IListService>();
        }


        [Fact]

        public async System.Threading.Tasks.Task GetListTest()
        {
            var mockLists = GetFakeLists();

            listServiceMock.Setup(x => x.GetLists()).Returns(mockLists);
            var listController = new ListsController(listServiceMock.Object);

            var result = await listController.GetLists();

            Assert.NotNull(result);
        }


        public static System.Threading.Tasks.Task<List<HiveAPI.Models.List>> GetFakeLists()
        {

            var list = new List<HiveAPI.Models.List>() {
                new List
                {
                    ListName = "Test",
                    WorkSpaceId = 1,
                }
             };
            
            var task = System.Threading.Tasks.Task.FromResult(list);


            
            return task;
        }
    }
}
