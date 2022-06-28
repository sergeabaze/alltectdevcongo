using System;

namespace AllTech.FrameWork.Models
{
    public class DateTimeData
    {
        public DateTimeData()
        { }

        public DateTimeData(DateTime newTime, TimeSpan newDuration, string newTitle, string newDetails)
        {
            Time = newTime;
            Duration = newDuration;
            Title = newTitle;
            Details = newDetails;
        }

        public DateTime Time { get; set; }
        public TimeSpan Duration { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
    }
}
