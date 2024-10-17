using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserJobController : ControllerBase
{

    DataContextDapper _dapper;

    public UserJobController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
    }

    [HttpGet("GetUserJob")]
    public IEnumerable<UserJobInfo> GetUsersJob()
    {
        string sql = @"
            SELECT [UserId],
                [JobTitle],
                [Department]
            FROM TutorialAppSchema.UserJobInfo";
        IEnumerable<UserJobInfo> usersJob = _dapper.LoadData<UserJobInfo>(sql);
        return usersJob;
    }


    [HttpGet("GetSingleUsersJob/{userId}")]
    public UserJobInfo GetSingleUsersJob(int userId)
    {
        string sql = @"
            SELECT [UserId],
                [JobTitle],
                [Department]
            FROM TutorialAppSchema.UserJobInfo WHERE UserId = " + userId.ToString();

        UserJobInfo userJobInfo = _dapper.LoadDataSingle<UserJobInfo>(sql);
        return userJobInfo;
    }
    [HttpPut("EditUserJobInfo")]
    public IActionResult EditUser(UserJobInfo userJob)
    {
        string sql = @"
            UPDATE TutorialAppSchema.UserJobInfo
                SET  [JobTitle] = '" + userJob.JobTitle +
                "',[Department] = '" + userJob.Department +
            "' WHERE UserId = " + userJob.UserId;

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to Update UserJob Info");
    }

    [HttpPost("AddUserJobInfo")]
    public IActionResult AddUser(UserJobInfoDto userJob)
    {
        string sql = @"
        INSERT INTO TutorialAppSchema.UserJobInfo(
            [JobTitle],
            [Department]
            ) VALUES (" +
                "'" + userJob.JobTitle +
                "', '" + userJob.Department +
            "')";

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to Add User Job");
    }

    [HttpDelete("DeleteUserJob/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        string sql = @"
            DELETE FROM TutorialAppSchema.UserJobInfo
                WHERE UserId = " + userId.ToString();

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to Delete User Job");
    }
}

