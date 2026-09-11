using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMS.Framework.Extensions
{
    public class ActionStatus
    {
        #region Member
      
        public bool IsSuccess { get; set; } = false;
        public ResponseVM? Response { get; set; }
        public Exception? Exception { get; set; }
        public bool HasException => Exception != null;
        #endregion
        #region Constructor
        public ActionStatus(bool isSuccess, ResponseVM response)
        {
            IsSuccess = isSuccess;
        }
       
        public ActionStatus(ResponseVM error)
        {
            IsSuccess = false;
            Response = error;
        }
        
        public ActionStatus(string locationCode, Exception exception)
        {
            IsSuccess = false;
            Exception = exception;
            Response = new ResponseVM(locationCode);
        }
        
        public ActionStatus(ActionStatus actionStatus)
        {
            IsSuccess = actionStatus.IsSuccess;
            Exception = actionStatus.Exception;
            Response = actionStatus.Response;
        }
        #endregion
        #region Operators
       
        public static implicit operator bool(ActionStatus actionStatus)
        {
            return actionStatus.IsSuccess;
        }
       
        public static explicit operator ActionStatus(bool isSuccess)
        {
            return new ActionStatus(isSuccess, new ResponseVM("DEFAULT"));
        }
        #endregion
    }
    
    public class ActionStatus<T> : ActionStatus
    {
        #region Member
       
        public T? Result { get; set; }
        
        public int TotalCount { get; set; }
        #endregion
        #region Constructor
        
        public ActionStatus(bool isSuccess, T result) : base(isSuccess, new ResponseVM("DEFAULT"))
        {
            Result = result;
        }
       
        public ActionStatus(bool isSuccess, T result, int totalCount) : base(isSuccess, new ResponseVM("DEFAULT"))
        {
            Result = result;
            TotalCount = totalCount;
        }
        
        public ActionStatus(ResponseVM error) : base(error)
        {
            Result = default;
        }
       
        public ActionStatus(string locationCode, Exception exception) : base(locationCode, exception)
        {
            Result = default;
        }
        public ActionStatus(ActionStatus actionStatus) : base(actionStatus)
        {
            Result = default;
        }
        #endregion
    }
}
