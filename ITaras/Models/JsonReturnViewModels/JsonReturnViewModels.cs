using System;

namespace ITaras.Models.JsonReturnViewModels
{
    public class JsonModelReturnViewUser
    {       
        public bool isError { get; set; }
        public bool createSuccess { get; set; }
        public bool editSuccess { get; set; }
        public bool isExit { get; set; }
        public string messages { get; set; }
    }
}
