namespace Domain.Desks.Enums;

public enum DeskErrorCode
{
    InvalidCode = 1,
    NotFound = 2,
    DeskIsBooked = 3,
    ReservationDaysLimitExceeded = 4,
    DeskIsNotBooked = 5
}