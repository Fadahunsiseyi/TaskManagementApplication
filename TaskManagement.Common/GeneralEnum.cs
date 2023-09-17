namespace TaskManagement.Common.Enums;


public enum TasksPriority
{
    Low=1,
    Medium,
    High
}


public enum TasksStatus
{
    Pending=1,
    InProgress,
    Completed
}


public enum NotificationsType
{
    DueDateReminder,
    StatusUpdate
}
