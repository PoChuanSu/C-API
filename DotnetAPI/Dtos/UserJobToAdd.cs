namespace DotnetAPI.Dtos
{
    public partial class UserJobInfoDto
    {
        public string JobTitle { get; set; }
        public string Department { get; set; }

        public UserJobInfoDto()
        {
            if (JobTitle == null)
            {
                JobTitle = "";
            }

            if (Department == null)
            {
                Department = "";
            }
        }
    }
}