namespace ElectronicMedia.Core
{
    public class ResultDto<T>
    {
        public T Data { get; set; }
        private ApiResultStatus status;
        public ApiResultStatus Status
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(errorMessage)){
                    return ApiResultStatus.Failed;
                }
                return status;
            }
            set { status = value; }
        }
        private string errorMessage;
        public bool IsSuccessful { get { return string.IsNullOrWhiteSpace(errorMessage); } }
        public string ErrorMessage
        {
            get
            {
                return errorMessage;
            }
            set
            {
                errorMessage = value;
            }
        }

    }
}