namespace ElectronicMedia.Core
{
    public class PageRequestFilter
    {
        private IEnumerable<string> _value;
        public string ColumnName { get; set; }
        public bool IsNullValue { get; set; }
        public IEnumerable<string> Value
        {
            get { return this.IsNullValue ? new List<string>() : _value; }
            set
            {
                _value = value;
            }
        }
        public bool IncludeNullValue { get; set; } = false;
    }
}