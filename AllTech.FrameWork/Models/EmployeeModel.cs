using System.Collections.ObjectModel;

namespace AllTech.FrameWork.Models
{
    public class EmployeeModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ObservableCollection<EmployeeModel> Subordinates { get; set; }
    }
}
