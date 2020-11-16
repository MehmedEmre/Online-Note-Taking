using MyEvernote.Entities.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyEvernote.BusinessLayer.Results
{
    public class BuisnessLayerResult<T> where T : class
    {
        public List<ErrorMessage> Errors { get; set; }
        public T Result { set; get; }


        public BuisnessLayerResult()
        {
            Errors = new List<ErrorMessage>();
        }


        public void AddError(ErrorMessageCode code , string message)
        {
            Errors.Add(new ErrorMessage() { Code = code,Message = message });
        }

    }
}
