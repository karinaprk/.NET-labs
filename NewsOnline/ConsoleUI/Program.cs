using BusinessLogic.Services;
using ConsoleUI;
using ConsoleUI.Controllers;
using DataAccess;

using var context = new NewsContext();

var unitOfWork = new UnitOfWork(context);
    
var userService = new UserService(unitOfWork);
var tagService = new TagService(unitOfWork);
var newsService = new NewsService(unitOfWork);
    
var userController = new UserController(userService);
var tagController = new TagController(tagService);
var newsController = new NewsController(newsService);
    
var app = new Application(userController, newsController, tagController);
    
app.Run();