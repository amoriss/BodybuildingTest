using BodybuildingTest.Models.UserInformation;
using Microsoft.AspNetCore.Mvc;

namespace BodybuildingTest.Controllers;
public class UserInfoController : Controller
{
    private readonly IUserInfoRepository repo;
    public UserInfoController(IUserInfoRepository repo)
    {
        this.repo = repo;
    }
    public IActionResult Index()
    {
        var userInfo = repo.GetAllUserInfo();
        return View(userInfo);
    }
    public IActionResult ViewUserInfo(int id)
    {
        var userInfo = repo.GetUserInfo(id);
        return View(userInfo);
    }
    public IActionResult UpdateUserInfo(int id)
    {
        UserInfo userInfo = repo.GetUserInfo(id);
        if (userInfo == null)
        {
            return View("UserNotFound");
        }
        return View(userInfo);
    }
    //This method can update the user's username, email, password, and first and last names.
    public IActionResult UpdateUserInfoToDatabase(UserInfo userInfo)
    {
        repo.UpdateUserInfo(userInfo);

        return RedirectToAction("ViewUserInfo", new { id = userInfo.UserID });
    }

    public IActionResult InsertUser() 
    {
        var user = repo.AssignNewUser();
        return View(user); 
    }

    //Creating a New User
    public IActionResult NewUser(UserInfo newUser)
    {
        try
        {
            repo.AddUser(newUser);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return RedirectToAction("Index");
        }
    }
}
