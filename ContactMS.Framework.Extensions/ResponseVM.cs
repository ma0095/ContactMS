using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMS.Framework.Extensions
{
    public class ResponseVM
    {
        #region Member
       
        public string? ResponseCode { get; set; }

        private string? _ResponseMessage;
        
        public string? ResponseMessage
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ResponseCode))
                {
                    return string.Empty;
                }
                return string.IsNullOrWhiteSpace(ResponseCodes.ResourceManager.GetString(ResponseCode))
                    ? _ResponseMessage
                    : ResponseCodes.ResourceManager.GetString(ResponseCode);
            }

            set => _ResponseMessage = value;
        }
        #endregion
        #region Contructor
        
        public ResponseVM(string successCode)
        {
            ResponseCode = successCode;
        }
        public ResponseVM(string successCode, string responseMessage)
        {
            ResponseCode = successCode;
            ResponseMessage = responseMessage;
        }
        #endregion
    }
}
