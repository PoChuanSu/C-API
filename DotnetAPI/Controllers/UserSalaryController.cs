using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserSalaryController : ControllerBase
{

    DataContextDapper _dapper;

    public UserSalaryController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
    }

    [HttpGet("GetUserSalary")]
    public IEnumerable<UserSalary> GetUserSalary()
    {
        string sql = @"
            SELECT [UserId],
                [Salary]
            FROM TutorialAppSchema.UserSalary";
        IEnumerable<UserSalary> userSalary = _dapper.LoadData<UserSalary>(sql);
        return userSalary;
    }


    [HttpGet("GetUserSalary/{userId}")]
    public UserSalary GetSingleUserSalary(int userId)
    {
        string sql = @"
            SELECT [UserId],
                [Salary]
            FROM TutorialAppSchema.UserSalary WHERE UserId = " + userId.ToString();

        UserSalary userSalary = _dapper.LoadDataSingle<UserSalary>(sql);
        return userSalary;
    }
    [HttpPut("EditUserSalary")]
    public IActionResult EditUserSalary(UserSalary userSalary)
    {
        string sql = @"
            UPDATE TutorialAppSchema.UserSalary
                SET  [Salary] = '" + userSalary.Salary +
            "' WHERE UserId = " + userSalary.UserId;

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to Update User");
    }

    [HttpPost("AddUserSalary")]
    public IActionResult AddUserSalary(UserSalaryDto userSalary)
    {
        string sql = @"
        INSERT INTO TutorialAppSchema.UserSalary(
            [Salary]
            ) VALUES (" +
                "'" + userSalary.Salary +
            "')";

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to Add User");
    }

    [HttpDelete("DeleteUserSalary/{userId}")]
    public IActionResult DeleteUserSalary(int userId)
    {
        string sql = @"
            DELETE FROM TutorialAppSchema.UserSalary
                WHERE UserId = " + userId.ToString();

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to Delete User");
    }
}

