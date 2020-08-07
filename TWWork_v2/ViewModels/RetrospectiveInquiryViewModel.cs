using TWWork_v2.Enums;

namespace TWWork_v2.ViewModels
{
    public class RetrospectiveInquiryViewModel : BaseViewModel
    {
        public DeviceEnum DeviceName { get; set; }
        public PushOrPopEnum PushOrPop { get; set; }
    }
}