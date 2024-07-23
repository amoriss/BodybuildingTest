using System.Data;
using BodybuildingTest.Models.Exercises;
using Dapper;

namespace BodybuildingTest.Models.UserInformation;

public class UserInfoRepository : IUserInfoRepository
{
    private readonly IDbConnection _conn;
    public UserInfoRepository(IDbConnection conn)
    {
        _conn = conn;
    }
    public IEnumerable<UserInfo> GetAllUserInfo()
    {
        return _conn.Query<UserInfo>("SELECT * FROM USERINFO;");
    }
    public UserInfo GetUserInfo(int id)
    {
        return _conn.QuerySingle<UserInfo>("SELECT * FROM USERINFO WHERE USERID = @id", new { id = id });
    }
    public void UpdateUserInfo(UserInfo userInfo)
    {
        _conn.Execute("UPDATE userInfo SET username = @username, email = @email," +
            "password = @password, create_time = @create_time, FirstName = @FirstName," +
            "LastName = @LastName, Most_Recent_Workout = @Most_Recent_Workout WHERE UserID = @id",
         new
         {
             username = userInfo.Username,
             email = userInfo.Email,
             id = userInfo.UserID,
             password = userInfo.Password,
             create_time = userInfo.Create_Time,
             firstName = userInfo.FirstName,
             lastName = userInfo.LastName,
             most_recent_workout = userInfo.Most_Recent_Workout
         });
    }
    public void AddUser(UserInfo newUser)
    {
        _conn.Execute("INSERT INTO userinfo (FIRSTNAME, LASTNAME, USERNAME, EMAIL, PASSWORD) VALUES" +
            "(@FirstName, @LastName, @Username, @Email, @Password);",
            new
            {
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Username = newUser.Username,
                Email = newUser.Email,
                Password = newUser.Password,
            });
    }
    public IEnumerable<UserInfo> GetCurrentUsers()
    {
        return _conn.Query<UserInfo>("SELECT * FROM userinfo;");
    }
    public IEnumerable<Exercise> GetExercises()
    {
        return _conn.Query<Exercise>("SELECT * FROM exercises;");
    }

    public UserInfo AssignNewUser()
    {
        var exerciseList = GetExercises();
        var userInfo = new UserInfo();
        userInfo.AllWorkouts = exerciseList;
        return userInfo;
    }
}
