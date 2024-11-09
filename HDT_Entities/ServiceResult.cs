namespace HDT_22810201_Entities
{
	public class ServiceResult
	{
		public bool IsSuccess { get; }
		public string Message { get; }
		public ServiceResult(bool bl, string msg)
		{
			IsSuccess = bl;
			Message = msg;
		}
	}
}