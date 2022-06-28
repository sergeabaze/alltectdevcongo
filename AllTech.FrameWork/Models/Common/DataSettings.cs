namespace AllTech.FrameWork.Models
{
    public class DataSettings: ObservableModel
    {
        public DataSettings()
        {
            DataPoints = 100;
        }
        public int DataPoints { get; set; }

    }
}