using Microsoft.AspNetCore.Mvc;
using Moq;
using MyTask_Management_System.Controllers;
using MyTask_Management_System.Core.Model;
using MyTask_Management_System.Core;
using MyTask_Management_System.Core.Repository;
using MyTask_Management_System.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class TaskControllerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly TaskController _controller;

    public TaskControllerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _controller = new TaskController(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task GetAllTasks_WithTasks_ReturnsOkResult()
    {
        // Arrange
        var tasks = new List<TaskModel>
        {
            new TaskModel { Id = 1, Title = "Task 1", Description = "Description 1" },
            new TaskModel { Id = 2, Title = "Task 2", Description = "Description 2" }
        };
        _unitOfWorkMock.Setup(x => x.TaskModels.GellAllAsync()).ReturnsAsync(tasks);

        // Act
        var result = await _controller.GetAllTasks();

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result);
        var returnedTasks = Assert.IsType<List<TaskModel>>(actionResult.Value);
        Assert.Equal(2, returnedTasks.Count);
    }

    [Fact]
    public async Task GetTaskById_WithValidId_ReturnsOkResult()
    {
        // Arrange
        var task = new TaskModel { Id = 1, Title = "Task 1", Description = "Description 1" };
        _unitOfWorkMock.Setup(x => x.TaskModels.GetByIDAsync(1)).ReturnsAsync(task);

        // Act
        var result = await _controller.GetTaskById(1);

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result);
        var returnedTask = Assert.IsType<TaskModel>(actionResult.Value);
        Assert.Equal("Task 1", returnedTask.Title);
    }

    [Fact]
    public async Task DeleteTask_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        _unitOfWorkMock.Setup(x => x.TaskModels.GetByIDAsync(1)).ReturnsAsync((TaskModel)null);

        // Act
        var result = await _controller.DeleteTask(1);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }
}
