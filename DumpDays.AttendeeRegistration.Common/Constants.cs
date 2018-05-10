namespace DumpDays.AttendeeRegistration.Common
{
    public enum WorkStatuses
    {
        Pupil,
        Student,
        Employed,
        Unemployed,
        Retired
    }

    public enum AgeGroups
    {
        Todler,
        Child,
        Teenager,
        YoungAdult,
        Adult,
        OldAdult,
        Senior
    }

    public enum Roles
    {
        Moderator,
        Administrator
    }

    public enum ActionResult
    {
        Success,
        Failure
    }

    public static class Constants
    {
        public static string TimeSpanFormat = @"hh\:mm\:ss";
        public static string DateTimeFormat = "d.M.yyyy. HH:mm";
    }
}
