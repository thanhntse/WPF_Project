using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class BookingDetail
{
    public int BookingReservationId { get; set; }

    public int RoomId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public decimal? ActualPrice { get; set; }

    public virtual BookingReservation BookingReservation { get; set; } = null!;

    public virtual RoomInformation Room { get; set; } = null!;
}
