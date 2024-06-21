

namespace ForthAssignment.Core.Aplication.Core
{
	public class Result<TData> where TData : class
	{
        public Result()
        {
            IsSuccess = true;
        }
        public string? Message { get; set; }
		public bool IsSuccess {  get; set; }
		public TData? Data { get; set; }
	}
}
