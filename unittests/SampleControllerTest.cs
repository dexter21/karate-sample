using Xunit;
using api.Controllers;
using Microsoft.AspNetCore.Mvc;
using api.Items;
using System.Linq;

namespace unittests
{
    public class SampleControllerTest
    {
        private TasksController GetController()
        {
            var controller = new TasksController();
            controller.DeleteAll();
            return controller;
        }

        [Fact]
        public void Get_Empty()
        {
            var controller = GetController();
            var actual = controller.Get();
            Assert.Empty(actual);
        }
        
        [Fact]
        public void Add()
        {
            var name = new Item("test1");
            var controller = GetController();
            var resultAdd = controller.Add(name) as CreatedResult;
            Assert.NotNull(resultAdd);
            Assert.Equal(1, resultAdd.Value);

            var resultGet = controller.Get();
            Assert.Single(resultGet);
            Assert.Equal(name.Name, resultGet.FirstOrDefault()?.Name);
        }
        
        [Fact]
        public void Get_Many()
        {
            var names = new [] { "test1", "test2", "test3" };
            var items = names.Select(x => new Item(x)).ToArray();
            var controller = GetController();
            foreach (var item in items)
            {
                controller.Add(item);
            }
            var actual = controller.Get().ToArray();
            Assert.Equal(names.Length, actual.Length);
            for (int i = 0; i < names.Length; i++)
            {
                Assert.Equal(names[i], actual[i].Name);
            }
        }
        
        [Fact]
        public void Update_Valid()
        {
            var item = new Item("test1");
            var controller = GetController();
            var resultAdd = controller.Add(new Item("default name")) as CreatedResult;

            var createdId = (int)resultAdd.Value;
            var resultUpdate = controller.Update(createdId, item) as OkResult;
            Assert.NotNull(resultUpdate);
            
            var resultGet = controller.Get(createdId);
            Assert.Equal(item, resultGet.Value);
        }
        
        [Fact]
        public void Update_NotFound()
        {
            var item = new Item("test1");
            var controller = GetController();
            var resultAdd = controller.Add(item) as CreatedResult;
            Assert.NotNull(resultAdd);

            var resultUpdate = controller.Update(-1, item) as NotFoundResult;
            Assert.NotNull(resultUpdate);
        }
        
        [Fact]
        public void Get_One_Valid()
        {
            var item = new Item("test1");
            var controller = GetController();
            var resultAdd = controller.Add(item) as CreatedResult;

            var createdId = (int)resultAdd.Value;
            var resultGet = controller.Get(createdId);
            Assert.Equal(item.Name, resultGet.Value.Name);
        }
        
        [Fact]
        public void Get_One_NotFound()
        {
            var item = new Item("test1");
            var controller = GetController();
            var resultAdd = controller.Add(item) as CreatedResult;

            var resultGet = controller.Get(-1);
            Assert.NotNull(resultGet.Result as NotFoundResult);
        }
        
        [Fact]
        public void Delete_Valid()
        {
            var item = new Item("test1");
            var controller = GetController();
            var resultAdd = controller.Add(item) as CreatedResult;

            var createdId = (int)resultAdd.Value;
            var resultDelete = controller.Delete(createdId) as NoContentResult;
            Assert.NotNull(resultDelete);
            
            var resultGet = controller.Get(createdId);
            Assert.NotNull(resultGet.Result as NotFoundResult);
        }
        
        [Fact]
        public void Delete_NotFound()
        {
            var item = new Item("test1");
            var controller = GetController();
            controller.Add(item);

            var resultUpdate = controller.Delete(-1) as NoContentResult;
            Assert.NotNull(resultUpdate);
        }
    }
}
